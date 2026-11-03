using DChild.Gameplay.Characters.Players.Modules;
using DChild.Gameplay.Combat;
using DChild.Gameplay.Projectiles;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Players.SoulSkills
{
    public class SkullThrowModifier : ISoulSkillModule
    {
        [SerializeField, HideLabel]
        private int m_damageValue;
        private int m_originalDamage;

        public void AttachTo(int soulSkillInstanceID, IPlayer player)
        {
            var playerObject = GameplaySystem.playerManager.player.character;
            var projectile = playerObject.GetComponentInChildren<ProjectileThrow>();
            projectile.SetDamageModifier(m_damageValue);
        }



        public void DetachFrom(int soulSkillInstanceID, IPlayer player)
        {
            var playerObject = GameplaySystem.playerManager.player.character;
            var projectile = playerObject.GetComponentInChildren<ProjectileThrow>();
            projectile?.SetDamageModifier(-m_damageValue);
        }
    }
}

