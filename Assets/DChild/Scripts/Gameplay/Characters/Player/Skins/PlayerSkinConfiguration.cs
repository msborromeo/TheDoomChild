using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Player.Skins
{
    [RequireComponent(typeof(SkeletonRenderer))]
    public class PlayerSkinConfiguration : MonoBehaviour
    {
        private SkeletonRenderer m_skeletonRenderer;

        [SerializeField]
        private SkinData m_defaultSkin;

        private SkinData m_currentSkin;

        private void OnEnable()
        {
            m_skeletonRenderer = GetComponent<SkeletonRenderer>();

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

        private void ClearCurrentSkin()
        {
            m_skeletonRenderer.CustomMaterialOverride.Clear();
        }

        private void ApplyAtlasOverride(AtlasMaterialOverride atlas)
        {
            m_skeletonRenderer.CustomMaterialOverride[m_defaultSkin.atlasOverrides.material] = atlas.material;
        }
    }
}

