#if UNITY_2018_3 || UNITY_2019 || UNITY_2018_3_OR_NEWER
#define NEW_PREFAB_SYSTEM
#endif
#define SPINE_OPTIONAL_MATERIALOVERRIDE

using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

namespace DChild.Gameplay.Characters.Player.Skins
{

#if NEW_PREFAB_SYSTEM
    [ExecuteAlways]
#else
    [ExecuteInEditMode]
#endif
    public partial class PlayerSkinSystem_Jara : MonoBehaviour
    {

        [Header("References")]
        public SkeletonRenderer skeletonRenderer;

        [Header("Skins")]
        [SerializeField] private List<SkinMaterialSet> skins = new List<SkinMaterialSet>();

        [Header("Runtime")]
        [SerializeField] private string currentSkinId;

        private SkinMaterialSet currentSkin;

        #region Unity

        void OnEnable()
        {
            if (skeletonRenderer == null)
                skeletonRenderer = GetComponent<SkeletonRenderer>();

            if (skeletonRenderer == null)
            {
                Debug.LogError("SkeletonRenderer not found.");
                return;
            }

            skeletonRenderer.Initialize(false);

            if (!string.IsNullOrEmpty(currentSkinId))
                ApplySkin(currentSkinId);
        }

        void OnDisable()
        {
            ClearCurrentSkin();
        }

        #endregion

        #region Public API (PlayMaker & C# Friendly)

        /// <summary>
        /// Apply a skin by ID (string).
        /// Safe to call from PlayMaker Call Method.
        /// </summary>
        public void ApplySkin(string skinId)
        {
            if (string.IsNullOrEmpty(skinId))
                return;

            if (skeletonRenderer == null || skeletonRenderer.skeleton == null)
                return;

            // Remove old skin
            ClearCurrentSkin();

            currentSkinId = skinId;
            currentSkin = skins.Find(s => s.skinId == skinId);

            if (currentSkin == null)
            {
                Debug.LogWarning($"Skin not found: {skinId}");
                return;
            }

            ApplyAtlasOverrides(currentSkin);
            ApplySlotOverrides(currentSkin);
        }

        /// <summary>
        /// Clears all material overrides.
        /// </summary>
        public void ClearCurrentSkin()
        {
            if (skeletonRenderer == null)
                return;

#if SPINE_OPTIONAL_MATERIALOVERRIDE
            skeletonRenderer.CustomMaterialOverride.Clear();
#endif
            skeletonRenderer.CustomSlotMaterials.Clear();
        }

        /// <summary>
        /// Returns the currently active skin ID.
        /// </summary>
        public string GetCurrentSkin()
        {
            return currentSkinId;
        }

        #endregion

        #region Internal Logic

        void ApplyAtlasOverrides(SkinMaterialSet skin)
        {
#if SPINE_OPTIONAL_MATERIALOVERRIDE
            //foreach (var atlas in skin.atlasOverrides)
            //{
            //    if (atlas.overrideDisabled || atlas.originalMaterial == null)
            //        continue;

            //    skeletonRenderer.CustomMaterialOverride[atlas.originalMaterial] =
            //        atlas.replacementMaterial;
            //}
#endif
        }

        void ApplySlotOverrides(SkinMaterialSet skin)
        {
            //foreach (var slot in skin.slotOverrides)
            //{
            //    if (slot.overrideDisabled || string.IsNullOrEmpty(slot.slotName))
            //        continue;

            //    Slot slotObj = skeletonRenderer.skeleton.FindSlot(slot.slotName);
            //    if (slotObj == null)
            //        continue;

            //    skeletonRenderer.CustomSlotMaterials[slotObj] = slot.material;
            //}
        }
    }

#endregion
}
