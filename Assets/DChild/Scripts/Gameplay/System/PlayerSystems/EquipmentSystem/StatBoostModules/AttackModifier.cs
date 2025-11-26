using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.EquipmentSystem;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace DChild.Gameplay.EquipmentSystem
{
    public class AttackModifier : IEquipmentStatBoostModule
    {
        [SerializeField] private StatBoostType m_boostType;
        public StatBoostType boostType => m_boostType;

        [SerializeField]
        private int m_attackModifierValue;

        public StatBoostType GetBoostType() => StatBoostType.Attack;

        public void AttachTo(IPlayer player)
        {
            var curdamage = player.stats.GetTotalStat(PlayerStat.Attack);
            float damage = curdamage * (m_attackModifierValue / 100f);
            int Calculateddamage = (int)Math.Ceiling(damage);
            player.stats.AddStat(PlayerStat.Attack, Calculateddamage);
        }

        public void DetachFrom(IPlayer player)
        {
            var curdamage = player.stats.GetTotalStat(PlayerStat.Attack);
            float damage = curdamage * (m_attackModifierValue / 100f);
            int Calculateddamage = (int)Math.Ceiling(damage);
            player.stats.AddStat(PlayerStat.Attack, -Calculateddamage);
        }
    }
}

