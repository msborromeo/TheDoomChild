using DChild.Gameplay.Characters.Players.SoulSkills;
using DChild.Gameplay.Systems;
using Sirenix.OdinInspector;
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
        private List<SoulSkill> m_soulSkillList;

        public SoulSlot Slot => m_slot;
        public List<SoulSkill> soulSkillList => m_soulSkillList;
    }
}

