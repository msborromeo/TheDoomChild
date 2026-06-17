using Holysoft.Gameplay.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.Characters.Player.CombatArt.Leveling
{
    public class CombatArtLevelUI : MonoBehaviour
    {
        [SerializeField]
        private CombatArtLevel m_reference;
        [SerializeField] private TextMeshProUGUI m_levelLabel;
        [SerializeField, BoxGroup("EXP")] private Slider m_expSlider;
        [SerializeField, BoxGroup("EXP")] private SimpleTextStatUI m_expUI;

        public void SyncWithReference()
        {
            m_levelLabel.text = m_reference.currentLevel.ToString();

            var currentExp = m_reference.exp.currentValue;
            var maxExp = m_reference.exp.maxValue;

            m_expSlider.value = ((float)currentExp / maxExp);
            m_expUI.currentValue = currentExp;
            m_expUI.maxValue = maxExp;
        }
    }
}