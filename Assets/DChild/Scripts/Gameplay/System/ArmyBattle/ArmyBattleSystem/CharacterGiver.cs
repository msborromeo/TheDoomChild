using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle
{
    public class CharacterGiver : MonoBehaviour
    {
        public void RecruitCharacter(ArmyCharacterData Character)
        {
            GameplaySystem.playerManager.armyBattleCharacterRecruiter.SetAsRecruited(Character, true);
        }

        public void RecruitCharacter(List<ArmyCharacterData> Characters)
        {
            foreach(ArmyCharacterData ch in Characters)
            {
                GameplaySystem.playerManager.armyBattleCharacterRecruiter.SetAsRecruited(ch, true);
            }
        }
    }
}
