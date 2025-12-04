using DChild.Gameplay.Characters.Players;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.EquipmentSystem
{
    public class CritChanceModifier : IEquipmentStatBoostModule
    {
        [SerializeField, SuffixLabel("%", Overlay = true)]
        private float m_critChanceValue;
        public StatBoostType GetBoostType() => StatBoostType.Crit_Rate;
        public float GetModifierValue() => m_critChanceValue;

        public void AttachTo(IPlayer player)
        {
            player.criticalHitHandle.ModifyCritChance(m_critChanceValue);
        }

        public void DetachFrom(IPlayer player)
        {
            player.criticalHitHandle.ModifyCritChance(-m_critChanceValue);
        }

    }
}

