using DChild.Gameplay.Combat;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Players.Modules
{
    public class EarthShaker : AttackBehaviour, IPlayerCritAttack
    {
        [SerializeField, HideLabel]
        private EarthShakerStatsInfo m_configuration;
        [SerializeField]
        private Vector2 m_momentumVelocity;
        //[SerializeField, MinValue(0.1f)]
        //private float m_fallSpeed;
        [SerializeField]
        private ParticleSystem m_chargeFX;
        [SerializeField]
        private ParticleSystem m_preLoopFX;
        [SerializeField]
        private ParticleSystem m_fallLoopFX;
        [SerializeField]
        private Collider2D m_fallCollider;
        [SerializeField]
        private ParticleSystem m_impactFX;
        [SerializeField]
        private Collider2D m_impactCollider;
        //[SerializeField, MinValue(0)]
        //private float m_fallDamageModifier = 1;
        //[SerializeField, MinValue(0)]
        //private float m_impactDamageModifier = 1;
        //GIGA NIGGA
        [SerializeField, Range(0f, 100f)]
        private float m_critChance;
        [SerializeField, MinValue(0), Tooltip("Multiply modifier by this value on critical hit")]
        private float m_critModifier;
        [SerializeField]
        private ParticleFX m_critFX;

        private bool m_canEarthShaker;
        private IPlayerModifer m_modifier;
        private Rigidbody2D m_rigidbody;
        private Damageable m_damageable;
        private int m_earthShakerAnimationParameter;
        private float m_originalGravity;


        public event EventAction<EventActionArgs> OnImpact;

        public bool CanEarthShaker() => m_canEarthShaker;

        public override void Initialize(ComplexCharacterInfo info)
        {
            base.Initialize(info);
            m_modifier = info.modifier;
            m_rigidbody = info.rigidbody;
            m_damageable = info.damageable;
            m_originalGravity = m_rigidbody.gravityScale;
            m_earthShakerAnimationParameter = info.animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.EarthShaker);
            m_canEarthShaker = true;
        }

        public void SetConfiguration(EarthShakerStatsInfo info)
        {
            m_configuration.CopyInfo(info);
        }

        public override void Cancel()
        {
            m_chargeFX?.Stop(true);
            m_preLoopFX?.Stop(true);
            m_fallLoopFX?.Stop(true);
            m_fallLoopFX?.Clear();
            m_fallCollider.enabled = false;
            m_impactFX?.Stop(true);
            m_impactCollider.enabled = false;
            m_rigidbody.gravityScale = m_originalGravity;
            m_rigidbody.velocity = Vector2.zero;
            m_canEarthShaker = true;
            m_animator.SetBool(m_earthShakerAnimationParameter, !m_canEarthShaker);
            m_state.isDoingEarthShaker = false;
            base.Cancel();
        }

        public void Impact()
        {
            //m_state.waitForBehaviour = true;
            m_attacker.SetDamageModifier(/*m_impactDamageModifier*/m_configuration.impactDamageModifier * m_modifier.Get(PlayerModifier.AttackDamage), 
                m_critChance,
                m_critModifier,
                m_critFX);
            m_rigidBody.WakeUp();
            m_fallLoopFX?.Stop(true);
            m_fallCollider.enabled = false;
            m_fallLoopFX.gameObject.SetActive(false); //feels hacky but it works fine 
            m_impactFX?.Play(true);
            m_impactCollider.enabled = true;
            m_rigidbody.velocity = Vector2.zero;
            OnImpact?.Invoke(this, EventActionArgs.Empty);
            //m_animator.SetBool(m_earthShakerAnimationParameter, false);
        }

        public void HandlePreFall()
        {
            m_state.waitForBehaviour = true;
            m_chargeFX?.Stop(true);
            m_preLoopFX?.Play(true);
            m_fallCollider.enabled = true;
            m_rigidbody.gravityScale = m_originalGravity;
            m_rigidbody.velocity = Vector2.down * /*m_fallSpeed*/m_configuration.fallSpeed;
        }

        public void HandleFall()
        {
            if ((m_fallLoopFX?.isPlaying ?? false) == false)
            {
                m_preLoopFX?.Stop(true);
                m_fallLoopFX?.Play(true);
            }
            m_rigidbody.velocity = Vector2.down * /*m_fallSpeed*/m_configuration.fallSpeed;
        }

        public void StartExecution()
        {
            m_damageable.SetInvulnerability(Invulnerability.Level_1);
            m_attacker.SetDamageModifier(/*m_fallDamageModifier*/m_configuration.fallDamageModifier * m_modifier.Get(PlayerModifier.AttackDamage), 
                m_critChance, 
                m_critModifier,
                m_critFX);
            m_rigidbody.velocity = /*Vector2.zero*/new Vector2(m_rigidbody.velocity.x * m_momentumVelocity.x, m_rigidbody.velocity.y * m_momentumVelocity.y);
            m_originalGravity = m_rigidbody.gravityScale;
            m_rigidbody.gravityScale = 0;
            m_chargeFX?.Play(true);
            m_state.isAttacking = true;
            m_state.canAttack = false;
            m_state.isDoingEarthShaker = true;
            m_animator.SetBool(m_animationParameter, true);
            m_canEarthShaker = false;
            m_animator.SetBool(m_earthShakerAnimationParameter, !m_canEarthShaker);
            m_fallLoopFX.gameObject.SetActive(true);
        }

        public void EndExecution()
        {
            m_damageable.SetInvulnerability(Invulnerability.None);
            m_fallLoopFX?.Stop(true);
            m_impactFX?.Stop(true);
            m_canEarthShaker = true;
            m_impactCollider.enabled = false;
            m_rigidbody.gravityScale = m_originalGravity;
            m_animator.SetBool(m_earthShakerAnimationParameter, !m_canEarthShaker);
            m_state.isDoingEarthShaker = false;
            base.AttackOver();
        }

        public void SetCritConfiguration(PlayerCritStatsInfo info)
        {
            m_critChance = info.critChance;
            m_critModifier = info.critModifier;
        }

        public void SetCritConfiguration(List<PlayerCritStatsInfo> info)
        {
        }

        public void SetCritConfiguration(PlayerCritStatsInfo overheadInfo, PlayerCritStatsInfo midairForwardInfo, PlayerCritStatsInfo midairOverheadInfo, PlayerCritStatsInfo crouchInfo)
        {
        }

        public void SetCritConfiguration(PlayerCritStatsInfo forwardInfo, PlayerCritStatsInfo overheadInfo, PlayerCritStatsInfo midairForwardInfo, PlayerCritStatsInfo midairOverheadInfo, PlayerCritStatsInfo crouchInfo)
        {
        }

        public override void IncreaseCritChance(float critChance)
        {
            m_critChance += critChance;
        }

        public override void IncreaseCritDamage(float critDamage)
        {
            m_critModifier += critDamage;
        }
    }
}
