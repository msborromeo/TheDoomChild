using DChild.Gameplay.Characters.Players.SoulSkills;
using DChild.Gameplay.Systems;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.SoulEquipment
{
    public class SoulEquipment
    {
        [SerializeField]
        private SoulSlot m_slot;
        [SerializeField]
        private List<SoulSkill> m_soulSkillList;
    }
}

