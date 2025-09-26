using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Characters.Players.SoulSkills;
using DChild.Gameplay.Inventories;
using DChild.Gameplay.SoulSkills;
using Holysoft.Collections;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.EquipmentSystem
{
    public class PlayerSoulEquipmentHandle : SerializedMonoBehaviour, ISerializable<PlayerSoulEquipmentData>
    {
        [System.Serializable]
        public class LeveledEquipment
        {
            [SerializeField]
            private SoulEquipmentItem m_item;
            [SerializeField]
            private int m_exp;

            public LeveledEquipment(SoulEquipmentItem item, int exp)
            {
                m_item = item;
                m_exp = exp;
            }

            public SoulEquipmentItem item => m_item;
            public int exp => m_exp;
        }

        [SerializeField]
        private SoulEquipmentList m_data;

        [SerializeField]
        private IPlayer m_player;

        [SerializeField]
        private PlayerSoulSkillHandle m_soulSkillHandle;

        [SerializeField]
        private Dictionary<SoulSlot, SoulEquipmentItem> m_equippedSoulSlotEquipmentPair = new Dictionary<SoulSlot, SoulEquipmentItem>();

        [SerializeField]
        private List<LeveledEquipment> m_acquiredSoulEquipment = new List<LeveledEquipment>();

        private List<LeveledEquipment> m_eqiuppedItems = new List<LeveledEquipment>();

        private void OnEnable()
        {
            m_player.inventory.SoulEquipmentAcquired += OnSoulEquipmentAcquired;
        }

        private void OnDisable()
        {
            m_player.inventory.SoulEquipmentAcquired -= OnSoulEquipmentAcquired;
        }

        private void OnSoulEquipmentAcquired(object sender, SoulEquipmentAcquiredEventArgs eventArgs)
        {
            AddAcquiredSoulEquipmentItem(eventArgs.Item);
        }

        public void LoadData(PlayerSoulEquipmentData data)
        {
            if(data != null)
            {
                m_equippedSoulSlotEquipmentPair.Clear();
                m_acquiredSoulEquipment.Clear();
                m_eqiuppedItems.Clear();

                Dictionary<int, LeveledEquipment> equipmentIDPair = new Dictionary<int, LeveledEquipment>();

                for (int i = 0; i < data.acquiredEquipmentID.Length; i++)
                {
                    SoulEquipmentItem item = m_data.GetInfo(data.acquiredEquipmentID[i]);
                    var currentExp = Mathf.FloorToInt(item.soulEquipment.ExpRequired * data.equipmentExpPercent[i]);
                    var leveledEquipment = new LeveledEquipment(item, currentExp);

                    m_acquiredSoulEquipment.Add(leveledEquipment);
                    equipmentIDPair.Add(item.id, leveledEquipment);
                }

                for (int i = 0; i < (int)SoulSlot._COUNT; i++)
                {
                    var id = data.equippedEquipmentID[i];
                    if (id != -1)
                    {
                        var levelEquipment = equipmentIDPair[id];
                        m_equippedSoulSlotEquipmentPair.Add((SoulSlot)i, levelEquipment.item);
                        m_eqiuppedItems.Add(levelEquipment);
                    }
                }
            }
        }

        public PlayerSoulEquipmentData SaveData()
        {
            return new PlayerSoulEquipmentData(m_acquiredSoulEquipment, m_equippedSoulSlotEquipmentPair);
        }

        [Button]
        public void EquipSoulEquipment(SoulEquipmentItem soulEquipment)
        {
            var equipment = soulEquipment.soulEquipment;
            if (m_equippedSoulSlotEquipmentPair.ContainsKey(equipment.Slot))
                return;

            m_equippedSoulSlotEquipmentPair.Add(equipment.Slot, soulEquipment);
            //Logic for setting soul skill as activated when equipped
            foreach(SoulSkill soulSkill in equipment.soulSkillList)
            {
                m_soulSkillHandle.AddAsActivated(soulSkill,false);
            }
        }

        [Button]
        public void UnequipSoulEquipment(SoulEquipmentItem soulEquipment)
        {
            var equipment = soulEquipment.soulEquipment;
            m_equippedSoulSlotEquipmentPair.Remove(equipment.Slot);
            //Logic for setting soul skill as deactivated when unequipped
            foreach (SoulSkill soulSkill in equipment.soulSkillList)
            {
                m_soulSkillHandle.RemoveAsActivated(soulSkill);
            }
        }

        [Button]
        public void AddAcquiredSoulEquipmentItem(SoulEquipmentItem soulEquipment)
        {
            var leveledItem = new LeveledEquipment(soulEquipment, 0);

            var equipment = soulEquipment.soulEquipment;
            m_acquiredSoulEquipment.Add(leveledItem);
            //Logic to set soul skills in acquired equipment as activated
            foreach(SoulSkill soulSkill in equipment.soulSkillList)
            {
                m_soulSkillHandle.AddAsAcquired(soulSkill.id);
                m_soulSkillHandle.SetActivationRestriction(soulSkill.id, false);
            }
        }
    }
}

