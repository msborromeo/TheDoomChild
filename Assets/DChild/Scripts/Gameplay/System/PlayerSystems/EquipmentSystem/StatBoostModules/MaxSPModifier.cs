using DChild.Gameplay.Characters.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.EquipmentSystem
{
    public class MaxSPModifier : IEquipmentStatBoostModule
    {
        [SerializeField]
        private int m_SPModifierValue;

        private int m_oldMaxSP;

        public void AttachTo(IPlayer player)
        {
            m_oldMaxSP = player.magic.maxValue;
            player.magic.SetMaxValue(m_SPModifierValue);
            var currentSPValue = player.magic.currentValue;
            player.stats.AddStat(PlayerStat.Magic, currentSPValue);
            player.magic.ResetValueToMax();
        }

        public void DetachFrom(IPlayer player)
        {
            player.magic.SetMaxValue(m_oldMaxSP);
            var currentSPValue = player.magic.currentValue;
            player.stats.AddStat(PlayerStat.Magic, -currentSPValue);
            player.magic.ResetValueToMax();
        }

        public StatBoostType GetBoostType() => StatBoostType.SP;

        public float GetModifierValue() => m_SPModifierValue;
    }
}

