using DChild.Gameplay.Characters.Player.CombatArt.Leveling;
using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Characters.Players.SoulSkills;
using DChild.Gameplay.Combat;
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

            public void GainEXP(int exp)
            {
                m_exp += exp;
            }
        }

        [SerializeField]
        private SoulEquipmentList m_data;

        [SerializeField]
        private IPlayer m_player;

        [SerializeField]
        private PlayerSoulSkillHandle m_soulSkillHandle;

        [SerializeField]
        private Attacker m_attacker;

        [SerializeField]
        private Dictionary<SoulSlot, SoulEquipmentItem> m_equippedSoulSlotEquipmentPair = new Dictionary<SoulSlot, SoulEquipmentItem>();

        [SerializeField]
        private List<LeveledEquipment> m_acquiredSoulEquipment;

        private List<LeveledEquipment> m_eqiuppedItems;

        public event Action EquipmentChanged;

        private void OnEnable()
        {
            m_player.inventory.SoulEquipmentAcquired += OnSoulEquipmentAcquired;
            m_player.attackModule.TargetDamaged += OnTargetDamaged;
            m_player.damageableModule.DamageTaken += OnDamageTaken;
        }

        private void OnDisable()
        {
            m_player.inventory.SoulEquipmentAcquired -= OnSoulEquipmentAcquired;
            m_player.attackModule.TargetDamaged -= OnTargetDamaged;
            m_player.damageableModule.DamageTaken -= OnDamageTaken;
        }

        private void OnDamageTaken(object sender, Damageable.DamageEventArgs eventArgs)
        {
            if(m_player.damageableModule.isAlive == false)
            {
                //Unequip all equipment on death
                for(int i = m_eqiuppedItems.Count - 1; i >= 0; i--)
                {
                    UnequipSoulEquipment(m_eqiuppedItems[i].item);
                }
            }
        }

        private void OnSoulEquipmentAcquired(object sender, SoulEquipmentAcquiredEventArgs eventArgs)
        {
            AddAcquiredSoulEquipmentItem(eventArgs.Item);
        }


        private void OnTargetDamaged(object sender, CombatConclusionEventArgs eventArgs)
        {
            if (eventArgs.target.instance.isAlive)
                return;

            if (m_eqiuppedItems.Count <= 0)
                return;

            var expPoints = eventArgs.target.instance.transform.GetComponent<CombatArtExperienceDropper>().Data.exp;

            for (int i = 0; i < m_eqiuppedItems.Count; i++)
            {
                if (m_acquiredSoulEquipment.Contains(m_eqiuppedItems[i]))
                {
                    for (int j = 0; j < m_acquiredSoulEquipment.Count; j++)
                    {
                        if (m_acquiredSoulEquipment[j] == m_eqiuppedItems[i])
                        {
                            m_acquiredSoulEquipment[j].GainEXP(expPoints);

                            if (m_acquiredSoulEquipment[j].exp >= m_acquiredSoulEquipment[j].item.soulEquipment.ExpRequired)
                            {
                                foreach (SoulSkill skill in m_acquiredSoulEquipment[j].item.soulEquipment.soulSkillList)
                                {
                                    skill.SetFullyLearned(true);
                                    m_soulSkillHandle.AddAsAcquired(skill.id);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void LoadData(PlayerSoulEquipmentData data)
        {
            ResetAllSoulSkillInEquipment();
            if (m_eqiuppedItems.Count > 0)
            {
                for(int i = m_eqiuppedItems.Count - 1; i >= 0; i--)
                {
                    if (m_eqiuppedItems[i] != null)
                    {
                        UnequipSoulEquipment(m_eqiuppedItems[i].item);
                    }
                }
            }

            if (data != null)
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

                    AddAcquiredSoulEquipmentItem(leveledEquipment.item);
                    equipmentIDPair.Add(item.id, leveledEquipment);
                    //sets soulskill fully learned after being reset to false on the start of the loading phase.
                    if(leveledEquipment.exp >= item.soulEquipment.ExpRequired)
                    {
                        SetSoulSkillFullyLearned(item.soulEquipment, true);
                    }
                }

                for (int i = 0; i < (int)SoulSlot._COUNT; i++)
                {
                    var id = data.equippedEquipmentID[i];
                    if (id != -1)
                    {
                        var levelEquipment = equipmentIDPair[id];
                        EquipSoulEquipment(levelEquipment.item);
                    }
                }
            }

            //Debug Only
            DebugAcquireAllEquipment(m_data);
            EquipmentChanged?.Invoke();
        }

        public PlayerSoulEquipmentData SaveData()
        {
            return new PlayerSoulEquipmentData(m_acquiredSoulEquipment, m_equippedSoulSlotEquipmentPair);
        }

        [Button]
        public void EquipSoulEquipment(SoulEquipmentItem soulEquipment)
        {
            TryEquipSoulEquipment(soulEquipment);
        }

        public bool TryEquipSoulEquipment(SoulEquipmentItem soulEquipment)
        {
            if (soulEquipment == null)
                return false;

            var equipment = soulEquipment.soulEquipment;

            //Check if soul equipment is acquired already 
            var isAcquired = false;
            for(int i = 0; i < m_acquiredSoulEquipment.Count; i++)
            {
                if (m_acquiredSoulEquipment[i].item == soulEquipment)
                {
                    isAcquired = true;
                }
            }

            if (isAcquired == false)
                return false;

            if (m_equippedSoulSlotEquipmentPair.TryGetValue(equipment.Slot, out SoulEquipmentItem equippedItem))
            {
                if (equippedItem == soulEquipment)
                    return true;

                if (!TryUnequipSoulEquipment(equippedItem))
                    return false;
            }

            m_equippedSoulSlotEquipmentPair.Add(equipment.Slot, soulEquipment);

            //find item to equip in acquired items
            for (int i = 0; i < m_acquiredSoulEquipment.Count; i++)
            {
                if (m_acquiredSoulEquipment[i].item == soulEquipment)
                {
                    m_eqiuppedItems.Add(m_acquiredSoulEquipment[i]);
                }
            }
            if (equipment.soulSkillList != null)
            {
                //logic to set soul skills in acquired equipment as activated
                foreach (SoulSkill soulskill in equipment.soulSkillList)
                {
                    if (soulskill.isFullyLearned)
                    {
                       m_soulSkillHandle.SetActivationRestriction(soulskill.id,false);
                        m_soulSkillHandle.AddAsActivated(soulskill,false);
                    }
                    else
                    {
                        m_soulSkillHandle.AddAsAcquired(soulskill.id);
                        m_soulSkillHandle.AddAsActivated(soulskill, false);
                    }
                    
                }
            }
            //if item has soul skill
            //if (equipment.soulSkillList != null)
            //{
            //    //Logic for setting soul skill as activated when equipped
            //    foreach (SoulSkill soulSkill in equipment.soulSkillList)
            //    {
            //        m_soulSkillHandle.AddAsActivated(soulSkill, false);
            //    }
            //}
        
            foreach(IEquipmentStatBoostModule statBoost in equipment.statBoostList)
            {
                statBoost.AttachTo(m_player);
            }

            EquipmentChanged?.Invoke();
            return true;
        }

        [Button]
        public void UnequipSoulEquipment(SoulEquipmentItem soulEquipment)
        {
            TryUnequipSoulEquipment(soulEquipment);
        }

        public bool TryUnequipSoulEquipment(SoulEquipmentItem soulEquipment)
        {
            if (soulEquipment == null)
                return false;

            var equipment = soulEquipment.soulEquipment;

            //Check if soul equipment is acquired already 
            var isAcquired = false;
            for (int i = 0; i < m_acquiredSoulEquipment.Count; i++)
            {
                if (m_acquiredSoulEquipment[i].item == soulEquipment)
                {
                    isAcquired = true;
                }
            }

            if (isAcquired == false)
                return false;

            if (!m_equippedSoulSlotEquipmentPair.TryGetValue(equipment.Slot, out SoulEquipmentItem equippedItem) ||
                equippedItem != soulEquipment)
                return false;

            m_equippedSoulSlotEquipmentPair.Remove(equipment.Slot);
            m_eqiuppedItems.RemoveAll(item => item.item == soulEquipment);

            // check if equiped item has soulskill
            if(soulEquipment.soulEquipment.soulSkillList != null)
            {
                foreach (SoulSkill soulSkill in equipment.soulSkillList)
                {
                    if (soulSkill.isFullyLearned)
                    {
                        m_soulSkillHandle.RemoveAsActivated(soulSkill, false);
                    }
                    else
                    {
                        m_soulSkillHandle.RemoveAsAcquired(soulSkill.id);
                    }
                    
                }

            }


            foreach (IEquipmentStatBoostModule statBoost in equipment.statBoostList)
            {
                statBoost.DetachFrom(m_player);
            }

            EquipmentChanged?.Invoke();
            return true;
        }

        [Button]
        public void AddAcquiredSoulEquipmentItem(SoulEquipmentItem soulEquipment)
        {
            var leveledItem = new LeveledEquipment(soulEquipment, 0);

            var equipment = soulEquipment.soulEquipment;
            m_acquiredSoulEquipment.Add(leveledItem);
            //check whether eqquipment has a soulskill
            //if (equipment.soulSkillList != null)
            //{
            //    //logic to set soul skills in acquired equipment as activated
            //    foreach (SoulSkill soulskill in equipment.soulSkillList)
            //    {
            //        m_soulSkillHandle.AddAsAcquired(soulskill.id);
            //        m_soulSkillHandle.SetActivationRestriction(soulskill.id, false);
            //    }
            //}

        }

        [Button]
        public void DebugAcquireAllEquipment(SoulEquipmentList soulEquipmentList)
        {
            ResetAllSoulSkillInEquipment();
            int[] ids = soulEquipmentList.GetIDs();
            for (int x = 0; x < ids.Length; x++)
            {
                AddAcquiredSoulEquipmentItem(soulEquipmentList.GetInfo(ids[x]));
            }

            foreach(var equipment in m_acquiredSoulEquipment)
            {
                var x = equipment.item.soulEquipment.soulSkillList;
                if(x != null)
                {
                    if (equipment.exp >= equipment.item.soulEquipment.ExpRequired)
                    {
                        SetSoulSkillFullyLearned(equipment.item.soulEquipment, true);
                    }
                    
                }
            }

            //foreach (int equipment in ids)
            //{
            //    AddAcquiredSoulEquipmentItem(m_data.GetInfo(equipment));
            //}
        }

        public List<LeveledEquipment> GetAcquiredSoulEquipment()
        {
            return m_acquiredSoulEquipment;
        }

        public SoulEquipmentList GetFullSoulEquipmentList()
        {
            return m_data;
        }
        public void SetSoulSkillFullyLearned(SoulEquipment equipment, bool setFullyLearned)
        {
            if(equipment.soulSkillList != null)
            {
                foreach (SoulSkill skill in equipment.soulSkillList)
                {
                    equipment.soulSkillList[0].SetFullyLearned(setFullyLearned);
                }
            }
        }

        public void ResetAllSoulSkillInEquipment()
        {

            if(m_data != null)
            {
                for(int x = 0; x < m_acquiredSoulEquipment.Count; x++) 
                {
                    SetSoulSkillFullyLearned(m_acquiredSoulEquipment[x].item.soulEquipment, false);
                }
            }
        }
        public bool TryGetEquippedSoulEquipment(SoulSlot slot, out SoulEquipmentItem equipmentItem)
        {
            return m_equippedSoulSlotEquipmentPair.TryGetValue(slot, out equipmentItem);
        }
        public void Initialize()
        {
            //initialize equipment for debugging
            //remove on final iteration of script
            m_acquiredSoulEquipment = new List<LeveledEquipment>();
            m_eqiuppedItems = new List<LeveledEquipment>();
            ResetAllSoulSkillInEquipment();
            DebugAcquireAllEquipment(GetFullSoulEquipmentList());
        }
    }
}

