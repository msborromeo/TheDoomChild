using System;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Player.Skins
{
    [Serializable]
    public class SkinMaterialSet
    {
        [Tooltip("Unique ID used to apply this skin")]
        public string skinId;

        [Header("Atlas Override")]
        private AtlasMaterialOverride m_atlasOverrides = new AtlasMaterialOverride();
        public AtlasMaterialOverride atlasOverrides => m_atlasOverrides;
        [Header("Slot Override")]
        private SlotMaterialOverride m_slotOverrides = new SlotMaterialOverride();
        public SlotMaterialOverride slotOverrides => m_slotOverrides;
    }
}
