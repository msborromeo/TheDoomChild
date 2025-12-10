using DChild.Gameplay.Characters.Players.SoulSkills;
using DChild.Gameplay.Systems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.EquipmentSystem
{
    [System.Serializable]
    public class SoulEquipment
    {
        [SerializeField]
        private string m_name;
        [SerializeField]
        private SoulSlot m_slot;
        [SerializeField]
        private int m_expRequired = 200;
        [OdinSerialize]
        private List<IEquipmentStatBoostModule> m_statBoostList;
        [SerializeField]
        private List<SoulSkill> m_soulSkillList;

        public SoulSlot Slot => m_slot;
        public int ExpRequired => m_expRequired;
        public List<IEquipmentStatBoostModule> statBoostList => m_statBoostList;
        public List<SoulSkill> soulSkillList => m_soulSkillList;
    }
}

