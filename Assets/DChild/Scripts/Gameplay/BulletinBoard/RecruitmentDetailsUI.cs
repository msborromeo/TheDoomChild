using System.Linq;
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
                character.requiresSoulEssence, character.requiresDefeatedBoss,
                character.requiresArmyBattleWins) switch
            {
                (true, _, _, _, _, _, _,_) => character.requiredCharacter.name,
                (_, true, _, _, _, _, _,_) => character.requiredItems.Any(item => item.itemName.Contains("heal"))
                    ? "Healing Potions" 
                    : character.requiredItems[0].itemName,
                (_, _, true, _, _, _, _,_) => character.combatArt.ToString(),
                (_, _, _, true, _, _, _, _) => character.primarySkill.ToString(),
                (_, _, _, _, true, _, _, _) => $"{character.requiredNPCCount} characters",
                (_, _, _, _, _, true, _, _) => $"{character.requiredSoulEssence} soul essence",
                (_, _, _, _, _, _, true, _) => $"{character.defeatedBoss} soul essence",
                (_, _, _, _, _, _, _, true) => $"{character.battlesWon} army battles",
                _ => "N/A"
            };

            m_conditions.text = requirement;
            m_recruitmentFee.text = $"{character.requiredSoulEssence}";
        }

    }
}