using DChild.Gameplay.Characters.Players;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.EquipmentSystem
{
    public class SPRegenRateModifier : IEquipmentStatBoostModule
    {
        [SerializeField, SuffixLabel("%", Overlay = true)]
        private float m_shadowRegenerationRateValue;

        private float m_defaultShadowRegenValue;
        private float m_bonusApplied;

        public void AttachTo(IPlayer player)
        {
            m_defaultShadowRegenValue = player.modifiers.Get(PlayerModifier.ShadowMagicRegeneration);
            m_bonusApplied = m_defaultShadowRegenValue * (m_shadowRegenerationRateValue / 100f);
            player.modifiers.Add(PlayerModifier.ShadowMagicRegeneration, m_bonusApplied);
        }

        public void DetachFrom(IPlayer player)
        {
            player.modifiers.Add(PlayerModifier.ShadowMagicRegeneration, -m_bonusApplied);
        }
    }
}

