using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.EquipmentSystem
{
    [System.Serializable]
    public class PlayerSoulEquipmentData
    {
        [SerializeField]
        private List<SoulEquipment> m_acquiredEquipment = new List<SoulEquipment>();
        [SerializeField]
        private Dictionary<SoulSlot, SoulEquipment> m_equippedSoulEquipment = new Dictionary<SoulSlot, SoulEquipment>();

        public PlayerSoulEquipmentData(List<SoulEquipment> acquiredEquipment, Dictionary<SoulSlot, SoulEquipment> equippedEquipment)
        {
            m_acquiredEquipment = acquiredEquipment;
            m_equippedSoulEquipment = equippedEquipment;
        }

        public List<SoulEquipment> acquiredEquipment => m_acquiredEquipment;
        public Dictionary<SoulSlot, SoulEquipment> equippedEquipment => m_equippedSoulEquipment;
    }
}

