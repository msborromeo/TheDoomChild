using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DChild.Gameplay.Items;
using DChild.Gameplay.Inventories;
using DChild.Gameplay;
using DChild.Gameplay.ArmyBattle;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DChild.Gameplay.ArmyBattle
{
    public class ArmyBattleRewardGiver : SerializedMonoBehaviour
    {
        [SerializeField]
        private ArmyBattleRewardData m_battleRewards;
        private PlayerInventory m_Inventory;
        // Start is called before the first frame update
        void Start()
        {
            if (!GameplaySystem.playerManager.player)
            {
                return;
            }
            m_Inventory = GameplaySystem.playerManager.player.inventory;
        }

        public void InitializeReward(ArmyBattleRewardData reward)
        {
            m_battleRewards = reward;
        }

        [Button]
        public void GiveReward()
        {
            if (!m_Inventory||!m_battleRewards)
            {
                return;
            }
            foreach (ItemData item in m_battleRewards.m_Items)
            {
                m_Inventory.AddItem(item, 1);
            }
            m_Inventory.AddSoulEssence(m_battleRewards.m_SoulEssence);
        }
    }
}
