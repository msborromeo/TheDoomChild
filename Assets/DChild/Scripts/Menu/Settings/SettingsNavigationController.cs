using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using Doozy.Runtime.UIManager.Input;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace DChild.Menu.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UIView))]
    public class SettingsNavigationController : MonoBehaviour
    {
        [Serializable]
        public class Category
        {
            public UIToggle tab;
            public Transform content;
            [Tooltip("Navigation order. When empty, use the content hierarchy order.")]
            public Selectable[] fields = new Selectable[0];
            [NonSerialized] public Selectable remembered;
        }

        [SerializeField] private Category[] m_categories = new Category[0];
        [SerializeField] private Selectable m_restoreDefaults;
        [SerializeField] private UIPopup m_rebindPopup;
        private readonly List<Selectable> m_available = new List<Selectable>();
        private readonly Dictionary<Selectable, bool> m_modalInteractability = new Dictionary<Selectable, bool>();
        private UIView m_view;
        private EventSystem m_eventSystem;
        private InputSystemUIInputModule m_input;
        private GameObject m_opener;
        private GameObject m_modalOpener;
        private Selectable m_lastField;
        private TMP_Dropdown[] m_dropdowns;
        private int m_category;
        private int m_modalFrame = -1;
        private int m_enterFrame = -1;
        private bool m_session;
        private bool m_inFields;
        private bool m_backBlocked;
        private bool m_navigationSuspended;
        private bool m_previousNavigation;
        private bool m_rebinding;
        private UIContainer m_restoreContainer;
        private UnityAction m_restoreAction;
        private Coroutine m_releaseBackRoutine;

        private bool HasModal
        {
            get
            {
                if (m_rebinding || Time.frameCount == m_modalFrame) return true;
                if (m_rebindPopup != null && m_rebindPopup.isActiveAndEnabled && !m_rebindPopup.isHidden) return true;
                foreach (var dropdown in m_dropdowns)
                    if (dropdown != null && (dropdown.IsExpanded ||
                        (dropdown is SettingsDropdown settings && settings.lastCancelFrame == Time.frameCount))) return true;
                return false;
            }
        }

        private static bool Available(Selectable selectable) =>
            selectable != null && selectable.IsActive() && selectable.IsInteractable();

        private void Awake()
        {
            m_view = GetComponent<UIView>();
            m_dropdowns = GetComponentsInChildren<TMP_Dropdown>(true);
            foreach (var category in m_categories)
            {
                if (category.fields.Length == 0 && category.content != null)
                {
                    var fields = new List<Selectable>();
                    foreach (var selectable in category.content.GetComponentsInChildren<Selectable>(true))
                    {
                        // Exclude dropdown templates and Doozy visual proxies.
                        if (selectable.GetComponentInParent<TMP_Dropdown>() is TMP_Dropdown dropdown && selectable != dropdown) continue;
                        if (selectable is UIButton || selectable is UIToggle || selectable is UISlider || selectable is Slider || selectable is TMP_Dropdown)
                            fields.Add(selectable);
                    }
                    category.fields = fields.ToArray();
                }
                AddTarget(category.tab);
                foreach (var field in category.fields) AddTarget(field);
            }
            AddTarget(m_restoreDefaults);
        }

        private void AddTarget(Selectable selectable)
        {
            if (selectable == null) return;
            var target = selectable.gameObject.AddComponent<SettingsNavigationTarget>();
            target.Initialize(this, selectable);
        }

        private void OnEnable()
        {
            m_view.OnShowCallback.Event.AddListener(BeginSession);
            m_view.OnVisibleCallback.Event.AddListener(FocusTab);
            m_view.OnHideCallback.Event.AddListener(StopNavigation);
            m_view.OnHiddenCallback.Event.AddListener(EndSession);
            if (!m_view.isHidden && !m_view.isHiding) BeginSession();
        }

        private void OnDisable()
        {
            m_view.OnShowCallback.Event.RemoveListener(BeginSession);
            m_view.OnVisibleCallback.Event.RemoveListener(FocusTab);
            m_view.OnHideCallback.Event.RemoveListener(StopNavigation);
            m_view.OnHiddenCallback.Event.RemoveListener(EndSession);
            EndSession();
            if (m_releaseBackRoutine != null) StopCoroutine(m_releaseBackRoutine);
            m_releaseBackRoutine = null;
            ReleaseBack();
        }

        private void BeginSession()
        {
            if (m_session) return;
            ClearPendingRestore();
            m_eventSystem = EventSystem.current;
            if (m_eventSystem == null) return;
            m_input = m_eventSystem.GetComponent<InputSystemUIInputModule>();
            m_opener = m_eventSystem.currentSelectedGameObject;
            if (m_opener != null && m_opener.transform.IsChildOf(transform)) m_opener = null;
            m_session = true;
            m_inFields = false;
            m_enterFrame = -1;
            m_lastField = null;
            foreach (var category in m_categories) category.remembered = null;
            for (int i = 0; i < m_categories.Length; i++)
                if (m_categories[i].tab != null && m_categories[i].tab.isOn) { m_category = i; break; }
            if (m_releaseBackRoutine != null) StopCoroutine(m_releaseBackRoutine);
            m_releaseBackRoutine = null;
            if (!m_backBlocked) { BackButton.Disable(); m_backBlocked = true; }
            if (m_input != null && m_input.cancel != null) m_input.cancel.action.performed += OnCancel;
        }

        private void FocusTab()
        {
            if (!m_session || m_categories.Length == 0) return;
            m_inFields = false;
            m_enterFrame = -1;
            if (Available(m_categories[m_category].tab))
            {
                m_categories[m_category].tab.SetIsOn(true);
                m_categories[m_category].tab.Select();
            }
            else foreach (var category in m_categories)
                if (Available(category.tab)) { category.tab.Select(); break; }
        }

        private void StopNavigation()
        {
            m_enterFrame = -1;
            if (m_eventSystem != null && m_eventSystem.currentSelectedGameObject != null &&
                m_eventSystem.currentSelectedGameObject.transform.IsChildOf(transform))
                m_eventSystem.SetSelectedGameObject(null);
        }

        private void EndSession()
        {
            if (!m_session) return;
            if (m_input != null && m_input.cancel != null) m_input.cancel.action.performed -= OnCancel;
            ResumeNavigation();
            if (m_backBlocked)
            {
                if (isActiveAndEnabled) m_releaseBackRoutine = StartCoroutine(ReleaseBackNextFrame());
                else ReleaseBack();
            }
            m_session = false;
            m_rebinding = false;
            m_modalOpener = null;
            if (m_eventSystem != null && m_opener != null)
            {
                if (Available(m_opener.GetComponent<Selectable>())) m_eventSystem.SetSelectedGameObject(m_opener);
                else
                {
                    // The calling view may still be animating back into view.
                    m_restoreContainer = m_opener.GetComponentInParent<UIContainer>();
                    if (m_restoreContainer != null && !m_restoreContainer.isVisible)
                    {
                        var opener = m_opener;
                        var eventSystem = m_eventSystem;
                        m_restoreAction = () =>
                        {
                            ClearPendingRestore();
                            if (eventSystem != null && opener != null && Available(opener.GetComponent<Selectable>()))
                                eventSystem.SetSelectedGameObject(opener);
                        };
                        m_restoreContainer.OnVisibleCallback.Event.AddListener(m_restoreAction);
                    }
                }
            }
            m_opener = null;
        }

        private void OnDestroy() => ClearPendingRestore();

        private IEnumerator ReleaseBackNextFrame()
        {
            yield return null;
            m_releaseBackRoutine = null;
            ReleaseBack();
        }

        private void ReleaseBack()
        {
            if (!m_backBlocked) return;
            BackButton.Enable();
            m_backBlocked = false;
        }

        private void ClearPendingRestore()
        {
            if (m_restoreContainer != null && m_restoreAction != null)
                m_restoreContainer.OnVisibleCallback.Event.RemoveListener(m_restoreAction);
            m_restoreContainer = null;
            m_restoreAction = null;
        }

        private void OnCancel(InputAction.CallbackContext context)
        {
            if (!m_session || !m_view.isVisible || HasModal) return;
            if (m_inFields) FocusTab();
            else
            {
                StopNavigation();
                // Send the existing graph's exit signal once, while automatic Back remains blocked.
                BackButton.stream.SendSignal();
            }
        }

        internal void Selected(Selectable selectable)
        {
            if (!m_session || HasModal) return;
            for (int i = 0; i < m_categories.Length; i++)
            {
                var category = m_categories[i];
                if (selectable == category.tab)
                {
                    m_category = i;
                    m_inFields = false;
                    category.tab.SetIsOn(true);
                    return;
                }
                if (Array.IndexOf(category.fields, selectable) >= 0)
                {
                    m_category = i;
                    m_inFields = true;
                    category.remembered = selectable;
                    m_lastField = selectable;
                    Reveal(selectable);
                    return;
                }
            }
            if (selectable == m_restoreDefaults) { m_inFields = true; m_lastField = selectable; }
        }

        private static void Reveal(Selectable selectable)
        {
            var scroll = selectable.GetComponentInParent<ScrollRect>();
            var rect = selectable.transform as RectTransform;
            if (scroll == null || scroll.content == null || rect == null) return;
            var viewport = scroll.viewport != null ? scroll.viewport : scroll.transform as RectTransform;
            if (viewport == null) return;
            Canvas.ForceUpdateCanvases();
            var bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(viewport, rect);
            float offset = bounds.max.y > viewport.rect.yMax ? viewport.rect.yMax - bounds.max.y :
                bounds.min.y < viewport.rect.yMin ? viewport.rect.yMin - bounds.min.y : 0;
            if (offset == 0) return;
            scroll.StopMovement();
            scroll.content.position += viewport.TransformVector(new Vector3(0, offset, 0));
        }

        internal void Submitted(Selectable selectable)
        {
            if (!m_session || !m_view.isVisible || HasModal) return;
            for (int i = 0; i < m_categories.Length; i++)
                if (m_categories[i].tab == selectable)
                {
                    m_category = i;
                    m_categories[i].tab.SetIsOn(true);
                    // Do not deliver this submit to the newly focused field.
                    m_enterFrame = Time.frameCount;
                    return;
                }
        }

        private void LateUpdate()
        {
            if (!m_session && !m_view.isHidden && !m_view.isHiding) BeginSession();
            if (!m_session || !m_view.isVisible || m_eventSystem == null) return;
            if (HasModal) return;
            if (m_navigationSuspended)
            {
                ResumeNavigation();
                if (m_modalOpener != null && Available(m_modalOpener.GetComponent<Selectable>()))
                    m_eventSystem.SetSelectedGameObject(m_modalOpener);
                m_modalOpener = null;
            }
            UpdateLinks();
            if (m_enterFrame >= 0 && Time.frameCount > m_enterFrame)
            {
                var category = m_categories[m_category];
                var view = category.content != null ? category.content.GetComponent<UIContainer>() : null;
                if (view != null && !view.isVisible) return;
                m_enterFrame = -1;
                m_inFields = true;
                var field = Available(category.remembered) ? category.remembered : FirstField();
                if (field != null) field.Select();
                else FocusTab();
            }
            var selected = m_eventSystem.currentSelectedGameObject;
            if (selected == null || !selected.transform.IsChildOf(transform) || !Available(selected.GetComponent<Selectable>()))
            {
                if (m_inFields && Available(m_lastField)) m_lastField.Select();
                else if (m_inFields && FirstField() != null) FirstField().Select();
                else FocusTab();
            }
        }

        private Selectable FirstField()
        {
            foreach (var field in m_categories[m_category].fields) if (Available(field)) return field;
            return Available(m_restoreDefaults) ? m_restoreDefaults : null;
        }

        private void UpdateLinks()
        {
            m_available.Clear();
            foreach (var category in m_categories) if (Available(category.tab)) m_available.Add(category.tab);
            LinkRows(true);
            m_available.Clear();
            foreach (var field in m_categories[m_category].fields) if (Available(field)) m_available.Add(field);
            if (Available(m_restoreDefaults)) m_available.Add(m_restoreDefaults);
            LinkRows(false);
        }

        private void LinkRows(bool tabs)
        {
            for (int i = 0; i < m_available.Count; i++)
            {
                int start = i, end = i;
                var row = m_available[i].GetComponentInParent<SettingsRebindNavigationRelay>();
                if (!tabs && row != null)
                {
                    while (start > 0 && m_available[start - 1].GetComponentInParent<SettingsRebindNavigationRelay>() == row) start--;
                    while (end + 1 < m_available.Count && m_available[end + 1].GetComponentInParent<SettingsRebindNavigationRelay>() == row) end++;
                }
                int previousIndex = start > 0 ? start - 1 : i;
                int nextIndex = end + 1 < m_available.Count ? end + 1 : i;
                // Keep the same column when moving between keybind/reset rows.
                if (!tabs && start > 0)
                {
                    var previousRow = m_available[previousIndex].GetComponentInParent<SettingsRebindNavigationRelay>();
                    int previousStart = previousIndex;
                    if (previousRow != null)
                        while (previousStart > 0 && m_available[previousStart - 1].GetComponentInParent<SettingsRebindNavigationRelay>() == previousRow) previousStart--;
                    previousIndex = Mathf.Min(previousStart + i - start, previousIndex);
                }
                if (!tabs && end + 1 < m_available.Count)
                {
                    var nextRow = m_available[nextIndex].GetComponentInParent<SettingsRebindNavigationRelay>();
                    int nextEnd = nextIndex;
                    if (nextRow != null)
                        while (nextEnd + 1 < m_available.Count && m_available[nextEnd + 1].GetComponentInParent<SettingsRebindNavigationRelay>() == nextRow) nextEnd++;
                    nextIndex = Mathf.Min(nextIndex + i - start, nextEnd);
                }
                var previous = m_available[previousIndex];
                var next = m_available[nextIndex];
                var navigation = new Navigation { mode = Navigation.Mode.Explicit, selectOnUp = previous, selectOnDown = next };
                if (tabs) { navigation.selectOnLeft = previous; navigation.selectOnRight = next; }
                else if (row != null)
                {
                    navigation.selectOnLeft = m_available[Mathf.Max(start, i - 1)];
                    navigation.selectOnRight = m_available[Mathf.Min(end, i + 1)];
                }
                m_available[i].navigation = navigation;
            }
        }

        public void BeginRebind()
        {
            if (!m_session || m_eventSystem == null) return;
            m_rebinding = true;
            m_enterFrame = -1;
            if (!m_navigationSuspended)
            {
                m_modalOpener = m_eventSystem.currentSelectedGameObject;
                m_previousNavigation = m_eventSystem.sendNavigationEvents;
                m_eventSystem.sendNavigationEvents = false;
                m_navigationSuspended = true;
                foreach (var category in m_categories)
                {
                    BlockInteraction(category.tab);
                    foreach (var field in category.fields) BlockInteraction(field);
                }
                BlockInteraction(m_restoreDefaults);
            }
        }

        public void EndRebind()
        {
            m_rebinding = false;
            m_modalFrame = Time.frameCount;
        }

        private void ResumeNavigation()
        {
            foreach (var entry in m_modalInteractability)
                if (entry.Key != null) entry.Key.interactable = entry.Value;
            m_modalInteractability.Clear();
            if (m_navigationSuspended && m_eventSystem != null) m_eventSystem.sendNavigationEvents = m_previousNavigation;
            m_navigationSuspended = false;
        }

        private void BlockInteraction(Selectable selectable)
        {
            if (selectable == null || m_modalInteractability.ContainsKey(selectable)) return;
            m_modalInteractability.Add(selectable, selectable.interactable);
            selectable.interactable = false;
        }
    }
}
