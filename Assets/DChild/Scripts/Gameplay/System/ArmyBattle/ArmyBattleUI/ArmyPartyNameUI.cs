using DChild.Gameplay.ArmyBattle.SpecialSkills;
using TMPro;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public class ArmyPartyNameUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_partyName;

        public void Display(IAttackingGroup group)
        {
            m_partyName.text = $"<uppercase>{group.GetCharacterGroup().name}</uppercase>";
        } 

        public void Display(ISpecialSkillGroup group)
        {
            m_partyName.text = $"<uppercase>{group.GetCharacterGroup().name}</uppercase>";
        }
    }
}