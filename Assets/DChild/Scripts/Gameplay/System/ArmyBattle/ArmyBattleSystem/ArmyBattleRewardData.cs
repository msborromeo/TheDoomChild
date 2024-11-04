using DChild.Gameplay.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DChild.Gameplay.ArmyBattle
{
    [CreateAssetMenu(fileName = "Reward", menuName = "DChild/Gameplay/Army/BattleRewards")]
    public class ArmyBattleRewardData : ScriptableObject
    {
        public List<ItemData> m_Items;
        public int m_SoulEssence;
        public ArmyCharacterData m_CharacterReward;
        public SoulSkillItem m_SoulSkill;
    }
}
