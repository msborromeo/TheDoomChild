using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Player.Skins
{
    [System.Serializable]
    public class SkinSaveData
    {
        [SerializeField]
        private int[] m_acquiredSkinsIDs;
        public int[] acquiredSkinsIDs => m_acquiredSkinsIDs;

        [SerializeField]
        private int m_equippedSkin;
        public int equippedSkin => m_equippedSkin;
        
        public SkinSaveData(PlayerSkinConfiguration configuration)
        {
            m_acquiredSkinsIDs = new int[configuration.acquiredSkins.Count];
            for(int i = 0; i < configuration.acquiredSkins.Count; i++)
            {
                var skin = configuration.acquiredSkins[i];
                m_acquiredSkinsIDs[i] = skin.id;
            }

            m_equippedSkin = configuration.equippedSkin.id;
        }

    }

    [System.Serializable]
    public class PlayerSkinConfiguration
    {
        [SerializeField]
        private List<SkinData> m_acquiredSkins;
        [SerializeField]
        private SkinData m_equippedSkin;

        public List<SkinData> acquiredSkins => m_acquiredSkins;
        public SkinData equippedSkin => m_equippedSkin;

        public PlayerSkinConfiguration(List<SkinData> acquiredSkins, SkinData equippedSkin)
        {
            m_acquiredSkins = acquiredSkins;
            m_equippedSkin = equippedSkin;
        }
    }
}

