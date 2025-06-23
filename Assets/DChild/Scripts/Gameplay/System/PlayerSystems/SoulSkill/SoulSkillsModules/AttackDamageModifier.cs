using DChild.Gameplay.Combat;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace DChild.Gameplay.Characters.Players.SoulSkills
{
    public struct AttackDamageModifier : ISoulSkillModule
    {
        [SerializeField, HideLabel]
        private int m_damageValue;

        public void AttachTo(int soulSkillInstanceID, IPlayer player)
        {
            //Damage temp = player.weapon.damage;
            //player.weapon.SetAddedDamage(Calculateddamage);
            //float damage = temp.value * (m_damageValue / 100f);
            //int Calculateddamage = (int)Math.Ceiling(damage);
            var curdamage = player.stats.GetTotalStat(PlayerStat.Attack);
            float damage = curdamage * (m_damageValue / 100f);
            int Calculateddamage = (int)Math.Ceiling(damage);
            player.stats.AddStat(PlayerStat.Attack, Calculateddamage);
        }

        public void DetachFrom(int soulSkillInstanceID, IPlayer player)
        {
            //player.weapon.SetAddedDamage(0);
            var curdamage = player.stats.GetTotalStat(PlayerStat.Attack);
            float damage = curdamage * (m_damageValue / 100f);
            int Calculateddamage = (int)Math.Ceiling(damage);
            player.stats.AddStat(PlayerStat.Attack, -Calculateddamage);
        }
    }
}