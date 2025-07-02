using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DChild.Localization;
using System;
using Holysoft.Event;
using System.Collections;
using Doozy.Runtime.UIManager.Containers;

namespace DChild.Gameplay.ArmyBattle.UI
{

    public class ArmyBannerUI : MonoBehaviour, IArmyNameInjector
    {
        [SerializeField]
        private TextMeshProUGUI m_armyName;
        [SerializeField]
        private Image m_armyIcon;
        [SerializeField]
        private TextMeshProUGUI m_damagePanel;
        public TextMeshProUGUI damagePanel => m_damagePanel;

        public event Action<TextMeshProUGUI, ArmyOverviewData> nameUpdate;

        public void Display(ArmyOverviewData overviewData)
        {
            m_armyName.text = overviewData.name.ToUpper();
            m_armyIcon.sprite = overviewData.icon;
            nameUpdate?.Invoke(m_armyName, overviewData);
        }

        public void DisplayReceivedDamage(int damage, bool turnEnded = false)
        {
            var container = m_damagePanel.GetComponentInParent<UIContainer>();
            if (turnEnded)
            {
                container.Hide();
                return;
            }

            m_damagePanel.text = $"-{damage}";
            container.Show();
        }
    }
}