using DChild.Gameplay.Characters.Players.SoulSkills;
using DChild.Gameplay.Systems;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.SoulSkills
{
    [ShowOdinSerializedPropertiesInInspector]
    [CreateAssetMenu(fileName = "SoulItem", menuName = "DChild/Database/Soul Equipment")]
    public class SoulEquipment : SoulSkill
    {
        [SerializeField]
        private SoulSlot m_soulSlot;


        public SoulSlot soulSlot => m_soulSlot;
    }
}

