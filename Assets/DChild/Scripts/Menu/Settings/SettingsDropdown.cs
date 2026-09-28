using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DChild.Menu.UI
{
    public class SettingsDropdown : TMP_Dropdown
    {
        private Canvas m_blocker;
        private GameObject m_dropDownList;
        private ScrollRect m_listScroll;
        private GameObject m_lastFocusedOption;

        public Canvas blocker { get => m_blocker; }
        public GameObject dropDownList { get => m_dropDownList; }
        public int lastCancelFrame { get; private set; } = -1;

        public override void OnCancel(BaseEventData eventData)
        {
            lastCancelFrame = Time.frameCount;
            base.OnCancel(eventData);
        }

        protected override GameObject CreateBlocker(Canvas rootCanvas)
        {
            var blocker = base.CreateBlocker(rootCanvas);
            transform.SetSiblingIndex(transform.GetSiblingIndex() - 1);
            m_blocker = blocker.GetComponent<Canvas>();
            m_blocker.overrideSorting = false;
            if (m_dropDownList != null)
                m_dropDownList.transform.SetParent(m_blocker.transform, true);
            return blocker;
        }

        protected override GameObject CreateDropdownList(GameObject template)
        {
            m_dropDownList = base.CreateDropdownList(template);
            m_listScroll = m_dropDownList.GetComponentInChildren<ScrollRect>(true);
            m_lastFocusedOption = null;
            return m_dropDownList;
        }

        protected override void DestroyDropdownList(GameObject dropdownList)
        {
            if (dropdownList == m_dropDownList)
            {
                m_dropDownList = null;
                m_listScroll = null;
                m_lastFocusedOption = null;
            }
            base.DestroyDropdownList(dropdownList);
        }

        private void LateUpdate()
        {
            var eventSystem = EventSystem.current;
            var selected = eventSystem != null ? eventSystem.currentSelectedGameObject : null;
            if (m_dropDownList == null || m_listScroll == null || !m_listScroll.vertical ||
                m_listScroll.content == null || selected == null ||
                !selected.transform.IsChildOf(m_listScroll.content) || selected.GetComponent<Toggle>() == null)
            {
                m_lastFocusedOption = null;
                return;
            }
            if (selected == m_lastFocusedOption) return;

            var viewport = m_listScroll.viewport != null ? m_listScroll.viewport : m_listScroll.transform as RectTransform;
            var row = selected.transform as RectTransform;
            if (viewport == null || row == null) return;

            // TMP selects the current option before it finishes positioning the generated rows.
            Canvas.ForceUpdateCanvases();
            m_lastFocusedOption = selected;
            var rowBounds = RectTransformUtility.CalculateRelativeRectTransformBounds(viewport, row);
            float offset = rowBounds.max.y > viewport.rect.yMax ? viewport.rect.yMax - rowBounds.max.y :
                rowBounds.min.y < viewport.rect.yMin ? viewport.rect.yMin - rowBounds.min.y : 0f;
            if (Mathf.Approximately(offset, 0f)) return;

            var contentBounds = RectTransformUtility.CalculateRelativeRectTransformBounds(viewport, m_listScroll.content);
            float hiddenHeight = contentBounds.size.y - viewport.rect.height;
            if (hiddenHeight <= 0f) return;

            m_listScroll.StopMovement();
            m_listScroll.verticalNormalizedPosition = Mathf.Clamp01(m_listScroll.verticalNormalizedPosition - offset / hiddenHeight);
        }
    }
}
