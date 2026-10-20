using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using Doozy.Runtime.UIManager.Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace DChild.Menu.Extras
{
    [DisallowMultipleComponent, RequireComponent(typeof(UIView))]
    public class ExtrasNavigationController : MonoBehaviour
    {
        [Serializable]
        public class Category
        {
            public UIToggle tab;
            public Transform content;
            [NonSerialized] public Selectable remembered;
            [NonSerialized] public readonly List<Selectable> items = new List<Selectable>();
            [NonSerialized] public readonly List<Selectable> candidates = new List<Selectable>();
            [NonSerialized] public readonly List<ExtrasImageContent> images = new List<ExtrasImageContent>();
            [NonSerialized] public int navigationHash;
        }

        [SerializeField] private Category[] m_categories = new Category[0];
        [SerializeField] private ExtrasImageShowcase m_showcase;
        private readonly Dictionary<Selectable, bool> m_blocked = new Dictionary<Selectable, bool>();
        private UIView m_view;
        private EventSystem m_eventSystem;
        private InputSystemUIInputModule m_input;
        private Selectable m_viewerFocus;
        private GameObject m_opener;
        private Selectable m_viewerOpener;
        private int m_category;
        private int m_enterFrame = -1;
        private int m_cancelFrame = -1;
        private bool m_session;
        private bool m_inContent;
        private bool m_ready;
        private bool m_backBlocked;
        private Coroutine m_releaseBack;
        private UIContainer m_restoreContainer;
        private UnityAction m_restoreAction;

        private static bool Available(Selectable item) => item != null && item.IsActive() && item.IsInteractable();
        private bool ViewerOpen => m_showcase != null && m_showcase.isOpen;
        private bool HasPopup => UIPopup.visiblePopups.Any();

        private void Awake()
        {
            m_view = GetComponent<UIView>();
            foreach (var category in m_categories) AddTarget(category.tab);
            if (m_showcase != null)
            {
                m_viewerFocus = m_showcase.GetComponent<Selectable>();
                if (m_viewerFocus == null) m_viewerFocus = m_showcase.gameObject.AddComponent<Selectable>();
                AddTarget(m_viewerFocus);
                foreach (var button in m_showcase.GetComponentsInChildren<Button>(true)) AddTarget(button);
            }
        }

        private void OnEnable()
        {
            m_view.OnShowCallback.Event.AddListener(BeginSession);
            m_view.OnVisibleCallback.Event.AddListener(ResetTabs);
            m_view.OnHideCallback.Event.AddListener(StopNavigation);
            m_view.OnHiddenCallback.Event.AddListener(EndSession);
            if (m_showcase != null)
            {
                m_showcase.Opened += OpenViewer;
                m_showcase.Closed += CloseViewer;
            }
            if (!m_view.isHidden && !m_view.isHiding) BeginSession();
        }

        private void OnDisable()
        {
            m_view.OnShowCallback.Event.RemoveListener(BeginSession);
            m_view.OnVisibleCallback.Event.RemoveListener(ResetTabs);
            m_view.OnHideCallback.Event.RemoveListener(StopNavigation);
            m_view.OnHiddenCallback.Event.RemoveListener(EndSession);
            EndSession();
            if (m_showcase != null)
            {
                m_showcase.Opened -= OpenViewer;
                m_showcase.Closed -= CloseViewer;
            }
            if (m_releaseBack != null) StopCoroutine(m_releaseBack);
            m_releaseBack = null;
            ReleaseBack();
        }

        private void BeginSession()
        {
            if (m_session) return;
            m_eventSystem = EventSystem.current;
            if (m_eventSystem == null) return;
            ClearPendingRestore();
            m_opener = m_eventSystem.currentSelectedGameObject;
            if (m_opener != null && m_opener.transform.IsChildOf(transform)) m_opener = null;
            m_input = m_eventSystem.GetComponent<InputSystemUIInputModule>();
            m_session = true;
            m_ready = false;
            m_category = 0;
            m_inContent = false;
            m_enterFrame = -1;
            m_cancelFrame = -1;
            foreach (var category in m_categories) category.remembered = null;
            if (m_releaseBack != null) StopCoroutine(m_releaseBack);
            m_releaseBack = null;
            if (!m_backBlocked) { BackButton.Disable(); m_backBlocked = true; }
            if (m_input != null && m_input.cancel != null) m_input.cancel.action.performed += OnCancel;
        }

        private void ResetTabs()
        {
            BeginSession();
            if (!m_session || m_categories.Length == 0) return;
            if (m_showcase != null) m_showcase.Close();
            foreach (var category in m_categories)
                if (category.tab != null) category.tab.AddToToggleGroup(category.tab.toggleGroup);
            m_category = 0;
            m_ready = true;
            Canvas.ForceUpdateCanvases();
            RefreshContent();
            FocusTab(true);
        }

        private void FocusTab(bool notify = false)
        {
            m_inContent = false;
            m_enterFrame = -1;
            if (m_categories.Length == 0) return;
            var tab = m_categories[m_category].tab;
            if (!Available(tab)) return;
            if (notify || !tab.isOn) tab.SetIsOn(true);
            tab.Select();
        }

        private void StopNavigation()
        {
            m_ready = false;
            m_enterFrame = -1;
            if (m_showcase != null) m_showcase.Close();
            RestoreInteraction();
            if (m_eventSystem != null && m_eventSystem.currentSelectedGameObject != null &&
                m_eventSystem.currentSelectedGameObject.transform.IsChildOf(transform))
                m_eventSystem.SetSelectedGameObject(null);
        }

        private void EndSession()
        {
            if (!m_session) return;
            m_session = false;
            StopNavigation();
            if (m_input != null && m_input.cancel != null) m_input.cancel.action.performed -= OnCancel;
            if (m_backBlocked)
            {
                if (isActiveAndEnabled) m_releaseBack = StartCoroutine(ReleaseBackNextFrame());
                else ReleaseBack();
            }
            if (m_eventSystem != null && m_opener != null)
            {
                if (Available(m_opener.GetComponent<Selectable>())) m_eventSystem.SetSelectedGameObject(m_opener);
                else
                {
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

        private IEnumerator ReleaseBackNextFrame()
        {
            yield return null;
            m_releaseBack = null;
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

        private void OnDestroy() => ClearPendingRestore();

        private void OnCancel(InputAction.CallbackContext context)
        {
            if (!m_session || !m_ready || !m_view.isVisible || HasPopup || m_cancelFrame == Time.frameCount) return;
            m_cancelFrame = Time.frameCount;
            if (ViewerOpen) m_showcase.Close();
            else if (m_inContent || m_enterFrame >= 0) FocusTab();
            else { StopNavigation(); BackButton.stream.SendSignal(); }
        }

        internal void Selected(Selectable selectable)
        {
            if (!m_session || !m_ready || ViewerOpen || HasPopup) return;
            for (int i = 0; i < m_categories.Length; i++)
            {
                var category = m_categories[i];
                if (selectable == category.tab)
                {
                    m_category = i;
                    m_inContent = false;
                    m_enterFrame = -1;
                    if (!category.tab.isOn) category.tab.SetIsOn(true);
                    return;
                }
                if (category.content != null && selectable.transform.IsChildOf(category.content))
                {
                    m_category = i;
                    m_inContent = true;
                    category.remembered = selectable;
                    Reveal(selectable);
                    return;
                }
            }
        }

        internal void Submitted(Selectable selectable)
        {
            if (!m_session || !m_ready || ViewerOpen || HasPopup || !m_view.isVisible) return;
            if (m_categories.Length > 0 && selectable == m_categories[m_category].tab)
                m_enterFrame = Time.frameCount;
        }

        internal void Moved(Selectable selectable, AxisEventData data)
        {
            if (!m_session || !m_ready || HasPopup || !m_view.isVisible) return;
            if (ViewerOpen)
            {
                if (data.moveDir == MoveDirection.Left) m_showcase.Previous();
                else if (data.moveDir == MoveDirection.Right) m_showcase.Next();
                data.Use();
            }
            else if (!m_inContent && data.moveDir == MoveDirection.Right)
            {
                m_enterFrame = Time.frameCount;
                data.Use();
            }
        }

        private void AddTarget(Selectable selectable)
        {
            if (selectable == null) return;
            var relay = selectable.GetComponent<ExtrasNavigationTarget>();
            if (relay != null) return;
            relay = selectable.gameObject.AddComponent<ExtrasNavigationTarget>();
            relay.Initialize(this, selectable);
        }

        private void RefreshContent()
        {
            for (int i = 0; i < m_categories.Length; i++)
            {
                var category = m_categories[i];
                category.items.Clear();
                if (category.content == null) continue;
                category.images.Clear();
                category.content.GetComponentsInChildren(true, category.images);
                foreach (var images in category.images)
                    images.Initialize(m_showcase);
                category.candidates.Clear();
                category.content.GetComponentsInChildren(true, category.candidates);
                int hash = 17;
                foreach (var item in category.candidates)
                {
                    // Scrollbars/arrows remain mouse controls; gallery focus follows actual item positions.
                    if (!(item is Button) && !(item is UIButton) && !(item is UIToggle)) continue;
                    if (item.GetComponentInParent<ScrollRect>() == null) continue;
                    var imageItem = item.GetComponent<ExtrasItem>();
                    var images = item.GetComponentInParent<ExtrasImageContent>();
                    if (imageItem != null && images != null && !images.CanShow(imageItem)) continue;
                    AddTarget(item);
                    if (!Available(item)) continue;
                    category.items.Add(item);
                    var rect = item.transform as RectTransform;
                    unchecked { hash = hash * 31 + item.GetInstanceID(); hash = hash * 31 + rect.position.GetHashCode();
                        hash = hash * 31 + rect.rect.size.GetHashCode(); }
                }
                if (hash == category.navigationHash) continue;
                category.navigationHash = hash;
                for (int j = 0; j < category.items.Count; j++)
                {
                    var item = category.items[j];
                    item.navigation = new Navigation
                    {
                        mode = Navigation.Mode.Explicit,
                        selectOnUp = Neighbour(category.items, item, Vector2.up),
                        selectOnDown = Neighbour(category.items, item, Vector2.down),
                        selectOnLeft = Neighbour(category.items, item, Vector2.left),
                        selectOnRight = Neighbour(category.items, item, Vector2.right)
                    };
                }
            }
            for (int i = 0; i < m_categories.Length; i++)
            {
                var tab = m_categories[i].tab;
                if (tab == null) continue;
                var previous = tab;
                var next = tab;
                for (int j = i - 1; j >= 0; j--) if (Available(m_categories[j].tab)) { previous = m_categories[j].tab; break; }
                for (int j = i + 1; j < m_categories.Length; j++) if (Available(m_categories[j].tab)) { next = m_categories[j].tab; break; }
                tab.navigation = new Navigation { mode = Navigation.Mode.Explicit, selectOnUp = previous,
                    selectOnDown = next, selectOnLeft = tab, selectOnRight = tab };
            }
        }

        private static Selectable Neighbour(List<Selectable> items, Selectable origin, Vector2 direction)
        {
            var rect = origin.transform as RectTransform;
            Vector3 position = rect.TransformPoint(rect.rect.center);
            Selectable best = origin;
            float bestForward = float.PositiveInfinity;
            float bestSideways = float.PositiveInfinity;
            foreach (var candidate in items)
            {
                if (candidate == origin) continue;
                var target = candidate.transform as RectTransform;
                Vector2 delta = origin.transform.parent.InverseTransformVector(target.TransformPoint(target.rect.center) - position);
                float forward = Vector2.Dot(delta, direction);
                float sideways = Mathf.Abs(Vector2.Dot(delta, new Vector2(-direction.y, direction.x)));
                if (forward <= 1f) continue;
                // Stay in the horizontal row; clamp to the nearest column in the next vertical row.
                if (direction.x != 0 && sideways > rect.rect.height * 0.5f) continue;
                if (forward < bestForward - 1f || Mathf.Abs(forward - bestForward) <= 1f && sideways < bestSideways)
                {
                    best = candidate;
                    bestForward = forward;
                    bestSideways = sideways;
                }
            }
            return best;
        }

        private void LateUpdate()
        {
            if (!m_session && !m_view.isHidden && !m_view.isHiding) BeginSession();
            if (!m_session || !m_view.isVisible || HasPopup) return;
            if (!m_ready) ResetTabs();
            if (m_categories.Length == 0) return;
            if (ViewerOpen)
            {
                var selected = m_eventSystem.currentSelectedGameObject;
                if (selected == null || !selected.transform.IsChildOf(m_showcase.transform) || !Available(selected.GetComponent<Selectable>()))
                    m_viewerFocus.Select();
                return;
            }
            RefreshContent();
            var category = m_categories[m_category];
            if (m_enterFrame >= 0 && Time.frameCount > m_enterFrame)
            {
                var container = category.content != null ? category.content.GetComponent<UIContainer>() : null;
                if (container != null && !container.isVisible) return;
                m_enterFrame = -1;
                var first = Available(category.remembered) && category.items.Contains(category.remembered)
                    ? category.remembered : category.items.Count > 0 ? category.items[0] : null;
                if (first != null) first.Select();
                else FocusTab();
            }
            var current = m_eventSystem.currentSelectedGameObject;
            var selectable = current != null ? current.GetComponent<Selectable>() : null;
            bool known = selectable != null && category.items.Contains(selectable);
            foreach (var entry in m_categories) if (selectable != null && selectable == entry.tab) known = true;
            if (!known || !Available(selectable))
            {
                if (m_inContent && Available(category.remembered) && category.items.Contains(category.remembered)) category.remembered.Select();
                else if (m_inContent && category.items.Count > 0) category.items[0].Select();
                else FocusTab();
            }
        }

        private static void Reveal(Selectable item)
        {
            var scroll = item.GetComponentInParent<ScrollRect>();
            if (scroll == null || scroll.content == null) return;
            Canvas.ForceUpdateCanvases();
            var viewport = scroll.viewport != null ? scroll.viewport : scroll.transform as RectTransform;
            var bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(viewport, item.transform);
            float x = bounds.min.x < viewport.rect.xMin ? viewport.rect.xMin - bounds.min.x :
                bounds.max.x > viewport.rect.xMax ? viewport.rect.xMax - bounds.max.x : 0;
            float y = bounds.max.y > viewport.rect.yMax ? viewport.rect.yMax - bounds.max.y :
                bounds.min.y < viewport.rect.yMin ? viewport.rect.yMin - bounds.min.y : 0;
            scroll.StopMovement();
            scroll.content.position += viewport.TransformVector(new Vector3(x, y, 0));
        }

        private void OpenViewer()
        {
            if (!m_session || !m_ready) return;
            m_enterFrame = -1;
            m_viewerOpener = m_eventSystem.currentSelectedGameObject != null
                ? m_eventSystem.currentSelectedGameObject.GetComponent<Selectable>() : null;
            foreach (var category in m_categories)
            {
                Block(category.tab);
                if (category.content != null)
                    foreach (var item in category.content.GetComponentsInChildren<Selectable>(true)) Block(item);
            }
            foreach (var item in m_showcase.GetComponentsInChildren<Selectable>(true))
                item.navigation = new Navigation { mode = Navigation.Mode.Explicit, selectOnUp = item,
                    selectOnDown = item, selectOnLeft = item, selectOnRight = item };
            m_viewerFocus.Select();
        }

        private void Block(Selectable item)
        {
            if (item == null || m_blocked.ContainsKey(item)) return;
            m_blocked.Add(item, item.interactable);
            item.interactable = false;
        }

        private void RestoreInteraction()
        {
            foreach (var pair in m_blocked) if (pair.Key != null) pair.Key.interactable = pair.Value;
            m_blocked.Clear();
        }

        private void CloseViewer()
        {
            RestoreInteraction();
            if (m_session && m_ready && Available(m_viewerOpener)) m_viewerOpener.Select();
            m_viewerOpener = null;
        }
    }
}
