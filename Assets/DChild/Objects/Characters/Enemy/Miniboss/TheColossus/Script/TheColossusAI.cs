using DChild.Gameplay.Characters.AI;
using DChild.Gameplay.Combat;
using DChild.Gameplay.Pooling;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Enemies
{
    [AddComponentMenu("DChild/Gameplay/Enemies/Miniboss/TheColossus")]
    public class TheColossusAI : CombatAIBrain<TheColossusAI.Info>
    {
        [System.Serializable]
        public class Info : BaseInfo
        {
            [SerializeField]
            private PhaseInfo<Phase> m_phaseInfo;
            public PhaseInfo<Phase> phaseInfo => m_phaseInfo;

            [Title("Attacks Info")]
            [SerializeField, TabGroup("PillarSmash")]
            private SimpleAttackInfo m_leftRightPillarSmashFirstSmashDown = new SimpleAttackInfo();
            public SimpleAttackInfo leftRightPillarSmashFirstSmashDown => m_leftRightPillarSmashFirstSmashDown;
            [SerializeField, TabGroup("PillarSmash")]
            private SimpleAttackInfo m_leftRightPillarSmashFirstSmashLoop = new SimpleAttackInfo();
            public SimpleAttackInfo leftRightPillarSmashFirstSmashLoop => m_leftRightPillarSmashFirstSmashLoop;
            [SerializeField, TabGroup("PillarSmash")]
            private SimpleAttackInfo m_pillarSmashLeftAttackEnd = new SimpleAttackInfo();
            public SimpleAttackInfo pillarSmashLeftAttackEnd => m_pillarSmashLeftAttackEnd;
            [SerializeField, TabGroup("PillarSmash")]
            private SimpleAttackInfo m_leftRightPillarSmashSecondSmashDown = new SimpleAttackInfo();
            public SimpleAttackInfo leftRightPillarSmashSecondSmashDown => m_leftRightPillarSmashSecondSmashDown;
            [SerializeField, TabGroup("PillarSmash")]
            private SimpleAttackInfo m_rightLeftPillarSmashFirstSmashDown = new SimpleAttackInfo();
            public SimpleAttackInfo rightLeftPillarSmashFirstSmashDown => m_rightLeftPillarSmashFirstSmashDown;
            [SerializeField, TabGroup("PillarSmash")]
            private SimpleAttackInfo m_rightLeftPillarSmashFirstSmashLoop = new SimpleAttackInfo();
            public SimpleAttackInfo rightLeftPillarSmashFirstSmashLoop => m_rightLeftPillarSmashFirstSmashLoop;
            [SerializeField, TabGroup("PillarSmash")]
            private SimpleAttackInfo m_pillarSmashRightAttackEnd = new SimpleAttackInfo();
            public SimpleAttackInfo pillarSmashRightAttackEnd => m_pillarSmashRightAttackEnd;
            [SerializeField, TabGroup("PillarSmash")]
            private SimpleAttackInfo m_rightLeftPillarSmashSecondSmashDown = new SimpleAttackInfo();
            public SimpleAttackInfo rightLeftPillarSmashSecondSmashDown => m_rightLeftPillarSmashSecondSmashDown;
            [SerializeField, TabGroup("PillarSmash")]
            private SimpleAttackInfo m_pillarSmashBothAttackLoop = new SimpleAttackInfo();
            public SimpleAttackInfo pillarSmashBothAttackLoop => m_pillarSmashBothAttackLoop;
            [SerializeField, TabGroup("PillarSmash")]
            private SimpleAttackInfo m_pillarSmashBothAttackEnd = new SimpleAttackInfo();
            public SimpleAttackInfo pillarSmashBothAttackEnd => m_pillarSmashBothAttackEnd;

            [Title("Other")]
            [SerializeField, TabGroup("PillarSmash")]
            private float m_pillarSmashDownDuration = 2f;
            public float pillarSmashDownDuration => m_pillarSmashDownDuration;

            [Title("Attacks Info")]
            [SerializeField, TabGroup("SwordProjectile")]
            private SimpleAttackInfo m_swordProjectileLeftPillarAttack = new SimpleAttackInfo();
            public SimpleAttackInfo swordProjectileLeftPillarAttack => m_swordProjectileLeftPillarAttack;
            [SerializeField, TabGroup("SwordProjectile")]
            private SimpleAttackInfo m_swordProjectileRightPillarAttack = new SimpleAttackInfo();
            public SimpleAttackInfo swordProjectileRighttPillarAttack => m_swordProjectileRightPillarAttack;
            [SerializeField, TabGroup("SwordProjectile")]
            private SimpleAttackInfo m_swordProjectileLeftPillarThenRightPillarAttack = new SimpleAttackInfo();
            public SimpleAttackInfo swordProjectileLeftPillarThenRightPillarAttack => m_swordProjectileLeftPillarThenRightPillarAttack;
            [SerializeField, TabGroup("SwordProjectile")]
            private SimpleAttackInfo m_swordProjectileRightPillarThenLeftPillarAttack = new SimpleAttackInfo();
            public SimpleAttackInfo swordProjectileRightPillarThenLeftPillarAttack => m_swordProjectileRightPillarThenLeftPillarAttack;
            [SerializeField, TabGroup("SwordProjectile")]
            private SimpleAttackInfo m_swordProjectileBothPillarsAttackLoop = new SimpleAttackInfo();
            public SimpleAttackInfo swordProjectileBothPillarsAttackLoop => m_swordProjectileBothPillarsAttackLoop;
            [SerializeField, TabGroup("SwordProjectile")]
            private SimpleAttackInfo m_swordProjectileBothPillarsAttackEnd = new SimpleAttackInfo();
            public SimpleAttackInfo swordProjectileBothPillarsAttackEnd => m_swordProjectileBothPillarsAttackEnd;
            [SerializeField, TabGroup("SwordProjectile")]
            private SimpleAttackInfo m_swordProjectileRightPillarAttackLoop = new SimpleAttackInfo();
            public SimpleAttackInfo swordProjectileRightPillarAttackLoop => m_swordProjectileRightPillarAttackLoop;
            [SerializeField, TabGroup("SwordProjectile")]
            private SimpleAttackInfo m_swordProjectileRightPillarAttackEnd = new SimpleAttackInfo();
            public SimpleAttackInfo swordProjectileRightPillarAttackEnd => m_swordProjectileRightPillarAttackEnd;
            [SerializeField, TabGroup("SwordProjectile")]
            private SimpleAttackInfo m_swordProjectileLeftPillarAttackLoop = new SimpleAttackInfo();
            public SimpleAttackInfo swordProjectileLeftPillarAttackLoop => m_swordProjectileLeftPillarAttackLoop;
            [SerializeField, TabGroup("SwordProjectile")]
            private SimpleAttackInfo m_swordProjectileLeftPillarAttackEnd = new SimpleAttackInfo();
            public SimpleAttackInfo swordProjectileLeftPillarAttackEnd => m_swordProjectileLeftPillarAttackEnd;

            [Title("Other")]
            [SerializeField, TabGroup("SwordProjectile")]
            private int m_projectileTimesFired;
            public int projectileTimesFired => m_projectileTimesFired;
            [SerializeField, TabGroup("SwordProjectile")]
            private float m_projectileInterval;
            public float projectileInterval => m_projectileInterval;

            [Title("Attacks Info")]
            [SerializeField, TabGroup("HeavyPillarSmash")]
            private SimpleAttackInfo m_heavyPillarSmashAttackStart = new SimpleAttackInfo();
            public SimpleAttackInfo heavyPillarSmashAttackStart => m_heavyPillarSmashAttackStart;
            [SerializeField, TabGroup("HeavyPillarSmash")]
            private SimpleAttackInfo m_heavyPillarSmashAttackLoop = new SimpleAttackInfo();
            public SimpleAttackInfo heavyPillarSmashAttackLoop => m_heavyPillarSmashAttackLoop;
            [SerializeField, TabGroup("HeavyPillarSmash")]
            private SimpleAttackInfo m_heavyPillarSmashAttackEnd = new SimpleAttackInfo();
            public SimpleAttackInfo heavyPillarSmashAttackEnd => m_heavyPillarSmashAttackEnd;

            [Title("Others")]
            [SerializeField, TabGroup("HeavyPillarSmash")]
            private float m_heavyPillarSlamDownDuration;
            public float heavyPillarSlamDownDuration => m_heavyPillarSlamDownDuration;
            [SerializeField, TabGroup("HeavyPillarSmash")]
            private float m_heavyPillarSlamShockwaveVFXDelay;
            public float heavyPillarSlamShockwaveVFXDelay => m_heavyPillarSlamShockwaveVFXDelay;

            [Title("Attacks Info")]
            [SerializeField, TabGroup("LaserBlast")]
            private SimpleAttackInfo m_clockwiseLaserAttack = new SimpleAttackInfo();
            public SimpleAttackInfo clockwiseLaserAttack => m_clockwiseLaserAttack;
            [SerializeField, TabGroup("LaserBlast")]
            private SimpleAttackInfo m_counterClockwiseLaserAttack = new SimpleAttackInfo();
            public SimpleAttackInfo counterClockwiseLaserAttack => m_counterClockwiseLaserAttack;
            [SerializeField, TabGroup("LaserBlast")]
            private SimpleAttackInfo m_leftToRightLaserAttack = new SimpleAttackInfo();
            public SimpleAttackInfo leftToRightLaserAttack => m_leftToRightLaserAttack;
            [SerializeField, TabGroup("LaserBlast")]
            private SimpleAttackInfo m_rightToLeftLaserAttack = new SimpleAttackInfo();
            public SimpleAttackInfo rightToLeftLaserAttack => m_rightToLeftLaserAttack;
            [SerializeField, TabGroup("LaserBlast")]
            private SimpleAttackInfo m_rightToLeftaserAttack = new SimpleAttackInfo();
            public SimpleAttackInfo rightToLeftaserAttack => m_rightToLeftaserAttack;
            [SerializeField, TabGroup("LaserBlast")]
            private SimpleAttackInfo m_rightToLeftToRightLaserAttack = new SimpleAttackInfo();
            public SimpleAttackInfo rightToLeftToRightLaserAttack => m_rightToLeftToRightLaserAttack;
            [SerializeField, TabGroup("LaserBlast")]
            private SimpleAttackInfo m_leftToRightToLeftLaserAttack = new SimpleAttackInfo();
            public SimpleAttackInfo leftToRightToLeftLaserAttack => m_leftToRightToLeftLaserAttack;

            [Title("Flinch Animations")]
            [SerializeField]
            private BasicAnimationInfo m_noDamageFlinchAnimation;
            public BasicAnimationInfo noDamageFlinchAnimation => m_noDamageFlinchAnimation;
            [SerializeField]
            private BasicAnimationInfo m_slightDamageFlinchAnimation;
            public BasicAnimationInfo slightDamageFlinchAnimation => m_slightDamageFlinchAnimation;
            [SerializeField]
            private BasicAnimationInfo m_mediumDamageFlinchAnimation;
            public BasicAnimationInfo mediumDamageFlinchAnimation => m_mediumDamageFlinchAnimation;
            [SerializeField]
            private BasicAnimationInfo m_heavyDamageFlinchAnimation;
            public BasicAnimationInfo heavyDamageFlinchAnimation => m_heavyDamageFlinchAnimation;
            [SerializeField]
            private BasicAnimationInfo m_lastHitDamageFlinchAnimation;
            public BasicAnimationInfo lastHitDamageFlinchAnimation => m_lastHitDamageFlinchAnimation;
            [SerializeField]
            private BasicAnimationInfo m_noMaskFlinchAnimation;
            public BasicAnimationInfo noMaskFlinchAnimation => m_noMaskFlinchAnimation;

            [Title("Behaviour Animations")]
            [SerializeField]
            private BasicAnimationInfo m_deathAnimation;
            public BasicAnimationInfo deathAnimation => m_deathAnimation;
            [SerializeField]
            private BasicAnimationInfo m_idleWithCoverAnimation;
            public BasicAnimationInfo idleWithCoverAnimation => m_idleWithCoverAnimation;
            [SerializeField]
            private BasicAnimationInfo m_idleWithoutCoverAnimation;
            public BasicAnimationInfo idleWithoutCoverAnimation => m_idleWithoutCoverAnimation;
            [SerializeField]
            private BasicAnimationInfo m_rageQuakeAnimation;
            public BasicAnimationInfo rageQuakeAnimation => m_rageQuakeAnimation;
            [SerializeField]
            private BasicAnimationInfo m_slightlyDamagedHeadAnimation;
            public BasicAnimationInfo slightlyDamagedHeadAnimation => m_slightDamageFlinchAnimation;
            [SerializeField]
            private BasicAnimationInfo m_mediumDamagedHeadAnimation;
            public BasicAnimationInfo mediumDamagedHeadAnimation => m_mediumDamagedHeadAnimation;
            [SerializeField]
            private BasicAnimationInfo m_heavyDamagedHeadAnimation;
            public BasicAnimationInfo heavyDamagedHeadAnimation => m_heavyDamagedHeadAnimation;

            public override void Initialize()
            {
#if UNITY_EDITOR
                #region Attack Animations
                m_leftRightPillarSmashFirstSmashDown.SetData(m_skeletonDataAsset);
                m_leftRightPillarSmashFirstSmashLoop.SetData(m_skeletonDataAsset);
                m_pillarSmashLeftAttackEnd.SetData(m_skeletonDataAsset);
                m_rightLeftPillarSmashFirstSmashDown.SetData(m_skeletonDataAsset);
                m_rightLeftPillarSmashFirstSmashLoop.SetData(m_skeletonDataAsset);
                m_pillarSmashRightAttackEnd.SetData(m_skeletonDataAsset);
                m_pillarSmashBothAttackLoop.SetData(m_skeletonDataAsset);
                m_pillarSmashBothAttackEnd.SetData(m_skeletonDataAsset);
                m_rightLeftPillarSmashSecondSmashDown.SetData(m_skeletonDataAsset);
                m_leftRightPillarSmashSecondSmashDown.SetData(m_skeletonDataAsset);
                m_swordProjectileLeftPillarAttack.SetData(m_skeletonDataAsset);
                m_swordProjectileRightPillarAttack.SetData(m_skeletonDataAsset);
                m_swordProjectileLeftPillarThenRightPillarAttack.SetData(m_skeletonDataAsset);
                m_swordProjectileRightPillarThenLeftPillarAttack.SetData(m_skeletonDataAsset);
                m_swordProjectileBothPillarsAttackLoop.SetData(m_skeletonDataAsset);
                m_swordProjectileBothPillarsAttackEnd.SetData(m_skeletonDataAsset);
                m_swordProjectileRightPillarAttackLoop.SetData(m_skeletonDataAsset);
                m_swordProjectileRightPillarAttackEnd.SetData(m_skeletonDataAsset);
                m_swordProjectileLeftPillarAttackLoop.SetData(m_skeletonDataAsset);
                m_swordProjectileLeftPillarAttackEnd.SetData(m_skeletonDataAsset);
                m_heavyPillarSmashAttackStart.SetData(m_skeletonDataAsset);
                m_heavyPillarSmashAttackLoop.SetData(m_skeletonDataAsset);
                m_heavyPillarSmashAttackEnd.SetData(m_skeletonDataAsset);
                m_clockwiseLaserAttack.SetData(m_skeletonDataAsset);
                m_counterClockwiseLaserAttack.SetData(m_skeletonDataAsset);
                m_leftToRightLaserAttack.SetData(m_skeletonDataAsset);
                m_rightToLeftLaserAttack.SetData(m_skeletonDataAsset);
                m_rightToLeftaserAttack.SetData(m_skeletonDataAsset);
                m_rightToLeftToRightLaserAttack.SetData(m_skeletonDataAsset);
                m_leftToRightToLeftLaserAttack.SetData(m_skeletonDataAsset);
                #endregion
                #region Basic Behaviour Animations
                m_noDamageFlinchAnimation.SetData(m_skeletonDataAsset);
                m_slightDamageFlinchAnimation.SetData(m_skeletonDataAsset);
                m_mediumDamageFlinchAnimation.SetData(m_skeletonDataAsset);
                m_heavyDamageFlinchAnimation.SetData(m_skeletonDataAsset);
                m_lastHitDamageFlinchAnimation.SetData(m_skeletonDataAsset);
                m_noMaskFlinchAnimation.SetData(m_skeletonDataAsset);
                m_deathAnimation.SetData(m_skeletonDataAsset);
                m_idleWithCoverAnimation.SetData(m_skeletonDataAsset);
                m_idleWithoutCoverAnimation.SetData(m_skeletonDataAsset);
                m_rageQuakeAnimation.SetData(m_skeletonDataAsset);
                m_slightlyDamagedHeadAnimation.SetData(m_skeletonDataAsset);
                m_mediumDamagedHeadAnimation.SetData(m_skeletonDataAsset);
                m_heavyDamagedHeadAnimation.SetData(m_skeletonDataAsset);
                #endregion
#endif
            }
        }
        [System.Serializable]
        public class PhaseInfo : IPhaseInfo
        {

        }

        private enum State
        {
            Phasing,
            Attacking,
            Idle,
            ReevaluateSituation,
            WaitBehaviourEnd
        }

        private enum Attack
        {
            PillarSlam,
            SwordProjectile,
            HeavyPillarSlam,
            LaserBlast
        }

        private enum CoverDamagedState
        {
            NotDamaged,
            SlightlyDamaged,
            MediumDamaged,
            HeavilyDamaged,
            NoMask
        }

        public enum Phase
        {
            PhaseOne,
            PhaseTwo,
            Wait
        }

        [SerializeField, TabGroup("Reference")]
        private Boss m_boss;
        [SerializeField, TabGroup("Reference")]
        private Hitbox m_hitbox;
        [SerializeField, TabGroup("Reference")]
        private Transform m_arenaCenter;
        [SerializeField, TabGroup("Reference")]
        private ColossusSwordProjectileShooter m_leftSwordProjectileShooter;
        [SerializeField, TabGroup("Reference")]
        private ColossusSwordProjectileShooter m_rightSwordProjectileShooter;
        [SerializeField, TabGroup("Reference")]
        private Transform m_leftPillarSmashFXSpawnPoint;
        [SerializeField, TabGroup("Reference")]
        private Transform m_rightPillarSmashFXSpawnPoint;

        [SerializeField, TabGroup("Modules")]
        private DeathHandle m_deathHandle;
        [SerializeField, TabGroup("Modules")]
        private Health m_health;

        [SerializeField, TabGroup("FX")]
        private ParticleFX m_pillarSmashFX;
        [SerializeField, TabGroup("FX")]
        private ParticleFX m_heavySlamAnticipationFX;
        [SerializeField, TabGroup("FX")]
        private ParticleFX m_heavySlamFX;
        [SerializeField, TabGroup("FX")]
        private ParticleFX m_swordProjectileFX;
        [SerializeField, TabGroup("FX")]
        private ParticleFX m_laserBeamChargeFX;
        [SerializeField, TabGroup("FX")]
        private ParticleFX m_laserBeamImpactFX;
        [SerializeField, TabGroup("FX")]
        private ParticleFX m_flinchFX;

        [SerializeField, TabGroup("Colliders")]
        private Collider2D m_leftPillarEnvironmentCollider;
        [SerializeField, TabGroup("Colliders")]
        private Collider2D m_rightPillarEnvironmentCollider;
        [SerializeField, TabGroup("Colliders")]
        private Collider2D m_leftPillarDamageCollider;
        [SerializeField, TabGroup("Colliders")]
        private Collider2D m_rightPillarDamageCollider;
        [SerializeField, TabGroup("Colliders")]
        private Collider2D m_pillarSmashImpactSmallDamageCollider;
        [SerializeField, TabGroup("Colliders")]
        private Collider2D m_pillarSmashImpactLargeDamageCollider;
        [SerializeField, TabGroup("Colliders")]
        private Collider2D m_heavyPillarSmashImpactDamageCollider;

        [ShowInInspector]
        private StateHandle<State> m_stateHandle;
        [ShowInInspector]
        private PhaseHandle<Phase, PhaseInfo> m_phaseHandle;

        [SerializeField]
        private SpineEventListener m_spineListener;

        [ShowInInspector]
        private RandomAttackDecider<Attack> m_currentAttackDecider;

        [SerializeField, BoxGroup("TESTING")]
        private bool m_testingMode;

        private PhaseInfo m_phaseInfo;
        private bool m_isCurrentPillarSmashStartingOnRight = false;

        private void ApplyPhaseData(PhaseInfo obj)
        {
            m_phaseInfo = obj;
            UpdateAttackDeciderList();
        }

        public override void ApplyData()
        {
            base.ApplyData();
        }

        private void ChangeState()
        {
            StopAllCoroutines();
            m_animation.DisableRootMotion();
            m_animation.SetEmptyAnimation(0, 0);
            m_stateHandle.OverrideState(State.Phasing);
            m_phaseHandle.ApplyChange();
        }

        public override void ReturnToSpawnPoint()
        {

        }

        protected override void OnTargetDisappeared()
        {

        }

        public override void SetTarget(IDamageable damageable, Character m_target = null)
        {
            if (damageable != null)
            {
                if (m_stateHandle.currentState == State.Idle)
                {
                    base.SetTarget(damageable, m_target);
                    m_stateHandle.OverrideState(State.ReevaluateSituation);
                }
            }
        }

        private IEnumerator FlinchRoutine()
        {
            var flinch = GetCurrentFlinchAnimation();
            m_animation.SetAnimation(0, flinch, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, flinch);
        }

        private IEnumerator ChangePhaseRoutine()
        {
            m_stateHandle.Wait(State.Attacking);

            m_currentAttackDecider.hasDecidedOnAttack = false;

            m_animation.SetAnimation(0, m_info.lastHitDamageFlinchAnimation.animation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.lastHitDamageFlinchAnimation);
            m_animation.SetAnimation(0, m_info.rageQuakeAnimation.animation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.rageQuakeAnimation.animation);

            if (m_phaseHandle.currentPhase == Phase.PhaseTwo)
            {
                m_currentAttackDecider = new RandomAttackDecider<Attack>();
                m_currentAttackDecider.DecideOnAttack(Attack.SwordProjectile);
                m_currentAttackDecider.hasDecidedOnAttack = true;
            }

            m_stateHandle.ApplyQueuedState();
        }

        private IEnumerator DefeatRoutine()
        {
            m_animation.SetAnimation(0, m_info.deathAnimation.animation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.deathAnimation.animation);
            this.gameObject.SetActive(false);
        }

        #region Attacks Utility
        private bool IsPlayerOnRightSide()
        {
            if (m_targetInfo.position.x > m_arenaCenter.position.x)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void TurnOffPillarColliders()
        {
            m_leftPillarDamageCollider.enabled = false;
            m_rightPillarDamageCollider.enabled = false;

            m_leftPillarEnvironmentCollider.enabled = false;
            m_rightPillarEnvironmentCollider.enabled = false;

            m_pillarSmashImpactLargeDamageCollider.enabled = false;
            m_pillarSmashImpactSmallDamageCollider.enabled = false;
        }

        public void TurnOnPillarDamageColliders()
        {
            m_leftPillarDamageCollider.enabled = true;
            m_rightPillarDamageCollider.enabled = true;

            m_leftPillarEnvironmentCollider.enabled = false;
            m_rightPillarEnvironmentCollider.enabled = false;
        }

        public void TurnOnPillarEnvironmentCollider()
        {
            m_leftPillarDamageCollider.enabled = false;
            m_rightPillarDamageCollider.enabled = false;

            m_leftPillarEnvironmentCollider.enabled = true;
            m_rightPillarEnvironmentCollider.enabled = true;
        }

        public void SpawnPillarSmashVFX(Vector2 position, bool IsLargeDamageCollider)
        {
            var smashVFX = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_pillarSmashFX.gameObject, position, Quaternion.identity);
            if (IsLargeDamageCollider)
            {
                smashVFX.transform.localScale.Set(m_pillarSmashImpactLargeDamageCollider.GetComponent<BoxCollider2D>().size.x, 1f, 1f);
            }
            smashVFX.GetComponent<ParticleFX>().Play();

            if (IsLargeDamageCollider)
            {
                m_pillarSmashImpactLargeDamageCollider.enabled = true;
                m_pillarSmashImpactLargeDamageCollider.transform.position = smashVFX.transform.position;
            }
            else
            {
                m_pillarSmashImpactSmallDamageCollider.enabled = true;
                m_pillarSmashImpactSmallDamageCollider.transform.position = smashVFX.transform.position;
            }
        }

        private void SpawnHeavyPillarSmashAnticipationVFX()
        {
            var leftSmashVFX = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_heavySlamAnticipationFX.gameObject, m_leftPillarSmashFXSpawnPoint.position, Quaternion.identity);
            var rightSmashVFX = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_heavySlamAnticipationFX.gameObject, m_rightPillarSmashFXSpawnPoint.position, Quaternion.identity);

            leftSmashVFX.GetComponent<ParticleFX>().Play();
            rightSmashVFX.GetComponent<ParticleFX>().Play();
        }

        #endregion

        #region Attacks
        private IEnumerator PillarSmashRoutine()
        {
            m_stateHandle.Wait(State.Idle);

            var SmashDownDuration = UnityEngine.Random.Range(1, 3);

            if (IsPlayerOnRightSide())
            {
                m_animation.SetAnimation(0, m_info.rightLeftPillarSmashFirstSmashDown.animation, false);
                yield return new WaitForAnimationComplete(m_animation.animationState, m_info.rightLeftPillarSmashFirstSmashDown.animation);
                SpawnPillarSmashVFX(m_rightPillarSmashFXSpawnPoint.position, false);

                m_animation.SetAnimation(0, m_info.rightLeftPillarSmashFirstSmashLoop.animation, true);
                yield return new WaitForSeconds(m_info.pillarSmashDownDuration);

                m_animation.SetAnimation(0, m_info.rightLeftPillarSmashSecondSmashDown.animation, false);
                yield return new WaitForAnimationComplete(m_animation.animationState, m_info.rightLeftPillarSmashSecondSmashDown.animation);
                SpawnPillarSmashVFX(m_leftPillarSmashFXSpawnPoint.position, true);

                m_animation.SetAnimation(0, m_info.pillarSmashBothAttackLoop.animation, true);
                yield return new WaitForSeconds(m_info.pillarSmashDownDuration);

                m_animation.SetAnimation(0, m_info.swordProjectileBothPillarsAttackEnd.animation, false);
                yield return new WaitForAnimationComplete(m_animation.animationState, m_info.swordProjectileBothPillarsAttackEnd.animation);
            }
            else
            {
                m_animation.SetAnimation(0, m_info.leftRightPillarSmashFirstSmashDown.animation, false);
                yield return new WaitForAnimationComplete(m_animation.animationState, m_info.leftRightPillarSmashFirstSmashDown.animation);
                SpawnPillarSmashVFX(m_leftPillarSmashFXSpawnPoint.position, false);

                m_animation.SetAnimation(0, m_info.leftRightPillarSmashFirstSmashLoop.animation, false);
                yield return new WaitForSeconds(SmashDownDuration);

                m_animation.SetAnimation(0, m_info.leftRightPillarSmashSecondSmashDown.animation, false);
                yield return new WaitForAnimationComplete(m_animation.animationState, m_info.leftRightPillarSmashSecondSmashDown.animation);
                SpawnPillarSmashVFX(m_rightPillarSmashFXSpawnPoint.position, true);

                m_animation.SetAnimation(0, m_info.pillarSmashBothAttackLoop.animation, true);
                yield return new WaitForSeconds(SmashDownDuration);

                m_animation.SetAnimation(0, m_info.pillarSmashBothAttackEnd.animation, false);
                yield return new WaitForAnimationComplete(m_animation.animationState, m_info.pillarSmashBothAttackEnd.animation);
            }

            TurnOffPillarColliders();

            m_currentAttackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }

        private IEnumerator HeavyPillarSmashRoutine()
        {
            m_stateHandle.Wait(State.Idle);

            TurnOnPillarDamageColliders();
            m_animation.SetAnimation(0, m_info.heavyPillarSmashAttackStart.animation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.heavyPillarSmashAttackStart.animation);
            SpawnHeavyPillarSmashAnticipationVFX();

            m_animation.SetAnimation(0, m_info.heavyPillarSmashAttackLoop.animation, true);
            TurnOnPillarEnvironmentCollider();
            yield return new WaitForSeconds(m_info.heavyPillarSlamShockwaveVFXDelay);
            m_heavySlamFX.Play();
            yield return new WaitForSeconds(2f);
            m_heavyPillarSmashImpactDamageCollider.enabled = true;

            m_animation.SetAnimation(0, m_info.heavyPillarSmashAttackEnd.animation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.heavyPillarSmashAttackEnd.animation);
            m_heavyPillarSmashImpactDamageCollider.enabled = false;
            TurnOffPillarColliders();

            m_currentAttackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }

        private IEnumerator SwordProjectileRoutine()
        {
            m_stateHandle.Wait(State.Idle);

            m_animation.SetAnimation(0, m_info.swordProjectileBothPillarsAttackLoop.animation, true);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.swordProjectileBothPillarsAttackLoop.animation);

            for (int i = 0;  i < m_info.projectileInterval; i++)
            {
                m_leftSwordProjectileShooter.ShootProjectile(m_targetInfo.position);

                yield return new WaitForSeconds(m_info.projectileInterval);

                m_rightSwordProjectileShooter.ShootProjectile(m_targetInfo.position);

                yield return new WaitForSeconds(m_info.projectileInterval);
            }

            m_animation.SetAnimation(0, m_info.swordProjectileBothPillarsAttackEnd.animation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.swordProjectileBothPillarsAttackEnd.animation);

            m_currentAttackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }

        private IEnumerator LaserRoutine()
        {
            m_stateHandle.Wait(State.Idle);

            m_currentAttackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }
        #endregion
        private void UpdateAttackDeciderList()
        {
            switch (m_phaseHandle.currentPhase)
            {
                case Phase.PhaseOne:
                    m_currentAttackDecider.SetList(new AttackInfo<Attack>(Attack.PillarSlam, 0));
                    break;
                case Phase.PhaseTwo:
                    m_currentAttackDecider.SetList(new AttackInfo<Attack>(Attack.HeavyPillarSlam, 0),
                                                    new AttackInfo<Attack>(Attack.SwordProjectile, 0),
                                                    new AttackInfo<Attack>(Attack.LaserBlast, 0));
                    break;
            }
        }

        protected override void OnDestroyed(object sender, EventActionArgs eventArgs)
        {
            base.OnDestroyed(sender, eventArgs);
            StopAllCoroutines();
            m_hitbox.Disable();
            if (!m_deathHandle.gameObject.activeSelf)
            {
                this.enabled = false;
                StartCoroutine(DefeatRoutine());
            }
        }

        private string GetCurrentFlinchAnimation()
        {
            var flinchAnim = m_info.noDamageFlinchAnimation.animation; ;
            if (m_health.currentValue > m_health.maxValue * 0.85)
            {
                flinchAnim = m_info.noDamageFlinchAnimation.animation;
            }
            if ((m_health.currentValue < m_health.maxValue * 0.85) && (m_health.currentValue > m_health.maxValue * 0.7))
            {
                flinchAnim = m_info.slightDamageFlinchAnimation.animation;
            }

            if ((m_health.currentValue < m_health.maxValue * 0.7) && (m_health.currentValue > m_health.maxValue * 0.7))
            {
                flinchAnim = m_info.mediumDamageFlinchAnimation.animation;
            }

            if ((m_health.currentValue < m_health.maxValue * 0.65) && (m_health.currentValue > m_health.maxValue * 0.5))
            {
                flinchAnim = m_info.heavyDamageFlinchAnimation.animation;
            }

            if (m_health.currentValue < m_health.maxValue * 0.5)
            {
                flinchAnim = m_info.noMaskFlinchAnimation.animation;
            }
            return flinchAnim;
        }

        private void SetCurrentHead()
        {
            if(m_health.currentValue > m_health.maxValue * 0.85)
            {
                m_animation.SetEmptyAnimation(1, 0);
            }
            if((m_health.currentValue < m_health.maxValue * 0.85) && (m_health.currentValue > m_health.maxValue * 0.7))
            {
                m_animation.SetAnimation(1, m_info.slightDamageFlinchAnimation.animation, true);
            }

            if ((m_health.currentValue < m_health.maxValue * 0.7) && (m_health.currentValue > m_health.maxValue * 0.7))
            {
                m_animation.SetAnimation(1, m_info.mediumDamagedHeadAnimation.animation, true);
            }

            if ((m_health.currentValue < m_health.maxValue * 0.65) && (m_health.currentValue > m_health.maxValue * 0.5))
            {
                m_animation.SetAnimation(1, m_info.heavyDamagedHeadAnimation.animation, true);
            }

            if(m_health.currentValue < m_health.maxValue * 0.5)
            {
                m_animation.SetEmptyAnimation(1, 0);
            }
        }

        private void OnDamageTaken(object sender, Damageable.DamageEventArgs eventArgs)
        {
            StopAllCoroutines();
            StartCoroutine(FlinchRoutine());
        }

        protected override void Awake()
        {
            base.Awake();
            m_damageable.DamageTaken += OnDamageTaken;
            m_deathHandle.SetAnimation(m_info.deathAnimation.animation);
            m_currentAttackDecider = new RandomAttackDecider<Attack>();
            m_stateHandle = new StateHandle<State>(State.Idle, State.WaitBehaviourEnd);
        }

        protected override void Start()
        {
            base.Start();

            TurnOffPillarColliders();
            m_heavyPillarSmashImpactDamageCollider.enabled = false;
            m_phaseHandle = new PhaseHandle<Phase, PhaseInfo>();
            m_phaseHandle.Initialize(Phase.PhaseOne, m_info.phaseInfo, m_character, ChangeState, ApplyPhaseData);
            m_phaseHandle.ApplyChange();
        }

        private void Update()
        {
            m_phaseHandle.MonitorPhase();
            SetCurrentHead();

           switch(m_stateHandle.currentState)
            {
                case State.Idle:
                    //Note: Make sure the Model's initial animation is not <None> nor <Idle_1_without_cover> because it causes this to bug out for some reason
                    if (m_phaseHandle.currentPhase == Phase.PhaseTwo)
                    {
                        m_animation.SetAnimation(0, m_info.idleWithoutCoverAnimation.animation, true);
                    }
                    else
                    {
                        m_animation.SetAnimation(0, m_info.idleWithCoverAnimation.animation, true);
                    }
                    break;
                case State.Phasing:
                    Debug.Log("Phase Time");
                    StartCoroutine(ChangePhaseRoutine());
                    break;
                case State.Attacking:
                    m_currentAttackDecider.DecideOnAttack();

                    switch (m_currentAttackDecider.chosenAttack.attack)
                    {
                        case Attack.PillarSlam:
                            StartCoroutine(PillarSmashRoutine());
                            break;
                        case Attack.HeavyPillarSlam:
                            StartCoroutine(HeavyPillarSmashRoutine());
                            break;
                        case Attack.SwordProjectile:
                            StartCoroutine(SwordProjectileRoutine());
                            break;
                        case Attack.LaserBlast:
                            StartCoroutine(LaserRoutine());
                            break;
                    }
                    break;
                case State.ReevaluateSituation:
                    if (m_testingMode)
                    {
                        m_stateHandle.SetState(State.Idle);
                        return;
                    }
                    else
                    {

                    }
                    break;
                case State.WaitBehaviourEnd:
                    return;

            }
        }

        [Button]
        private void TestAttack(Attack attack)
        {
            m_stateHandle.Wait(State.Attacking);
            m_currentAttackDecider.SetList(new AttackInfo<Attack>(attack, 0));
            m_currentAttackDecider.DecideOnAttack(attack);
            m_currentAttackDecider.hasDecidedOnAttack = true;
            m_stateHandle.ApplyQueuedState();
        }
    }
}

