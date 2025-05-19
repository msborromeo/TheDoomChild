using System;
using DChild.Gameplay.Combat;
using Holysoft.Event;
using DChild.Gameplay.Characters.AI;
using UnityEngine;
using Spine.Unity;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;

namespace DChild.Gameplay.Characters.Enemies
{

    [AddComponentMenu("DChild/Gameplay/Enemies/Boss/FrankyAI")]
    public class FrankyAI : CombatAIBrain<FrankyAI.Info>
    {
        [System.Serializable]
        public class Info : BaseInfo
        {
            [SerializeField]
            private PhaseInfo<Phase> m_phaseInfo;
            public PhaseInfo<Phase> phaseInfo => m_phaseInfo;

            [SerializeField]
            private MovementInfo m_move = new MovementInfo();
            public MovementInfo move => m_move;

            [Title("Attack Behaviours")]
            #region ShoulderBash
            [SerializeField, Range(0, 100)]
            private float m_shoulderBashReelSpeed;
            public float shoulderBashReelSpeed => m_shoulderBashReelSpeed;
            [Title("Attack Behaviours")]
            [SerializeField]
            private Vector2 m_shoulderBashVelocity;
            public Vector2 shoulderBashVelocity => m_shoulderBashVelocity;
            [SerializeField]
            private SimpleAttackInfo m_shoulderBashAttack = new SimpleAttackInfo();
            public SimpleAttackInfo shoulderBashAttack => m_shoulderBashAttack;

            [SerializeField]
            private BasicAnimationInfo m_shoulderBashLoopAnimation;
            public BasicAnimationInfo shoulderBashLoopAnimation => m_shoulderBashLoopAnimation;
            [SerializeField]
            private BasicAnimationInfo m_shoulderBashEndAnimation;
            public BasicAnimationInfo shoulderBashEndAnimation => m_shoulderBashEndAnimation;
            [SerializeField]
            private BasicAnimationInfo m_shoulderBashAnimation;
            public BasicAnimationInfo shoulderBashAnimation => m_shoulderBashAnimation;
            #endregion
            #region PunchCombo
            [SerializeField]
            private SimpleAttackInfo m_punchComboAttack;
            public SimpleAttackInfo punchComboAttack => m_punchComboAttack;

            [SerializeField]
            private BasicAnimationInfo m_punchComboAnimation;
            public BasicAnimationInfo punchComboAnimation => m_punchComboAnimation;
            #endregion
            #region ChainFistPunch
            [SerializeField]
            private float m_punchVelocity;
            public float punchVelocity => m_punchVelocity;
            [SerializeField]
            private BasicAnimationInfo m_chainFistPunchAttackAnticipation;
            public BasicAnimationInfo chainFistAttackAnticipation => m_chainFistPunchAttackAnticipation;
            [SerializeField]
            private SimpleAttackInfo m_chainFistPunchAttack = new SimpleAttackInfo();
            public SimpleAttackInfo chainFistPunchAttack => m_chainFistPunchAttack;
            [SerializeField]
            private BasicAnimationInfo m_chainFistPunchUpperAnimation;
            public BasicAnimationInfo chainFistPunchUpperAnimation => m_chainFistPunchUpperAnimation;
            #endregion
            #region LeapAttack
            [SerializeField]
            private SimpleAttackInfo m_leapAttack = new SimpleAttackInfo();
            public SimpleAttackInfo leapAttack => m_leapAttack;
            [SerializeField]
            private BasicAnimationInfo m_leapfirstAttackAnimation;
            public BasicAnimationInfo leapfirstAttackAnimation => m_leapfirstAttackAnimation;
            [SerializeField]
            private BasicAnimationInfo m_leapTransitionAnimation;
            public BasicAnimationInfo leapTransitionAnimation => m_leapTransitionAnimation;
            [SerializeField]
            private BasicAnimationInfo m_leapAttackEndAnimation;
            public BasicAnimationInfo leapAttackEndAnimation => m_leapAttackEndAnimation;
            [SerializeField, TabGroup("Leap Attack Values")]
            private float m_leapVelocity;
            public float leapVelocity => m_leapVelocity;
            [SerializeField, MinValue(0), TabGroup("Leap Attack Values")]
            private float m_leapTime;
            public float leapTime => m_leapTime;
            [SerializeField, TabGroup("Leap Attack Values")]
            private float m_transitionStart;
            public float transitionStart => m_transitionStart;
            #endregion

            #region ChainBash
            [SerializeField]
            private SimpleAttackInfo m_chainBash1Attack = new SimpleAttackInfo();
            public SimpleAttackInfo chainbash1Attack => m_chainBash1Attack;

            [SerializeField]
            private BasicAnimationInfo m_chainBash1AnimationStart;
            public BasicAnimationInfo chainBash1AnimationStart => m_chainBash1AnimationStart;

            [SerializeField]
            private BasicAnimationInfo m_chainBash1AnimationLoop;
            public BasicAnimationInfo chainBash1AnimationLoop => m_chainBash1AnimationLoop;

            [SerializeField]
            private BasicAnimationInfo m_chainBash1AnimationEnd;
            public BasicAnimationInfo chainBash1AnimationEnd => m_chainBash1AnimationEnd;

            [SerializeField]
            private BasicAnimationInfo m_chainBash2AnimationEnd;
            public BasicAnimationInfo chainBash2AnimationEnd => m_chainBash2AnimationEnd;

            [SerializeField]
            private BasicAnimationInfo m_chainBash2AnimationLoop;
            public BasicAnimationInfo chainBash2AnimationLoop => m_chainBash2AnimationLoop;

            [SerializeField]
            private float m_chainBashDuration;
            public float ChainBashDuration => m_chainBashDuration;
            #endregion
            #region RunAttack
            [SerializeField]
            private SimpleAttackInfo m_runAttack = new SimpleAttackInfo();
            public SimpleAttackInfo runAttack => m_runAttack;
            [SerializeField]
            private BasicAnimationInfo m_runAttackStartAnimation;
            public BasicAnimationInfo runAttackStartAnimation => m_runAttackStartAnimation;
            [SerializeField]
            private BasicAnimationInfo m_runAttackAnimation;
            public BasicAnimationInfo runAttackAnimation => m_runAttackAnimation;
            [SerializeField]
            private BasicAnimationInfo m_runAttackEndAnimation;
            public BasicAnimationInfo runAttackEndAnimation => m_runAttackEndAnimation;
            //[SerializeField, TabGroup("Run Attack Values")]
            //private float m_runAttackDistance;
            //public float runAttackDistance => m_runAttackDistance;
            [SerializeField, TabGroup("Run Attack Values")]
            private float m_runAttackSpeed;
            public float runAttackSpeed => m_runAttackSpeed;
            #endregion

            #region ChainShock
            [SerializeField]
            private SimpleAttackInfo m_chainShockAttack = new SimpleAttackInfo();
            public SimpleAttackInfo chainShockAttack => m_chainShockAttack;
            [SerializeField]
            private BasicAnimationInfo m_chainShockLoopAnimation;
            public BasicAnimationInfo chainShockLoopAnimation => m_chainShockLoopAnimation;
            [SerializeField]
            private BasicAnimationInfo m_chainShockEndAnimation;
            public BasicAnimationInfo chainShockEndAnimation => m_chainShockEndAnimation;
            [SerializeField]
            private float m_shockTime;
            public float shockTime => m_shockTime;
            #endregion

            #region ShockRampage
            [SerializeField]
            private SimpleAttackInfo m_shockRampageAttack = new SimpleAttackInfo();
            public SimpleAttackInfo shockRampageAttack => m_shockRampageAttack;

            #endregion

            #region PhaseDischarge
            [SerializeField]
            private SimpleAttackInfo m_phaseDischarge = new SimpleAttackInfo();
            public SimpleAttackInfo phaseDischarge => m_phaseDischarge;

            #endregion

            [SerializeField]
            private SimpleAttackInfo m_lightningStompAttack = new SimpleAttackInfo();
            public SimpleAttackInfo lightningStompAttack => m_lightningStompAttack;

            [SerializeField, TabGroup("Phase 1"), BoxGroup("Pattern Ranges")]
            private float m_phase1Pattern1Range;
            public float phase1Pattern1Range => m_phase1Pattern1Range;
            [SerializeField, TabGroup("Phase 1"), BoxGroup("Pattern Ranges")]
            private float m_phase2Pattern1Range;
            public float phase2Pattern1Range => m_phase2Pattern1Range;
            [SerializeField, TabGroup("Phase 2"), BoxGroup("Pattern Ranges")]
            private float m_phase2Pattern2Range;
            public float phase2Pattern2Range => m_phase2Pattern2Range;
            [SerializeField, TabGroup("Phase 2"), BoxGroup("Pattern Ranges")]
            private float m_phase3Pattern1Range;
            public float phase3Pattern1Range => m_phase3Pattern1Range;
            [SerializeField, TabGroup("Phase 2"), BoxGroup("Pattern Ranges")]
            private float m_phase3Pattern2Range;
            public float phase3Pattern2Range => m_phase3Pattern2Range;

            [Title("Misc")]
            [SerializeField]
            private float m_targetDistanceTolerance;
            public float targetDistanceTolerance => m_targetDistanceTolerance;

            [Title("Animations")]
            [SerializeField]
            private BasicAnimationInfo m_introAnimation;
            public BasicAnimationInfo introAnimation => m_introAnimation;
            [SerializeField]
            private BasicAnimationInfo m_idleAnimation;
            public BasicAnimationInfo idleAnimation => m_idleAnimation;
            [SerializeField]
            private BasicAnimationInfo m_idle2Animation;
            public BasicAnimationInfo idle2Animation => m_idle2Animation;
            [SerializeField]
            private BasicAnimationInfo m_deathAnimation;
            public BasicAnimationInfo deathAnimation => m_deathAnimation;
            [SerializeField]
            private BasicAnimationInfo m_turnAnimation;
            public BasicAnimationInfo turnAnimation => m_turnAnimation;
            [SerializeField]
            private BasicAnimationInfo m_roarAnimation;
            public BasicAnimationInfo roarAnimation => m_roarAnimation;
            [SerializeField]
            private BasicAnimationInfo m_hookTravelLoopAnimation;
            public BasicAnimationInfo hookTravelLoopAnimation => m_hookTravelLoopAnimation;
            [SerializeField]
            private BasicAnimationInfo m_hookBackLoopAnimation;
            public BasicAnimationInfo hookBackLoopAnimation => m_hookBackLoopAnimation;

            [Title("Projectiles")]
            [SerializeField]
            private SimpleProjectileAttackInfo m_stompProjectile;
            public SimpleProjectileAttackInfo stompProjectile => m_stompProjectile;

            [Title("FX")]
            [SerializeField]
            private GameObject m_lightningBoltFX;
            public GameObject lightningBoltFX => m_lightningBoltFX;

            [Title("Events")]
            [SerializeField, ValueDropdown("GetEvents")]
            private string m_phaseEvent;
            public string phaseEvent => m_phaseEvent;
            [SerializeField, ValueDropdown("GetEvents")]
            private string m_stopRoarEvent;
            public string stopRoarEvent => m_stopRoarEvent;
            [SerializeField, ValueDropdown("GetEvents")]
            private string m_stompEvent;
            public string stompEvent => m_stompEvent;
            [SerializeField, ValueDropdown("GetEvents")]
            private string m_leapEvent;
            public string leapEvent => m_leapEvent;
            [SerializeField, ValueDropdown("GetEvents")]
            private string m_aimChainBash;
            public string aimChainBash => m_aimChainBash;


            public override void Initialize()
            {
#if UNITY_EDITOR
                m_move.SetData(m_skeletonDataAsset);
                m_shoulderBashAttack.SetData(m_skeletonDataAsset);
                m_chainBash1Attack.SetData(m_skeletonDataAsset);
                m_punchComboAttack.SetData(m_skeletonDataAsset);
                m_chainFistPunchAttack.SetData(m_skeletonDataAsset);
                m_chainFistPunchAttackAnticipation.SetData(m_skeletonDataAsset);
                m_leapAttack.SetData(m_skeletonDataAsset);
                m_chainShockAttack.SetData(m_skeletonDataAsset);
                m_lightningStompAttack.SetData(m_skeletonDataAsset);
                m_stompProjectile.SetData(m_skeletonDataAsset);
                m_runAttack.SetData(m_skeletonDataAsset);
                m_shockRampageAttack.SetData(m_skeletonDataAsset);
                m_phaseDischarge.SetData(m_skeletonDataAsset);

                m_shoulderBashLoopAnimation.SetData(m_skeletonDataAsset);
                m_shoulderBashEndAnimation.SetData(m_skeletonDataAsset);
                m_shoulderBashAnimation.SetData(m_skeletonDataAsset);
                m_punchComboAnimation.SetData(m_skeletonDataAsset);
                m_chainFistPunchUpperAnimation.SetData(m_skeletonDataAsset);
                m_leapfirstAttackAnimation.SetData(m_skeletonDataAsset);
                m_leapTransitionAnimation.SetData(m_skeletonDataAsset);
                m_leapAttackEndAnimation.SetData(m_skeletonDataAsset);
                m_runAttackStartAnimation.SetData(m_skeletonDataAsset);
                m_runAttackAnimation.SetData(m_skeletonDataAsset);
                m_runAttackEndAnimation.SetData(m_skeletonDataAsset);
                m_chainShockLoopAnimation.SetData(m_skeletonDataAsset);
                m_chainShockEndAnimation.SetData(m_skeletonDataAsset);
                m_introAnimation.SetData(m_skeletonDataAsset);
                m_idleAnimation.SetData(m_skeletonDataAsset);
                m_idle2Animation.SetData(m_skeletonDataAsset);
                m_deathAnimation.SetData(m_skeletonDataAsset);
                m_turnAnimation.SetData(m_skeletonDataAsset);
                m_roarAnimation.SetData(m_skeletonDataAsset);
                m_hookTravelLoopAnimation.SetData(m_skeletonDataAsset);
                m_hookBackLoopAnimation.SetData(m_skeletonDataAsset);
                m_chainBash1AnimationStart.SetData(m_skeletonDataAsset);
                m_chainBash1AnimationEnd.SetData(m_skeletonDataAsset);
                m_chainBash2AnimationEnd.SetData(m_skeletonDataAsset);
                m_chainBash2AnimationLoop.SetData(m_skeletonDataAsset);
                m_chainBash1AnimationLoop.SetData(m_skeletonDataAsset);
#endif
            }
        }

        [System.Serializable]
        public class PhaseInfo : IPhaseInfo
        {
            [SerializeField]
            private List<float> m_patternCount;
            public List<float> patternCount => m_patternCount;
            [SerializeField]
            private int m_phaseIndex;
            public int phaseIndex => m_phaseIndex;
        }


        private enum State
        {
            Phasing,
            Intro,
            Idle,
            Turning,
            Attacking,
            Chasing,
            ReevaluateSituation,
            WaitBehaviourEnd,
        }

        private enum Pattern
        {
            ChainFist,
            PunchCombo,
            LeapAttack,
            ShoulderBash,
            RunningAttack,
            PhaseDischarge1,
            ChainedBash1,
            PhaseDischarge2,
            ShockRampage,
            ChainedBash2,
            ElectricStomp,
            WaitAttackEnd,
        }

        private enum Attack
        {
            Phase1Pattern1,
            Phase2Pattern1,
            Phase2Pattern2,
            Phase3Pattern1,
            Phase3Pattern2,
            WaitAttackEnd
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
        private GameObject m_spriteMask;
        /*[SerializeField, TabGroup("Reference")]
        private Collider2D m_aoeBB;
        [SerializeField, TabGroup("Reference")]
        private Collider2D m_punchBB;*/
        [SerializeField, TabGroup("Colliders")]
        private Collider2D m_punchLeftComboBB;
        [SerializeField, TabGroup("Colliders")]
        private Collider2D m_punchRightComboBB;
        [SerializeField, TabGroup("Colliders")]
        private Collider2D m_shoulderBashBB;
        [SerializeField, TabGroup("Colliders")]
        private Collider2D m_leapAttackBB;
        [SerializeField, TabGroup("Colliders")]
        private Collider2D m_chainFistBB;
        [SerializeField, TabGroup("Colliders")]
        private Collider2D[] m_chainBashBB;
        [SerializeField, TabGroup("Colliders")]
        private Collider2D m_runningAttackBB;
        [SerializeField, TabGroup("Colliders")]
        private Collider2D m_shockRampageBB;
        /*[SerializeField, TabGroup("Reference")]
        private Collider2D m_punchComboLastHitBB;
        [SerializeField, TabGroup("Reference")]
        private Collider2D m_runningAttackBB;
        [SerializeField, TabGroup("Reference")]
        private Collider2D m_shockRampageBB;*/
        [SerializeField, TabGroup("Modules")]
        private AnimatedTurnHandle m_turnHandle;
        [SerializeField, TabGroup("Modules")]
        private MovementHandle2D m_movement;
        [SerializeField, TabGroup("Modules")]
        private DeathHandle m_deathHandle;
        //[SerializeField, TabGroup("Cinematic")]
        //private PlayableDirector m_director;
        //[SerializeField, TabGroup("Cinematic")]
        //private PlayableAsset m_bossCapsuleIdleCinematic;
        [SerializeField, TabGroup("Sensors")]
        private RaySensor m_groundSensor;
        [SerializeField, TabGroup("Attackers")]
        private Attacker m_punchComboAttacker;
        [SerializeField, TabGroup("Attackers")]
        private Attacker m_punchComboLastHitAttacker;
        [SerializeField, TabGroup("Attackers")]
        private Attacker m_LeapAttackAttacker;
        [SerializeField, TabGroup("Attackers")]
        private Attacker m_chainFistAttacker;
        [SerializeField, TabGroup("Attackers")]
        private Attacker m_chainedBashAttacker;
        [SerializeField, TabGroup("Attackers")]
        private Attacker m_runningAttack;
        [SerializeField, TabGroup("Attackers")]
        private Attacker m_shockRampage;
        [SerializeField, TabGroup("Effects")]
        private GameObject m_wallStickStartFX;
        [SerializeField, TabGroup("Effects")]
        private GameObject m_wallStickLoopFX;
        [SerializeField, TabGroup("Effects")]
        private GameObject m_wallStickEndFX;
        [SerializeField, TabGroup("Effects")]
        private GameObject m_leapFX;
        [SerializeField, TabGroup("Effects")]
        private ParticleFX m_orbLightningFX;
        [SerializeField, TabGroup("Effects")]
        private ParticleFX m_stompFX;
        [SerializeField, TabGroup("Effects")]
        private ParticleFX m_bodyLightningFX;
        [SerializeField, TabGroup("Effects")]
        private ParticleFX m_phase3FX;
        List<GameObject> m_lightningBoltEffects;
        [SerializeField]
        private SpineEventListener m_spineListener;

        [ShowInInspector]
        private StateHandle<State> m_stateHandle;
        State m_turnState;
        [ShowInInspector]
        private PhaseHandle<Phase, PhaseInfo> m_phaseHandle;
        [ShowInInspector]
        private RandomAttackDecider<Attack> m_attackDecider;
        [SerializeField]
        private Attack m_currentAttack;
        private float m_currentAttackRange;
        private ProjectileLauncher m_projectileLauncher;

        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_headPoint;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_wristPoint;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_wallPosPoint;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_fistPoint;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_fistRefPoint;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_projectilePoint;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_wallRunPoint;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_CenterOfTheArena;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_chainBashStarting;
        [SerializeField, TabGroup("Chain")]
        private BoxCollider2D m_chainHurtBox;
        [SerializeField, TabGroup("Chain")]
        private BoxCollider2D m_leapHurtBox;

        private int m_currentPhaseIndex;
        private int m_buffedAttackCount;
        private float m_attackCount;
        private float[] m_patternCount;
        private float m_currentLeapDuration;
        private bool m_rangeAttack;
        private bool m_stickToGround;
        private bool m_stickToWall;
        [SerializeField]
        private bool m_isBuffed;
        private bool m_playerIsHitFromPunchCombo;
        private bool m_hasChosenAttack;
        private bool m_hasPhaseChanged;
        private Coroutine m_currentAttackCoroutine;
        private Coroutine m_leapRoutine;
        private int m_attackSpecialAttackLimit;

        public event EventAction<EventActionArgs> PhaseDischargeAction;
        public event EventAction<EventActionArgs> ElectricPushLeft;
        public event EventAction<EventActionArgs> ElectricPushRight;


        private bool m_isDetecting;

        private void ApplyPhaseData(PhaseInfo obj)
        {
            if(m_attackDecider != null)
            {
                UpdateAttackDeciderList();
            }
            base.ApplyData();
        }

        private void ChangeState()
        {
            if (m_hasPhaseChanged)
            {
                m_stateHandle.OverrideState(State.Phasing);
                m_hasPhaseChanged = true;
                m_animation.SetEmptyAnimation(0, 0);
                m_phaseHandle.ApplyChange();
            }
            //StartCoroutine(SmartChangePhaseRoutine());
        }

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

        private void PlayerTakenDamge(object sender, Damageable.DamageEventArgs eventArgs)
        {
            m_playerIsHitFromPunchCombo = true;
        }

        private void OnTurnDone(object sender, FacingEventArgs eventArgs)
        {
            if (m_stateHandle.currentState != State.Phasing /*&& !m_hasPhaseChanged*/)
            {
                m_animation.animationState.TimeScale = 1f;
                m_stateHandle.ApplyQueuedState();
            }
            m_phaseHandle.allowPhaseChange = true;
        }

        /*private void CustomTurn()
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            m_character.SetFacing(transform.localScale.x == 1 ? HorizontalDirection.Right : HorizontalDirection.Left);
        }*/


        private IEnumerator IntroRoutine()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            m_movement.Stop();
            m_hitbox.SetInvulnerability(Invulnerability.None);
            m_spriteMask.SetActive(false);
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }

        /*private IEnumerator SmartChangePhaseRoutine()
        {
            Debug.Log("Im changing phase");
            //yield return new WaitWhile(() => !m_phaseHandle.allowPhaseChange);
            if (m_phaseHandle.allowPhaseChange == false)
            {
                do
                {
                    yield return null;
                } while (m_phaseHandle.allowPhaseChange == false);
            }
            UpdateAttackDeciderList();
            switch (m_phaseHandle.currentPhase)
            {
                case Phase.PhaseOne:
                    break;

                case Phase.PhaseTwo:
                    m_attackCache.Add(Attack.LeapAttack);
                    m_attackSpecialAttackLimit = 10;
                    UpdateRangeCache(m_info.leapAttack.range, m_info.chainFistPunchAttack.range, m_info.punchComboAttack.range);
                    break;

                case Phase.PhaseThree:
                    m_attackSpecialAttackLimit = 12;
                    break;
            }
            StopCurrentAttackRoutine();
            SetAIToPhasing();
            Debug.Log("Im have changed phase");
            yield return null;
        }*/

        private IEnumerator ChangePhaseRoutine()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            m_hitbox.SetInvulnerability(Invulnerability.MAX);
            m_animation.SetAnimation(0, m_info.roarAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.roarAnimation.animation);
            m_hasPhaseChanged = false;
            DecidedOnAttack(false);
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }
        #region Attacks
        private IEnumerator ShoulderBashRoutine()
        {
            m_phaseHandle.allowPhaseChange = false;

            m_animation.EnableRootMotion(true, false);
            m_animation.SetAnimation(0, m_info.shoulderBashAnimation, false).MixDuration = 0;
            //yield return new WaitForSeconds(0.5f);
            //m_character.physics.SetVelocity(m_info.shoulderBashVelocity.x * transform.localScale.x, 0);
            //yield return new WaitForSeconds(0.15f);
            //m_movement.Stop();
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.shoulderBashAnimation);
            m_animation.SetAnimation(0, m_info.idleAnimation, true).MixDuration = 0;
            DecidedOnAttack(false);
            m_animation.DisableRootMotion();
            m_currentAttackCoroutine = null;
            m_stateHandle.ApplyQueuedState();
            yield return null;
            m_attackCount++;
            m_phaseHandle.allowPhaseChange = true;
        }

        private IEnumerator PunchComboRoutine()
        {
            if (m_isBuffed && m_buffedAttackCount <= 2)
            {
                m_punchComboAttacker.SetDamageModifier(1.1f);
                m_punchComboLastHitAttacker.SetDamageModifier(1.1f);
            }
            else
            {
                m_punchComboAttacker.SetDamageModifier(1f);
            };

            m_playerIsHitFromPunchCombo = false;
            m_targetInfo.transform.GetComponent<Damageable>().DamageTaken += PlayerTakenDamge;//
            m_phaseHandle.allowPhaseChange = false;
            m_animation.EnableRootMotion(true, false);
            m_animation.SetAnimation(0, m_info.punchComboAnimation, false).MixDuration = 0;
            yield return new WaitForSeconds(.8f);
            m_punchRightComboBB.enabled = true;
            yield return new WaitForSeconds(0.4f);
            m_punchRightComboBB.enabled = false;
            m_punchLeftComboBB.enabled = true;
            yield return new WaitForSeconds(0.4f);
            m_punchLeftComboBB.enabled = false;
            //m_punchComboLastHitBB.enabled = true;
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.punchComboAnimation);
            //m_punchComboLastHitBB.enabled = false;
            DecidedOnAttack(false);
            m_animation.DisableRootMotion();
            m_targetInfo.transform.GetComponent<Damageable>().DamageTaken -= PlayerTakenDamge;//

            if (m_phaseHandle.currentPhase == Phase.PhaseOne && m_playerIsHitFromPunchCombo)
            {
               // m_currentAttack = Attack.ShoulderBash;
                m_currentAttackRange = m_info.shoulderBashAttack.range;
                m_hasChosenAttack = true;
            }
            else
            {
                m_animation.SetAnimation(0, m_info.idle2Animation, true);
                yield return new WaitForSeconds(1f);
                m_animation.SetAnimation(0, m_info.idleAnimation, true);
                yield return null;

                if (m_isBuffed)
                {
                    m_isBuffed = false;
                }
                else
                {
                    m_attackCount++;
                }
            }

            m_currentAttackCoroutine = null;
            m_stateHandle.ApplyQueuedState();
            yield return null;

            //if (m_playerIsHitFromPunchCombo)
            //{
            //    m_playerIsHitFromPunchCombo = false;
            //    if (m_phaseHandle.currentPhase == Phase.PhaseOne)
            //    {
            //        m_attackCount++;
            //        m_currentAttack = Attack.ShoulderBash;
            //        yield return null;
            //    }
            //    m_currentAttackCoroutine = null;
            //    m_stateHandle.ApplyQueuedState();
            //    yield return null;
            //}
            //else
            //{
            //    if(m_isBuffed)
            //    {
            //        m_isBuffed = false;
            //    }
            //    else
            //    {
            //        m_attackCount++;
            //    }
            //    m_currentAttackCoroutine = null;
            //    m_stateHandle.ApplyQueuedState();
            //    yield return null;
            //}

        }

        private IEnumerator ChainShockRoutine()
        {
            m_phaseHandle.allowPhaseChange = false;

            m_fistPoint.GetComponent<SkeletonUtilityBone>().enabled = true;
            m_fistPoint.GetComponent<SkeletonUtilityBone>().mode = SkeletonUtilityBone.Mode.Override;
            m_animation.SetAnimation(0, m_info.chainShockAttack.animation, false);
            yield return new WaitForSeconds(.65f);
            m_fistRefPoint.GetComponent<CircleCollider2D>().enabled = true;
            m_fistPoint.position = m_wristPoint.position;
            m_animation.SetAnimation(0, m_info.hookTravelLoopAnimation, true);
            var wallPos = WallPosition();
            while (Vector2.Distance(m_fistPoint.position, wallPos) > 1.5f)
            {
                m_fistPoint.position = Vector2.MoveTowards(m_fistPoint.position, wallPos, 5);
                yield return null;
            }
            var fxPos = new Vector2(m_fistPoint.position.x + (5f * transform.localScale.x), m_fistPoint.position.y);
            var wallStickStartFX = GameSystem.poolManager.GetPool<FXPool>().GetOrCreateItem(m_wallStickStartFX);
            wallStickStartFX.transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.localScale.x > 0 ? 90 : 270));
            wallStickStartFX.transform.position = fxPos;
            var wallStickLoopFX = GameSystem.poolManager.GetPool<FXPool>().GetOrCreateItem(m_wallStickLoopFX);
            wallStickLoopFX.transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.localScale.x > 0 ? 90 : 270));
            wallStickLoopFX.transform.position = fxPos;
            m_chainHurtBox.gameObject.SetActive(true);
            m_chainHurtBox.size = new Vector2((Vector2.Distance(m_wristPoint.position, wallPos)) * .65f, /*m_chainHurtBox.size.y*/ 5f);
            m_chainHurtBox.offset = new Vector2(m_chainHurtBox.size.x * .5f, -2f);
            m_animation.SetAnimation(0, m_info.chainShockLoopAnimation, true);
            var spawnPoint = new Vector2(transform.position.x + (15f * transform.localScale.x), transform.position.y + 4);
            //List<GameObject> m_lightningBoltEffects = new List<GameObject>();
            while (Vector2.Distance(spawnPoint, fxPos) > 10)
            {
                spawnPoint = new Vector2(spawnPoint.x + (5f * transform.localScale.x), spawnPoint.y);
                m_lightningBoltEffects.Add(this.InstantiateToScene(m_info.lightningBoltFX, spawnPoint, Quaternion.identity));
                //var lightningBoltFX = this.InstantiateToScene(m_info.lightningBoltFX, spawnPoint, Quaternion.identity);
                yield return null;
            }
            yield return new WaitForSeconds(m_info.shockTime);
            for (int i = 0; i < m_lightningBoltEffects.Count; i++)
            {
                Destroy(m_lightningBoltEffects[i]);
            }
            m_lightningBoltEffects.Clear();
            wallStickLoopFX.GetComponent<FX>().Stop();
            var wallStickEndFX = GameSystem.poolManager.GetPool<FXPool>().GetOrCreateItem(m_wallStickEndFX);
            wallStickEndFX.transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.localScale.x > 0 ? 90 : 270));
            wallStickEndFX.transform.position = fxPos;
            m_chainHurtBox.gameObject.SetActive(false);
            m_animation.SetAnimation(0, m_info.hookBackLoopAnimation, true);
            while (Vector2.Distance(m_fistPoint.position, m_wristPoint.position) > 1.5f)
            {
                m_fistPoint.position = Vector2.MoveTowards(m_fistPoint.position, m_wristPoint.position, 5);
                yield return null;
            }
            m_fistRefPoint.GetComponent<CircleCollider2D>().enabled = false;
            m_animation.SetAnimation(0, m_info.chainShockEndAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.chainShockEndAnimation);
            m_fistPoint.GetComponent<SkeletonUtilityBone>().enabled = false;
            m_fistPoint.GetComponent<SkeletonUtilityBone>().mode = SkeletonUtilityBone.Mode.Follow;
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            DecidedOnAttack(false);
            m_currentAttackCoroutine = null;
            m_stateHandle.ApplyQueuedState();
            yield return null;
            m_phaseHandle.allowPhaseChange = true;
        }

        private IEnumerator ChainedBashIRoutine()
        {
            enabled = false;
            m_phaseHandle.allowPhaseChange = false;

            m_fistPoint.GetComponent<SkeletonUtilityBone>().enabled = true;
            m_fistPoint.GetComponent<SkeletonUtilityBone>().mode = SkeletonUtilityBone.Mode.Override;
            m_wallPosPoint.SetParent(null);
            m_wallPosPoint.position = WallPosition();
            yield return new WaitForSeconds(0.5f);
            //m_animation.SetAnimation(0, m_info.chainBash1AnimationStart.animation, false);
            //yield return new WaitForAnimationComplete(m_animation.animationState, m_info.chainBash1AnimationStart);
            var fistRefPointCollider = m_fistRefPoint.GetComponent<CircleCollider2D>();
            yield return new WaitForSeconds(0.5f);
            fistRefPointCollider.enabled = true;
            yield return null;
            m_fistPoint.position = m_wristPoint.position;
            //m_animation.SetAnimation(0, m_info.hookTravelLoopAnimation, true);
            m_animation.SetAnimation(0, m_info.chainBash1AnimationStart.animation, false);
            while (Vector2.Distance(m_fistPoint.position, m_wallPosPoint.position) > 3f)
            {
                m_fistPoint.position = Vector2.MoveTowards(m_fistPoint.position, m_wallPosPoint.position, 5f);
                yield return null;
            }
            yield return new WaitForSeconds(0.7f);
            var fxPos = new Vector2(m_fistPoint.position.x + (5f * transform.localScale.x), m_fistPoint.position.y);
            var wallStickStartFX = GameSystem.poolManager.GetPool<FXPool>().GetOrCreateItem(m_wallStickStartFX);
            wallStickStartFX.transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.localScale.x > 0 ? 90 : 270));
            wallStickStartFX.transform.position = fxPos;
            var wallStickLoopFX = GameSystem.poolManager.GetPool<FXPool>().GetOrCreateItem(m_wallStickLoopFX);
            wallStickLoopFX.transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.localScale.x > 0 ? 90 : 270));
            wallStickLoopFX.transform.position = fxPos;
            m_wallPosPoint.gameObject.SetActive(true);
            StartCoroutine(StickToWallRoutine(m_wallPosPoint.position));
            GetComponentInChildren<SkeletonRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            fistRefPointCollider.enabled = false;
            yield return new WaitForSeconds(0.5f);
            m_animation.SetAnimation(0, m_info.hookTravelLoopAnimation, true);
            while (Vector2.Distance(transform.position, m_fistPoint.position) > 15f)
            {
                //Debug.Log(Vector2.Distance(transform.position, m_fistPoint.position));
                m_movement.MoveTowards(Vector2.one * transform.localScale.x, m_info.shoulderBashReelSpeed);
                yield return null;
            }
            wallStickLoopFX.GetComponent<FX>().Stop();
            var wallStickEndFX = GameSystem.poolManager.GetPool<FXPool>().GetOrCreateItem(m_wallStickEndFX);
            wallStickEndFX.transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.localScale.x > 0 ? 90 : 270));
            wallStickEndFX.transform.position = fxPos;
            m_wallPosPoint.gameObject.SetActive(false);
            GetComponentInChildren<SkeletonRenderer>().maskInteraction = SpriteMaskInteraction.None;
            m_stickToWall = false;
            m_fistRefPoint.GetComponent<CircleCollider2D>().enabled = false;
            var chainBashAnimation = m_animation.SetAnimation(0, m_info.chainBash1AnimationLoop, true);
            yield return new WaitForSeconds(m_info.ChainBashDuration);
            yield return new WaitForSpineAnimationComplete(chainBashAnimation);
            m_animation.SetAnimation(0, m_info.chainBash1AnimationEnd, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.chainBash1AnimationEnd);
            yield return new WaitForSeconds(.5f);
            m_wallPosPoint.SetParent(m_hitbox.transform);
            m_wallPosPoint.position = new Vector2(0, 0);
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            DecidedOnAttack(false);
            m_currentAttackCoroutine = null;
            m_fistPoint.GetComponent<SkeletonUtilityBone>().mode = SkeletonUtilityBone.Mode.Follow;
            yield return null;
            m_fistPoint.GetComponent<SkeletonUtilityBone>().enabled = false;
            yield return new WaitForSeconds(.5f);
            enabled = true;
            if (m_isBuffed)
            {
                m_attackCount = 0;
                //m_currentAttack = Attack.LeapAttack;
                m_currentAttackRange = m_info.leapAttack.range;
            }
            m_stateHandle.ApplyQueuedState();
            yield return new WaitForSeconds(0.2f);
            m_phaseHandle.allowPhaseChange = true;
            yield return null;

        }

        private IEnumerator ChainedBashIIRoutine()
        {
            enabled = false;
            m_phaseHandle.allowPhaseChange = false;

            m_fistPoint.GetComponent<SkeletonUtilityBone>().enabled = true;
            m_fistPoint.GetComponent<SkeletonUtilityBone>().mode = SkeletonUtilityBone.Mode.Override;
            yield return null;
            m_wallPosPoint.SetParent(null);
            m_wallPosPoint.position = WallPosition();
            //m_animation.SetAnimation(0, m_info.chainBash1AnimationStart.animation, false);
            //yield return new WaitForAnimationComplete(m_animation.animationState, m_info.chainBash1AnimationStart);
            var fistRefPointCollider = m_fistRefPoint.GetComponent<CircleCollider2D>();
            yield return new WaitForSeconds(0.5f);
            fistRefPointCollider.enabled = true;
            yield return null;
            m_fistPoint.position = m_wristPoint.position;
            //m_animation.SetAnimation(0, m_info.hookTravelLoopAnimation, true);
            m_animation.SetAnimation(0, m_info.chainBash1AnimationStart.animation, false);
            while (Vector2.Distance(m_fistPoint.position, m_wallPosPoint.position) > 3f)
            {
                m_fistPoint.position = Vector2.MoveTowards(m_fistPoint.position, m_wallPosPoint.position, 5f);
                yield return null;
            }
            var fxPos = new Vector2(m_fistPoint.position.x + (5f * transform.localScale.x), m_fistPoint.position.y);
            var wallStickStartFX = GameSystem.poolManager.GetPool<FXPool>().GetOrCreateItem(m_wallStickStartFX);
            wallStickStartFX.transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.localScale.x > 0 ? 90 : 270));
            wallStickStartFX.transform.position = fxPos;
            var wallStickLoopFX = GameSystem.poolManager.GetPool<FXPool>().GetOrCreateItem(m_wallStickLoopFX);
            wallStickLoopFX.transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.localScale.x > 0 ? 90 : 270));
            wallStickLoopFX.transform.position = fxPos;
            m_wallPosPoint.gameObject.SetActive(true);
            StartCoroutine(StickToWallRoutine(m_wallPosPoint.position));
            GetComponentInChildren<SkeletonRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            fistRefPointCollider.enabled = false;
            m_animation.SetAnimation(0, m_info.hookTravelLoopAnimation, true);
            while (Vector2.Distance(transform.position, m_fistPoint.position) > 15f)
            {
                //Debug.Log(Vector2.Distance(transform.position, m_fistPoint.position));
                m_movement.MoveTowards(Vector2.one * transform.localScale.x, m_info.shoulderBashReelSpeed);
                yield return null;
            }
            wallStickLoopFX.GetComponent<FX>().Stop();
            var wallStickEndFX = GameSystem.poolManager.GetPool<FXPool>().GetOrCreateItem(m_wallStickEndFX);
            wallStickEndFX.transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.localScale.x > 0 ? 90 : 270));
            wallStickEndFX.transform.position = fxPos;
            m_wallPosPoint.gameObject.SetActive(false);
            GetComponentInChildren<SkeletonRenderer>().maskInteraction = SpriteMaskInteraction.None;
            m_stickToWall = false;
            m_fistRefPoint.GetComponent<CircleCollider2D>().enabled = false;
            var chainBashAnimation = m_animation.SetAnimation(0, m_info.chainBash2AnimationLoop, true);
            if (m_character.facing == HorizontalDirection.Left)
            {
                ElectricPushLeft?.Invoke(this, EventActionArgs.Empty);
            }
            else
            {
                ElectricPushRight?.Invoke(this, EventActionArgs.Empty);
            }
            yield return new WaitForSeconds(m_info.ChainBashDuration);
            yield return new WaitForSpineAnimationComplete(chainBashAnimation);
            m_animation.SetAnimation(0, m_info.chainBash2AnimationEnd, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.chainBash2AnimationEnd);
            yield return new WaitForSeconds(.5f);
            m_wallPosPoint.SetParent(m_hitbox.transform);
            m_wallPosPoint.position = new Vector2(0, 0);
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            DecidedOnAttack(false);
            m_currentAttackCoroutine = null;
            m_fistPoint.GetComponent<SkeletonUtilityBone>().mode = SkeletonUtilityBone.Mode.Follow;
            yield return null;
            m_fistPoint.GetComponent<SkeletonUtilityBone>().enabled = false;
            enabled = true;
            m_phaseHandle.allowPhaseChange = true;
            yield return null;

            if (m_isBuffed)
            {
                m_attackCount = 0;
                //m_currentAttack = Attack.LeapAttack;
                m_currentAttackRange = m_info.leapAttack.range;

            }
            m_stateHandle.ApplyQueuedState();
        }

        private IEnumerator ChainFistPunchRoutine()
        {
            m_phaseHandle.allowPhaseChange = false;
            m_animation.SetAnimation(0, m_info.chainFistAttackAnticipation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.chainFistAttackAnticipation);
            var attackAnim = ChoosePunchAnimation();
            m_animation.SetAnimation(0, attackAnim, false);
            yield return new WaitForSeconds(0.65f);
            m_fistRefPoint.GetComponent<CircleCollider2D>().enabled = true;
            //m_punchBB.enabled = true;
            //m_character.physics.SetVelocity(m_info.punchVelocity * transform.localScale.x, attackAnim == m_info.chainFistPunchAttack.animation ? 0 : 25);
            yield return new WaitForSeconds(0.25f);
            m_fistRefPoint.GetComponent<CircleCollider2D>().enabled = false;
            //m_punchBB.enabled = false;
            m_movement.Stop();
            yield return new WaitForAnimationComplete(m_animation.animationState, attackAnim);
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            if (!IsFacingTarget())
            {
                CustomTurn();
            }
            attackAnim = ChoosePunchAnimation();
            m_animation.SetAnimation(0, attackAnim, false);
            yield return new WaitForSeconds(0.65f);
            //m_punchBB.enabled = true;
            //m_character.physics.SetVelocity(m_info.punchVelocity * transform.localScale.x, attackAnim == m_info.chainFistPunchAttack.animation ? 0 : 25);
            yield return new WaitForSeconds(0.25f);
            //m_punchBB.enabled = false;
            m_movement.Stop();
            yield return new WaitForAnimationComplete(m_animation.animationState, attackAnim);
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            DecidedOnAttack(false);
            if (m_isBuffed)
            {
                //m_currentAttack = Attack.ComboPunch;
                m_currentAttackRange = m_info.punchComboAttack.range;
            }
            else
            {
                m_attackCount++;
            }
            m_currentAttackCoroutine = null;
            m_stateHandle.ApplyQueuedState();
            yield return null;

            m_phaseHandle.allowPhaseChange = true;
        }

        private string ChoosePunchAnimation()
        {
            //return m_targetInfo.position.y > transform.position.y + 10 /*+ 5f*/ ? m_info.chainFistPunchUpperAnimation : m_info.chainFistPunchAttack.animation; ;
            return m_info.chainFistPunchAttack.animation;
        }

        private IEnumerator LightningStompRoutine()
        {
            m_phaseHandle.allowPhaseChange = false;

            m_animation.SetAnimation(0, m_info.lightningStompAttack.animation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.lightningStompAttack.animation);
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            DecidedOnAttack(false);
            if (m_isBuffed)
            {
                //m_currentAttack = Attack.ComboPunch;
                m_currentAttackRange = m_info.punchComboAttack.range;
            }
            m_currentAttackCoroutine = null;
            m_stateHandle.ApplyQueuedState();
            yield return null;

            m_phaseHandle.allowPhaseChange = true;
        }

        private void LaunchProjectile()
        {
            m_stompFX.Play();
            var target = new Vector2(m_projectilePoint.position.x + (5 * transform.localScale.x), m_projectilePoint.position.y);
            m_projectileLauncher.AimAt(target);
            m_projectileLauncher.LaunchProjectile();
        }
        private void AimChainBash()
        {

        }
        private IEnumerator LeapAttackRoutine(int repeats)
        {
            m_phaseHandle.allowPhaseChange = false;
            m_leapHurtBox.enabled = true;
            for (int i = 0; i < repeats; i++)
            {
                m_fistRefPoint.GetComponent<CircleCollider2D>().enabled = false;
                if (/*i < repeats-1*/!IsFacingTarget())
                {
                    m_animation.SetAnimation(0, m_info.leapTransitionAnimation, false).MixDuration = 0;
                    yield return new WaitForSeconds(m_info.transitionStart);
                    CustomTurn();
                }
                else
                {
                    m_animation.SetEmptyAnimation(0, 0);
                }
                var leapAnim = i == 0 ? m_info.leapfirstAttackAnimation.animation : m_info.leapAttack.animation;
                m_animation.SetAnimation(0, leapAnim, false).AnimationStart = i == 0 ? 0 : m_info.transitionStart;
                m_animation.animationState.GetCurrent(0).MixDuration = 0;
                //while (m_currentLeapDuration < .65f)
                //{
                //    m_movement.MoveTowards(Vector2.one * transform.localScale.x, UnityEngine.Random.Range(m_info.leapVelocity * .1f, m_info.leapVelocity));
                //    m_currentLeapDuration += Time.deltaTime;
                //    yield return null;
                //}
                var target = new Vector2(m_targetInfo.position.x - (20 * transform.localScale.x), m_targetInfo.position.y);
                var targetDistance = Vector2.Distance(target, transform.position);
                var velocity = targetDistance / m_info.leapTime;
                float time = 0;
                float animTime = 1 / (m_info.leapTime / 0.75f);
                m_animation.animationState.TimeScale = animTime;
                while (time < m_info.leapTime)
                {
                    m_character.physics.SetVelocity(velocity * transform.localScale.x, 0);
                    time += Time.deltaTime;
                    yield return null;
                }
                m_fistRefPoint.GetComponent<CircleCollider2D>().enabled = true;
                m_currentLeapDuration = 0;
                m_movement.Stop();
                yield return new WaitForAnimationComplete(m_animation.animationState, leapAnim);
            }
            m_leapHurtBox.enabled = false;
            m_fistRefPoint.GetComponent<CircleCollider2D>().enabled = false;
            m_animation.SetAnimation(0, m_info.leapAttackEndAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.leapAttackEndAnimation);
            m_stickToGround = false;
            DecidedOnAttack(false);
            m_currentAttackCoroutine = null;
            m_leapRoutine = null;
            if (m_phaseHandle.currentPhase == Phase.PhaseOne)
            {
                m_attackCount = 0;
            }
            else
            {
                if (m_isBuffed)
                {
                    m_isBuffed = false;
                }
                else
                {
                    m_attackCount++;
                }
            }
            m_stateHandle.OverrideState(State.Chasing);
            m_phaseHandle.allowPhaseChange = true;
            yield return null;
        }

        private IEnumerator PhaseDischargeRoutine()
        {
            enabled = false;
            m_stateHandle.Wait(State.ReevaluateSituation);
            //m_hitbox.SetInvulnerability(Invulnerability.None);
            //m_hasPhaseChanged = false;
            Debug.Log("is facing?" + IsFacing(m_CenterOfTheArena.position));
            if (!IsFacing(m_CenterOfTheArena.position))
            {
                CustomTurn();
            }
            m_animation.SetAnimation(0, m_info.runAttackStartAnimation, false);

            yield return new WaitForAnimationComplete(m_animation.skeletonAnimation.state, m_info.runAttackStartAnimation);
            m_animation.SetAnimation(0, m_info.runAttackAnimation, true);
            while (Vector2.Distance(transform.position, m_CenterOfTheArena.position) > 15f)
            {
                m_movement.MoveTowards(Vector2.right * transform.localScale.x, m_info.runAttackSpeed);
                yield return null;
            }
            m_animation.SetAnimation(0, m_info.runAttackEndAnimation, false);
            m_movement.Stop();

            if (m_currentPhaseIndex == 3)
            {
                m_animation.SetAnimation(0, m_info.phaseDischarge, false);
                yield return new WaitForSeconds(0.7f);
                PhaseDischargeAction?.Invoke(this, EventActionArgs.Empty);
            }
            else
            {
                m_animation.SetAnimation(0, m_info.phaseDischarge, false);
            }

            //yield return new WaitForSeconds(3.9f);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.phaseDischarge);
            m_isBuffed = true;
            m_attackCount = 0f;
            m_hitbox.SetInvulnerability(Invulnerability.None);
            switch (m_currentPhaseIndex)
            {
                case 2:
                    //ChooseAttack(3);
                    break;

                case 3:

                    //ChooseAttack(4);
                    break;
            }
            m_hasChosenAttack = true;
            StartCoroutine(StickToGroundRoutine(GroundPosition().y));
            //yield return StartCoroutine(LeapAttackRoutine(3));
            m_stateHandle.ApplyQueuedState();
            enabled = true;
            yield return null;
        }

        private IEnumerator ShockRampageRoutine()
        {

            m_phaseHandle.allowPhaseChange = false;
            m_fistPoint.GetComponent<SkeletonUtilityBone>().enabled = true;
            m_fistPoint.GetComponent<SkeletonUtilityBone>().mode = SkeletonUtilityBone.Mode.Follow;
            yield return null;
            m_animation.SetAnimation(0, m_info.shockRampageAttack.animation, false);
            //m_shockRampageBB.enabled = true;
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.shockRampageAttack);

            if (m_isBuffed)
            {
                m_isBuffed = false;
            }

            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.idleAnimation);
            //m_shockRampageBB.enabled = false;
            DecidedOnAttack(false);
            m_currentAttackCoroutine = null;
            m_fistPoint.GetComponent<SkeletonUtilityBone>().enabled = false;
            m_phaseHandle.allowPhaseChange = true;
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }

        private IEnumerator RunningAttackRoutine()
        {
            m_wallRunPoint.position = WallPosition();
            m_wallRunPoint.SetParent(null);
            m_animation.SetAnimation(0, m_info.runAttackStartAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.skeletonAnimation.state, m_info.runAttackStartAnimation);
            //m_runningAttackBB.enabled = true;
            m_animation.SetAnimation(0, m_info.runAttackAnimation, true);
            while (Mathf.Abs(transform.position.x - m_wallRunPoint.position.x) > 5f)
            {
                m_movement.MoveTowards(Vector2.right * transform.localScale.x, m_info.runAttackSpeed);
                yield return null;
            }
            //if (Vector2.Distance(transform.position, m_wallRunPoint.position) > m_info.runAttack.range)
            //{
            //    m_movement.MoveTowards(Vector2.right * transform.localScale.x, m_info.runAttackSpeed);
            //}
            //if(m_wallRunPoint.position.x > m_info.runAttackDistance || m_wallRunPoint.position.x < m_info.runAttackDistance)
            //{
            //    m_movement.MoveTowards(Vector2.right * transform.localScale.x, m_info.runAttackSpeed);

            //}
            //if (Vector2.Distance(m_wallRunPoint.position, m_wallPosPoint.position) < m_info.runAttackDistance)
            //{
            //    m_movement.MoveTowards(Vector2.right * transform.localScale.x, m_info.runAttackSpeed);
            //}
            m_animation.SetAnimation(0, m_info.runAttackEndAnimation, false);
            m_movement.Stop();
            m_wallRunPoint.SetParent(m_hitbox.transform);
            m_wallRunPoint.position = new Vector2(0, 0);
            //m_runningAttackBB.enabled = false;
            DecidedOnAttack(false);
            m_currentAttackCoroutine = null;
            m_stateHandle.ApplyQueuedState();
            yield return null;
            if (m_isBuffed)
            {
                //m_currentAttack = Attack.ShockRampage;
                m_currentAttackRange = m_info.shockRampageAttack.range;
            }

        }
        #endregion

        private IEnumerator StickToGroundRoutine(float groundPoint)
        {
            m_stickToGround = true;
            while (m_stickToGround)
            {
                transform.position = new Vector2(transform.position.x, groundPoint);
                yield return null;
            }
        }

        private IEnumerator StickToWallRoutine(Vector2 wallPoint)
        {
            m_stickToWall = true;
            while (m_stickToWall)
            {
                m_fistPoint.position = new Vector2(wallPoint.x, wallPoint.y);
                m_wallPosPoint.position = new Vector2(m_wallPosPoint.position.x, wallPoint.y);
                yield return null;
            }
        }
        #region Attacks
        private IEnumerator PunchCombo()
        {
            while (Vector2.Distance(transform.position, m_targetInfo.position) > 15f)
            {
                m_animation.SetAnimation(0, m_info.move.animation, true);
                m_movement.MoveTowards(new Vector2(m_targetInfo.position.x - transform.position.x, 0f).normalized, m_info.move.speed);
                if (!IsFacingTarget())
                {
                    CustomTurn();
                }
                yield return null;
            }
            m_animation.SetAnimation(0, m_info.punchComboAttack, false);
            m_targetInfo.GetTargetDamagable().DamageTaken += PlayerDamagedPunchCombo;
            m_punchLeftComboBB.enabled = true;
            m_punchRightComboBB.enabled = true;
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.punchComboAttack);
            m_punchLeftComboBB.enabled = false;
            m_punchRightComboBB.enabled = false;
            if (!m_hitByPunchCombo)
            {
                m_animation.SetAnimation(0, m_info.idle2Animation, false);
                yield return new WaitForAnimationComplete(m_animation.animationState, m_info.idle2Animation);
            }
            else
            {
                yield return ShoulderBash();
            }
            if (m_isBuffed)
            {
                m_isBuffed = false;
                yield return null;
            }
            else
            {
                m_attackCounter++;
            }
            yield return null;
        }

        private IEnumerator ChainFist()
        {
            m_animation.SetAnimation(0, m_info.chainFistAttackAnticipation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.chainFistAttackAnticipation);
            m_animation.SetAnimation(0, m_info.chainFistPunchAttack, false);
            m_chainFistBB.enabled = true;
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.chainFistPunchAttack);
            m_chainFistBB.enabled = false;
            m_animation.SetAnimation(0, m_info.idle2Animation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.idle2Animation);
            if (m_isBuffed)
            {
                yield return PunchCombo();
            }
            else
            {
                m_attackCounter++;
            }
            yield return null;
        }
        private IEnumerator ShoulderBash()
        {
            while (Vector2.Distance(transform.position, m_targetInfo.position) > 10f)
            {
                m_animation.SetAnimation(0, m_info.move.animation, true);
                m_movement.MoveTowards(new Vector2(m_targetInfo.position.x - transform.position.x, 0f).normalized, m_info.move.speed);
                if (!IsFacingTarget())
                {
                    CustomTurn();
                }
                yield return null;
            }
            m_animation.EnableRootMotion(true, false);
            m_animation.SetAnimation(0, m_info.shoulderBashAnimation, false);
            m_shoulderBashBB.enabled = true;
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.shoulderBashAnimation);
            m_shoulderBashBB.enabled = false;
            m_animation.SetAnimation(0, m_info.idleAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.idleAnimation);
            m_animation.DisableRootMotion();
            m_attackCounter++;
            yield return null;
        }
        private IEnumerator LeapAttack()
        {
            m_animation.SetAnimation(0, m_info.leapTransitionAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.leapTransitionAnimation);
            m_leapAttackBB.enabled = false;
            m_animation.SetAnimation(0, m_info.idle2Animation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.idle2Animation);
            if(m_phaseHandle.currentPhase == Phase.PhaseOne)
            {
                m_attackCounter = 0;
            }
            else
            {
                if (!m_isBuffed)
                {
                    m_attackCounter++;
                }
                else
                {
                    m_isBuffed = false;
                }
            }
            yield return null;
        }
        private IEnumerator PhaseDistarge1()
        {
            while(Vector2.Distance(transform.position, m_CenterOfTheArena.position) > 2f)
            {
                Debug.Log(Vector2.Distance(transform.position, m_CenterOfTheArena.position));
                m_animation.SetAnimation(0, m_info.move.animation, true);
                m_movement.MoveTowards(new Vector2(m_CenterOfTheArena.position.x - transform.position.x, 0f).normalized, m_info.move.speed);
                yield return null;
            }
            m_animation.SetAnimation(0, m_info.phaseDischarge, false);
            yield return new WaitForSeconds(0.5f);
            m_orbLightningFX.Play();
            PhaseDischargeAction?.Invoke(this, EventActionArgs.Empty);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.phaseDischarge);
            m_orbLightningFX.Stop();
            m_attackCounter = 0;
            m_animation.SetAnimation(0, m_info.idle2Animation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.idle2Animation);
            var random = UnityEngine.Random.RandomRange(0, 2);
            if(random == 0)
            {
                if (!IsFacingTarget()) { CustomTurn(); }
                m_chainedBashAttacker.SetDamageModifier(1.1f);
                yield return ChainBash();
                m_punchComboAttacker.SetDamageModifier(1.1f);
                yield return PunchCombo();
            }
            else
            {
                if (!IsFacingTarget()) { CustomTurn(); }
                m_chainFistAttacker.SetDamageModifier(1.1f);
                yield return ChainFist();
                m_punchComboAttacker.SetDamageModifier(1.1f);
                yield return PunchCombo();
            }
            m_chainedBashAttacker.SetDamageModifier(1f);
            m_punchComboAttacker.SetDamageModifier(1f);
            m_chainFistAttacker.SetDamageModifier(1f);
            m_punchComboAttacker.SetDamageModifier(1f);
            yield return null;
        }
        private IEnumerator PhaseDistarge2()
        {
            while(Vector2.Distance(transform.position, m_CenterOfTheArena.position) > 2f)
            {
                Debug.Log(Vector2.Distance(transform.position, m_CenterOfTheArena.position));
                m_animation.SetAnimation(0, m_info.move.animation, true);
                m_movement.MoveTowards(new Vector2(m_CenterOfTheArena.position.x - transform.position.x, 0f).normalized, m_info.move.speed);
                yield return null;
            }
            m_animation.SetAnimation(0, m_info.phaseDischarge, false);
            yield return new WaitForSeconds(0.5f);
            m_orbLightningFX.Play();
            PhaseDischargeAction?.Invoke(this, EventActionArgs.Empty);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.phaseDischarge);
            m_orbLightningFX.Stop();
            m_attackCounter = 0;
            m_animation.SetAnimation(0, m_info.idle2Animation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.idle2Animation);
            var random = UnityEngine.Random.RandomRange(0, 3);
            if (random == 0)
            {
                if (!IsFacingTarget()) { CustomTurn(); }
                m_chainedBashAttacker.SetDamageModifier(1.1f);
                yield return ChainBash2();
                m_LeapAttackAttacker.SetDamageModifier(1.1f);
                yield return LeapAttack();
            }
            else if (random == 1)
            {
                if (!IsFacingTarget()) { CustomTurn(); }
                m_punchComboAttacker.SetDamageModifier(1.1f);
                yield return ElectricStomp();
                yield return PunchCombo();
            }
            else
            {
                m_runningAttack.SetDamageModifier(1.1f);
                yield return RunningAttack();
                m_shockRampage.SetDamageModifier(1.1f);
                yield return ShockRampage();
            }
            m_LeapAttackAttacker.SetDamageModifier(1f);
            m_punchComboAttacker.SetDamageModifier(1f);
            m_runningAttack.SetDamageModifier(1f);
            yield return null;
        }
        private GameObject SpawnFX(GameObject fxPrefab, Vector2 position)
        {
            var fx = GameSystem.poolManager.GetPool<FXPool>().GetOrCreateItem(fxPrefab);
            fx.transform.rotation = Quaternion.Euler(0, 0, transform.localScale.x > 0 ? 90 : 270);
            fx.transform.position = position;
            return fxPrefab;
        }

        private IEnumerator ChainBash()
        {
            m_animation.SetAnimation(0, m_info.chainBash1AnimationStart.animation, false);
            m_chainBashBB[0].enabled = true;
            yield return new WaitForSeconds(1f);
            RaycastHit2D hit = Physics2D.Raycast(m_chainBashStarting.position, Vector2.right * transform.localScale.x, 1000, DChildUtility.GetEnvironmentMask());

            if (!hit.collider)
            {
                yield break;
            }
            var fistBone = m_fistPoint.GetComponent<SkeletonUtilityBone>();
            fistBone.enabled = true;
            fistBone.mode = SkeletonUtilityBone.Mode.Override;
            Vector2 targetPos = hit.point;
            m_fistPoint.position = targetPos;
            m_wallPosPoint.position = new Vector2(targetPos.x, targetPos.y);
            m_wallPosPoint.localScale = m_character.facing == HorizontalDirection.Right? new Vector3(2, 2, 2) : new Vector3(-2, 2, 2);
            m_wallPosPoint.gameObject.SetActive(true);
            Vector2 fxPos = new Vector2(targetPos.x + (5f * transform.localScale.x), targetPos.y);
            SpawnFX(m_wallStickStartFX, fxPos);
            var loopFX = SpawnFX(m_wallStickLoopFX, fxPos);
            yield return new WaitForSeconds(0.5f);
            m_animation.SetAnimation(0, m_info.hookTravelLoopAnimation, true);
            float stopDistance = 23f;
            m_chainBashBB[1].enabled = true;
            while (Vector2.Distance(transform.position, m_wallPosPoint.position) > stopDistance)
            {
                m_movement.MoveTowards(Vector2.one * transform.localScale.x, m_info.shoulderBashReelSpeed);
                yield return null;
            }
            Destroy(loopFX.gameObject);
            m_wallPosPoint.gameObject.SetActive(false);
            m_chainBashBB[0].enabled = false;
            m_chainBashBB[1].enabled = false;
            var bashLoopAnim = m_animation.SetAnimation(0, m_info.chainBash1AnimationLoop, true);
            yield return new WaitForSeconds(m_info.ChainBashDuration);
            yield return new WaitForSpineAnimationComplete(bashLoopAnim);
            m_animation.SetAnimation(0, m_info.chainBash1AnimationEnd, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.chainBash1AnimationEnd);
            m_fistPoint.GetComponent<SkeletonUtilityBone>().mode = SkeletonUtilityBone.Mode.Follow;
            m_fistPoint.GetComponent<SkeletonUtilityBone>().enabled = false;
            m_wallPosPoint.localPosition = Vector2.zero;
            m_animation.SetAnimation(0, m_info.idleAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.idleAnimation);
            CustomTurn();
        }
        [SerializeField]
        private Transform m_arenaLeftSide;
        [SerializeField]
        private Transform m_arenaRightSide;
        private IEnumerator RunningAttack()
        {
            var runTowards = m_character.facing == HorizontalDirection.Left? m_arenaLeftSide : m_arenaRightSide;
            if(Vector2.Distance(transform.position, runTowards.position) < 10)
            {
                CustomTurn();
                runTowards = m_character.facing == HorizontalDirection.Left? m_arenaLeftSide : m_arenaRightSide;
            }
            m_runningAttackBB.enabled = true;
            m_animation.SetAnimation(0, m_info.runAttackStartAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.runAttackStartAnimation);
            m_animation.SetAnimation(0, m_info.runAttackAnimation, true);
            while(Vector2.Distance(transform.position, runTowards.position) > 15f)
            {
                m_movement.MoveTowards(new Vector2(runTowards.position.x - transform.position.x, 0f).normalized, m_info.move.speed * 4);
                yield return null;
            }
            m_animation.SetAnimation(0, m_info.runAttackEndAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.runAttackEndAnimation);
            m_runningAttackBB.enabled = false;
            if (m_phaseHandle.currentPhase == Phase.PhaseThree)
            {
                if (m_isBuffed)
                {
                    yield return ShockRampage();
                }
            }
            m_animation.SetAnimation(0, m_info.idle2Animation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.idle2Animation);
            if (!IsFacingTarget()) { CustomTurn(); }
            m_attackCounter++;
            yield return null;
        }
        private IEnumerator ShockRampage()
        {
            m_shockRampageBB.enabled = true;
            m_animation.SetAnimation(0, m_info.shockRampageAttack, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.shockRampageAttack);
            m_shockRampageBB.enabled = false;
            yield return null;
        }
        private IEnumerator ChainBash2()
        {
            m_animation.SetAnimation(0, m_info.chainBash1AnimationStart.animation, false);
            m_chainBashBB[0].enabled = true;
            yield return new WaitForSeconds(1f);
            RaycastHit2D hit = Physics2D.Raycast(m_chainBashStarting.position, Vector2.right * transform.localScale.x, 1000, DChildUtility.GetEnvironmentMask());

            if (!hit.collider)
            {
                yield break;
            }
            var fistBone = m_fistPoint.GetComponent<SkeletonUtilityBone>();
            fistBone.enabled = true;
            fistBone.mode = SkeletonUtilityBone.Mode.Override;
            Vector2 targetPos = hit.point;
            m_fistPoint.position = targetPos;
            m_wallPosPoint.position = new Vector2(targetPos.x, targetPos.y);
            m_wallPosPoint.localScale = m_character.facing == HorizontalDirection.Right ? new Vector3(2, 2, 2) : new Vector3(-2, 2, 2);
            m_wallPosPoint.gameObject.SetActive(true);
            Vector2 fxPos = new Vector2(targetPos.x + (5f * transform.localScale.x), targetPos.y);
            SpawnFX(m_wallStickStartFX, fxPos);
            var loopFX = SpawnFX(m_wallStickLoopFX, fxPos);
            yield return new WaitForSeconds(0.5f);
            m_animation.SetAnimation(0, m_info.hookTravelLoopAnimation, true);
            float stopDistance = 23f;
            m_chainBashBB[1].enabled = true;
            while (Vector2.Distance(transform.position, m_wallPosPoint.position) > stopDistance)
            {
                m_movement.MoveTowards(Vector2.one * transform.localScale.x, m_info.shoulderBashReelSpeed);
                yield return null;
            }
            if (m_character.facing == HorizontalDirection.Left)
            {
                ElectricPushLeft?.Invoke(this, EventActionArgs.Empty);
            }
            else
            {
                ElectricPushRight?.Invoke(this, EventActionArgs.Empty);
            }
            if (loopFX != null) loopFX.GetComponent<FX>().Stop();
            m_wallPosPoint.gameObject.SetActive(false);
            m_chainBashBB[0].enabled = false;
            m_chainBashBB[1].enabled = false;
            var bashLoopAnim = m_animation.SetAnimation(0, m_info.chainBash1AnimationLoop, true);
            yield return new WaitForSeconds(m_info.ChainBashDuration);
            yield return new WaitForSpineAnimationComplete(bashLoopAnim);
            m_animation.SetAnimation(0, m_info.chainBash1AnimationEnd, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.chainBash1AnimationEnd);
            m_fistPoint.GetComponent<SkeletonUtilityBone>().mode = SkeletonUtilityBone.Mode.Follow;
            m_fistPoint.GetComponent<SkeletonUtilityBone>().enabled = false;
            m_wallPosPoint.localPosition = Vector2.zero;
            m_animation.SetAnimation(0, m_info.idleAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.idleAnimation);
            CustomTurn();
            yield return null;
        }
        private IEnumerator ElectricStomp()
        {
            if (m_isBuffed)
            {
                yield return PunchCombo();
            }
            else
            {
                m_attackCounter++;
            }
            yield return null;
        }
        #endregion
        #region Patterns
        private IEnumerator Phase1Pattern1Routine()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            if(m_attackCounter >= 5)
            {
                yield return LeapAttack();
            }
            while(Vector2.Distance(transform.position, m_targetInfo.position) > 15f)
            {
                m_animation.SetAnimation(0, m_info.move.animation, true);
                m_movement.MoveTowards(new Vector2(m_targetInfo.position.x - transform.position.x, 0f).normalized, m_info.move.speed);
                if (!IsFacingTarget())
                {
                    CustomTurn();
                }
                yield return null;
            }
            m_movement.Stop();
            var random = UnityEngine.Random.RandomRange(0, 3);
            if(random == 0)
            {
                if (!IsFacingTarget()){ CustomTurn();}
                yield return PunchCombo();
            }
            else if(random == 2)
            {
                if (!IsFacingTarget()) { CustomTurn(); }
                yield return ChainFist();
            }
            else
            {
                if (!IsFacingTarget()) { CustomTurn(); }
                yield return ShoulderBash();
            }
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            DecidedOnAttack(false);
            m_animation.DisableRootMotion();
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }
        private IEnumerator Phase2Pattern1Routine()
        {
            yield return PhaseDistarge1(); 
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            DecidedOnAttack(false);
            m_animation.DisableRootMotion();
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }
        private IEnumerator Phase2Pattern2Routine()
        {
            if (m_attackCounter >= 10)
            {
                yield return PhaseDistarge1();
            }
            else
            {
                if (Vector2.Distance(transform.position, m_targetInfo.position) > 20f)
                {
                    var random = UnityEngine.Random.RandomRange(0, 2);
                    if (random == 0)
                    {
                        yield return RunningAttack();
                    }
                    else
                    {
                        yield return ChainBash();
                    }
                }
                else
                {
                    var random = UnityEngine.Random.RandomRange(0, 3);
                    if (random == 0)
                    {
                        yield return PunchCombo();
                    }
                    else if (random == 1)
                    {
                        yield return ChainFist();
                    }
                    else
                    {
                        yield return LeapAttack();
                    }
                }
                yield return null;
            }
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            DecidedOnAttack(false);
            m_animation.DisableRootMotion();
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }
        private IEnumerator Phase3Pattern1Routine()
        {
            yield return PhaseDistarge2();
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            DecidedOnAttack(false);
            m_animation.DisableRootMotion();
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }
        private IEnumerator Phase3Pattern2Routine()
        {
            if(m_attackCounter >= 12)
            {
                yield return PhaseDistarge2();
                yield return null;
            }
            else
            {
                if (Vector2.Distance(transform.position, m_targetInfo.position) > 20f)
                {
                    var random = UnityEngine.Random.RandomRange(0, 2);
                    if (random == 0)
                    {
                        yield return RunningAttack();
                    }
                    else
                    {
                        yield return ChainBash();
                    }
                }
                else
                {
                    var random = UnityEngine.Random.RandomRange(0, 3);
                    if (random == 0)
                    {
                        yield return PunchCombo();
                    }
                    else if (random == 1)
                    {
                        yield return ChainFist();
                    }
                    else
                    {
                        yield return LeapAttack();
                    }
                }
            }
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            DecidedOnAttack(false);
            m_animation.DisableRootMotion();
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }
        #endregion

        protected override void OnDestroyed(object sender, EventActionArgs eventArgs)
        {
            base.OnDestroyed(sender, eventArgs);
            StopAllCoroutines();
            m_bodyLightningFX.Stop();
            m_wallStickLoopFX.GetComponent<FX>().Stop();
            m_phase3FX.Stop();
            StartCoroutine(StickToGroundRoutine(GroundPosition().y));
            for (int i = 0; i < m_lightningBoltEffects.Count; i++)
            {
                Destroy(m_lightningBoltEffects[i]);
            }
            m_lightningBoltEffects.Clear();
            m_chainHurtBox.gameObject.SetActive(false);
            //m_deathFX.Play();
            m_movement.Stop();
            m_isDetecting = false;
        }
        private void DecidedOnAttack(bool condition)
        {
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
                    m_attackDecider.SetList(new AttackInfo<Attack>(Attack.Phase2Pattern1, m_info.phase2Pattern1Range),
                        (new AttackInfo<Attack>(Attack.Phase2Pattern2, m_info.phase2Pattern2Range)));
                    break;
                case Phase.PhaseThree:
                    m_attackDecider.SetList(new AttackInfo<Attack>(Attack.Phase3Pattern1, m_info.phase3Pattern1Range),
                        (new AttackInfo<Attack>(Attack.Phase3Pattern2, m_info.phase3Pattern2Range)));
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

        private Vector2 WallPosition()
        {
            var wristPoint = new Vector2(m_wristPoint.position.x, m_wristPoint.position.y + 2f);
            RaycastHit2D hit = Physics2D.Raycast(wristPoint, Vector2.right * transform.localScale.x, 1000, DChildUtility.GetEnvironmentMask());
            return hit.point;
        }

        private Vector2 GroundPosition()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1000, DChildUtility.GetEnvironmentMask());
            return hit.point;
        }

        protected override void Awake()
        {
            base.Awake();
            //m_turnHandle.TurnDone += OnTurnDone;
            m_deathHandle.SetAnimation(m_info.deathAnimation.animation);
            m_projectileLauncher = new ProjectileLauncher(m_info.stompProjectile.projectileInfo, m_projectilePoint);
            //m_patternDecider = new RandomAttackDecider<Pattern>();
            m_lightningBoltEffects = new List<GameObject>();
            m_attackDecider = new RandomAttackDecider<Attack>();
            m_stateHandle = new StateHandle<State>(State.Idle, State.WaitBehaviourEnd);
            UpdateAttackDeciderList();
        }

        private void PhaseFX()
        {
            //m_aoeBB.enabled = true;
            m_orbLightningFX.Play();
            m_bodyLightningFX.Play();
            if (m_currentPhaseIndex == 3)
            {
                m_phase3FX.Play();
            }
        }

        private void PhaseFXStop()
        {

            //m_aoeBB.enabled = false;
            m_orbLightningFX.Stop();
        }

        private void LeapEvent()
        {
            if (!IsFacingTarget()) { CustomTurn(); }
            var fxPool = GameSystem.poolManager.GetPool<FXPool>().GetOrCreateItem(m_leapFX);
            fxPool.Play();
            m_leapAttackBB.enabled = true;
            fxPool.transform.position = new Vector2(transform.position.x + (23f * transform.localScale.x), transform.position.y - 1.5f);
        }
        [SerializeField]
        private int m_attackCounter;
        private bool m_hitByPunchCombo;
        private void PlayerDamagedPunchCombo(object sender, Damageable.DamageEventArgs eventArgs)
        {
            m_hitByPunchCombo = true;
            m_attackCounter++;
        }
        protected override void Start()
        {
            //base.Start();
            m_spineListener.Subscribe(m_info.phaseEvent, PhaseFX);
            m_spineListener.Subscribe(m_info.leapEvent, LeapEvent);
            m_spineListener.Subscribe(m_info.stopRoarEvent, PhaseFXStop);
            m_spineListener.Subscribe(m_info.stompEvent, LaunchProjectile);
            m_spineListener.Subscribe(m_info.aimChainBash, AimChainBash);
            m_animation.DisableRootMotion();
            m_fistRefPoint.GetComponent<CircleCollider2D>().enabled = false;
            m_phaseHandle = new PhaseHandle<Phase, PhaseInfo>();
            m_phaseHandle.Initialize(Phase.PhaseOne, m_info.phaseInfo, m_character, ChangeState, ApplyPhaseData);
            m_phaseHandle.ApplyChange();
        }

        private void Update()
        {
            if (m_isBuffed)
            {
                //enable arm glow and fx
            }
            m_phaseHandle.MonitorPhase();
            switch (m_stateHandle.currentState)
            {
                case State.Idle:
                    m_animation.SetAnimation(0, m_info.idleAnimation, true);
                    break;
                case State.Intro:
                    if (!IsFacingTarget()) { CustomTurn(); }
                    StartCoroutine(IntroRoutine());
                    break;
                case State.Phasing:
                    StopAllCoroutines();
                    StartCoroutine(ChangePhaseRoutine());
                    break;
                case State.Turning:
                    Debug.Log("Turning");
                    m_phaseHandle.allowPhaseChange = false;
                    m_stateHandle.Wait(m_turnState);
                    m_turnHandle.Execute(m_info.turnAnimation.animation, m_info.idleAnimation.animation);

                    m_movement.Stop();
                    break;
                case State.Attacking:
                    m_hitbox.SetInvulnerability(Invulnerability.None);
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
                        case Attack.Phase2Pattern2:
                            StartCoroutine(Phase2Pattern2Routine());
                            break;
                        case Attack.Phase3Pattern1:
                            StartCoroutine(Phase3Pattern1Routine());
                            break;
                        case Attack.Phase3Pattern2:
                            StartCoroutine(Phase3Pattern2Routine());
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
            //m_currentCD = 0;
        }

        public override void ReturnToSpawnPoint()
        {
        }

        protected override void OnForbidFromAttackTarget()
        {
        }
    }
}