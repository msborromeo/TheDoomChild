using DChild.Gameplay.Characters.Players.SoulSkills;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
#endif
namespace DChild.Gameplay.Characters.Players.SoulSkills
{
    public class MaxHpModifier : ISoulSkillModule
    {
        [SerializeField]
        private int m_maxHealthValue;

        private int m_oldMaxHealth;
        public void AttachTo(int soulSkillInstanceID, IPlayer player)
        {
            m_oldMaxHealth = player.health.maxValue;
            player.health.SetMaxValue(m_maxHealthValue);
            var currentHealthValue = player.health.currentValue;
            player.stats.AddStat(PlayerStat.Health, currentHealthValue);
            //need to reset to max value?
            player.health.ResetValueToMax();

        }

        public void DetachFrom(int soulSkillInstanceID, IPlayer player)
        {
            player.health.SetMaxValue(m_oldMaxHealth);
            var currentHealthValue = player.health.currentValue;
            player.stats.AddStat(PlayerStat.Health, -currentHealthValue);
            //need to reset to max value?
            player.health.ResetValueToMax();
        }

        #region TestingWaters
        //to test, make this a MonoBehaviour and attach it to a GameObject, Assign Player object from scene into the IPlayer
        //[Button]
        //private void AttachToTest(IPlayer player)
        //{
        //    m_oldMaxHealth = player.health.maxValue;
        //    player.health.SetMaxValue(m_maxHealthValue);
        //    var currentHealthValue = player.health.currentValue;
        //    player.stats.AddStat(PlayerStat.Health, currentHealthValue);
        //    need to reset to max value?  
        //    player.health.ResetValueToMax();
        //}
        //[Button]
        //private void DetatchToTest(IPlayer player)
        //{
        //    player.health.SetMaxValue(m_oldMaxHealth);
        //    var currentHealthValue = player.health.currentValue;
        //    player.stats.AddStat(PlayerStat.Health, -currentHealthValue);
        //    need to reset to max value?
        //    player.health.ResetValueToMax();
        //}
        #endregion
    }
}
