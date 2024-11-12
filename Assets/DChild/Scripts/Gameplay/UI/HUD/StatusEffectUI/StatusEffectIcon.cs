using Doozy.Runtime.UIManager.Containers;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace DChild.Gameplay.Combat.StatusAilment.UI
{
    public class StatusEffectIcon : MonoBehaviour
    {
        private IStatusEffectInfo m_infoToMonitor;
        private StatusEffectType m_type;

        public StatusEffectType type => m_type;
        
        [SerializeField]
        private UIContainer m_container;

        [SerializeField]
        private Image m_activeIcon;

        public void Monitor(IStatusEffectInfo info, StatusEffectIconData iconData)
        {
            m_infoToMonitor = info;
            m_type = info.type;
            gameObject.name = $"StatusEffectIcon ({type})";
            enabled = true;
            /*gameObject.SetActive(true);*/
            m_activeIcon.sprite = iconData.activeIcon;
            m_container.Show();
            UpdateUI(info.durationPercentage);
        }

        public void Hide()
        {
            m_infoToMonitor = null;
            UpdateUI(0);
            enabled = false;
            /*gameObject.SetActive(false);*/
            m_container.Hide();
        }

        private void UpdateUI(float durationPercentage)
        {
            m_activeIcon.fillAmount = durationPercentage;
        }

        private void Awake()
        {
            enabled = false;
        }

        private void LateUpdate()
        {
            if (m_infoToMonitor != null)
            {
                UpdateUI(m_infoToMonitor.durationPercentage);
            }
        }
    }

}