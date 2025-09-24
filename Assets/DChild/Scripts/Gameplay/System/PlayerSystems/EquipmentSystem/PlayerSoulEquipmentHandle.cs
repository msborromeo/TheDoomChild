using DChild.Gameplay.Characters.Players;
using Holysoft.Collections;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.SoulEquipment
{
    public class PlayerSoulEquipmentHandle : SerializedMonoBehaviour, ISerializable<PlayerSoulEquipmentData>
    {
        [SerializeField]
        private IPlayer m_player;

        [SerializeField]
        private Dictionary<SoulSlot, SoulEquipment> m_equippedSoulEquipment = new Dictionary<SoulSlot, SoulEquipment>();

        [SerializeField]
        private List<SoulEquipment> m_acquiredSoulEquipment = new List<SoulEquipment>();

        public void LoadData(PlayerSoulEquipmentData data)
        {
            if(data != null)
            {
                m_equippedSoulEquipment.Clear();
                m_acquiredSoulEquipment.Clear();

                for(int i = 0; data.acquiredEquipment.Count > 0; i++)
                {
                    m_acquiredSoulEquipment.Add(data.acquiredEquipment[i]);
                }

                foreach(var entry in data.equippedEquipment)
                {
                    m_equippedSoulEquipment.Add(entry.Key, entry.Value);
                }
            }
        }

        public PlayerSoulEquipmentData SaveData()
        {
            return new PlayerSoulEquipmentData(m_acquiredSoulEquipment, m_equippedSoulEquipment);
        }

        public void EquipSoulEquipment(SoulEquipment soulEquipment)
        {
            if (m_equippedSoulEquipment.ContainsKey(soulEquipment.Slot))
                return;

            m_equippedSoulEquipment.Add(soulEquipment.Slot, soulEquipment);
        }

        public void UnequipSoulEquipment(SoulEquipment soulEquipment)
        {
            m_equippedSoulEquipment.Remove(soulEquipment.Slot);
        }

        public void AddAcquiredSoulEquipment(SoulEquipment soulEquipment)
        {
            m_acquiredSoulEquipment.Add(soulEquipment);
        }
    }
}

