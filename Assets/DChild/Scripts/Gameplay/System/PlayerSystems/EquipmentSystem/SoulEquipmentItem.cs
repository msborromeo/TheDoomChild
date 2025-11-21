using DChild.Gameplay.Items;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.EquipmentSystem
{
    [CreateAssetMenu(fileName = "SoulEquipment", menuName = "DChild/Database/Soul Equipment Item")]
    public class SoulEquipmentItem : ItemData
    {
        [OdinSerialize, ToggleGroup("m_enableEdit")]
        private SoulEquipment m_soulEquipment;
        public SoulEquipment soulEquipment => m_soulEquipment;
        
        [Button]
        private void UpdateID()
        {
            m_ID = Mathf.Abs(GetInstanceID());
        }
    }
}

