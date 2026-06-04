using DChild.Gameplay.Characters.Enemies;
using TMPro;
using UnityEngine;
using Dchild.Localization;
using DChild.Menu.Bestiary;
using System;

namespace DChild.Gameplay.Combat.UI
{
    public class BossNameUI : MonoBehaviour , ICodexBestiaryLocalizer
    {
        [SerializeField]
        private TextMeshProUGUI m_bossName;
        [SerializeField]
        private TextMeshProUGUI m_bossTitle;
        [SerializeField]
        private TextMeshProUGUI m_bossNameOnly;
        
        [SerializeField] private TextMeshProUGUI m_baybayinName;

        public event Action<BestiaryData> localizeBestiaryData;

        public void SetName(Boss boss)
        {
            m_baybayinName.text = boss.creatureName;

            var hasTitle = boss.creatureTitle != string.Empty || boss.creatureTitle != "";
            if (hasTitle)
            {
                m_bossName.text = boss.creatureName;
                m_bossTitle.text = boss.creatureTitle;
            }
            else
            {
                m_bossNameOnly.text = boss.creatureName;
            }
            m_bossName.enabled = hasTitle;
            m_bossTitle.enabled = hasTitle;
            m_bossNameOnly.enabled = !hasTitle;
            localizeBestiaryData?.Invoke(boss.GetBestiaryData());
        }
    }
}