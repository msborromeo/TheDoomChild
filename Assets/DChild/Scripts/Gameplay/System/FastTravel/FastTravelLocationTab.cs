using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.FastTravel
{
    [RequireComponent(typeof(UIToggle))]
    public class FastTravelLocationTab : MonoBehaviour
    {
        [SerializeField, OnValueChanged("OnDataChange")]
        private FastTravelPageData m_locationList;
        [SerializeField]
        private Image m_icon;
        public FastTravelPageData locationList => m_locationList;

        private UIToggle m_toggle;
        public UIToggle toggle => m_toggle;
        public void OnDataChange()
        {
            m_icon.sprite = m_locationList?.tabIcon ?? null;
            m_icon.color = m_icon != null ? Color.white : Color.black;
        }
        private void Awake()
        {
            m_toggle = GetComponent<UIToggle>();
        }
    }
}