using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.EquipmentSystem
{
    public class ResistanceModifier : IEquipmentStatBoostModule
    {
        [SerializeField]
        private float m_resistanceValue;

        public void AttachTo(IPlayer player)
        {
            foreach (DamageType damageType in Enum.GetValues(typeof(DamageType)))
            {
                player.attackResistance.AddResistance(damageType, m_resistanceValue);
            }
        }

        public void DetachFrom(IPlayer player)
        {
            foreach (DamageType damageType in Enum.GetValues(typeof(DamageType)))
            {
                player.attackResistance.ReduceResistance(damageType, -m_resistanceValue);
            }
        }

        public StatBoostType GetBoostType() => StatBoostType.Defense;

        public float GetModifierValue() => m_resistanceValue;
    }
}

