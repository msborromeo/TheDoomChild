using DChild.Gameplay.Characters.Player.Skins;
using Holysoft.Event;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DChild.Gameplay.UI.PlayerStats
{
    public class PlayerSkinsUIManager: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_skinNameLabel;
        [SerializeField] private TextMeshProUGUI m_skinIndexLabel;

        private PlayerSkinHandle m_skinHandle;

        public void Initialize()
        {
            m_skinHandle = GameplaySystem.playerManager.player.skinHandle;
        }

        public void SaveCurrentSkin(SkinData skinData) => m_skinHandle.ApplySkin(skinData);

    }
}