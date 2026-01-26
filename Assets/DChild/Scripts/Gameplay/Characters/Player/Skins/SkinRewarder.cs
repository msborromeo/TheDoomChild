using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Player.Skins
{
    public class SkinRewarder : MonoBehaviour
    {
        [SerializeField]
        private SkinData m_rewardSkin;

        [Button]
        public void RewardSkin()
        {
            GameplaySystem.playerManager.player.skinHandle.AddAcquiredSkin(m_rewardSkin);
        }
    }
}

