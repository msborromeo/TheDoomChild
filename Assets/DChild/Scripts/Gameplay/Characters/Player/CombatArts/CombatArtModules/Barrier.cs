using DChild.Gameplay.Characters.Players.Modules;
using DChild.Gameplay.Combat;
using Holysoft.Gameplay;
using Sirenix.OdinInspector;
using Spine.Unity;
using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Players.BattleAbilityModule
{
    [System.Serializable]
    public struct BarrierStatsInfo
    {
        [SerializeField]
        private float m_sourceRequiredAmount;
        public float sourceRequiredAmount => m_sourceRequiredAmount;
        [SerializeField]
        private float m_sourceConsumptionRate;
        public float sourceConsumptionRate => m_sourceConsumptionRate;

        public void CopyInfo(BarrierStatsInfo reference)
        {
            m_sourceRequiredAmount = reference.sourceRequiredAmount;
            m_sourceConsumptionRate = reference.sourceConsumptionRate;
        }
    }

    public class Barrier : AttackBehaviour, IInterruptableCombatArtModule
    {
        [SerializeField]
        private BarrierStatsInfo m_configuration;

        [SerializeField]
        private SkeletonAnimation m_attackFX;

        [SerializeField]
        private float m_barrierMovementCooldown;
        [SerializeField]
        private Info m_barrierInfo;
        [SerializeField, BoxGroup("Physics")]
        private Character m_character;
        [SerializeField, BoxGroup("Physics")]
        private Rigidbody2D m_physics;
        [SerializeField]
        private Hitbox m_hitbox;
        [SerializeField, BoxGroup("FX")]
        private Animator m_barrierFX;
        [SerializeField, BoxGroup("FX")]
        private MaterialReplacementExample m_materialReplacement;

        [SerializeField]
        private Vector2 m_pushForce;

        private bool m_isDoingBarrier;
        private bool m_canMove;
        private IPlayerModifer m_modifier;
        private int m_barrierStateAnimationParameter;
        private float m_barrierMovementCooldownTimer;
        private ICappedStat m_source;
        private float m_stackedConsumptionRate;

        private Animator m_fxAnimator;
        private SkeletonAnimation m_skeletonAnimation;

        public bool CanMove() => m_canMove;
        public bool IsDoingBarrier() => m_isDoingBarrier;
        public bool HaveEnoughSourceForExecution() => m_configuration.sourceRequiredAmount <= m_source.currentValue;

        private Coroutine m_barrierHoldRoutine;

        public override void Initialize(ComplexCharacterInfo info)
        {
            base.Initialize(info);

            m_source = info.magic;
            m_modifier = info.modifier;
            m_barrierStateAnimationParameter = info.animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.Barrier);
            //m_canbarrier = true;
            m_canMove = true;
            m_barrierMovementCooldownTimer = m_barrierMovementCooldown;

            m_fxAnimator = m_attackFX.gameObject.GetComponentInChildren<Animator>();
            m_skeletonAnimation = m_attackFX.gameObject.GetComponent<SkeletonAnimation>();
        }

        public void ConsumeSource()
        {
            m_stackedConsumptionRate += (m_configuration.sourceConsumptionRate * GameplaySystem.time.deltaTime) * m_modifier.Get(PlayerModifier.ShadowMagic_Requirement);

            if (m_stackedConsumptionRate >= 1)
            {
                var integer = Mathf.FloorToInt(m_stackedConsumptionRate);
                m_stackedConsumptionRate -= integer;
                m_source.ReduceCurrentValue(integer);
            }
        }

        public override void Reset()
        {
            m_state.waitForBehaviour = false;
            m_state.isAttacking = false;
            //m_barrierInfo.ShowCollider(false);
            m_animator.SetBool(m_barrierStateAnimationParameter, false);
            base.Reset();
        }

        public void Execute()
        {
            m_state.waitForBehaviour = false;
            m_state.isExecutingCombatArt = true;
            m_state.isAttacking = true;
            m_state.canAttack = false;
            m_animator.SetBool(m_animationParameter, true);
            m_animator.SetBool(m_barrierStateAnimationParameter, true);
            m_barrierMovementCooldownTimer = m_barrierMovementCooldown;
            m_isDoingBarrier = true;
            //m_attacker.SetDamageModifier(m_slashComboInfo[m_currentSlashState].damageModifier * m_modifier.Get(PlayerModifier.AttackDamage));
        }

        public void EndExecution()
        {
            m_canMove = true;
            m_state.waitForBehaviour = false;
            m_state.isAttacking = false;
            m_barrierFX.SetBool("BarrierIsOn", false);
            m_materialReplacement.replacementEnabled = false;
            m_isDoingBarrier = false;
            m_animator.SetBool(m_barrierStateAnimationParameter, false);
            m_state.isExecutingCombatArt = false;
            base.AttackOver();
        }

        public void SetCanMove(bool canMove)
        {
            m_canMove = canMove;
        }

        public override void Cancel()
        {
            //if (m_barrierHoldRoutine != null)
            //{
            //    StopCoroutine(m_barrierHoldRoutine);
            //    m_barrierHoldRoutine = null;
            //}
            m_physics.velocity = Vector2.zero;
            //m_barrierInfo.ShowCollider(false);

            m_barrierFX.SetBool("BarrierIsOn", false);
            m_materialReplacement.replacementEnabled = false;
            m_isDoingBarrier = false;
            m_animator.SetBool(m_barrierStateAnimationParameter, false);
            m_state.isExecutingCombatArt = false;
            base.Cancel();
        }

        public void EnableShield(bool value)
        {
            m_rigidBody.WakeUp();
            //m_barrierInfo.ShowCollider(value);
            m_attackFX.transform.position = m_barrierInfo.fxPosition.position;
            m_physics.velocity = Vector2.zero;

            m_hitbox.SetCanBlockDamageState(value);
            if (value)
            {
                m_barrierFX.SetBool("BarrierIsOn", true);
                m_materialReplacement.replacementEnabled = true;
                m_isDoingBarrier = true;
            }
            else
            {
                m_barrierFX.SetBool("BarrierIsOn", false);
                m_materialReplacement.replacementEnabled = false;
                m_isDoingBarrier = false;
            }

            m_physics.AddForce(new Vector2(m_character.facing == HorizontalDirection.Right ? m_pushForce.x : -m_pushForce.x, m_pushForce.y), ForceMode2D.Impulse);
        }

        public void HandleMovementTimer()
        {
            if (m_barrierMovementCooldownTimer > 0)
            {
                m_barrierMovementCooldownTimer -= GameplaySystem.time.deltaTime;
                m_canMove = false;
            }
            else
            {
                //Debug.Log("Can Move");
                m_barrierMovementCooldownTimer = m_barrierMovementCooldown;
                m_canMove = true;
            }
        }
        public void SetConfiguration(BarrierStatsInfo info)
        {
            m_configuration.CopyInfo(info);
        }

        private IEnumerator BarrierHoldRoutine()
        {
            while (true)
            {
                m_state.waitForBehaviour = false;
                m_state.isAttacking = true;
                yield return null;
            }
        }
    }
}
