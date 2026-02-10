using System;
using DChild.Gameplay;
using DChild.Gameplay.Characters;
using DChild.Gameplay.Combat;
using Holysoft.Event;
using DChild.Gameplay.Characters.AI;
using UnityEngine;
using Spine;
using Spine.Unity;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using DChild;
using DChild.Gameplay.Characters.Enemies;
using DChild.Temp;
using Spine.Unity.Modules;
using DChild.Gameplay.Pooling;
using DChild.Gameplay.Projectiles;
using Language.Lua;

namespace DChild.Gameplay.Characters.Enemies
{
    [AddComponentMenu("DChild/Gameplay/Enemies/Boss/MotherMantis")]
    public class MotherMantisAI : CombatAIBrain<MotherMantisAI.Info>
    {
        [System.Serializable]
        public class Info : BaseInfo
        {
            [SerializeField]
            private PhaseInfo<Phase> m_phaseInfo;
            public PhaseInfo<Phase> phaseInfo => m_phaseInfo;

            //Basic Behaviours
            [SerializeField]
            private MovementInfo m_move = new MovementInfo();
            public MovementInfo move => m_move;
            [SerializeField]
            private MovementInfo m_moveLowHP = new MovementInfo();
            public MovementInfo moveLowHP => m_moveLowHP;

            [SerializeField, MinValue(0)]
            private float m_attackCD;
            public float attackCD => m_attackCD;


            [Title("Attack Behaviours")]
            [SerializeField]
            private SimpleAttackInfo m_clawattack = new SimpleAttackInfo();
            public SimpleAttackInfo clawattack => m_clawattack;
            [SerializeField]
            private SimpleAttackInfo m_jump;
            public SimpleAttackInfo jump => m_jump;
            [SerializeField]
            private BasicAnimationInfo m_landingAnimation;
            public BasicAnimationInfo landingAnimation => m_landingAnimation;
            [SerializeField]
            private BasicAnimationInfo m_backgroundLandingAnimation;
            public BasicAnimationInfo backgroundLandingAnimation => m_backgroundLandingAnimation;
            [SerializeField]
            private BasicAnimationInfo m_backgroundJumpAnimation;
            public BasicAnimationInfo backgroundJumpAnimation => m_backgroundJumpAnimation;
            [SerializeField]
            private BasicAnimationInfo m_petalBackgroundLeft;
            public BasicAnimationInfo petalBackgroundLeft => m_petalBackgroundLeft;
            [SerializeField]
            private BasicAnimationInfo m_petalBackgroundRight;
            public BasicAnimationInfo petalBackgroundRight => m_petalBackgroundRight;
            [SerializeField]
            private BasicAnimationInfo m_petalBackgroundBoth;
            public BasicAnimationInfo petalBackgroundBoth => m_petalBackgroundBoth;
            [SerializeField]
            private BasicAnimationInfo m_petalRain;
            public BasicAnimationInfo petalRain => m_petalRain;

            [SerializeField]
            private float m_seedAmount;
            public float seedAmount => m_seedAmount;

            [Title("Spawned Objects")]
            [SerializeField]
            private GameObject m_FlowerBulb;
            public GameObject flowerBulb => m_FlowerBulb;
            /*[SerializeField]
            private GameObject m_spikeVines;
            public GameObject spikeVines => m_spikeVines;*/
            [SerializeField]
            private GameObject m_seedProjectile;
            public GameObject seedProjectile => m_seedProjectile;

            [SerializeField, TabGroup("Phase 1")]
            private float m_phase1Pattern1Range;
            public float phase1Pattern1Range => m_phase1Pattern1Range;
            [SerializeField, TabGroup("Phase 1")]
            private float m_phase1Pattern2Range;
            public float phase1Pattern2Range => m_phase1Pattern2Range;
            [SerializeField, TabGroup("Phase 1")]
            private float m_phase1Pattern3Range;
            public float phase1Pattern3Range => m_phase1Pattern3Range;
            [SerializeField, TabGroup("Phase 1")]
            private float m_phase1Pattern4Range;
            public float phase1Pattern4Range => m_phase1Pattern4Range;
            [SerializeField, TabGroup("Phase 2")]
            private float m_phase2Pattern1Range;
            public float phase2Pattern1Range => m_phase2Pattern1Range;
            [SerializeField, TabGroup("Phase 2")]
            private float m_phase2Pattern2Range;
            public float phase2Pattern2Range => m_phase2Pattern2Range;
            [SerializeField, TabGroup("Phase 2")]
            private float m_phase2Pattern3Range;
            public float phase2Pattern3Range => m_phase2Pattern3Range;
            [SerializeField, TabGroup("Phase 2")]
            private float m_phase2Pattern4Range;
            public float phase2Pattern4Range => m_phase2Pattern4Range;
            [SerializeField, TabGroup("Phase 3")]
            private float m_phase3Pattern1Range;
            public float phase3Pattern1Range => m_phase3Pattern1Range;
            [SerializeField, TabGroup("Phase 3")]
            private float m_phase3Pattern2Range;
            public float phase3Pattern2Range => m_phase3Pattern2Range;
            [SerializeField, TabGroup("Phase 3")]
            private float m_phase3Pattern3Range;
            public float phase3Pattern3Range => m_phase3Pattern3Range;
            [SerializeField, TabGroup("Phase 3")]
            private float m_phase3Pattern4Range;
            public float phase3Pattern4Range => m_phase3Pattern4Range;
            [SerializeField, TabGroup("Phase 3")]
            private float m_phase3Pattern5Range;
            public float phase3Pattern5Range => m_phase3Pattern5Range;
            [SerializeField, TabGroup("Phase 3")]
            private float m_phase3Pattern6Range;
            public float phase3Pattern6Range => m_phase3Pattern6Range;
            [SerializeField, TabGroup("Phase 3")]
            private float m_phase3Pattern7Range;
            public float phase3Pattern7Range => m_phase3Pattern7Range;

            [SerializeField, TabGroup("Intervals")]
            private float m_bulbExplosionInterval;
            public float bulbExplosionInterval => m_bulbExplosionInterval;
            [SerializeField, TabGroup("Intervals")]
            private float m_patternEndInteral;
            public float patternEndInteral => m_patternEndInteral;
            [SerializeField, TabGroup("Intervals")]
            private float m_flowerSpore2Interval;
            public float flowerSpore2Interval => m_flowerSpore2Interval;

            [Title("Misc")]
            [SerializeField]
            private float m_targetDistanceTolerance;
            public float targetDistanceTolerance => m_targetDistanceTolerance;

            [Title("Animations")]
            //Animations
            [SerializeField]
            private BasicAnimationInfo m_rageAnimation;
            public BasicAnimationInfo rageAnimation => m_rageAnimation;
            [SerializeField]
            private BasicAnimationInfo m_idlephase1Animation;
            public BasicAnimationInfo idlephase1Animation => m_idlephase1Animation;
            [SerializeField]
            private BasicAnimationInfo m_idlephase2Animation;
            public BasicAnimationInfo idlephase2Animation => m_idlephase2Animation;
            [SerializeField]
            private BasicAnimationInfo m_idlephase3Animation;
            public BasicAnimationInfo idlephase3Animation => m_idlephase3Animation;
            [SerializeField]
            private BasicAnimationInfo m_backgroundidleAnimation;
            public BasicAnimationInfo backgroundidleAnimation => m_backgroundidleAnimation;
            [SerializeField]
            private BasicAnimationInfo m_deathAnimation;
            public BasicAnimationInfo deathAnimation => m_deathAnimation;
            [SerializeField]
            private BasicAnimationInfo m_turnAnimation;
            public BasicAnimationInfo turnAnimation => m_turnAnimation;
            [SerializeField]
            private BasicAnimationInfo m_flinchAnimation;
            public BasicAnimationInfo flinchAnimation => m_flinchAnimation;

            [Title("Projectiles")]
           
            [SerializeField]
            private SimpleProjectileAttackInfo m_petalProjectile;
            public SimpleProjectileAttackInfo petalProjectile => m_petalProjectile;

            

            public override void Initialize()
            {
#if UNITY_EDITOR
                m_move.SetData(m_skeletonDataAsset);
                m_moveLowHP.SetData(m_skeletonDataAsset);

                m_clawattack.SetData(m_skeletonDataAsset);
                m_jump.SetData(m_skeletonDataAsset);
                m_landingAnimation.SetData(m_skeletonDataAsset);
                m_backgroundLandingAnimation.SetData(m_skeletonDataAsset);
                m_backgroundJumpAnimation.SetData(m_skeletonDataAsset);
                m_petalProjectile.SetData(m_skeletonDataAsset);
                m_petalBackgroundLeft.SetData(m_skeletonDataAsset);
                m_petalBackgroundRight.SetData(m_skeletonDataAsset);
                m_petalBackgroundBoth.SetData(m_skeletonDataAsset);
                m_petalRain.SetData(m_skeletonDataAsset);

                m_rageAnimation.SetData(m_skeletonDataAsset);
                m_idlephase1Animation.SetData(m_skeletonDataAsset);
                m_idlephase2Animation.SetData(m_skeletonDataAsset);
                m_idlephase3Animation.SetData(m_skeletonDataAsset);
                m_backgroundidleAnimation.SetData(m_skeletonDataAsset);
                m_deathAnimation.SetData(m_skeletonDataAsset);
                m_turnAnimation.SetData(m_skeletonDataAsset);
                m_flinchAnimation.SetData(m_skeletonDataAsset);

#endif
            }
        }

        [System.Serializable]
        public class PhaseInfo : IPhaseInfo
        {
            
            [SerializeField]
            private int m_phaseIndex;
            public int phaseIndex => m_phaseIndex;

            //[SerializeField, PreviewField]
            //protected SkeletonDataAsset m_skeletonDataAsset;

            //protected IEnumerable GetSkins()
            //{
            //    ValueDropdownList<string> list = new ValueDropdownList<string>();
            //    var reference = m_skeletonDataAsset.GetAnimationStateData().SkeletonData.Skins.ToArray();
            //    for (int i = 0; i < reference.Length; i++)
            //    {
            //        list.Add(reference[i].Name);
            //    }
            //    return list;
            //}
        }


        private enum State
        {
            Phasing,
            Intro,
            Idle,
            Flinch,
            Turning,
            Attacking,
            Chasing,
            ReevaluateSituation,
            WaitBehaviourEnd,
        }
        private enum Pattern
        {
            AttackPattern1,
            AttackPattern2,
            AttackPattern3,
            WaitAttackEnd,
        }
        private enum Attack
        {
            Phase1Pattern1,
            Phase2Pattern1,
            Phase3Pattern1,
            WaitAttackEnd,
        }

        public enum Phase
        {
            PhaseOne,
            PhaseTwo,
            PhaseThree,
            Wait,
        }

        private bool[] m_attackUsed;
        private List<Attack> m_attackCache;
        private List<float> m_attackRangeCache;

        [SerializeField, TabGroup("Reference")]
        private Boss m_boss;
        [SerializeField, TabGroup("Reference")]
        private Hitbox m_hitbox;
        [SerializeField, TabGroup("Reference")]
        private GameObject m_bodyCollider;
        [SerializeField, TabGroup("Reference")]
        private GameObject m_damageCollider;
        [SerializeField, TabGroup("Reference")]
        private Transform m_modelTransform;
        [SerializeField, TabGroup("Reference")]
        private float m_petalAmount;
        [SerializeField, TabGroup("Reference")]
        private SkeletonAnimation m_skeleton;
        [SerializeField, TabGroup("Reference")]
        private GameObject m_leftBounds;
        [SerializeField, TabGroup("Reference")]
        private GameObject m_rightBounds;
        [SerializeField, TabGroup("Modules")]
        private AnimatedTurnHandle m_turnHandle;
        [SerializeField, TabGroup("Modules")]
        private MovementHandle2D m_movement;
        //[SerializeField, TabGroup("Modules")]
        //private PatrolHandle m_patrolHandle;
        [SerializeField, TabGroup("Modules")]
        private AttackHandle m_attackHandle;
        [SerializeField, TabGroup("Modules")]
        private DeathHandle m_deathHandle;
        [TabGroup("Sensors")]
        public RaySensor m_groundSensor;
        //[SerializeField, TabGroup("Modules")]
        //private FlinchHandler m_flinchHandle;
        [SerializeField, TabGroup("Effects")]
        private ParticleFX m_deathFX;
        [SerializeField, TabGroup("Effects")]
        private ParticleFX m_petalStartFX;
        [SerializeField, TabGroup("Effects")]
        private ParticleFX m_petalLoopFX;
        [SerializeField, TabGroup("Effects")]
        private ParticleFX m_petalEndFX;
        [SerializeField, TabGroup("Effects")]
        private ParticleFX m_seedLaunchFX;
        [SerializeField, TabGroup("Effects")]
        private ParticleFX m_landFX;
        [SerializeField, TabGroup("Effects")]
        private ParticleFX m_landingCueFX;
        [SerializeField, TabGroup("Effects")]
        private ParticleFX m_flinchFX;
        [SerializeField]
        private SpineEventListener m_spineListener;

        private Transform m_stingerPos;

        [ShowInInspector]
        private StateHandle<State> m_stateHandle;
        State m_turnState;
        //[ShowInInspector]
        private PhaseHandle<Phase, PhaseInfo> m_phaseHandle;
        [ShowInInspector]
        private RandomAttackDecider<Attack> m_attackDecider;
        //private Attack m_previousAttack;
        //private Attack m_chosenAttack;
        private Attack m_currentAttack;
        private float m_currentAttackRange;

        private ProjectileLauncher m_petalLauncher;


        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_currentSpawnPoint;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_backgroundSpawnPoint;
        /*[SerializeField, TabGroup("Spawn Points")]
        private Transform m_stalagmiteLandingSpawnPoint;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_stalagmiteLandingSpawnPoint2;*/
        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_stalagmiteSpawnPointMain;
        [SerializeField, TabGroup("Spawn Points")]
        private List<Transform> m_stalagmiteSpawnPoint1;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform[] m_stalagmiteSpawnPoint2;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_petalProjectileSpawnPoint;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform[] m_flowerSpawnPointA;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform[] m_flowerSpawnPointB;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform[] m_flowerSpawnPointC;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform[] m_flowerSpawnPointD;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform[] m_flowerSpawnPointE;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform[] m_flowerSpawnPointF;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform[] m_flowerSpawnPointG;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform[] m_flowerSafeSpawnPowent;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_leftStalagSpawn;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_rightStalagSpawn;
        /*[SerializeField, TabGroup("Spawn Points")]
        private Transform[] m_StalagmiteSpawnPoint;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform[] m_StalagmiteSpawnPoint2;*/


        private float m_groundPosition;
        private List<Vector2> m_targetPositions;

        private bool m_stickToGround;
        private bool m_seedSpawning;
        private float m_currentCD;

        [SerializeField, TabGroup("PatternTracker")]
        private int[] FlowerPattern1;
        [SerializeField, TabGroup("PatternTracker")]
        private int[] FlowerPattern2;
        [SerializeField, TabGroup("PatternTracker")]
        private int[] FlowerPattern3;
        [SerializeField, TabGroup("PatternTracker")]
        private int[] FlowerPattern4;
        [SerializeField, TabGroup("PatternTracker")]
        private int[] FlowerPattern5;
        [SerializeField, TabGroup("PatternTracker")]
        private int[] FlowerPattern6;
        [SerializeField, TabGroup("PatternTracker")]
        private int[] FlowerPattern7;
        [SerializeField, TabGroup("PatternTracker")]
        private int[] FlowerPattern8;
        [SerializeField, TabGroup("PatternTracker")]
        private int[] FlowerPattern9;
        private bool m_hasPhaseChanged;

        private int m_currentPhaseIndex;
        private float m_currentPetalAmount;
        private float m_currentCooldownSpeed;
        private int m_currentSummonAmmount;
        //private float m_currentDroneSummonSpeed;
        float m_currentRecoverTime;
        //bool m_isPhasing;
        [SerializeField]
        private float m_distance;

        private string m_moveAnim;
        private float m_moveSpeed;
        private bool m_isDetecting;
        private Vector2 m_targetPos;
        public EventAction<EventActionArgs> OnPetalRain;
        public EventAction<EventActionArgs> OnMantisLand;

        private void ApplyPhaseData(PhaseInfo obj)
        {
            if (m_attackDecider != null)
            {
                UpdateAttackDeciderList();
            }
            base.ApplyData();
        }

        private void ChangeState()
        {
            if (!m_hasPhaseChanged)
            {
                m_stateHandle.OverrideState(State.Phasing);
                m_hasPhaseChanged = true;
                m_animation.SetEmptyAnimation(0, 0);
                m_phaseHandle.ApplyChange();
            }
        }
        private void OnTurnRequest(object sender, EventActionArgs eventArgs) => m_stateHandle.OverrideState(State.Turning);

        public override void SetTarget(IDamageable damageable, Character m_target = null)
        {
            if (damageable != null)
            {
                base.SetTarget(damageable, m_target);
                if (!m_isDetecting)
                {
                    m_isDetecting = true;
                    m_stateHandle.OverrideState(State.Intro);
                }
            }
        }
        private void OnTurnDone(object sender, FacingEventArgs eventArgs)
        {
            m_animation.animationState.TimeScale = 1f;
                m_stateHandle.ApplyQueuedState();
            m_phaseHandle.allowPhaseChange = true;
        }
        private void CustomTurn()
        {
            if (!IsFacingTarget())
            {
                //m_turnHandle.Execute(m_info.turnAnimation, m_info.idleAnimation);
                transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
                m_character.SetFacing(transform.localScale.x == 1 ? HorizontalDirection.Right : HorizontalDirection.Left);
            }
        }
        private IEnumerator ChangePhaseRoutine()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            m_hitbox.SetInvulnerability(Invulnerability.MAX); //wasTrue
            m_currentCD = 0;
            m_bodyCollider.SetActive(false);
            //m_animation.EnableRootMotion(true, false);
            m_animation.SetAnimation(0, m_info.rageAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.rageAnimation.animation);
            m_hitbox.SetInvulnerability(Invulnerability.None);
            m_hasPhaseChanged = false;
            DecidedOnAttack(false);
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }

        protected override void OnDestroyed(object sender, EventActionArgs eventArgs)
        {
            base.OnDestroyed(sender, eventArgs);
            StopAllCoroutines();
            //transform.position = new Vector2(transform.position.x, m_groundPosition);
            m_stickToGround = true;
            m_seedLaunchFX.Stop();
            m_deathFX.Play();
            m_movement.Stop();
            m_isDetecting = false;
        }

        #region PetalAttack
        private void LaunchPetalProjectile(Vector2 target, Transform spawnPoint)
        {
         
            m_petalLauncher = new ProjectileLauncher(m_info.petalProjectile.projectileInfo, spawnPoint);
            m_petalLauncher.AimAt(target);
            m_petalLauncher.LaunchProjectile();
        }

        private Vector2 CalculatePositions()
        {
            var target = m_targetInfo.position;
            var point = new Vector2(UnityEngine.Random.Range(-20, 20) + target.x, UnityEngine.Random.Range(-20, 20) + target.y); //Locked to Ground
            return point;
        }
        #endregion
        #region Attacks
        private IEnumerator ClawRoutine()
        {
            m_animation.SetAnimation(0, m_info.clawattack.animation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.clawattack.animation);
            m_animation.SetAnimation(0, m_info.idlephase1Animation, true);
            m_isPlayerDamaged = false;
            yield return null;
        }
        private IEnumerator JumpAttack1Routine()
        {
            m_hitbox.SetInvulnerability(Invulnerability.MAX);
            m_animation.SetAnimation(0, m_info.jump.animation, false);
            yield return new WaitForSeconds(1.5f);
            transform.position = new Vector2(m_targetInfo.position.x, transform.position.y - 5);
            m_landingCueFX.Play();
            yield return new WaitForSeconds(1f);
            m_animation.SetAnimation(0, m_info.landingAnimation, false);
            m_targetInfo.GetTargetDamagable().DamageTaken += PlayerDamaged;
            yield return new WaitForSeconds(0.6f);
            m_landFX.Play();
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.landingAnimation.animation);
            m_targetInfo.GetTargetDamagable().DamageTaken -= PlayerDamaged;
            m_hitbox.SetInvulnerability(Invulnerability.None);
            yield return null;
        }
        private IEnumerator JumpAttack2Routine(int patternWhat = 0, int safeSpawn = 0)
        {
            if (patternWhat == 0 && safeSpawn == 0)
            {
                yield return new WaitForSeconds(1.5f);
                transform.position = new Vector2(m_targetInfo.position.x, transform.position.y - 5);
            }
            else
            {
                yield return new WaitForSeconds(1.5f);
                transform.position = new Vector2(m_flowerSafeSpawnPowent[safeSpawn].position.x, m_flowerSafeSpawnPowent[safeSpawn].position.y + 25);
            }
            m_skeleton.GetComponent<MeshRenderer>().sortingLayerName = "PlayableGround";
            m_landingCueFX.Play();
            yield return new WaitForSeconds(1f);
            m_animation.SetAnimation(0, m_info.landingAnimation, false);
            yield return new WaitForSeconds(0.6f);
            m_landFX.Play();
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.landingAnimation.animation);
            m_damageCollider.SetActive(true);
            m_hitbox.SetInvulnerability(Invulnerability.None);
        }
        private IEnumerator PetalFXRoutine(Vector2 target)
        {
            m_petalStartFX.Play();
            m_animation.SetAnimation(0, m_info.petalRain, false);
            yield return new WaitForSeconds(1.25f);
            m_petalEndFX.Play();
            for (int i = 0; i < m_currentPetalAmount; i++)
            {
                var xOffset = (m_targetPositions[i].x - target.x) * .2f;
                //var yOffset = point.y - target.y; //Precise
                var yOffset = (m_targetPositions[i].y - transform.position.y) * .2f; //Locked to Ground
                //m_currentSpawnPoint.position = new Vector2(UnityEngine.Random.Range(-5, 5) + m_petalProjectileSpawnPoint.position.x, UnityEngine.Random.Range(-5, 5) + m_petalProjectileSpawnPoint.position.y); //Random
                m_currentSpawnPoint.position = new Vector2(xOffset + m_petalProjectileSpawnPoint.position.x, yOffset + m_petalProjectileSpawnPoint.position.y); //In a straight path
                yield return new WaitForSeconds(.05f);
                LaunchPetalProjectile(m_targetPositions[i], m_currentSpawnPoint);
            }
            m_targetPositions.Clear();
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.petalRain);
            OnPetalRain?.Invoke(this, EventActionArgs.Empty);
            //LaunchPetalProjectile(target);
            yield return null;
        }
        private IEnumerator FlowerSporePattern(int randomFlowerPatternNumber)
        {
            switch (randomFlowerPatternNumber)
            {
                case 1:
                    for (int i = 0; i < FlowerPattern1.Length; i++)
                    {
                        if (FlowerPattern1[i] == 0)
                        {
                            for (int x = 0; x < m_flowerSpawnPointA.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundRight, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointA[x].position, m_flowerSpawnPointA[x].rotation);
                                yield return new WaitForSeconds(m_info.bulbExplosionInterval);
                            }
                        }
                        if (FlowerPattern1[i] == 1)
                        {
                            for (int x = 0; x < m_flowerSpawnPointB.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundLeft, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointB[x].position, m_flowerSpawnPointB[x].rotation);
                                yield return new WaitForSeconds(m_info.bulbExplosionInterval);
                            }
                        }
                        m_animation.SetAnimation(0, m_info.backgroundidleAnimation, true);
                        yield return new WaitForSeconds(m_info.patternEndInteral);
                    }
                    break;
                case 2:
                    for (int i = 0; i < FlowerPattern2.Length; i++)
                    {
                        if (FlowerPattern2[i] == 0)
                        {
                            for (int x = 0; x < m_flowerSpawnPointA.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundRight, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointA[x].position, m_flowerSpawnPointA[x].rotation);
                                yield return new WaitForSeconds(m_info.bulbExplosionInterval);
                            }
                        }
                        if (FlowerPattern2[i] == 1)
                        {
                            for (int x = 0; x < m_flowerSpawnPointB.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundLeft, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointB[x].position, m_flowerSpawnPointB[x].rotation);
                                yield return new WaitForSeconds(m_info.bulbExplosionInterval);
                            }
                        }
                        m_animation.SetAnimation(0, m_info.backgroundidleAnimation, true);
                        yield return new WaitForSeconds(m_info.patternEndInteral);
                    }
                    break;
                case 3:
                    for (int i = 0; i < FlowerPattern3.Length; i++)
                    {
                        if (FlowerPattern3[i] == 0)
                        {
                            for (int x = 0; x < m_flowerSpawnPointA.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundRight, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointA[x].position, m_flowerSpawnPointA[x].rotation);
                                yield return new WaitForSeconds(m_info.bulbExplosionInterval);
                            }
                        }
                        if (FlowerPattern3[i] == 1)
                        {
                            for (int x = 0; x < m_flowerSpawnPointB.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundLeft, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointB[x].position, m_flowerSpawnPointB[x].rotation);
                                yield return new WaitForSeconds(m_info.bulbExplosionInterval);
                            }
                        }
                        m_animation.SetAnimation(0, m_info.backgroundidleAnimation, true);
                        yield return new WaitForSeconds(m_info.patternEndInteral);
                    }
                    break;
                case 4:
                    for (int i = 0; i < FlowerPattern4.Length; i++)
                    {
                        if (FlowerPattern4[i] == 0)
                        {
                            for (int x = 0; x < m_flowerSpawnPointA.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundRight, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointA[x].position, m_flowerSpawnPointA[x].rotation);
                                yield return new WaitForSeconds(m_info.bulbExplosionInterval);
                            }
                        }
                        if (FlowerPattern4[i] == 1)
                        {
                            for (int x = 0; x < m_flowerSpawnPointB.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundLeft, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointB[x].position, m_flowerSpawnPointB[x].rotation);
                                yield return new WaitForSeconds(m_info.bulbExplosionInterval);
                            }
                        }
                        m_animation.SetAnimation(0, m_info.backgroundidleAnimation, true);
                        yield return new WaitForSeconds(m_info.patternEndInteral);
                    }
                    break;
                case 5:
                    for (int i = 0; i < FlowerPattern5.Length; i++)
                    {
                        if (FlowerPattern5[i] == 0)
                        {
                            for (int x = 0; x < m_flowerSpawnPointA.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundRight, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointA[x].position, m_flowerSpawnPointA[x].rotation);
                                yield return new WaitForSeconds(m_info.bulbExplosionInterval);
                            }
                        }
                        if (FlowerPattern5[i] == 1)
                        {
                            for (int x = 0; x < m_flowerSpawnPointB.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundLeft, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointB[x].position, m_flowerSpawnPointB[x].rotation);
                                yield return new WaitForSeconds(m_info.bulbExplosionInterval);
                            }
                        }
                        m_animation.SetAnimation(0, m_info.backgroundidleAnimation, true);
                        yield return new WaitForSeconds(m_info.patternEndInteral);
                    }
                    break;
                case 6:
                    for (int i = 0; i < FlowerPattern6.Length; i++)
                    {
                        if (FlowerPattern6[i] == 2)
                        {
                            for (int x = 0; x < m_flowerSpawnPointC.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointC[x].position, m_flowerSpawnPointC[x].rotation);

                            }
                        }
                        if (FlowerPattern6[i] == 3)
                        {
                            for (int x = 0; x < m_flowerSpawnPointD.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointD[x].position, m_flowerSpawnPointD[x].rotation);
                            }
                        }
                        if (FlowerPattern6[i] == 4)
                        {
                            for (int x = 0; x < m_flowerSpawnPointE.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointE[x].position, m_flowerSpawnPointE[x].rotation);
                            }
                        }
                        if (FlowerPattern6[i] == 5)
                        {
                            for (int x = 0; x < m_flowerSpawnPointF.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointF[x].position, m_flowerSpawnPointF[x].rotation);
                            }
                        }
                        if (FlowerPattern6[i] == 6)
                        {
                            for (int x = 0; x < m_flowerSpawnPointG.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointG[x].position, m_flowerSpawnPointG[x].rotation);
                            }
                        }
                        yield return new WaitForSeconds(m_info.flowerSpore2Interval);
                    }
                    break;
                case 7:
                    for (int i = 0; i < FlowerPattern7.Length; i++)
                    {
                        if (FlowerPattern7[i] == 2)
                        {
                            for (int x = 0; x < m_flowerSpawnPointC.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointC[x].position, m_flowerSpawnPointC[x].rotation);

                            }
                        }
                        if (FlowerPattern7[i] == 3)
                        {
                            for (int x = 0; x < m_flowerSpawnPointD.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointD[x].position, m_flowerSpawnPointD[x].rotation);
                            }
                        }
                        if (FlowerPattern7[i] == 4)
                        {
                            for (int x = 0; x < m_flowerSpawnPointE.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointE[x].position, m_flowerSpawnPointE[x].rotation);
                            }
                        }
                        if (FlowerPattern7[i] == 5)
                        {
                            for (int x = 0; x < m_flowerSpawnPointF.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointF[x].position, m_flowerSpawnPointF[x].rotation);
                            }
                        }
                        if (FlowerPattern7[i] == 6)
                        {
                            for (int x = 0; x < m_flowerSpawnPointG.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointG[x].position, m_flowerSpawnPointG[x].rotation);
                            }
                        }

                        yield return new WaitForSeconds(m_info.flowerSpore2Interval);
                    }
                    break;
                case 8:
                    for (int i = 0; i < FlowerPattern8.Length; i++)
                    {
                        if (FlowerPattern8[i] == 2)
                        {
                            for (int x = 0; x < m_flowerSpawnPointC.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointC[x].position, m_flowerSpawnPointC[x].rotation);

                            }
                        }
                        if (FlowerPattern8[i] == 3)
                        {
                            for (int x = 0; x < m_flowerSpawnPointD.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointD[x].position, m_flowerSpawnPointD[x].rotation);
                            }
                        }
                        if (FlowerPattern8[i] == 4)
                        {
                            for (int x = 0; x < m_flowerSpawnPointE.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointE[x].position, m_flowerSpawnPointE[x].rotation);
                            }
                        }
                        if (FlowerPattern8[i] == 5)
                        {
                            for (int x = 0; x < m_flowerSpawnPointF.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointF[x].position, m_flowerSpawnPointF[x].rotation);
                            }
                        }
                        if (FlowerPattern8[i] == 6)
                        {
                            for (int x = 0; x < m_flowerSpawnPointG.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointG[x].position, m_flowerSpawnPointG[x].rotation);
                            }
                        }

                        yield return new WaitForSeconds(m_info.flowerSpore2Interval);
                    }
                    break;
                case 9:
                    for (int i = 0; i < FlowerPattern9.Length; i++)
                    {
                        if (FlowerPattern9[i] == 2)
                        {
                            for (int x = 0; x < m_flowerSpawnPointC.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointC[x].position, m_flowerSpawnPointC[x].rotation);

                            }
                        }
                        if (FlowerPattern9[i] == 3)
                        {
                            for (int x = 0; x < m_flowerSpawnPointD.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointD[x].position, m_flowerSpawnPointD[x].rotation);
                            }
                        }
                        if (FlowerPattern9[i] == 4)
                        {
                            for (int x = 0; x < m_flowerSpawnPointE.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointE[x].position, m_flowerSpawnPointE[x].rotation);
                            }
                        }
                        if (FlowerPattern9[i] == 5)
                        {
                            for (int x = 0; x < m_flowerSpawnPointF.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointF[x].position, m_flowerSpawnPointF[x].rotation);
                            }
                        }
                        if (FlowerPattern9[i] == 6)
                        {
                            for (int x = 0; x < m_flowerSpawnPointG.Length; x++)
                            {
                                m_animation.SetAnimation(0, m_info.petalBackgroundBoth, true);
                                Instantiate(m_info.flowerBulb, m_flowerSpawnPointG[x].position, m_flowerSpawnPointG[x].rotation);
                            }
                        }

                        yield return new WaitForSeconds(m_info.flowerSpore2Interval);
                    }
                    break;
            }
            yield return null;
        }
        private void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        [SerializeField]
        private GameObject m_petalStalagmite;
        private IEnumerator StalagmiteSeedLaunchRoutine1()
        {
            m_petalStalagmite.GetComponent<PetalStalagtite>().m_motherMantisAI = this.gameObject;
            m_seedSpawning = true;
            m_targetPos = m_targetInfo.position;
            Shuffle(m_stalagmiteSpawnPoint1);
            var spawnPointsSelected = UnityEngine.Random.Range(7, 9);
            spawnPointsSelected = Mathf.Min(spawnPointsSelected, m_stalagmiteSpawnPoint1.Count);
            List<Transform> selectedSpawnPoints = m_stalagmiteSpawnPoint1.GetRange(0, spawnPointsSelected);

            //yield return new WaitForSeconds(3.5f);
            for (int i = 0; i < selectedSpawnPoints.Count; i++)
            {
                var distanceLeft = Vector3.Distance(m_targetPos, m_leftBounds.transform.position);
                var distanceRight = Vector3.Distance(m_targetPos, m_rightBounds.transform.position);
                if (distanceLeft < distanceRight)
                {
                    m_stalagmiteSpawnPointMain.position = new Vector2(m_rightStalagSpawn.position.x, m_rightStalagSpawn.position.y);
                }
                else
                {
                    m_stalagmiteSpawnPointMain.position = new Vector2(m_leftStalagSpawn.position.x, m_leftStalagSpawn.position.y);
                }
                var spawnPoint = selectedSpawnPoints[i].transform.position;
                //var numberOfSpawnPointToSelect
                GameObject projectile = m_info.seedProjectile;
                var instance = GameSystem.poolManager.GetPool<ProjectilePool>().GetOrCreateItem(projectile);
                instance.transform.position = spawnPoint;
                var component = instance.GetComponent<Projectile>();
                //instance.GetComponent<MotherMantisSeed>().OnStalagmiteSummoned += OnStalagmiteInstantiate;
                component.ResetState();
                yield return new WaitForSeconds(1f);
            }
            m_seedSpawning = false;
            yield return null;
        }
        private IEnumerator SeedLaunchRoutine1()
        {
            m_movement.Stop();
            m_hitbox.SetInvulnerability(Invulnerability.MAX);
            m_animation.SetAnimation(0, m_info.jump.animation, false);
            yield return new WaitForSeconds(2f);
            if (!IsFacingTarget()) { CustomTurn(); }
            yield return StalagmiteSeedLaunchRoutine1();
           // yield return new WaitForSeconds(6f);
            transform.position = new Vector2(m_stalagmiteSpawnPointMain.position.x + 4f, m_stalagmiteSpawnPointMain.position.y);
            /*var distanceLeft = Vector3.Distance(m_targetPos, m_leftBounds.transform.position);
            var distanceRight = Vector3.Distance(m_targetPos, m_rightBounds.transform.position);
            if (distanceLeft < distanceRight)
            {
                transform.position = new Vector2(m_rightBounds.transform.position.x - m_distance, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(m_leftBounds.transform.position.x + m_distance, transform.position.y);
            }*/
            //yield return new WaitForSeconds(4.5f);
            m_landingCueFX.Play();
            yield return new WaitForSeconds(1f);
            m_animation.SetAnimation(0, m_info.landingAnimation, false);
            yield return new WaitForSeconds(0.6f);
            m_landFX.Play();
            OnMantisLand?.Invoke(this, EventActionArgs.Empty);
            m_hitbox.SetInvulnerability(Invulnerability.None);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.landingAnimation.animation);
            if (!IsFacingTarget())
                CustomTurn();
            m_animation.SetAnimation(0, m_info.idlephase3Animation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.idlephase3Animation.animation);
            yield return null;
        }
        [SerializeField]
        private float[] sizeChange;
        private IEnumerator StalagmiteSeedLaunchRoutine2()
        {
            m_petalStalagmite.GetComponent<PetalStalagtite>().m_motherMantisAI = this.gameObject;
            m_seedSpawning = true;
            //var x = 0;
            foreach(var stalag2spawn in m_stalagmiteSpawnPoint2)
            {
                var spawnPoint = stalag2spawn.position;
                GameObject projectile = m_info.seedProjectile;
                var instance = GameSystem.poolManager.GetPool<ProjectilePool>().GetOrCreateItem(projectile);
                //instance.transform.localScale = new Vector2(projectile.transform.localScale.x, projectile.transform.localScale.y - sizeChange[x]);
                instance.transform.position = spawnPoint;
                var component = instance.GetComponent<Projectile>();
                component.ResetState();
                //x++;
                yield return new WaitForSeconds(.5f);
            }
            /*for (int i = 0; i < m_info.seedAmount; i++)
            {
                var spawnPoint = new Vector2(m_stalagmiteSpawnPoint2.position.x + (UnityEngine.Random.Range(-40f, 40f)), m_stalagmiteSpawnPoint2.position.y);
                GameObject projectile = m_info.seedProjectile;
                var instance = GameSystem.poolManager.GetPool<ProjectilePool>().GetOrCreateItem(projectile);
                instance.transform.position = spawnPoint;
                var component = instance.GetComponent<Projectile>();
                component.ResetState();
                yield return new WaitForSeconds(.5f);
            }*/
            m_seedSpawning = false;
            yield return null;
        }
        private IEnumerator SeedLaunchRoutine2()
        {
            var centerPoint = CalculateCenterPoint(m_leftBounds.transform.position, m_rightBounds.transform.position);
            m_movement.Stop();
            m_hitbox.SetInvulnerability(Invulnerability.MAX);
            m_animation.SetAnimation(0, m_info.jump.animation, false);
            yield return new WaitForSeconds(2f);
            transform.position = new Vector3(centerPoint.x, transform.position.y, 0);
            if (!IsFacingTarget()) { CustomTurn(); }
            yield return StalagmiteSeedLaunchRoutine2();
            //yield return new WaitForSeconds(4.5f);
            m_landingCueFX.Play();
            yield return new WaitForSeconds(1f);
            m_animation.SetAnimation(0, m_info.landingAnimation, false);
            yield return new WaitForSeconds(0.6f);
            m_landFX.Play();
            OnMantisLand?.Invoke(this, EventActionArgs.Empty);
            m_hitbox.SetInvulnerability(Invulnerability.None);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.landingAnimation.animation);
            m_targetInfo.GetTargetDamagable().DamageTaken += PlayerHit;
            if (!IsFacingTarget())
                CustomTurn();
            m_animation.SetAnimation(0, m_info.idlephase3Animation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.idlephase3Animation.animation);
            //StartCoroutine(PetalLaunchRoutine());
            yield return null;
        }

        private IEnumerator FlowerSpore1Routine()
        {
            m_movement.Stop();
            m_hitbox.SetInvulnerability(Invulnerability.MAX); //wasTrue
            m_damageCollider.SetActive(false);
            m_animation.SetAnimation(0, m_info.jump.animation, false);
            yield return new WaitForSeconds(1.5f);
            transform.position = m_backgroundSpawnPoint.position;
            m_animation.SetAnimation(0, m_info.backgroundLandingAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.backgroundLandingAnimation.animation);
            m_animation.SetAnimation(0, m_info.backgroundidleAnimation, false);
            var random = UnityEngine.Random.RandomRange(0, 5);
            switch (random)
            {
                case 0:
                    yield return FlowerSporePattern(1);
                    break;
                case 1:
                    yield return FlowerSporePattern(2);
                    break;
                case 2:
                    yield return FlowerSporePattern(3);
                    break;
                case 3:
                    yield return FlowerSporePattern(4);
                    break;
                case 4:
                    yield return FlowerSporePattern(5);
                    break;
            }
            m_animation.SetAnimation(0, m_info.backgroundJumpAnimation, false);
            yield return null;
        }
        private int m_spore2SafeSpot;
        private IEnumerator FlowerSpore2Routine()
        {
            m_movement.Stop();
            m_hitbox.SetInvulnerability(Invulnerability.MAX); //wasTrue
            m_damageCollider.SetActive(false);
            m_animation.SetAnimation(0, m_info.jump.animation, false);
            yield return new WaitForSeconds(1.5f);
            transform.position = m_backgroundSpawnPoint.position;
            m_animation.SetAnimation(0, m_info.backgroundLandingAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.backgroundLandingAnimation.animation);
            m_animation.SetAnimation(0, m_info.backgroundidleAnimation, false);
            var random = 0;
            var health = GetComponentInChildren<BasicHealth>();
            if (health.currentValue <= 150)
            {
                random = UnityEngine.Random.RandomRange(0, 4);
            }
            switch (random)
            {
                case 0:
                    yield return FlowerSporePattern(6);
                    m_spore2SafeSpot = 2;
                    break;
                case 1:
                    yield return FlowerSporePattern(7);
                    m_spore2SafeSpot = 0;
                    break;
                case 2:
                    yield return FlowerSporePattern(8);
                    m_spore2SafeSpot = 4;
                    break;
                case 3:
                    yield return FlowerSporePattern(9);
                    m_spore2SafeSpot = 1;
                    break;
            }
            m_animation.SetAnimation(0, m_info.backgroundJumpAnimation, false);
            yield return null;
        }
        Vector2 CalculateCenterPoint(Vector2 pos1, Vector2 pos2)
        {
            return (pos1 + pos2) / 2f;
        }
        #endregion
        #region Patterns
        private IEnumerator Phase1Pattern1Routine()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);

            float m_followElapsedTime = 0f;
            float m_followDuration = 2f;
            while (Vector2.Distance(transform.position, m_targetInfo.position) > 30f && m_followElapsedTime < m_followDuration)
            {
                m_animation.SetAnimation(0, m_info.move, true);
                Vector2 direction = new Vector2(m_targetInfo.position.x - transform.position.x, 0f).normalized;
                m_movement.MoveTowards(direction, m_info.move.speed);
                if (!IsFacingTarget())
                {
                    CustomTurn();
                }
                m_followElapsedTime += Time.deltaTime;
                yield return null;
            }
            if (Vector2.Distance(transform.position, m_targetInfo.position) < 20f)
            {
                if (!IsFacingTarget()) { CustomTurn(); }
                yield return ClawRoutine();
            }
            else
            {
                int random = UnityEngine.Random.Range(0, 2);
                if (random == 0)
                {
                    yield return JumpAttack1Routine();
                    if (m_isPlayerDamaged)
                    {
                        if (!IsFacingTarget()) { CustomTurn(); }
                        yield return ClawRoutine();
                    }
                }
                else
                {
                    Vector2 targetPoint = m_targetInfo.position;

                    if (!IsFacingTarget())
                        CustomTurn();
                    for (int i = 0; i < m_currentPetalAmount; i++)
                    {
                        m_targetPositions.Add(CalculatePositions());
                    }
                    yield return PetalFXRoutine(targetPoint);
                }
            }
            m_animation.SetAnimation(0, m_info.idlephase1Animation, false);
            DecidedOnAttack(false);
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }

        private IEnumerator Phase2Pattern1Routine()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            var m_followElapsedTime = 0f;
            var m_followDuration = 1.5f;
            while (Vector2.Distance(transform.position, m_targetInfo.position) > 30f && m_followElapsedTime < m_followDuration)
            {
                m_animation.SetAnimation(0, m_info.move, true);
                Vector2 direction = new Vector2(m_targetInfo.position.x - transform.position.x, 0f).normalized;
                m_movement.MoveTowards(direction, m_info.move.speed);
                if (!IsFacingTarget())
                {
                    CustomTurn();
                }
                m_followElapsedTime += Time.deltaTime;
                yield return null;
            }
            var random = UnityEngine.Random.RandomRange(0, 2);
            if(random == 0)
            {
                Vector2 targetPoint = m_targetInfo.position;
                if (!IsFacingTarget())
                    CustomTurn();
                for (int i = 0; i < m_currentPetalAmount; i++)
                {
                    m_targetPositions.Add(CalculatePositions());
                }
                yield return PetalFXRoutine(targetPoint);
            }
            else
            {
                yield return FlowerSpore1Routine();
                yield return JumpAttack2Routine();
            }
            m_animation.SetAnimation(0, m_info.idlephase2Animation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.idlephase2Animation);
            DecidedOnAttack(false);
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }
        private IEnumerator Phase3Pattern1Routine()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            var health = GetComponentInChildren<BasicHealth>();
            if (health.currentValue <= 150 && !m_seedSpawning && !m_playerHitByStalagmite2)
            {
                yield return SeedLaunchRoutine2();
                Vector2 targetPoint = m_targetInfo.position;
                if (!IsFacingTarget())
                    CustomTurn();
                for (int i = 0; i < m_currentPetalAmount; i++)
                {
                    m_targetPositions.Add(CalculatePositions());
                }
                yield return PetalFXRoutine(targetPoint);
            }
            var m_followElapsedTime = 0f;
            var m_followDuration = 1.5f;
            while (Vector2.Distance(transform.position, m_targetInfo.position) > 30f && m_followElapsedTime < m_followDuration)
            {
                m_animation.SetAnimation(0, m_info.moveLowHP, true);
                Vector2 direction = new Vector2(m_targetInfo.position.x - transform.position.x, 0f).normalized;
                m_movement.MoveTowards(direction, m_info.moveLowHP.speed);
                if (!IsFacingTarget())
                {
                    CustomTurn();
                }
                m_followElapsedTime += Time.deltaTime;
                yield return null;
            }
            var random = UnityEngine.Random.RandomRange(0, 5);
            if (random == 0)
            {
                Vector2 targetPoint = m_targetInfo.position;
                if (!IsFacingTarget())
                    CustomTurn();
                for (int i = 0; i < m_currentPetalAmount; i++)
                {
                    m_targetPositions.Add(CalculatePositions());
                }
                yield return PetalFXRoutine(targetPoint);
            }
            else if (random == 1)
            {
                yield return FlowerSpore1Routine();
                yield return JumpAttack2Routine();
                m_animation.SetAnimation(0, m_info.idlephase2Animation, false);
                yield return new WaitForAnimationComplete(m_animation.animationState, m_info.idlephase2Animation);
            }
            else if (random == 2)
            {
                yield return FlowerSpore2Routine();
                yield return JumpAttack2Routine(1, m_spore2SafeSpot);
                m_animation.SetAnimation(0, m_info.idlephase2Animation, false);
                yield return new WaitForAnimationComplete(m_animation.animationState, m_info.idlephase2Animation);
            }
            else if (random == 3)
            {
                yield return SeedLaunchRoutine1();
                Vector2 targetPoint = m_targetInfo.position;
                if (!IsFacingTarget())
                    CustomTurn();
                for (int i = 0; i < m_currentPetalAmount; i++)
                {
                    m_targetPositions.Add(CalculatePositions());
                }
                yield return PetalFXRoutine(targetPoint);
            }
            else
            {
                if (m_playerHitByStalagmite2)
                {
                    yield return SeedLaunchRoutine2();
                    Vector2 targetPoint = m_targetInfo.position;
                    if (!IsFacingTarget())
                        CustomTurn();
                    for (int i = 0; i < m_currentPetalAmount; i++)
                    {
                        m_targetPositions.Add(CalculatePositions());
                    }
                    yield return PetalFXRoutine(targetPoint);
                }
                yield return null;
            }
            m_animation.SetAnimation(0, m_info.idlephase3Animation, false);
            DecidedOnAttack(false);
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }
        #endregion
        private void DecidedOnAttack(bool condition)
        {
            // m_patternDecider.hasDecidedOnAttack = condition;
            m_attackDecider.hasDecidedOnAttack = condition;
        }
        private void UpdateAttackDeciderList()
        {
            switch (m_phaseHandle.currentPhase)
            {
                case Phase.PhaseOne:
                    m_attackDecider.SetList(new AttackInfo<Attack>(Attack.Phase1Pattern1, m_info.phase1Pattern1Range));
                    break;
                case Phase.PhaseTwo:
                    m_attackDecider.SetList(new AttackInfo<Attack>(Attack.Phase2Pattern1, m_info.phase2Pattern1Range));
                    break;
                case Phase.PhaseThree:
                    m_attackDecider.SetList(new AttackInfo<Attack>(Attack.Phase3Pattern1, m_info.phase3Pattern1Range));
                    break;
            }
            DecidedOnAttack(false);
        }

        public override void ApplyData()
        {
            if (m_attackDecider != null)
            {
                UpdateAttackDeciderList();
            }
          
            base.ApplyData();
        }
        protected override void Awake()
        {
            base.Awake();
            //m_attackHandle.AttackDone += OnAttackDone;
            m_turnHandle.TurnDone += OnTurnDone;
            m_deathHandle.SetAnimation(m_info.deathAnimation.animation);
            m_attackCache = new List<Attack>();
            m_attackRangeCache = new List<float>();
            m_attackUsed = new bool[m_attackCache.Count];
            m_currentPetalAmount = m_petalAmount;
            m_attackDecider = new RandomAttackDecider<Attack>();
            m_stateHandle = new StateHandle<State>(State.Idle, State.WaitBehaviourEnd);
            UpdateAttackDeciderList();
        }

        protected override void OnDestroy()
        {
            m_turnHandle.TurnDone -= OnTurnDone;
            base.OnDestroy();
        }

        private bool m_isPlayerDamaged;
        private void PlayerDamaged(object sender, Damageable.DamageEventArgs eventArgs)
        {
            m_isPlayerDamaged = true;
        }

        private bool m_playerHitByStalagmite2;
        private void PlayerHit(object sender, Damageable.DamageEventArgs eventArgs)
        {
            m_playerHitByStalagmite2 = true;
            m_targetInfo.GetTargetDamagable().DamageTaken -= PlayerHit;
        }
        protected override void Start()
        {
            base.Start();
            //m_flinchHandle.gameObject.SetActive(false);
            //m_spineListener.Subscribe(m_info.mantisEvent, LaunchProjectile);
            //m_currentCooldownSpeed = UnityEngine.Random.Range(m_info.attackCD * .5f, m_info.attackCD * 2f);
            m_animation.DisableRootMotion();
            m_moveAnim = m_info.move.animation;
            m_moveSpeed = m_info.move.speed;
            m_targetPositions = new List<Vector2>();

            m_phaseHandle = new PhaseHandle<Phase, PhaseInfo>();
            m_phaseHandle.Initialize(Phase.PhaseOne, m_info.phaseInfo, m_character, ChangeState, ApplyPhaseData);
            m_phaseHandle.ApplyChange();
        }

        private void Update()
        {
            m_phaseHandle.MonitorPhase();
            switch (m_stateHandle.currentState)
            {
                case State.Idle:
                    if (m_currentPhaseIndex == 1)
                    {
                        m_animation.SetAnimation(0, m_info.idlephase1Animation, true);
                    }
                    if (m_currentPhaseIndex == 2)
                    {
                        m_animation.SetAnimation(0, m_info.idlephase2Animation, true);
                    }
                    if (m_currentPhaseIndex == 3)
                    {
                        m_animation.SetAnimation(0, m_info.idlephase3Animation, true);
                    }
                    break;
                case State.Intro:
                    if (IsFacingTarget())
                    {
                        m_animation.SetAnimation(0, m_info.idlephase1Animation, true);
                        m_hitbox.SetInvulnerability(Invulnerability.None);
                        m_animation.DisableRootMotion();
                        m_stateHandle.OverrideState(State.Chasing);
 
                    }
                    else
                    {
                        m_turnState = State.Intro;
                        if (m_animation.GetCurrentAnimation(0).ToString() != m_info.turnAnimation.animation)
                            m_stateHandle.SetState(State.Turning);
                    }
                    break;
                case State.Phasing:
                    StopAllCoroutines();
                    StartCoroutine(ChangePhaseRoutine());
                    break;
                case State.Turning:
                    m_phaseHandle.allowPhaseChange = false;
                    m_stateHandle.Wait(m_turnState);
                    //m_animation.animationState.TimeScale = 2f;
                    if (m_currentPhaseIndex == 1)
                    {
                        m_turnHandle.Execute(m_info.turnAnimation.animation, m_info.idlephase1Animation.animation);
                    }
                    if (m_currentPhaseIndex == 2)
                    {
                        m_turnHandle.Execute(m_info.turnAnimation.animation, m_info.idlephase2Animation.animation);
                    }
                    if (m_currentPhaseIndex == 3)
                    {
                        m_turnHandle.Execute(m_info.turnAnimation.animation, m_info.idlephase3Animation.animation);
                    }
                    m_movement.Stop();
                    break;
                case State.Attacking:
                    m_stateHandle.Wait(State.ReevaluateSituation);
                    if(m_attackDecider.hasDecidedOnAttack == false)
                    {
                        m_attackDecider.DecideOnAttack();
                    }
                    switch (m_attackDecider.chosenAttack.attack)
                    {
                        case Attack.Phase1Pattern1:
                            StartCoroutine(Phase1Pattern1Routine());
                            break;
                        case Attack.Phase2Pattern1:
                            StartCoroutine(Phase2Pattern1Routine());
                            break;
                        case Attack.Phase3Pattern1:
                            StartCoroutine(Phase3Pattern1Routine());
                            break;
                    }
                    break;
                case State.Chasing:
                    m_stateHandle.SetState(State.Attacking);
                    break;

                case State.ReevaluateSituation:
                    if (m_targetInfo.isValid)
                    {
                        m_stateHandle.SetState(State.Attacking);
                    }
                    else
                    {
                        m_stateHandle.SetState(State.Idle);
                    }
                    break;
                case State.WaitBehaviourEnd:
                    return;
            }
        }

        protected override void OnTargetDisappeared()
        {
            m_stickToGround = false;
            m_currentCD = 0;
        }

        public override void ReturnToSpawnPoint()
        {
        }

        protected override void OnForbidFromAttackTarget()
        {
        }
    }
}
