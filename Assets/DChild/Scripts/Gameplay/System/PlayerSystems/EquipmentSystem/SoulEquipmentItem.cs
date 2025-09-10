using DChild.Gameplay.Items;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.SoulSkills
{
    [CreateAssetMenu(fileName = "SoulEquipment", menuName = "DChild/Database/Soul Equipment Item")]
    public class SoulEquipmentItem : ItemData
    {
        [SerializeField, ToggleGroup("m_enableEdit")]
        private SoulEquipment m_soulEquipment;
        public SoulEquipment soulEquipment => m_soulEquipment;
    }
}

