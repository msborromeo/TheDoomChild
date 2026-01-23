using DChild.Gameplay.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Gameplay.Characters.Player.Skins
{
    [CreateAssetMenu(fileName = "Player Skin Data", menuName = "DChild/Gameplay/Character/Player Skin Data")]
    public class SkinData : ItemData
    {
        [Header("Atlas Override")]
        [SerializeField]
        private AtlasMaterialOverride m_atlasOverrides = new AtlasMaterialOverride();
        public AtlasMaterialOverride atlasOverrides => m_atlasOverrides;
    }
}

