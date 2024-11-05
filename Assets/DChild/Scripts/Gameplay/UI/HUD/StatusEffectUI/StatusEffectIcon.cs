using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DChild.Gameplay.Combat.StatusAilment.UI
{
    public class StatusEffectIcon : MonoBehaviour
    {
        private IStatusEffectInfo m_infoToMonitor;

        public StatusEffectType type => m_infoToMonitor.type;

        public void Monitor(IStatusEffectInfo info, StatusEffectIconData iconData)
        {
            m_infoToMonitor = info;
            gameObject.name = $"StatusEffectIcon ({type})";
            enabled = true;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            m_infoToMonitor = null;
            UpdateUI(0);
            enabled = false;
            gameObject.SetActive(false);
        }

        private void UpdateUI(float durationPercentage)
        {

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