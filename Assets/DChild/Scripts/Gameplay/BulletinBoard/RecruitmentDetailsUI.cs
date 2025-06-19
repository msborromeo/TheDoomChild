using DChild.Gameplay.Characters.Players;
using System.Linq;
using System.Text.RegularExpressions;
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
                (_, _, true, _, _, _, _,_) => character.combatArt.connectedCombatArt != CombatArt.Barrier ? $"{character.combatArt.combatArtName}" : "Barrier II",
                (_, _, _, true, _, _, _, _) => Regex.Replace($"{character.primarySkill}", "([A-Z])([a-z]*)", " $1$2"),
                (_, _, _, _, true, _, _, _) => $"Recruited {character.requiredNPCCount} characters.",
                (_, _, _, _, _, true, _, _) => $"{character.requiredSoulEssence} Soul Essence",
                (_, _, _, _, _, _, true, _) => $"Defeated Boss: {character.defeatedBoss.creatureName}",
                (_, _, _, _, _, _, _, true) => $"Win {character.battlesWon} army battles.",
                _ => "N/A"
            };

            m_conditions.text = requirement;
            m_recruitmentFee.text = $"{character.recruitmentCost}";
        }

    }
}