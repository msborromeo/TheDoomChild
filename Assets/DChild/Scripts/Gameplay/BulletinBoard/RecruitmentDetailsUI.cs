using TMPro;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI.BulletinBoard
{
    public class RecruitmentDetailsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_conditions;
        [SerializeField] private TextMeshProUGUI m_recruitmentFee;

        public void Display(CharacterRecruitmentData character)
        {
            if (character == null)
                return;

            var requirement = (character.requiresCharacter, 
                character.requiresItem, character.requiresCombatArt, 
                character.requiresPrimarySkill, character.requiresNPCCount,
                character.requiresSoulEssence) switch
            {
                (true, _, _, _, _, _) => character.requiredCharacter.name,
                (_, true, _, _, _, _) => character.requiredItem.itemName,
                (_, _, true, _, _, _) => character.combatArt.ToString(),
                (_, _, _, true, _, _) => character.primarySkill.ToString(),
                (_, _, _, _, true, _) => $"{character.requiredNPCCount} characters",
                (_, _, _, _, _, true) => $"{character.requiredSoulEssence} soul essence",
                _ => string.Empty
            };

            m_conditions.text = requirement;
            m_recruitmentFee.text = $"{character.requiredSoulEssence}";
        }

    }
}