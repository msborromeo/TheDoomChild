using DChild.Gameplay.ArmyBattle.SpecialSkills;
using TMPro;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public class AttackingGroupPowerUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_armyPower;

        public void Display(IAttackingGroup group)
        {
            m_armyPower.text = $"{group.GetAttackPower()}";
        }

        public void Display(ISpecialSkillGroup group)
        {
            m_armyPower.text = $"{group.GetSpecialSkill().type}";
        }
    }
}