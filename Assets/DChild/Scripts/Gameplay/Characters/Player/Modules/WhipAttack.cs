using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Players.Modules
{
    public class WhipAttack : AttackBehaviour, IPlayerCritAttack
    {
        public struct WhipAttackEventArgs : IEventActionArgs
        {
            public Type type;

            public WhipAttackEventArgs(Type type)
            {
                this.type = type;
            }
        }
        public enum Type
        {
            Ground_Forward,
            Ground_Overhead,
            MidAir_Forward,
            MidAir_Overhead,
            Crouch_Forward
        }

        [SerializeField, HideLabel]
        private WhipAttackStatsInfo m_configuration;
        //[SerializeField]
        //private float m_whipMovementCooldown;
        //[SerializeField]
        //private Vector2 m_momentumVelocity;
        [SerializeField]
        private Info m_groundForward;
        [SerializeField]
        private Info m_groundOverhead;
        [SerializeField]
        private Info m_midAirForward;
        [SerializeField]
        private Info m_midAirOverhead;
        [SerializeField]
        private Info m_crouchForward;
        //[SerializeField]
        //private float m_aerialGravity;

        [SerializeField, BoxGroup("Whip VFX")]
        private ElementalSwordFX m_overheadWhipVFX;
        [SerializeField, BoxGroup("Whip VFX")]
        private ElementalSwordFX m_crouchForwardVFX;
        [SerializeField, BoxGroup("Whip VFX")]
        private ElementalSwordFX m_midairOverheadVFX;
        [SerializeField, BoxGroup("Whip VFX")]
        private ElementalSwordFX m_midairForwardVFX;

        private bool m_canMove;
        private IPlayerModifer m_modifier;
        private int m_whipAttackAnimationParameter;
        private int m_yInputParameter;
        private List<Type> m_executedTypes;
        private Rigidbody2D m_rigidbody;
        private float m_cacheGravity;
        private bool m_adjustGravity;
        private bool m_canAirWhip;
        private float m_whipMovementCooldownTimer;

        public event EventAction<WhipAttackEventArgs> OnWhip;

        public bool CanMove() => m_canMove;
        public bool CanAirWhip() => m_canAirWhip;
        public bool IsGravityAdjusted() => m_adjustGravity;

        public override void Initialize(ComplexCharacterInfo info)
        {
            base.Initialize(info);

            m_modifier = info.modifier;
            m_executedTypes = new List<Type>();
            m_whipAttackAnimationParameter = info.animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.WhipAttack);
            m_yInputParameter = info.animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.YInput);
            m_rigidbody = info.rigidbody;
            m_cacheGravity = m_rigidbody.gravityScale;
            m_adjustGravity = true;
            m_canAirWhip = true;
        }

        public void SetConfiguration(WhipAttackStatsInfo info)
        {
            m_configuration.CopyInfo(info);
        }

        public override void Cancel()
        {
            m_rigidbody.gravityScale = m_configuration.defaultGravity;
            m_state.waitForBehaviour = false;
            m_adjustGravity = true;

            if (m_executedTypes.Count > 0)
            {
                base.Cancel();
                m_animator.SetBool(m_whipAttackAnimationParameter, false);

                for (int i = 0; i < m_executedTypes.Count; i++)
                {
                    var type = m_executedTypes[i];
                    EnableCollision(type, false);
                }

                m_executedTypes.Clear();
            }
        }

        public void EnableCollision(Type type, bool value)
        {
            m_rigidBody.WakeUp();

            switch (type)
            {
                case Type.Ground_Forward:
                    m_groundForward.ShowCollider(value);
                    break;
                case Type.Ground_Overhead:
                    m_groundOverhead.ShowCollider(value);
                    break;
                case Type.MidAir_Forward:
                    m_midAirForward.ShowCollider(value);
                    break;
                case Type.MidAir_Overhead:
                    m_midAirOverhead.ShowCollider(value);
                    break;
                case Type.Crouch_Forward:
                    m_crouchForward.ShowCollider(value);
                    break;
            }

            if (value)
            {
                Record(type);
            }
            else
            {
                m_executedTypes.Remove(type);
            }
        }

        public void Execute(Type type)
        {
            m_canMove = false;
            m_state.canAttack = false;
            m_state.isAttacking = true;
            m_state.waitForBehaviour = true;
            m_animator.SetBool(m_animationParameter, true);
            m_animator.SetBool(m_whipAttackAnimationParameter, true);

            switch (type)
            {
                case Type.Ground_Forward:
                    //m_state.canAttack = true;
                    //m_state.isAttacking = false;
                    //m_state.waitForBehaviour = false;
                    m_animator.SetFloat(m_yInputParameter, 0);
                    m_timer = m_groundForward.nextAttackDelay;
                    m_attacker.SetDamageModifier(m_groundForward.damageModifier * m_modifier.Get(PlayerModifier.AttackDamage), 
                        m_groundForward.critChance, 
                        m_groundForward.critModifier, 
                        m_groundForward.critFX);
                    break;
                case Type.Ground_Overhead:
                    m_animator.SetFloat(m_yInputParameter, 1);
                    m_timer = m_groundOverhead.nextAttackDelay;
                    m_attacker.SetDamageModifier(m_groundOverhead.damageModifier * m_modifier.Get(PlayerModifier.AttackDamage), 
                        m_groundOverhead.critChance, 
                        m_groundOverhead.critModifier, 
                        m_groundOverhead.critFX);
                    m_overheadWhipVFX.Play();
                    break;
                case Type.MidAir_Forward:
                    m_animator.SetFloat(m_yInputParameter, 0);
                    m_timer = m_midAirForward.nextAttackDelay;
                    m_attacker.SetDamageModifier(m_midAirForward.damageModifier * m_modifier.Get(PlayerModifier.AttackDamage), 
                        m_midAirForward.critChance, 
                        m_midAirForward.critModifier,
                        m_midAirForward.critFX);
                    m_midairForwardVFX.Play();
                    m_canAirWhip = false;

                    if (m_adjustGravity == true)
                    {
                        //m_cacheGravity = m_rigidbody.gravityScale;
                        m_rigidbody.gravityScale = /*m_aerialGravity*/m_configuration.aerialGravity;
                        m_rigidbody.velocity = /*Vector2.zero*/new Vector2(m_rigidbody.velocity.x * m_configuration.momentumVelocity.x, m_rigidbody.velocity.y * m_configuration.momentumVelocity.y);
                    }

                    break;
                case Type.MidAir_Overhead:
                    m_animator.SetFloat(m_yInputParameter, 1);
                    m_timer = m_midAirOverhead.nextAttackDelay;
                    m_attacker.SetDamageModifier(m_midAirOverhead.damageModifier * m_modifier.Get(PlayerModifier.AttackDamage), 
                        m_midAirOverhead.critChance, 
                        m_midAirOverhead.critModifier,
                        m_midAirOverhead.critFX);
                    m_midairOverheadVFX.Play();
                    m_canAirWhip = false;

                    if (m_adjustGravity == true)
                    {
                        //m_cacheGravity = m_rigidbody.gravityScale;
                        m_rigidbody.gravityScale = /*m_aerialGravity*/m_configuration.aerialGravity;
                        m_rigidbody.velocity = /*Vector2.zero*/new Vector2(m_rigidbody.velocity.x * m_configuration.momentumVelocity.x, m_rigidbody.velocity.y * m_configuration.momentumVelocity.y);
                    }

                    break;
                case Type.Crouch_Forward:
                    m_animator.SetFloat(m_yInputParameter, -1);
                    m_timer = m_crouchForward.nextAttackDelay;
                    m_attacker.SetDamageModifier(m_crouchForward.damageModifier * m_modifier.Get(PlayerModifier.AttackDamage),
                        m_crouchForward.critChance, 
                        m_crouchForward.critModifier, 
                        m_crouchForward.critFX);
                    m_crouchForwardVFX.Play();
                    break;
            }
            Record(type);
            OnWhip?.Invoke(this, new WhipAttackEventArgs(type));
        }

        public void PlayFXFor(Type type, bool play)
        {
            switch (type)
            {
                case Type.Ground_Forward:
                    m_groundForward.PlayFX(play);
                    //m_attackFX.transform.position = m_groundOverhead.fxPosition.position;
                    break;
                case Type.Ground_Overhead:
                    m_groundOverhead.PlayFX(play);
                    //m_attackFX.transform.position = m_groundOverhead.fxPosition.position;
                    //m_fxAnimator.SetTrigger("GroundOverhead");

                    break;
                case Type.Crouch_Forward:
                    m_crouchForward.PlayFX(play);
                    //m_attackFX.transform.position = m_crouch.fxPosition.position;
                    //m_fxAnimator.SetTrigger("Crouch");

                    break;
                case Type.MidAir_Forward:
                    m_midAirForward.PlayFX(play);
                    //m_attackFX.transform.position = m_midAirForward.fxPosition.position;
                    //m_fxAnimator.Play("JumpSlash");

                    break;
                case Type.MidAir_Overhead:
                    m_midAirOverhead.PlayFX(play);
                    //m_attackFX.transform.position = m_midAirOverhead.fxPosition.position;
                    //m_fxAnimator.SetTrigger("JumpOverhead");
                    break;
            }
        }
        public void ClearFXFor(Type type)
        {
            switch (type)
            {
                case Type.Ground_Forward:
                    m_groundForward.ClearFX();
                    break;
                case Type.Ground_Overhead:
                    m_groundOverhead.ClearFX();
                    break;
                case Type.Crouch_Forward:
                    m_crouchForward.ClearFX();
                    break;
                case Type.MidAir_Forward:
                    m_midAirForward.ClearFX();
                    break;
                case Type.MidAir_Overhead:
                    m_midAirOverhead.ClearFX();
                    break;
            }
        }

        public override void AttackOver()
        {
            base.AttackOver();

            m_canMove = true;
            if (m_state.isDoingCombo == true)
            {
                m_state.isDoingCombo = false;
            }

            m_animator.SetBool(m_whipAttackAnimationParameter, false);
            m_rigidbody.gravityScale = m_configuration.defaultGravity;
            m_adjustGravity = false;
        }

        public void HandleNextAttackDelay()
        {
            if (m_timer >= 0)
            {
                m_timer -= GameplaySystem.time.deltaTime;
                if (m_timer <= 0)
                {
                    m_timer = 1.5f;
                    m_state.canAttack = true;
                }
            }
        }

        public void ResetAerialGravityControl()
        {
            m_adjustGravity = true;
        }

        public void ResetAirAttacks()
        {
            m_canAirWhip = true;
        }

        public void ClearExecutedCollision()
        {
            //for (int i = 0; i < m_executedTypes.Count; i++)
            //{
            //    var type = m_executedTypes[i];
            //    EnableCollision(type, false);
            //}

            foreach (Type type in Enum.GetValues(typeof(Type)))
            {
                EnableCollision(type, false);
            }

            m_executedTypes.Clear();
        }

        private void Record(Type type)
        {
            if (m_executedTypes.Contains(type) == false)
            {
                m_executedTypes.Add(type);
            }
        }

        public void HandleMovementTimer()
        {
            if (m_whipMovementCooldownTimer > 0)
            {
                m_whipMovementCooldownTimer -= GameplaySystem.time.deltaTime;
                m_canMove = false;
            }
            else
            {
                if (!m_animator.GetBool(m_whipAttackAnimationParameter))
                {
                    //Debug.Log("Can Move");
                    m_whipMovementCooldownTimer = /*m_whipMovementCooldown*/m_configuration.whipMovementCooldown;
                    m_canMove = true;
                }
            }
        }

        public void SetCritConfiguration(PlayerCritStatsInfo info)
        {

        }

        public void SetCritConfiguration(List<PlayerCritStatsInfo> info)
        {

        }

        public void SetCritConfiguration(PlayerCritStatsInfo overheadInfo, PlayerCritStatsInfo midairForwardInfo, PlayerCritStatsInfo midairOverheadInfo, PlayerCritStatsInfo crouchInfo)
        {

        }

        public void SetCritConfiguration(PlayerCritStatsInfo forwardInfo, PlayerCritStatsInfo overheadInfo, PlayerCritStatsInfo midairForwardInfo, PlayerCritStatsInfo midairOverheadInfo, PlayerCritStatsInfo crouchInfo)
        {
            m_groundForward.SetCritConfiguration(forwardInfo);
            m_groundOverhead.SetCritConfiguration(overheadInfo);
            m_midAirForward.SetCritConfiguration(midairForwardInfo);
            m_midAirOverhead.SetCritConfiguration(midairOverheadInfo);
            m_crouchForward.SetCritConfiguration(crouchInfo);
        }

        public override void IncreaseCritChance(float critChance)
        {
            m_groundForward.IncreaseCritChance(critChance);
            m_groundOverhead.IncreaseCritChance(critChance);
            m_midAirForward.IncreaseCritChance(critChance);
            m_midAirOverhead.IncreaseCritChance(critChance);
            m_crouchForward.IncreaseCritChance(critChance);
        }

        public override void IncreaseCritDamage(float critDamage)
        {
            m_groundForward.IncreaseCritDamage(critDamage);
            m_groundOverhead.IncreaseCritDamage(critDamage);
            m_midAirForward.IncreaseCritDamage(critDamage);
            m_midAirOverhead.IncreaseCritDamage(critDamage);
            m_crouchForward.IncreaseCritDamage(critDamage);
        }
    }
}
