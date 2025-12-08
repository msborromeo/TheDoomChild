using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.EquipmentSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DChild.Gameplay.Combat
{
    public class CritDamageModifier : IEquipmentStatBoostModule
    {
        [SerializeField, MinValue(0), Tooltip("Multiply modifier by this value on critical hit")]
        private float m_critDamageModifier;

        public void AttachTo(IPlayer player)
        {
            player.criticalHitHandle.ModifyCritDamage(m_critDamageModifier);
        }

        public void DetachFrom(IPlayer player)
        {
            player.criticalHitHandle.ModifyCritDamage(-m_critDamageModifier);
        }

        public StatBoostType GetBoostType() => StatBoostType.Crit_DMG;

        public float GetModifierValue() => m_critDamageModifier;
    }
}

