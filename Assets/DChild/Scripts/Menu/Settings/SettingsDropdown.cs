using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DChild.Menu.UI
{
    public class SettingsDropdown : TMP_Dropdown
    {
        private Canvas m_blocker;
        private GameObject m_dropDownList;

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
            return m_dropDownList;
        }
    }
}
