using DChild.Gameplay.ArmyBattle.SpecialSkills;
using DChild.Localization;
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

            var localize = GetComponentInChildren<Localize>();
            var groupId = group.id.ToString("000");

            localize.SetTerm($"ArmyBattle/Groups/{groupId}/AG_{groupId}_Name");

            //m_partyName.text = $"{paramManager.GetParameterValue("PARTY_NAME")}";
        }

        public void Display(ISpecialSkillGroup group)
        {
            m_partyName.text = $"{group.GetCharacterGroup().name}";
        }
    }
}