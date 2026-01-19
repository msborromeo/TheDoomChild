using UnityEngine;

namespace DChild.Gameplay.Characters.Player.Skins
{
    [CreateAssetMenu(fileName = "Player Skin Data", menuName = "DChild/Gameplay/Character/Player Skin Data")]
    public class SkinData : ScriptableObject
    {
        [SerializeField]
        private string m_skinName;
        [SerializeField]
        private string skinName => m_skinName;

        [Header("Atlas Override")]
        [SerializeField]
        private AtlasMaterialOverride m_atlasOverrides = new AtlasMaterialOverride();
        public AtlasMaterialOverride atlasOverrides => m_atlasOverrides;
    }
}

