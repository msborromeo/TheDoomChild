using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DChild.Localization;
using System;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public class ArmyBannerUI : MonoBehaviour , IArmyNameInjector
    {
        [SerializeField]
        private TextMeshProUGUI m_armyName;
        [SerializeField]
        private Image m_armyIcon;

        public event Action<TextMeshProUGUI, ArmyOverviewData> nameUpdate;

        public void Display(ArmyOverviewData overviewData)
        {
            m_armyName.text = overviewData.name.ToUpper();
            m_armyIcon.sprite = overviewData.icon;
            nameUpdate?.Invoke(m_armyName,overviewData);
        }
    }
}