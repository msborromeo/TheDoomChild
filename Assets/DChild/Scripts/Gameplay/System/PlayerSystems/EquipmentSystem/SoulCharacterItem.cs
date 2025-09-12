using DChild.Gameplay.Items;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.SoulSkills
{
    [CreateAssetMenu(fileName = "SoulCharacter", menuName = "DChild/Database/Soul Character Item")]
    public class SoulCharacterItem : ItemData
    {
        [SerializeField, ToggleGroup("m_enableEdit")]
        private SoulCharacter m_soulCharacter;
        public SoulCharacter soulCharacter => m_soulCharacter;
    }
}

