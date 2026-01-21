using Spine.Unity;
using System;
using UnityEngine;

namespace DChild.Gameplay.Characters.Player.Skins
{
    [Serializable]
    public struct SlotMaterialOverride
    {
        public bool overrideDisabled;

        [SpineSlot]
        public string slotName;

        public Material material;
    }
}
