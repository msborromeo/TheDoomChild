using Holysoft.Collections;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Player.Skins
{
    public class PlayerSkinHandle : SerializedMonoBehaviour, ISerializable<SkinSaveData>
    {

        [SerializeField]
        private FullSkinList m_fullSkinList;
        public FullSkinList fullSkinList => m_fullSkinList;

        [SerializeField]
        private SkinData m_defaultSkin;

        private SkinData m_currentSkin;
        public SkinData currentSkin => m_currentSkin;

        [SerializeField]
        private SkeletonRenderer m_skeletonRenderer;
        
        [SerializeField]
        private List<SkinData> m_acquiredSkins;
        public List<SkinData> acquiredSkins => m_acquiredSkins;

        private void OnEnable()
        {
            if (m_currentSkin == null)
                m_currentSkin = m_defaultSkin;
        }


        [Button]
        public void ApplySkin(SkinData skinData)
        {
            ClearCurrentSkin();

            ApplyAtlasOverride(skinData.atlasOverrides);

            m_currentSkin = skinData;
        }

        public void ResetToDefault()
        {
            ApplyAtlasOverride(m_defaultSkin.atlasOverrides);

            m_currentSkin = m_defaultSkin;
        }

        [Button]
        public void AddAcquiredSkin(SkinData skinData)
        {
            if (m_acquiredSkins.Contains(skinData))
                return;

            m_acquiredSkins.Add(skinData);
        }

        private void ClearCurrentSkin()
        {
            m_skeletonRenderer.CustomMaterialOverride.Clear();
        }

        private void ApplyAtlasOverride(AtlasMaterialOverride atlas)
        {
            m_skeletonRenderer.CustomMaterialOverride[m_defaultSkin.atlasOverrides.material] = atlas.material;
        }

        public SkinSaveData SaveData()
        {
            return new SkinSaveData(new PlayerSkinConfiguration(m_acquiredSkins, m_currentSkin));
        }

        public void LoadData(SkinSaveData data)
        {
            Debug.Log("Skins Loaded");
            if(data != null)
            {
                m_acquiredSkins.Clear();

                for (int i = 0; i < data.acquiredSkinsIDs.Length; i++)
                {
                    SkinData skin = m_fullSkinList.GetInfo(data.acquiredSkinsIDs[i]);
                    AddAcquiredSkin(skin);
                }

                if(m_acquiredSkins.Contains(m_defaultSkin) == false)
                {
                    AddAcquiredSkin(m_defaultSkin);
                }

                m_currentSkin = m_fullSkinList.GetInfo(data.equippedSkin);
            }

            ApplySkin(m_currentSkin);
        }
    }
}

