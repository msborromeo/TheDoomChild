using DChild.Gameplay.Characters.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DChild.Gameplay.EquipmentSystem
{
    public class HPModifier : IEquipmentStatBoostModule
    {
        [SerializeField]
        private int m_HPModifierValue;

        private int m_oldMaxHealth;

        public StatBoostType GetBoostType() => StatBoostType.HP;

        public void AttachTo(IPlayer player)
        {
            m_oldMaxHealth = player.health.maxValue;
            player.health.SetMaxValue(m_HPModifierValue);
            var currentHealthValue = player.health.currentValue;
            player.stats.AddStat(PlayerStat.Health, currentHealthValue);
            //need to reset to max value?
            player.health.ResetValueToMax();
        }

        public void DetachFrom(IPlayer player)
        {
            player.health.SetMaxValue(m_oldMaxHealth);
            var currentHealthValue = player.health.currentValue;
            player.stats.AddStat(PlayerStat.Health, -currentHealthValue);
            //need to reset to max value?
            player.health.ResetValueToMax();
        }

    }
}

