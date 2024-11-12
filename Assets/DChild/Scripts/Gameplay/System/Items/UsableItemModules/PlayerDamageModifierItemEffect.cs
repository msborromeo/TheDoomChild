using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Combat;
using DChild.Gameplay.Combat.StatusAilment;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace DChild.Gameplay.Items
{
    [System.Serializable]

    public struct PlayerDamageModifierItemEffect : IDurationItemEffect
    {
        [SerializeField, SuffixLabel("%", overlay: true)]
        private int m_addeddamage;


        public void StartEffect(IPlayer player)
        {
            int temp = player.stats.GetBaseStat(PlayerStat.Attack);
            float damage = temp * (m_addeddamage / 100f);
            int Calculateddamage = (int)Math.Ceiling(damage);
            player.weapon.SetAddedDamage(Calculateddamage);
        }

        public void StopEffect(IPlayer player)
        {
            //will conflict if other items are using this
            //function and we don't want their effects to stop
            player.weapon.SetAddedDamage(0);
        }
    }
}
