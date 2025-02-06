using DChild.Gameplay.ArmyBattle.SpecialSkills;
using I2.Loc;
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
            var paramManager = GetComponentInChildren<LocalizationParamsManager>();
            paramManager.SetParameterValue("PARTY_NAME", group.GetCharacterGroup().name);

            m_partyName.text = $"{paramManager.GetParameterValue("PARTY_NAME")}";
        } 

        public void Display(ISpecialSkillGroup group)
        {
            m_partyName.text = $"{group.GetCharacterGroup().name}";
        }
    }
}