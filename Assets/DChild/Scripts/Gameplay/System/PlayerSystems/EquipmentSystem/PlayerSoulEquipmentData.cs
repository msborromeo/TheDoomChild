using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.EquipmentSystem
{
    [System.Serializable]
    public class PlayerSoulEquipmentData
    {
        //Assume same index refers to the same items for acquiredEquipment and equipmentExpPercent
        [SerializeField]
        private int[] m_acquiredEquipmentID;
        [SerializeField]
        private float[] m_equipmentExpPercent;
        [SerializeField]
        private int[] m_equippedSoulEquipmentID; //This should have a Length Equal to SoulSlot even if its not equipping anything.

        public PlayerSoulEquipmentData(List<PlayerSoulEquipmentHandle.LeveledEquipment> acquiredEquipment, Dictionary<SoulSlot, SoulEquipmentItem> equippedEquipment)
        {
            m_acquiredEquipmentID = new int[acquiredEquipment.Count];
            m_equipmentExpPercent = new float[acquiredEquipment.Count];

            for (int i = 0; i < acquiredEquipment.Count; i++)
            {
                var item = acquiredEquipment[i];
                m_acquiredEquipmentID[i] = item.item.id;
                m_equipmentExpPercent[i] = (float)item.exp / item.item.soulEquipment.ExpRequired;
            }

            m_equippedSoulEquipmentID = new int[(int)SoulSlot._COUNT];
            for (int i = 0; i < (int)SoulSlot._COUNT; i++)
            {

                if (equippedEquipment.TryGetValue((SoulSlot)i, out SoulEquipmentItem value))
                {
                    m_equippedSoulEquipmentID[i] = value.id;
                }
                else
                {
                    m_equippedSoulEquipmentID[i] = -1;
                }
            }
        }

        public int[] acquiredEquipmentID => m_acquiredEquipmentID;
        public float[] equipmentExpPercent => m_equipmentExpPercent;
        public int[] equippedEquipmentID
        {
            get
            {
                if(m_equippedSoulEquipmentID.Length != (int)SoulSlot._COUNT)
                {
                    Debug.LogWarning("Equipment Soul Item ID Count Does not match the Soul Slot Count " +
                        "\n Resetting Values to unequip all items");
                    ResetEquippedItemValues();
                    return m_equippedSoulEquipmentID;
                }

                return m_equippedSoulEquipmentID;
            }
        }

        [CustomContextMenu("Reset Values", "ResetEquippedItemValues")]
        private void ResetEquippedItemValues()
        {
            m_equippedSoulEquipmentID = new int[(int)SoulSlot._COUNT];
            for (int i = 0; i < (int)SoulSlot._COUNT; i++)
            {
                m_equippedSoulEquipmentID[i] = -1;
            }
        }

    }
}

