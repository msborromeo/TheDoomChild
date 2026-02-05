using DChild.Gameplay.Characters.AI;
using DChild.Gameplay.Combat;
using DChild.Gameplay.Pooling;
using DChild.Gameplay.Projectiles;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
    using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using Random = UnityEngine.Random;

namespace DChild.Gameplay.Characters.Enemies
{
    [AddComponentMenu("DChild/Gameplay/Enemies/Boss/TheOneThirdForm")]
    public class TheOneThirdFormAI : CombatAIBrain<TheOneThirdFormAI.Info>
    {
        [System.Serializable]
        public class Info : BaseInfo
        {
            [TitleGroup("Phase Info")]

            [SerializeField]
            private PhaseInfo<Phase> m_phaseInfo;
            public PhaseInfo<Phase> phaseInfo => m_phaseInfo;
            [SerializeField]
            private PhaseInfo<Phase> m_phaseInfo_2;
            public PhaseInfo<Phase> phaseInfo_2 => m_phaseInfo_2;

            [SerializeField]
            private MovementInfo m_moveSideways = new MovementInfo();
            public MovementInfo moveSideways => m_moveSideways;

            [TitleGroup("Pattern Ranges")]
            [SerializeField]
            private float m_targetDistanceTolerance;
            public float targetDistanceTolerance => m_targetDistanceTolerance;
            [SerializeField, BoxGroup("Phase 1")]
            private float m_phase1Pattern1Range;
            public float phase1Pattern1Range => m_phase1Pattern1Range;
            [SerializeField, BoxGroup("Phase 1")]
            private float m_phase1Pattern2Range;
            public float phase1Pattern2Range => m_phase1Pattern2Range;
            [SerializeField, BoxGroup("Phase 1")]
            private float m_phase1Pattern3Range;
            public float phase1Pattern3Range => m_phase1Pattern3Range;
            [SerializeField, BoxGroup("Phase 1")]
            private float m_phase1Pattern4Range;
            public float phase1Pattern4Range => m_phase1Pattern4Range;
            [SerializeField, BoxGroup("Phase 1")]
            private float m_phase1Pattern5Range;
            public float phase1Pattern5Range => m_phase1Pattern5Range;
            [SerializeField, BoxGroup("Phase 1")]
            private float m_phase1Pattern6Range;
            public float phase1Pattern6Range => m_phase1Pattern6Range;
            [SerializeField, BoxGroup("Phase 2")]
            private float m_phase2Pattern1Range;
            public float phase2Pattern1Range => m_phase2Pattern1Range;
            [SerializeField, BoxGroup("Phase 2")]
            private float m_phase2Pattern2Range;
            public float phase2Pattern2Range => m_phase2Pattern2Range;
            [SerializeField, BoxGroup("Phase 2")]
            private float m_phase2Pattern3Range;
            public float phase2Pattern3Range => m_phase2Pattern3Range;
            [SerializeField, BoxGroup("Phase 2")]
            private float m_phase2Pattern4Range;
            public float phase2Pattern4Range => m_phase2Pattern4Range;
            [SerializeField, BoxGroup("Phase 2")]
            private float m_phase2Pattern5Range;
            public float phase2Pattern5Range => m_phase2Pattern5Range;
            [SerializeField, BoxGroup("Phase 2")]
            private float m_phase2Pattern6Range;
            public float phase2Pattern6Range => m_phase2Pattern6Range;
            [SerializeField, BoxGroup("Phase 2")]
            private float m_phase2Pattern7Range;
            public float phase2Pattern7Range => m_phase2Pattern7Range;
            [SerializeField, BoxGroup("Phase 3")]
            private float m_phase3Pattern1Range;
            public float phase3Pattern1Range => m_phase3Pattern1Range;
            [SerializeField, BoxGroup("Phase 3")]
            private float m_phase3Pattern2Range;
            public float phase3Pattern2Range => m_phase3Pattern2Range;
            [SerializeField, BoxGroup("Phase 3")]
            private float m_phase3Pattern3Range;
            public float phase3Pattern3Range => m_phase3Pattern3Range;
            [SerializeField, BoxGroup("Phase 3")]
            private float m_phase3Pattern4Range;
            public float phase3Pattern4Range => m_phase3Pattern4Range;
            [SerializeField, BoxGroup("Phase 3")]
            private float m_phase3Pattern5Range;
            public float phase3Pattern5Range => m_phase3Pattern5Range;
            [SerializeField, BoxGroup("Phase 3")]
            private float m_phase3Pattern6Range;
            public float phase3Pattern6Range => m_phase3Pattern6Range;
            [SerializeField, BoxGroup("Phase 3")]
            private float m_phase3Pattern7Range;
            public float phase3Pattern7Range => m_phase3Pattern7Range;
            [SerializeField, BoxGroup("Phase 4")]
            private float m_phase4Pattern1Range;
            public float phase4Pattern1Range => m_phase4Pattern1Range;
            [SerializeField, BoxGroup("Phase 4")]
            private float m_phase4Pattern2Range;
            public float phase4Pattern2Range => m_phase4Pattern2Range;
            [SerializeField, BoxGroup("Phase 4")]
            private float m_phase4Pattern3Range;
            public float phase4Pattern3Range => m_phase4Pattern3Range;
            [SerializeField, BoxGroup("Phase 4")]
            private float m_phase4Pattern4Range;
            public float phase4Pattern4Range => m_phase4Pattern4Range;
            [SerializeField, BoxGroup("Phase 4")]
            private float m_phase4Pattern5Range;
            public float phase4Pattern5Range => m_phase4Pattern5Range;
            [SerializeField, BoxGroup("Phase 4")]
            private float m_phase4Pattern6Range;
            public float phase4Pattern6Range => m_phase4Pattern6Range;
            [SerializeField, BoxGroup("Phase 4")]
            private float m_phase4Pattern7Range;
            public float phase4Pattern7Range => m_phase4Pattern7Range;
            [SerializeField, BoxGroup("Phase 4")]
            private float m_phase4Pattern8Range;
            public float phase4Pattern8Range => m_phase4Pattern8Range;
            [SerializeField, BoxGroup("Phase 4")]
            private float m_phase4Pattern9Range;
            public float phase4Pattern9Range => m_phase4Pattern9Range;
            [SerializeField, BoxGroup("Phase 4")]
            private float m_phase4Pattern10Range;
            public float phase4Pattern10Range => m_phase4Pattern10Range;
            [SerializeField, BoxGroup("Phase 4")]
            private float m_phase4Pattern11Range;
            public float phase4Pattern11Range => m_phase4Pattern11Range;
            [SerializeField, BoxGroup("Phase 5")]
            private float m_phase5Pattern1Range;
            public float phase5Pattern1Range => m_phase5Pattern1Range;
            [SerializeField, BoxGroup("Phase 5")]
            private float m_phase5Pattern2Range;
            public float phase5Pattern2Range => m_phase5Pattern2Range;
            [SerializeField, BoxGroup("Phase 5")]
            private float m_phase5Pattern3Range;
            public float phase5Pattern3Range => m_phase5Pattern3Range;
            [SerializeField, BoxGroup("Phase 5")]
            private float m_phase5Pattern4Range;
            public float phase5Pattern4Range => m_phase5Pattern4Range;
            [SerializeField, BoxGroup("Phase 5")]
            private float m_phase5Pattern5Range;
            public float phase5Pattern5Range => m_phase5Pattern5Range;
            [SerializeField, BoxGroup("Phase 5")]
            private float m_phase5Pattern6Range;
            public float phase5Pattern6Range => m_phase5Pattern6Range;
            [SerializeField, BoxGroup("Phase 5")]
            private float m_phase5Pattern7Range;
            public float phase5Pattern7Range => m_phase5Pattern7Range;
            [SerializeField, BoxGroup("Phase 5")]
            private float m_phase5Pattern8Range;
            public float phase5Pattern8Range => m_phase5Pattern8Range;
            [SerializeField, BoxGroup("Phase 5")]
            private float m_phase5Pattern9Range;
            public float phase5Pattern9Range => m_phase5Pattern9Range;
            [SerializeField, BoxGroup("Phase 5")]
            private float m_phase5Pattern10Range;
            public float phase5Pattern10Range => m_phase5Pattern10Range;
            [SerializeField, BoxGroup("Phase 5")]
            private float m_phase5Pattern11Range;
            public float phase5Pattern11Range => m_phase5Pattern11Range;

            [TitleGroup("Attack Pattern Cooldown States")]
            [SerializeField, MinValue(0)]
            private List<float> m_phase1PatternCooldown;
            public List<float> phase1PatternCooldown => m_phase1PatternCooldown;
            [SerializeField, MinValue(0)]
            private List<float> m_phase2PatternCooldown;
            public List<float> phase2PatternCooldown => m_phase2PatternCooldown;
            [SerializeField, MinValue(0)]
            private List<float> m_phase3PatternCooldown;
            public List<float> phase3PatternCooldown => m_phase3PatternCooldown;
            [SerializeField, MinValue(0)]
            private List<float> m_phase4PatternCooldown;
            public List<float> phase4PatternCooldown => m_phase4PatternCooldown;
            [SerializeField, MinValue(0)]
            private List<float> m_phase5PatternCooldown;
            public List<float> phase5PatternCooldown => m_phase5PatternCooldown;

            #region Animation
            [TitleGroup("Animations")]
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_blinkAnimation;
            public string blinkAnimation => m_blinkAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_exhaustedAnimation;
            public string exhaustedAnimation => m_exhaustedAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_exhaustedToIdleAnimation;
            public string exhaustedToIdleAnimation => m_exhaustedToIdleAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_idleToExhausted;
            public string idleToExhausted => m_idleToExhausted;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_introAnimation;
            public string introAnimation => m_introAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_introAnimation2;
            public string introAnimation2 => m_introAnimation2;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_introIdleAnimation;
            public string introIdleAnimation => m_introIdleAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_miniEyeIdle;
            public string miniEyeIdle => m_miniEyeIdle;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_rageQuake;
            public string rageQuake => m_rageQuake;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_eyeSquintAnimation;
            public string eyeSquintAnimation => m_eyeSquintAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_eyeSquintLoop;
            public string eyeSquintLoop => m_eyeSquintLoop;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_unsquintAnimation;
            public string unsquintAnimation => m_unsquintAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_blackHoleMouth;
            public string blackHoleMouth => m_blackHoleMouth;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_idleAnimation;
            public string idleAnimation => m_idleAnimation;
            [SerializeField, BoxGroup("Projectile")]
            private ProjectileInfo m_sphereBomb;
            public ProjectileInfo sphereBomb => m_sphereBomb;
            #endregion

            public override void Initialize()
            {

            }
        }

        [System.Serializable]
        public class PhaseInfo : IPhaseInfo
        {
            [SerializeField]
            private List<float> m_fullCooldown;
            public List<float> fullCooldown => m_fullCooldown;
            //[SerializeField]
            //private List<float> m_patternCooldown;
            //public List<float> patternCooldown => m_patternCooldown;
        }

        private enum State
        {
            Phasing,
            Intro,
            Idle,
            Attacking,
            Cooldown,
            Chasing,
            ReevaluateSituation,
            WaitBehaviourEnd,
        }

        private enum Attack
        {

            #region OldAttackPatternsThatAreNotFuckingClear
            //Phase1Pattern1,
            //Phase1Pattern2,
            //Phase1Pattern3,
            //Phase1Pattern4,
            //Phase1Pattern5,
            //Phase1Pattern6,
            //Phase2Pattern1,
            //Phase2Pattern2,
            //Phase2Pattern3,
            //Phase2Pattern4,
            //Phase2Pattern5,
            //Phase2Pattern6,
            //Phase2Pattern7,
            //Phase3Pattern1,
            //Phase3Pattern2,
            //Phase3Pattern3,
            //Phase3Pattern4,
            //Phase3Pattern5,
            //Phase3Pattern6,
            //Phase3Pattern7,
            //Phase4Pattern1,
            //Phase4Pattern2,
            //Phase4Pattern3,
            //Phase4Pattern4,
            //Phase4Pattern5,
            //Phase4Pattern6,
            //Phase4Pattern7,
            //Phase4Pattern8,
            //Phase4Pattern9,
            //Phase4Pattern10,
            //Phase4Pattern11,
            //Phase5Pattern1,
            //Phase5Pattern2,
            //Phase5Pattern3,
            //Phase5Pattern4,
            //Phase5Pattern5,
            //Phase5Pattern6,
            //Phase5Pattern7,
            //Phase5Pattern8,
            //Phase5Pattern9,
            //Phase5Pattern10,
            //Phase5Pattern11,
            #endregion
            TentacleGroundStab1,
            TentacleGroundStab2,
            TentacleBlast1,
            MonolithSlamPhase1,
            BubbleImprisonment,
            //end of phase one 

            //TentacleGroundStab2
            TentacleGroundStab1AndCeiling,
            ChasingGroundBlast,
            TentacleBlast2,
            MonolithSlamPhase2,
            MouthBlast2,
            //end of phase two

            //TentacleGroundStab2
            TentacleStab1AndCeilingPhase3,
            ChasingGroundBlastPhaseTree,
            //TentacleBlast2,
            MonolithSlamPhase3,
            //MouthBlast2
            GrabberSwipeAndWallSlam,
            SlidingStoneWall,
            //end of phase three

            ChasingGroundBlastAndMouthBlast2,
            TentacleBlast2PhaseFour,
            MouthBlastCeiling1,
            SphereBomb,
            BubbleImprisonmentPhaseFour,
            //GrabberSwipeAndWallSlam,
            //SlidingStoneWall,
            //ChasingGroundBlast,
            //end of phase four

            ChasingGroundBlastMouthBlast2AndMouthBlast1,
            //MouthBlastCeiling1,
            MouthBlast1And2,
            SphereBomb1,
            SphereBomb2,
            //BubbleImprisonment,



            WaitAttackEnd,
        }

        public enum Phase
        {
            PhaseOne,
            PhaseTwo,
            PhaseThree,
            PhaseFour,
            PhaseFive,
            Wait,
        }
        [SerializeField, TabGroup("Reference")]
        private Boss m_boss;
        [SerializeField, TabGroup("Sphere Bombs")]
        private Transform[] m_projectilePoint;
        [SerializeField, TabGroup("Reference")]
        private Hitbox m_hitbox;
        [SerializeField, TabGroup("Reference")]
        private GameObject m_model;
        [SerializeField, TabGroup("Reference")]
        private ObstacleChecker m_obstacleChecker;
        [SerializeField, TabGroup("Reference")]
        private GameObject m_theOneHitbox;
        [TabGroup("Reference")]
        public bool m_isPlayerBackArena = false;
        [TabGroup("Reference")]
        private bool m_cutsceneTriggersForPhaseTwo = false;
        [SerializeField, TabGroup("Reference")]
        private GameObject m_theOneMiniBlackHole;
        [SerializeField, TabGroup("Reference")]
        private ParticleSystem m_theOneMiniBlackHoleVFX;
        [SerializeField, TabGroup("AttackItemBinds")]
        private MouthBlastIIAttack[] m_mouthBlastTwo;
        [SerializeField, TabGroup("AttackItemBinds")]
        private TentacleBlast[] m_tentacleBlast;
        [SerializeField, TabGroup("AttackItemBinds")]
        private ChasingGroundTentacleAttack m_chasingGroundAttack;
        [SerializeField, TabGroup("Eye")]
        private Vector2 m_eyeCenter;
        [SerializeField, TabGroup("Eye")]
        private float m_maxDistance;
        [SerializeField, TabGroup("Eye")]
        private Transform m_eyeTheOne;
        [SerializeField, TabGroup("Eye")]
        private GameObject m_eyeSquint;
        [SerializeField, TabGroup("Eye")]
        private GameObject m_eyeExhausted;
        [SerializeField, TabGroup("Eye")]
        private GameObject m_eyeOpen;
        [SerializeField, TabGroup("Eye")]
        private int m_hitCounterPhaseOne;
        [SerializeField, TabGroup("Eye")]
        private int m_hitCounterPhaseTwo;
        [SerializeField, TabGroup("Eye")]
        private int m_hitCounterPhaseThree;
        [SerializeField, TabGroup("Eye")]
        private int m_hitCounterPhaseFour;
        [SerializeField, TabGroup("Eye")]
        private int m_hitCounterPhaseFive;    
        [SerializeField, TabGroup("Eye")]
        private float m_eyeTimerToOpenFromSquint;
        [ReadOnly, SerializeField, TabGroup("Eye")]
        private int m_hitCounterChangeable;
        [SerializeField, ReadOnly, TabGroup("Eye")]
        private int m_hitCounter;
        [ReadOnly, SerializeField, TabGroup("Eye")]
        private float m_storeMaxDistance;
        [SerializeField, TabGroup("Cinematics")]
        private PlayableDirector m_inwardBlackHole;
        [SerializeField, TabGroup("Cinematics")]
        private PlayableDirector m_outwardBlackHole;
        [TabGroup("Sphere Bombs")]
        public List<Projectile> m_sphereBombList;
        [SerializeField]
        private SpineEventListener m_spineListener;

        [ShowInInspector]
        private StateHandle<State> m_stateHandle;
        [ShowInInspector]
        private PhaseHandle<Phase, PhaseInfo> m_phaseHandle;

        public PhaseHandle<Phase, PhaseInfo> phaseHandle => m_phaseHandle;

        [ShowInInspector]
        private RandomAttackDecider<Attack> m_attackDecider;
        private Attack m_currentAttack;
        private float m_currentAttackRange;

        private bool[] m_attackUsed;
        private List<Attack> m_attackCache;
        private List<float> m_attackRangeCache;

        private List<float> m_currentFullCooldown;
        private List<float> m_patternCooldown;

        private Vector2 m_lastTargetPos;
        private float m_currentCooldown;
        private float m_pickedCooldown;



        #region Behavior Coroutines
        private Coroutine m_changePhaseCoroutine;
        private Coroutine m_currentAttackCoroutine;
        #endregion

        [SerializeField]
        private bool m_areMonolithsSpawned = false;
        private bool m_areTentacleWallsPresent = false;
        private bool m_isBlackBloodFloodPresent = false;
        [SerializeField]
        private bool m_areObstaclesPresent = false;

        public event EventAction<EventActionArgs> AttackDone;
        public event EventAction<EventActionArgs> ObstaclesAdded;
        public event EventAction<EventActionArgs> ObstaclesCleared;

        public event EventAction<EventActionArgs> LockPlayerQuickItem;
        public event EventAction<EventActionArgs> UnLockPlayerQuickItem;
        [SerializeField]
        private bool m_removeTentacleBlastAttacks;

        private ProjectileLauncher m_projectileLauncher;
        private void UpdateAttackDeciderListTentacleBlast()
        {
            Debug.Log("decider list two");
            //m_attackDecider.SetList(new AttackInfo<Attack>(Attack.TentacleGroundStab1, m_info.phase1Pattern1Range),
            //                        new AttackInfo<Attack>(Attack.TentacleGroundStab2, m_info.phase1Pattern1Range));
            switch (m_phaseHandle.currentPhase)
            {
                case Phase.PhaseOne:
                    m_attackDecider.SetList(new AttackInfo<Attack>(Attack.TentacleGroundStab1, m_info.phase1Pattern1Range),
                                            new AttackInfo<Attack>(Attack.TentacleGroundStab2, m_info.phase1Pattern1Range),
                                            // new AttackInfo<Attack>(Attack.TentacleBlast1,m_info.phase1Pattern1Range),
                                            new AttackInfo<Attack>(Attack.MonolithSlamPhase1, m_info.phase1Pattern1Range),
                                            new AttackInfo<Attack>(Attack.BubbleImprisonment, m_info.phase1Pattern1Range));
                    break;
                case Phase.PhaseTwo:
                    m_attackDecider.SetList(new AttackInfo<Attack>(Attack.TentacleGroundStab2, m_info.phase1Pattern1Range),
                                            new AttackInfo<Attack>(Attack.TentacleGroundStab1AndCeiling, m_info.phase1Pattern1Range),
                                            new AttackInfo<Attack>(Attack.ChasingGroundBlast, m_info.phase1Pattern1Range),
                                            //new AttackInfo<Attack>(Attack.TentacleBlast2, m_info.phase1Pattern1Range),
                                            new AttackInfo<Attack>(Attack.MonolithSlamPhase2, m_info.phase1Pattern1Range),
                                            new AttackInfo<Attack>(Attack.MouthBlast2, m_info.phase1Pattern1Range));
                    break;
                case Phase.PhaseThree:
                    m_attackDecider.SetList(new AttackInfo<Attack>(Attack.TentacleGroundStab2, m_info.phase1Pattern1Range),
                                         new AttackInfo<Attack>(Attack.TentacleStab1AndCeilingPhase3, m_info.phase1Pattern1Range),
                                         new AttackInfo<Attack>(Attack.ChasingGroundBlastPhaseTree, m_info.phase1Pattern1Range),
                                         //new AttackInfo<Attack>(Attack.TentacleBlast2, m_info.phase1Pattern1Range),
                                         new AttackInfo<Attack>(Attack.MonolithSlamPhase3, m_info.phase1Pattern1Range),
                                         new AttackInfo<Attack>(Attack.MouthBlast2, m_info.phase1Pattern1Range),
                                         new AttackInfo<Attack>(Attack.GrabberSwipeAndWallSlam, m_info.phase1Pattern1Range),
                                         new AttackInfo<Attack>(Attack.SlidingStoneWall, m_info.phase1Pattern1Range));
                    break;
                case Phase.PhaseFour:
                    m_attackDecider.SetList(new AttackInfo<Attack>(Attack.ChasingGroundBlastAndMouthBlast2, m_info.phase1Pattern1Range),
                                        //new AttackInfo<Attack>(Attack.TentacleBlast2PhaseFour, m_info.phase1Pattern1Range),
                                        new AttackInfo<Attack>(Attack.MouthBlastCeiling1, m_info.phase1Pattern1Range),
                                        new AttackInfo<Attack>(Attack.SphereBomb1, m_info.phase1Pattern1Range),
                                        new AttackInfo<Attack>(Attack.MonolithSlamPhase3, m_info.phase1Pattern1Range),
                                        new AttackInfo<Attack>(Attack.BubbleImprisonment, m_info.phase1Pattern1Range),
                                        new AttackInfo<Attack>(Attack.SlidingStoneWall, m_info.phase1Pattern1Range));
                    break;
                case Phase.PhaseFive:
                    m_attackDecider.SetList(new AttackInfo<Attack>(Attack.ChasingGroundBlastMouthBlast2AndMouthBlast1, m_info.phase1Pattern1Range),
                                       new AttackInfo<Attack>(Attack.MouthBlastCeiling1, m_info.phase1Pattern1Range),
                                       new AttackInfo<Attack>(Attack.MouthBlast1And2, m_info.phase1Pattern1Range),
                                       new AttackInfo<Attack>(Attack.SphereBomb1, m_info.phase1Pattern1Range),
                                       new AttackInfo<Attack>(Attack.SphereBomb2, m_info.phase1Pattern1Range),
                                       new AttackInfo<Attack>(Attack.GrabberSwipeAndWallSlam, m_info.phase1Pattern1Range),
                                       new AttackInfo<Attack>(Attack.BubbleImprisonment, m_info.phase1Pattern1Range));
                    break;
            }
            // m_attackDecider.hasDecidedOnAttack = false;
            m_removeTentacleBlastAttacks = false;
        }
        private void UpdateAttackDeciderList()
        {
            Debug.Log("Decider list one");
            #region OldAttackDecider.SetList
            /*  m_attackDecider.SetList(new AttackInfo<Attack>(Attack.Phase1Pattern1, m_info.phase1Pattern1Range)
                                    , new AttackInfo<Attack>(Attack.Phase1Pattern2, m_info.phase1Pattern2Range)
                                    , new AttackInfo<Attack>(Attack.Phase1Pattern3, m_info.phase1Pattern3Range)
                                    , new AttackInfo<Attack>(Attack.Phase1Pattern4, m_info.phase1Pattern4Range)
                                    , new AttackInfo<Attack>(Attack.Phase1Pattern5, m_info.phase1Pattern5Range)
                                    , new AttackInfo<Attack>(Attack.Phase1Pattern6, m_info.phase1Pattern6Range)
                                    , new AttackInfo<Attack>(Attack.Phase2Pattern1, m_info.phase2Pattern1Range)
                                    , new AttackInfo<Attack>(Attack.Phase2Pattern2, m_info.phase2Pattern2Range)
                                    , new AttackInfo<Attack>(Attack.Phase2Pattern3, m_info.phase2Pattern3Range)
                                    , new AttackInfo<Attack>(Attack.Phase2Pattern4, m_info.phase2Pattern4Range)
                                    , new AttackInfo<Attack>(Attack.Phase2Pattern5, m_info.phase2Pattern5Range)
                                    , new AttackInfo<Attack>(Attack.Phase2Pattern6, m_info.phase2Pattern6Range)
                                    , new AttackInfo<Attack>(Attack.Phase2Pattern7, m_info.phase2Pattern7Range)
                                    , new AttackInfo<Attack>(Attack.Phase3Pattern1, m_info.phase3Pattern1Range)
                                    , new AttackInfo<Attack>(Attack.Phase3Pattern2, m_info.phase3Pattern2Range)
                                    , new AttackInfo<Attack>(Attack.Phase3Pattern3, m_info.phase3Pattern3Range)
                                    , new AttackInfo<Attack>(Attack.Phase3Pattern4, m_info.phase3Pattern4Range)
                                    , new AttackInfo<Attack>(Attack.Phase3Pattern5, m_info.phase3Pattern5Range)
                                    , new AttackInfo<Attack>(Attack.Phase3Pattern6, m_info.phase3Pattern6Range)
                                    , new AttackInfo<Attack>(Attack.Phase3Pattern7, m_info.phase3Pattern7Range)
                                    , new AttackInfo<Attack>(Attack.Phase4Pattern1, m_info.phase4Pattern1Range)
                                    , new AttackInfo<Attack>(Attack.Phase4Pattern2, m_info.phase4Pattern2Range)
                                    , new AttackInfo<Attack>(Attack.Phase4Pattern3, m_info.phase4Pattern3Range)
                                    , new AttackInfo<Attack>(Attack.Phase4Pattern4, m_info.phase4Pattern4Range)
                                    , new AttackInfo<Attack>(Attack.Phase4Pattern5, m_info.phase4Pattern5Range)
                                    , new AttackInfo<Attack>(Attack.Phase4Pattern6, m_info.phase4Pattern6Range)
                                    , new AttackInfo<Attack>(Attack.Phase4Pattern7, m_info.phase4Pattern7Range)
                                    , new AttackInfo<Attack>(Attack.Phase4Pattern8, m_info.phase4Pattern8Range)
                                    , new AttackInfo<Attack>(Attack.Phase4Pattern9, m_info.phase4Pattern9Range)
                                    , new AttackInfo<Attack>(Attack.Phase4Pattern10, m_info.phase4Pattern10Range)
                                    , new AttackInfo<Attack>(Attack.Phase4Pattern11, m_info.phase4Pattern11Range)
                                    , new AttackInfo<Attack>(Attack.Phase5Pattern1, m_info.phase5Pattern1Range)
                                    , new AttackInfo<Attack>(Attack.Phase5Pattern2, m_info.phase5Pattern2Range)
                                    , new AttackInfo<Attack>(Attack.Phase5Pattern3, m_info.phase5Pattern3Range)
                                    , new AttackInfo<Attack>(Attack.Phase5Pattern4, m_info.phase5Pattern4Range)
                                    , new AttackInfo<Attack>(Attack.Phase5Pattern5, m_info.phase5Pattern5Range)
                                    , new AttackInfo<Attack>(Attack.Phase5Pattern6, m_info.phase5Pattern6Range)
                                    , new AttackInfo<Attack>(Attack.Phase5Pattern7, m_info.phase5Pattern7Range)
                                    , new AttackInfo<Attack>(Attack.Phase5Pattern8, m_info.phase5Pattern8Range)
                                    , new AttackInfo<Attack>(Attack.Phase5Pattern9, m_info.phase5Pattern9Range)
                                    , new AttackInfo<Attack>(Attack.Phase5Pattern10, m_info.phase5Pattern10Range)
                                    , new AttackInfo<Attack>(Attack.Phase5Pattern11, m_info.phase5Pattern11Range));*/
            #endregion
            //m_attackDecider.SetList(new AttackInfo<Attack>(Attack.SphereBomb2, m_info.phase1Pattern1Range));
            /*new AttackInfo<Attack>(Attack.TentacleGroundStab2, m_info.phase1Pattern1Range));*/
            switch (m_phaseHandle.currentPhase)
            {
                case Phase.PhaseOne:
                    m_attackDecider.SetList(new AttackInfo<Attack>(Attack.TentacleGroundStab1, m_info.phase1Pattern1Range),
                                            new AttackInfo<Attack>(Attack.TentacleGroundStab2, m_info.phase1Pattern1Range),
                                            new AttackInfo<Attack>(Attack.TentacleBlast1, m_info.phase1Pattern1Range),
                                            new AttackInfo<Attack>(Attack.MonolithSlamPhase1, m_info.phase1Pattern1Range),
                                            new AttackInfo<Attack>(Attack.BubbleImprisonment, m_info.phase1Pattern1Range));
                    break;
                case Phase.PhaseTwo:
                    m_attackDecider.SetList(new AttackInfo<Attack>(Attack.TentacleGroundStab2, m_info.phase1Pattern1Range),
                                            new AttackInfo<Attack>(Attack.TentacleGroundStab1AndCeiling, m_info.phase1Pattern1Range),
                                            new AttackInfo<Attack>(Attack.ChasingGroundBlast, m_info.phase1Pattern1Range),
                                            new AttackInfo<Attack>(Attack.TentacleBlast2, m_info.phase1Pattern1Range),
                                            new AttackInfo<Attack>(Attack.MonolithSlamPhase2, m_info.phase1Pattern1Range),
                                            new AttackInfo<Attack>(Attack.MouthBlast2, m_info.phase1Pattern1Range));
                    break;
                case Phase.PhaseThree:
                    m_attackDecider.SetList(new AttackInfo<Attack>(Attack.TentacleGroundStab2, m_info.phase1Pattern1Range),
                                         new AttackInfo<Attack>(Attack.TentacleStab1AndCeilingPhase3, m_info.phase1Pattern1Range),
                                         new AttackInfo<Attack>(Attack.ChasingGroundBlastPhaseTree, m_info.phase1Pattern1Range),
                                         new AttackInfo<Attack>(Attack.TentacleBlast2, m_info.phase1Pattern1Range),
                                         new AttackInfo<Attack>(Attack.MonolithSlamPhase3, m_info.phase1Pattern1Range),
                                         new AttackInfo<Attack>(Attack.MouthBlast2, m_info.phase1Pattern1Range),
                                         new AttackInfo<Attack>(Attack.GrabberSwipeAndWallSlam, m_info.phase1Pattern1Range),
                                         new AttackInfo<Attack>(Attack.SlidingStoneWall, m_info.phase1Pattern1Range));
                    break;
                case Phase.PhaseFour:
                    m_attackDecider.SetList(new AttackInfo<Attack>(Attack.ChasingGroundBlastAndMouthBlast2, m_info.phase1Pattern1Range),
                                        new AttackInfo<Attack>(Attack.TentacleBlast2, m_info.phase1Pattern1Range),
                                        new AttackInfo<Attack>(Attack.MouthBlastCeiling1, m_info.phase1Pattern1Range),
                                        new AttackInfo<Attack>(Attack.SphereBomb, m_info.phase1Pattern1Range),
                                        new AttackInfo<Attack>(Attack.MonolithSlamPhase3, m_info.phase1Pattern1Range),
                                        new AttackInfo<Attack>(Attack.BubbleImprisonment, m_info.phase1Pattern1Range),
                                        new AttackInfo<Attack>(Attack.SlidingStoneWall, m_info.phase1Pattern1Range));
                    break;
                case Phase.PhaseFive:
                    m_attackDecider.SetList(new AttackInfo<Attack>(Attack.ChasingGroundBlastMouthBlast2AndMouthBlast1, m_info.phase1Pattern1Range),
                                       new AttackInfo<Attack>(Attack.MouthBlastCeiling1, m_info.phase1Pattern1Range),
                                       new AttackInfo<Attack>(Attack.MouthBlast1And2, m_info.phase1Pattern1Range),
                                       new AttackInfo<Attack>(Attack.SphereBomb1, m_info.phase1Pattern1Range),
                                       new AttackInfo<Attack>(Attack.SphereBomb2, m_info.phase1Pattern1Range),
                                       new AttackInfo<Attack>(Attack.GrabberSwipeAndWallSlam, m_info.phase1Pattern1Range),
                                       new AttackInfo<Attack>(Attack.BubbleImprisonment, m_info.phase1Pattern1Range));
                    break;
            }
            //m_attackDecider.hasDecidedOnAttack = false;
        }

        private IEnumerator IntroRoutine()
        {
            m_stateHandle.Wait(State.Attacking);
            m_hitbox.Disable();
            //add cinematics
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            yield return null;
            //yield return new WaitForAnimationComplete(m_animation.animationState, m_info.introAnimation);
            //m_animation.SetAnimation(0, m_info.introAnimation2, false);
            //yield return new WaitForAnimationComplete(m_animation.animationState, m_info.introAnimation2);
            //m_animation.SetAnimation(0, m_info.rageQuake, false);
            //yield return new WaitForAnimationComplete(m_animation.animationState, m_info.rageQuake);
            m_theOneHitbox.SetActive(true);
            var ramdomAttack = RandomShit(1, 3);
            if (ramdomAttack == 1)
            {
                m_attackDecider.DecideOnAttack(Attack.TentacleGroundStab1);
            }
            else
            {
                m_attackDecider.DecideOnAttack(Attack.TentacleGroundStab2);
            }
            m_hitbox.Enable();
            m_stateHandle.ApplyQueuedState();
        }

        #region UnusedShit
        private void ChooseAttack()
        {
            //m_attackDecider.DecideOnAttack();
            //switch (m_phaseHandle.currentPhase)
            //{
            //    case Phase.PhaseOne:
            //        Debug.Log("Current Phase: " + m_phaseHandle.currentPhase);
            //        if (m_areObstaclesPresent)
            //        {
            //            if (m_areMonolithsSpawned)
            //            {
            //                m_attackDecider.DecideOnAttack(Attack.Phase1Pattern1, Attack.Phase1Pattern3, Attack.Phase1Pattern6);
            //            }
            //            if (m_isBlackBloodFloodPresent)
            //            {
            //                m_attackDecider.DecideOnAttack(Attack.Phase1Pattern1, Attack.Phase1Pattern3, Attack.Phase1Pattern6);
            //            }
            //            if (m_areTentacleWallsPresent)
            //            {
            //                m_attackDecider.DecideOnAttack(Attack.Phase1Pattern1, Attack.Phase1Pattern3);
            //            }
            //        }
            //        else
            //        {
            //            m_attackDecider.DecideOnAttack(Attack.Phase1Pattern1, Attack.Phase1Pattern2, Attack.Phase1Pattern3, Attack.Phase1Pattern4, Attack.Phase1Pattern5, Attack.Phase1Pattern6);
            //        }
            //        break;
            //    case Phase.PhaseTwo:
            //        Debug.Log("Current Phase: " + m_phaseHandle.currentPhase);
            //        if (m_areObstaclesPresent)
            //        {
            //            if (m_areMonolithsSpawned)
            //            {
            //                m_attackDecider.DecideOnAttack(Attack.Phase2Pattern1, Attack.Phase2Pattern3, Attack.Phase2Pattern5, Attack.Phase2Pattern6);
            //            }
            //            if (m_isBlackBloodFloodPresent)
            //            {
            //                m_attackDecider.DecideOnAttack(Attack.Phase2Pattern1, Attack.Phase2Pattern3, Attack.Phase2Pattern5, Attack.Phase2Pattern6, Attack.Phase2Pattern7);
            //            }
            //            if (m_areTentacleWallsPresent)
            //            {
            //                m_attackDecider.DecideOnAttack(Attack.Phase2Pattern1, Attack.Phase2Pattern2, Attack.Phase2Pattern3, Attack.Phase2Pattern5, Attack.Phase2Pattern6);
            //            }
            //        }
            //        else
            //        {
            //            Debug.Log("Deciding on Phase 2 attacks");
            //            m_attackDecider.DecideOnAttack(Attack.Phase2Pattern1, Attack.Phase2Pattern2, Attack.Phase2Pattern3, Attack.Phase2Pattern4, Attack.Phase2Pattern5, Attack.Phase2Pattern6, Attack.Phase2Pattern7);
            //        }
            //        break;
            //    case Phase.PhaseThree:
            //        Debug.Log("Current Phase: " + m_phaseHandle.currentPhase);
            //        if (m_areObstaclesPresent)
            //        {
            //            if (m_areMonolithsSpawned)
            //            {
            //                m_attackDecider.DecideOnAttack(Attack.Phase3Pattern1, Attack.Phase3Pattern2, Attack.Phase3Pattern3, Attack.Phase3Pattern4, Attack.Phase3Pattern6, Attack.Phase3Pattern7);
            //            }
            //            if (m_isBlackBloodFloodPresent)
            //            {
            //                m_attackDecider.DecideOnAttack(Attack.Phase3Pattern1, Attack.Phase3Pattern2, Attack.Phase3Pattern4, Attack.Phase3Pattern5, Attack.Phase3Pattern6, Attack.Phase3Pattern7);
            //            }
            //            if (m_areTentacleWallsPresent)
            //            {
            //                m_attackDecider.DecideOnAttack(Attack.Phase3Pattern1, Attack.Phase3Pattern2, Attack.Phase3Pattern4, Attack.Phase3Pattern6, Attack.Phase3Pattern7);
            //            }
            //        }
            //        else
            //        {
            //            m_attackDecider.DecideOnAttack(Attack.Phase3Pattern1, Attack.Phase3Pattern2, Attack.Phase3Pattern3, Attack.Phase3Pattern4, Attack.Phase3Pattern5, Attack.Phase3Pattern6, Attack.Phase3Pattern7);
            //        }
            //        break;
            //    case Phase.PhaseFour:
            //        Debug.Log("Current Phase: " + m_phaseHandle.currentPhase);
            //        if (m_areObstaclesPresent)
            //        {
            //            if (m_areMonolithsSpawned)
            //            {
            //                m_attackDecider.DecideOnAttack(Attack.Phase4Pattern2, Attack.Phase4Pattern3, Attack.Phase4Pattern6, Attack.Phase4Pattern7, Attack.Phase4Pattern8, Attack.Phase4Pattern9, Attack.Phase4Pattern11);
            //            }
            //            if (m_isBlackBloodFloodPresent)
            //            {
            //                m_attackDecider.DecideOnAttack(Attack.Phase4Pattern2, Attack.Phase4Pattern3, Attack.Phase4Pattern6, Attack.Phase4Pattern7, Attack.Phase4Pattern8, Attack.Phase4Pattern11);
            //            }
            //            if (m_areTentacleWallsPresent)
            //            {
            //                m_attackDecider.DecideOnAttack(Attack.Phase4Pattern2, Attack.Phase4Pattern3, Attack.Phase4Pattern6, Attack.Phase4Pattern7, Attack.Phase4Pattern8, Attack.Phase4Pattern11);
            //            }
            //        }
            //        else
            //        {
            //            m_attackDecider.DecideOnAttack(Attack.Phase4Pattern2, Attack.Phase4Pattern3, Attack.Phase4Pattern4, Attack.Phase4Pattern6, Attack.Phase4Pattern7, Attack.Phase4Pattern8, Attack.Phase4Pattern9, Attack.Phase4Pattern11);
            //        }
            //        break;
            //    case Phase.PhaseFive:
            //        Debug.Log("Current Phase: " + m_phaseHandle.currentPhase);
            //        if (m_areObstaclesPresent)
            //        {
            //            if (m_areMonolithsSpawned)
            //            {
            //                m_attackDecider.DecideOnAttack(Attack.Phase5Pattern1, Attack.Phase5Pattern3, Attack.Phase5Pattern6, Attack.Phase5Pattern7, Attack.Phase5Pattern8, Attack.Phase5Pattern9, Attack.Phase5Pattern10, Attack.Phase5Pattern11);
            //            }
            //            if (m_isBlackBloodFloodPresent)
            //            {
            //                m_attackDecider.DecideOnAttack(Attack.Phase5Pattern1, Attack.Phase5Pattern3, Attack.Phase5Pattern6, Attack.Phase5Pattern7, Attack.Phase5Pattern8, Attack.Phase5Pattern10, Attack.Phase5Pattern11);
            //            }
            //            if (m_areTentacleWallsPresent)
            //            {
            //                m_attackDecider.DecideOnAttack(Attack.Phase5Pattern1, Attack.Phase5Pattern3, Attack.Phase5Pattern6, Attack.Phase5Pattern7, Attack.Phase5Pattern8, Attack.Phase5Pattern10, Attack.Phase5Pattern11);
            //            }
            //        }
            //        else
            //        {
            //            m_attackDecider.DecideOnAttack(Attack.Phase5Pattern1, Attack.Phase5Pattern3, Attack.Phase5Pattern4, Attack.Phase5Pattern6, Attack.Phase5Pattern7, Attack.Phase5Pattern8, Attack.Phase5Pattern9, Attack.Phase5Pattern10, Attack.Phase5Pattern11);
            //        }
            //        break;
            //    case Phase.Wait:
            //        break;
            //}

            //m_currentAttack = m_attackDecider.chosenAttack.attack;

            //if (!m_attackDecider.hasDecidedOnAttack)
            //{
            //    IsAllAttackComplete();
            //    for (int i = 0; i < m_attackCache.Count; i++)
            //    {
            //        if (areMonolithsSpawned)
            //        {
            //            m_attackDecider.DecideOnAttack(Attack.Phase1Pattern1, Attack.Phase1Pattern2, Attack.Phase1Pattern3, Attack.Phase1Pattern4, Attack.Phase1Pattern6);
            //        }
            //        else
            //        {
            //            m_attackDecider.DecideOnAttack();
            //        }
            //        if (m_attackCache[i] != m_currentAttack && !m_attackUsed[i])
            //        {
            //            m_attackUsed[i] = true;
            //            m_currentAttack = m_attackCache[i];
            //            m_currentAttackRange = m_attackRangeCache[i];
            //            return;
            //        }
            //    }
            //}
        } 

        #endregion

        protected override void Awake()
        {


            #region PossibleNeededCode
            //for (int i = 0; i < m_projectilePoint.Length; i++)
            //{
            //    m_projectileLauncher = new ProjectileLauncher(m_info.sphereBomb.projectileInfo, m_projectilePoint[i]);
            //}
            //m_damageable.DamageTaken += OnDamageTaken;
            //m_damageable.DamageTaken += OnDamageBlocked;
            //m_patternDecider = new RandomAttackDecider<Pattern>();
            #endregion
            base.Awake();
           // m_damageable.Destroyed += damageable_Destroyed;
            m_damageable.DamageTaken += DamageTakenPhaseOne;
            m_hitCounterChangeable = m_hitCounterPhaseOne;
            m_storeMaxDistance = m_maxDistance;
            m_damageable.DamageTaken += M_damageable_DamageTaken;
            m_attackDecider = new RandomAttackDecider<Attack>();
            m_stateHandle = new StateHandle<State>(State.Idle, State.WaitBehaviourEnd);
            m_sphereBombList = new List<Projectile>();
            UpdateAttackDeciderList();

            #region Caches
            /* m_attackCache = new List<Attack>();
            AddToAttackCache(
                  Attack.Phase1Pattern1
                , Attack.Phase1Pattern2
                , Attack.Phase1Pattern3
                , Attack.Phase1Pattern4
                , Attack.Phase1Pattern5
                , Attack.Phase1Pattern6
                , Attack.Phase2Pattern1
                , Attack.Phase2Pattern2
                , Attack.Phase2Pattern3
                , Attack.Phase2Pattern4
                , Attack.Phase2Pattern5
                , Attack.Phase2Pattern6
                , Attack.Phase3Pattern1
                , Attack.Phase3Pattern2
                , Attack.Phase3Pattern3
                , Attack.Phase3Pattern4
                , Attack.Phase3Pattern5
                , Attack.Phase3Pattern6
                , Attack.Phase3Pattern7
                , Attack.Phase4Pattern1
                , Attack.Phase4Pattern2
                , Attack.Phase4Pattern3
                , Attack.Phase4Pattern4
                , Attack.Phase4Pattern5
                , Attack.Phase4Pattern6
                , Attack.Phase4Pattern7
                , Attack.Phase4Pattern8
                , Attack.Phase4Pattern9
                , Attack.Phase4Pattern10
                , Attack.Phase4Pattern11
                , Attack.Phase5Pattern1
                , Attack.Phase5Pattern2
                , Attack.Phase5Pattern3
                , Attack.Phase5Pattern4
                , Attack.Phase5Pattern5
                , Attack.Phase5Pattern6
                , Attack.Phase5Pattern7
                , Attack.Phase5Pattern8
                , Attack.Phase5Pattern9
                , Attack.Phase5Pattern10
                , Attack.Phase5Pattern11);
            m_attackRangeCache = new List<float>();
            AddToRangeCache(
                  m_info.phase1Pattern1Range
                , m_info.phase1Pattern2Range
                , m_info.phase1Pattern3Range
                , m_info.phase1Pattern4Range
                , m_info.phase1Pattern5Range
                , m_info.phase1Pattern6Range
                , m_info.phase2Pattern1Range
                , m_info.phase2Pattern2Range
                , m_info.phase2Pattern3Range
                , m_info.phase2Pattern4Range
                , m_info.phase2Pattern5Range
                , m_info.phase2Pattern6Range
                , m_info.phase3Pattern1Range
                , m_info.phase3Pattern2Range
                , m_info.phase3Pattern3Range
                , m_info.phase3Pattern4Range
                , m_info.phase3Pattern5Range
                , m_info.phase3Pattern6Range
                , m_info.phase3Pattern7Range
                , m_info.phase4Pattern1Range
                , m_info.phase4Pattern2Range
                , m_info.phase4Pattern3Range
                , m_info.phase4Pattern4Range
                , m_info.phase4Pattern5Range
                , m_info.phase4Pattern6Range
                , m_info.phase4Pattern7Range
                , m_info.phase4Pattern8Range
                , m_info.phase5Pattern1Range
                , m_info.phase5Pattern2Range
                , m_info.phase5Pattern3Range
                , m_info.phase5Pattern4Range
                , m_info.phase5Pattern5Range
                , m_info.phase5Pattern6Range
                , m_info.phase5Pattern7Range
                , m_info.phase5Pattern8Range
                , m_info.phase5Pattern9Range
                , m_info.phase5Pattern10Range
                , m_info.phase5Pattern11Range);
            m_attackUsed = new bool[m_attackCache.Count];
            m_currentFullCooldown = new List<float>();
            m_patternCooldown = new List<float>();*/
            #endregion


            //m_theOneThirdFormAttacks.AttackStart += OnAttackStart;
            m_theOneThirdFormAttacks.AttackDone += OnAttackDone;
            AttackDone += OnAttackDone;
            ObstaclesAdded += OnObstaclesAdded;
            ObstaclesCleared += OnObstaclesEmptied;
            m_obstacleChecker.ObstacleAdded += OnObstaclesAdded;
            m_obstacleChecker.ObstaclesCleared += OnObstaclesEmptied;
            m_obstacleChecker.MonolithAdded += OnMonolithAdded;
            m_obstacleChecker.MonolithEmptied += OnMonolithEmptied;

            //m_areMonolithsSpawned = FindObjectOfType<ObstacleChecker>().isMonolithSlamObstaclePresent;
            m_areTentacleWallsPresent = FindObjectOfType<ObstacleChecker>().isWallTentaclesPresent;
            m_isBlackBloodFloodPresent = FindObjectOfType<ObstacleChecker>().isFloodingBlackBlood;
        }

        //private void damageable_Destroyed(object sender, EventActionArgs eventArgs)
        //{
        //    ReviveForPhaseTwo();
        //}

        protected override void OnDisable()
        {
            base.OnDisable();
            m_damageable.DamageTaken -= DamageTakenPhaseOne;
            m_theOneThirdFormAttacks.AttackDone -= OnAttackDone;
            AttackDone -= OnAttackDone;
            ObstaclesAdded -= OnObstaclesAdded;
            ObstaclesCleared -= OnObstaclesEmptied;
            m_obstacleChecker.ObstacleAdded -= OnObstaclesAdded;
            m_obstacleChecker.ObstaclesCleared -= OnObstaclesEmptied;
            m_obstacleChecker.MonolithAdded -= OnMonolithAdded;
            m_obstacleChecker.MonolithEmptied -= OnMonolithEmptied;
            m_damageable.DamageTaken -= M_damageable_DamageTaken;
        }

        private void DamageTakenPhaseOne(object sender, Damageable.DamageEventArgs eventArgs)
        {
            ReviveForPhaseTwo();
        }

        private void ReviveForPhaseTwo()
        {
            if (m_phaseHandle.currentPhase == Phase.PhaseOne)
            {
                if (m_damageable.health.currentValue <= 0)
                {
                    phaseHandle.MonitorPhase();
                    Debug.Log("health 0");
                    GameplaySystem.gamplayUIHandle.ToggleBossHealth(false);
                    m_damageable.health.SetHealthPercentage(0.1f);
                    
                }
            }
        }

        private bool m_inSquintStateAlready = false;
        private void M_damageable_DamageTaken(object sender, Damageable.DamageEventArgs eventArgs)
        {

            Debug.Log("Damage by Player boss?");
            
            m_hitCounter += 1;
            if (m_hitCounter >= m_hitCounterChangeable)
            {
                m_hitCounter = 0;
                Debug.Log("hit counter: " + m_hitCounter.ToString());
                m_damageable.DamageTaken -= M_damageable_DamageTaken;
                m_maxDistance = 5f;
                StartCoroutine(SquintState());
               
            }
        }
        private void OnMonolithEmptied(object sender, EventActionArgs eventArgs)
        {
            m_areMonolithsSpawned = false;
        }

        private void OnMonolithAdded(object sender, EventActionArgs eventArgs)
        {
            m_areMonolithsSpawned = true;
        }

        private void OnObstaclesEmptied(object sender, EventActionArgs eventArgs)
        {
            m_areObstaclesPresent = false;
        }

        private void OnObstaclesAdded(object sender, EventActionArgs eventArgs)
        {
            m_areObstaclesPresent = true;
        }

        protected override void Start()
        {
            //base.Start();

            //m_animation.DisableRootMotion();
            if (m_eyeTheOne != null)
            {
                // Set the initial center position of the eye
                m_eyeCenter = m_eyeTheOne.position;
            }
            m_phaseHandle = new PhaseHandle<Phase, PhaseInfo>();
            m_phaseHandle.Initialize(Phase.PhaseOne, m_info.phaseInfo, m_character, ChangeState, ApplyPhaseData);
            m_phaseHandle.ApplyChange();
        }

        protected override void LateUpdate()
        {
            //base.LateUpdate();
        }

        public override void Enable()
        {
            //temp solution
        }

        public override void Disable()
        {
            //temp solution
        }

        //private enum State
        //{
        //    Intro, 
        //    Phasing,
        //    Attacking,
        //    Idle,
        //    ReevaluateSituation,
        //    WaitBehaviourEnd,
        //}

        [SerializeField, TabGroup("Modules")]
        private PathFinderAgent m_agent;

        [SerializeField]
        private TheOneThirdFormAttacks m_theOneThirdFormAttacks;

        //private void OnAttackStart(object sender, EventActionArgs eventArgs)
        //{

        //}

        private void OnAttackDone(object sender, EventActionArgs eventArgs)
        {
            m_attackDecider.hasDecidedOnAttack = false;
            m_currentAttackCoroutine = null;
            m_stateHandle.OverrideState(State.ReevaluateSituation);
            Debug.Log("Attack Done");
        }

        public override void SetTarget(IDamageable damageable, Character m_target = null)
        {
            if (damageable != null)
            {
                base.SetTarget(damageable, m_target);
                m_stateHandle.OverrideState(State.Intro);
                //GameEventMessage.SendEvent("Boss Encounter");
            }
        }

        private void ChangeState()
        {
            //StopCurrentAttackRoutine();
            //SetAIToPhasing();
            Debug.Log("Change phase");
            m_theOneHitbox.SetActive(false);
            m_stateHandle.SetState(State.Phasing);
        }

        private void ApplyPhaseData(PhaseInfo obj)
        {
            #region Old ApplyPhaseData
            /*
            m_attackCache.Clear();
            m_attackRangeCache.Clear();
            if (m_patternCooldown.Count != 0)
                m_patternCooldown.Clear();
            switch (m_phaseHandle.currentPhase)
            {
                case Phase.PhaseOne:
                    //m_idleAnimation = m_info.idleCombatAnimation;
                    AddToAttackCache(Attack.Phase1Pattern1, Attack.Phase1Pattern2, Attack.Phase1Pattern3, Attack.Phase1Pattern4, Attack.Phase1Pattern5);
                    AddToRangeCache(m_info.phase1Pattern1Range, m_info.phase1Pattern2Range, m_info.phase1Pattern3Range, m_info.phase1Pattern4Range, m_info.phase1Pattern5Range, m_info.phase1Pattern6Range);
                    for (int i = 0; i < m_info.phase1PatternCooldown.Count; i++)
                        m_patternCooldown.Add(m_info.phase1PatternCooldown[i]);
                    break;
                case Phase.PhaseTwo:
                    //m_idleAnimation = m_info.idleCombatAnimation;
                    AddToAttackCache(Attack.Phase2Pattern1, Attack.Phase2Pattern2, Attack.Phase2Pattern3, Attack.Phase2Pattern4, Attack.Phase2Pattern5, Attack.Phase2Pattern6);
                    AddToRangeCache(m_info.phase2Pattern1Range, m_info.phase2Pattern2Range, m_info.phase2Pattern3Range, m_info.phase2Pattern4Range, m_info.phase2Pattern5Range, m_info.phase2Pattern6Range);
                    for (int i = 0; i < m_info.phase2PatternCooldown.Count; i++)
                        m_patternCooldown.Add(m_info.phase2PatternCooldown[i]);
                    break;
                case Phase.PhaseThree:
                    //m_idleAnimation = m_info.idleCombatAnimation;
                    AddToAttackCache(Attack.Phase3Pattern1, Attack.Phase3Pattern2, Attack.Phase3Pattern3, Attack.Phase3Pattern4, Attack.Phase3Pattern5, Attack.Phase3Pattern6, Attack.Phase3Pattern7);
                    AddToRangeCache(m_info.phase3Pattern1Range, m_info.phase3Pattern2Range, m_info.phase3Pattern3Range, m_info.phase3Pattern4Range, m_info.phase3Pattern5Range, m_info.phase3Pattern6Range, m_info.phase3Pattern7Range);
                    for (int i = 0; i < m_info.phase3PatternCooldown.Count; i++)
                        m_patternCooldown.Add(m_info.phase3PatternCooldown[i]);
                    break;
                case Phase.PhaseFour:
                    //m_idleAnimation = m_info.idleCombatAnimation;
                    AddToAttackCache(Attack.Phase4Pattern1, Attack.Phase4Pattern2, Attack.Phase4Pattern3, Attack.Phase4Pattern4, Attack.Phase4Pattern5, Attack.Phase4Pattern6, Attack.Phase4Pattern7, Attack.Phase4Pattern8, Attack.Phase4Pattern9, Attack.Phase4Pattern10, Attack.Phase4Pattern11);
                    AddToRangeCache(m_info.phase4Pattern1Range, m_info.phase4Pattern2Range, m_info.phase4Pattern3Range, m_info.phase4Pattern4Range, m_info.phase4Pattern5Range, m_info.phase4Pattern6Range, m_info.phase4Pattern7Range, m_info.phase4Pattern8Range, m_info.phase4Pattern9Range, m_info.phase4Pattern10Range, m_info.phase4Pattern11Range);
                    for (int i = 0; i < m_info.phase4PatternCooldown.Count; i++)
                        m_patternCooldown.Add(m_info.phase4PatternCooldown[i]);
                    break;
                case Phase.PhaseFive:
                    //m_idleAnimation = m_info.idleCombatAnimation;
                    AddToAttackCache(Attack.Phase5Pattern1, Attack.Phase5Pattern2, Attack.Phase5Pattern3, Attack.Phase5Pattern4, Attack.Phase5Pattern5, Attack.Phase5Pattern6, Attack.Phase5Pattern7, Attack.Phase5Pattern8, Attack.Phase5Pattern9, Attack.Phase5Pattern10, Attack.Phase5Pattern11);
                    AddToRangeCache(m_info.phase5Pattern1Range, m_info.phase5Pattern2Range, m_info.phase5Pattern3Range, m_info.phase5Pattern4Range, m_info.phase5Pattern5Range, m_info.phase5Pattern6Range, m_info.phase5Pattern7Range, m_info.phase5Pattern8Range, m_info.phase5Pattern9Range, m_info.phase5Pattern10Range, m_info.phase5Pattern11Range);
                    for (int i = 0; i < m_info.phase5PatternCooldown.Count; i++)
                        m_patternCooldown.Add(m_info.phase5PatternCooldown[i]);
                    break;
            }
            m_attackUsed = new bool[m_attackCache.Count];
            if (m_currentFullCooldown.Count != 0)
            {
                m_currentFullCooldown.Clear();
            }
            for (int i = 0; i < obj.fullCooldown.Count; i++)
            {
                m_currentFullCooldown.Add(obj.fullCooldown[i]);
            }*/
            #endregion
            if (m_attackDecider != null)
            {
                UpdateAttackDeciderList();
            }
        }
        [SerializeField]
        private bool m_skipCinematics;
        [SerializeField]
        private Transform m_cinematicBHPosition;
        [SerializeField]
        private UnityEvent m_onFirstDeath;
        
        public void TriggerCutsceneForPhase2()
        {
            m_cutsceneTriggersForPhaseTwo = true;
        }
        private IEnumerator ChangePhaseRoutine()
        {
            m_stateHandle.Wait(State.Attacking);
            m_theOneHitbox.SetActive(false);
            m_hitbox.Disable();
            m_hitCounter = 0;
            m_damageable.DamageTaken -= M_damageable_DamageTaken;
            if (m_phaseHandle.currentPhase == Phase.PhaseTwo)
            {
                //cinematics;
                GameplaySystem.gamplayUIHandle.ToggleBossHealth(false);
                m_animation.SetAnimation(0, m_info.exhaustedAnimation, true);
                m_onFirstDeath?.Invoke();
                if (m_skipCinematics)
                {
                    while (m_cutsceneTriggersForPhaseTwo == false)
                    {
                        yield return null;
                    }
                }  
                m_animation.SetAnimation(0, m_info.exhaustedToIdleAnimation, false);
                yield return new WaitForAnimationComplete(m_animation.animationState, m_info.exhaustedToIdleAnimation);
            }
            else
            {
                m_animation.SetAnimation(0, m_info.blackHoleMouth, false);
                yield return new WaitForAnimationComplete(m_animation.animationState, m_info.blackHoleMouth);
                m_theOneMiniBlackHole.transform.position = m_cinematicBHPosition.transform.position;
                m_theOneMiniBlackHole.SetActive(true);
                while (m_isPlayerBackArena == false)
                {
                    Debug.Log("player is not back to area");
                    yield return null;
                }
                m_outwardBlackHole.Play();
            }  
           
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            yield return new WaitForSeconds(1f);
            enabled = true;
            if (m_phaseHandle.currentPhase != Phase.PhaseTwo)
            {
                yield return ExhaustedState();
            }
            m_animation.SetAnimation(0, m_info.rageQuake, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.rageQuake);
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            switch (m_phaseHandle.currentPhase)
            {
                case Phase.PhaseTwo:
                    m_damageable.health.SetMaxValue(17000);
                    m_damageable.health.ResetValueToMax();
                    GameplaySystem.gamplayUIHandle.ToggleBossHealth(true);
                    m_hitCounterChangeable = m_hitCounterPhaseTwo;
                    var randomAttackPhaseTwo = RandomShit(1, 4);
                    if (randomAttackPhaseTwo == 1)
                    {
                        m_attackDecider.DecideOnAttack(Attack.ChasingGroundBlast);
                    }
                    else if (randomAttackPhaseTwo == 2)
                    {
                        m_attackDecider.DecideOnAttack(Attack.TentacleBlast2);
                    }
                    else
                    {
                        m_attackDecider.DecideOnAttack(Attack.MouthBlast2);
                    }
                    Debug.Log("Done transitioning to phase two");
                    break;
                case Phase.PhaseThree:
                    m_hitCounterChangeable = m_hitCounterPhaseThree;
                    var randomAttackPhaseThre = RandomShit(1, 4);
                    if (randomAttackPhaseThre == 1)
                    {
                        m_attackDecider.DecideOnAttack(Attack.ChasingGroundBlastPhaseTree);
                    }
                    else if (randomAttackPhaseThre == 2)
                    {
                        m_attackDecider.DecideOnAttack(Attack.TentacleBlast2);
                    }
                    else
                    {
                        m_attackDecider.DecideOnAttack(Attack.MouthBlast2);
                    }
                    Debug.Log("Done transitioning to phase three");
                    break;
                case Phase.PhaseFour:
                    m_hitCounterChangeable = m_hitCounterPhaseFour;
                    var randomAttackPhaseFour = RandomShit(1, 4);
                    if (randomAttackPhaseFour == 1)
                    {
                        m_attackDecider.DecideOnAttack(Attack.ChasingGroundBlastAndMouthBlast2);
                    }
                    else if (randomAttackPhaseFour == 2)
                    {
                        m_attackDecider.DecideOnAttack(Attack.TentacleBlast2);
                    }
                    else
                    {
                        m_attackDecider.DecideOnAttack(Attack.MouthBlastCeiling1);
                    }
                    Debug.Log("Done transitioning to phase four");
                    break;
                case Phase.PhaseFive:
                    m_hitCounterChangeable = m_hitCounterPhaseFive;
                    var randomAttackPhaseFive = RandomShit(1, 4);
                    if (randomAttackPhaseFive == 1)
                    {
                        m_attackDecider.DecideOnAttack(Attack.ChasingGroundBlastMouthBlast2AndMouthBlast1);
                    }
                    else if (randomAttackPhaseFive == 2)
                    {
                        m_attackDecider.DecideOnAttack(Attack.MouthBlastCeiling1);
                    }
                    else
                    {
                        m_attackDecider.DecideOnAttack(Attack.SphereBomb2);
                    }
                    Debug.Log("Done transitioning to phase five");
                    break;
            }  
            m_hitbox.Enable();
            m_theOneHitbox.SetActive(true);
            m_phaseHandle.ApplyChange();

            //change hp
            if (m_phaseHandle.currentPhase == Phase.PhaseTwo)
            {
                m_phaseHandle = new PhaseHandle<Phase, PhaseInfo>();
                m_phaseHandle.Initialize(Phase.PhaseTwo, m_info.phaseInfo_2, m_character, ChangeState, ApplyPhaseData);
                m_phaseHandle.ApplyChange();

            }
            m_damageable.DamageTaken += M_damageable_DamageTaken;
            m_isPlayerBackArena = false;
            m_stateHandle.ApplyQueuedState();
        }

        private IEnumerator SquintStateForTentacleStab()
        {
            if(m_inSquintStateAlready == false)
            {
                m_theOneHitbox.SetActive(false);
                m_animation.SetAnimation(0, m_info.eyeSquintAnimation, true);
                //m_animation.SetAnimation(0, m_info.eyeSquintLoop, false);
                //yield return new WaitForAnimationComplete(m_animation.animationState, m_info.eyeSquintLoop);
            }
            else
            {
                Debug.Log("skipped squint state");
                yield break;
            }
          
        }
        private IEnumerator EndSquintStateForTentacleStab()
        {
            if (m_inSquintStateAlready == false)
            {
                m_theOneHitbox.SetActive(true);
                m_animation.SetAnimation(0, m_info.unsquintAnimation, false);
                yield return new WaitForAnimationComplete(m_animation.animationState, m_info.unsquintAnimation);
            }
            else
            {
                Debug.Log("skipped end squint state");
                yield break;
            }
            
        }
        private IEnumerator TentacleGroundStabAttack1()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
           // m_animation.SetAnimation(0, m_info.idleAnimation, true);
            // end squint eye IK logic, either new AI prefab model or script only 
            yield return SquintStateForTentacleStab();
            for (int i = 0; i < 2; i++)
            {
                
                yield return m_theOneThirdFormAttacks.TentacleGroundStab(m_targetInfo);
                yield return new WaitForSeconds(3f);
            }
            var randomNumber = RandomShit(1, 3);
            if(randomNumber == 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    yield return m_theOneThirdFormAttacks.TentacleGroundStab(m_targetInfo);
                    yield return new WaitForSeconds(3f);
                }
               yield return EndSquintStateForTentacleStab();
                m_attackDecider.hasDecidedOnAttack = false;
                m_stateHandle.ApplyQueuedState();
                Debug.Log("Attack Done");
                Debug.Log("3 na");
            }
            else
            {
                yield return EndSquintStateForTentacleStab();
                m_attackDecider.hasDecidedOnAttack = false;
                m_stateHandle.ApplyQueuedState();
                Debug.Log("Attack Done");
                Debug.Log("3 na");
            }
            // end of squint eye IK logic, either new AI prefab model or script only 
            
        }
        private IEnumerator TentacleGroundStabAttack2()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            yield return m_theOneThirdFormAttacks.TentacleGroundStabTwo();
            yield return new WaitForSeconds(3f);
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
        }
        private void SetAIToPhasing()
        {
            m_phaseHandle.ApplyChange();
            m_animation.DisableRootMotion();
            m_animation.SetEmptyAnimation(0, 0);
            m_stateHandle.SetState(State.Phasing);
        }

        #region MouthBlastOne Attack
        private IEnumerator MouthBlastOneStart()
        {
            //m_animation.SetAnimation(0, m_info.eyeClosedAnimation, false);
            //yield return new WaitForAnimationComplete(m_animation.animationState, m_info.eyeClosedAnimation);
            //m_animation.SetAnimation(0, m_info.eyeMouthBlastAnticipationAnimation, true);
            //yield return new WaitForAnimationComplete(m_animation.animationState, m_info.eyeMouthBlastAnticipationAnimation);

            StartCoroutine(m_theOneThirdFormAttacks.MouthBlastOneCeiling());

            yield return SetPositionForMouthBlast();
        }

        private IEnumerator SetPositionForMouthBlast()
        {
            int side = UnityEngine.Random.Range(0, 2);
            if (side == 0)
            {
                m_model.transform.position = new Vector2(m_theOneThirdFormAttacks.mouthBlastOneAttack.mouthBlastOneLeftSide.position.x, m_model.transform.position.y);
            }
            else if (side == 1)
            {
                m_model.transform.position = new Vector2(m_theOneThirdFormAttacks.mouthBlastOneAttack.mouthBlastOneRightSide.position.x, m_model.transform.position.y);
            }
            yield return MoveMouthBlast(side);
        }



        private IEnumerator MoveMouthBlast(int side)
        {
            //m_animation.SetAnimation(0, m_info.animation, true);
            //yield return new WaitForAnimationComplete(m_animation.animationState, m_info.eyeMouthBlastAnimation);

            if (side == 0)
            {
                StartCoroutine(m_theOneThirdFormAttacks.mouthBlastOneAttack.ExecuteAttack());
                while (m_model.transform.position.x < m_theOneThirdFormAttacks.mouthBlastOneAttack.mouthBlastOneRightSide.position.x)
                {
                    m_model.transform.position = Vector2.MoveTowards(m_model.transform.position,
                        new Vector2(m_theOneThirdFormAttacks.mouthBlastOneAttack.mouthBlastOneRightSide.position.x,
                        m_model.transform.position.y), m_theOneThirdFormAttacks.mouthBlastOneAttack.mouthBlastOneMoveSpeed);
                    yield return new WaitForSeconds(0.002f * GameplaySystem.time.deltaTime);
                }
            }
            else if (side == 1)
            {
                StartCoroutine(m_theOneThirdFormAttacks.mouthBlastOneAttack.ExecuteAttack());
                while (m_model.transform.position.x > m_theOneThirdFormAttacks.mouthBlastOneAttack.mouthBlastOneLeftSide.position.x)
                {
                    m_model.transform.position = Vector2.MoveTowards(m_model.transform.position,
                        new Vector2(m_theOneThirdFormAttacks.mouthBlastOneAttack.mouthBlastOneLeftSide.position.x,
                        m_model.transform.position.y), m_theOneThirdFormAttacks.mouthBlastOneAttack.mouthBlastOneMoveSpeed);
                    yield return new WaitForSeconds(0.002f * GameplaySystem.time.deltaTime);
                }
            }
        }

        private IEnumerator MouthBlastEnd(Vector2 OriginalPosition)
        {
            StartCoroutine(m_theOneThirdFormAttacks.mouthBlastOneAttack.EndMouthBlast());
            while (m_model.transform.position.x != OriginalPosition.x)
            {
                m_model.transform.position = Vector2.MoveTowards(m_model.transform.position,
                    OriginalPosition, m_theOneThirdFormAttacks.mouthBlastOneAttack.mouthBlastOneMoveSpeed);
                yield return new WaitForSeconds(0.002f * GameplaySystem.time.deltaTime);
            }
        }

        private IEnumerator FullMouthBlastOneSequence()
        {
            Vector2 originalPosition = m_model.transform.position;
            yield return MouthBlastOneStart();
            yield return MouthBlastEnd(originalPosition);
            //AttackDone?.Invoke(this, EventActionArgs.Empty);
        }
        #endregion

        #region AttackCoroutines
        private IEnumerator TentacleGroundStab(float cooldown)
        {
            m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.TentacleGroundStab(m_targetInfo));

            yield return new WaitForSeconds(cooldown);
            m_attackDecider.hasDecidedOnAttack = false;
            m_currentAttackCoroutine = null;
            m_stateHandle.ApplyQueuedState();
        }

        private IEnumerator ChasingGroundTentacle(float cooldown)
        {
            var monolithPlatformsPresent = FindObjectOfType<ObstacleChecker>().monolithSlamObstacleList;

            if (monolithPlatformsPresent != null)
            {
                yield return null;
            }

            var blackBloodFloodPresent = FindObjectOfType<ObstacleChecker>().isFloodingBlackBlood;

            if (blackBloodFloodPresent)
            {
                yield return null;
            }

            yield return m_theOneThirdFormAttacks.ChasingGroundTentacle();
            yield return new WaitForSeconds(cooldown);

        }

        private IEnumerator TentacleBlastOne(float cooldown)
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            var obstaclePresent = m_obstacleChecker.monolithSlamObstacleList;
            // var monolithPlatformsPresent = FindObjectOfType<ObstacleChecker>().monolithSlamObstacleList;
            if (obstaclePresent.Count > 0)
            {
                yield return new WaitForSeconds(1f);
                m_attackDecider.hasDecidedOnAttack = false;
                m_stateHandle.ApplyQueuedState();
                Debug.Log("Skip tentacle due to monolith slam obstacle");
            }
            else
            {
                for (int i = 0; i < m_tentacleBlast.Length; i++)
                {
                    m_tentacleBlast[i].HasDamageTarget += tentacleBlast_HasDamageTarget;
                }
                
                yield return m_theOneThirdFormAttacks.TentacleBlastOne(m_targetInfo);
                yield return new WaitForSeconds(cooldown);
                for (int i = 0; i < m_tentacleBlast.Length; i++)
                {
                    m_tentacleBlast[i].HasDamageTarget -= tentacleBlast_HasDamageTarget;
                }
                m_removeTentacleBlastAttacks = true;
                m_attackDecider.hasDecidedOnAttack = false;
                m_stateHandle.ApplyQueuedState();
            }

        }

        private void tentacleBlast_HasDamageTarget(object sender, EventActionArgs eventArgs)
        {
            LockPlayerQuickItem?.Invoke(this, EventActionArgs.Empty);
            Debug.Log("hit by tentacle blast");
        }

        private IEnumerator TentacleBlastTwoPhase4()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            //m_animation.SetAnimation(0, m_info.idleAnimation, true);
            for (int i = 0; i < m_tentacleBlast.Length; i++)
            {
                m_tentacleBlast[i].HasDamageTarget += tentacleBlast_HasDamageTarget;
            }
            yield return m_theOneThirdFormAttacks.TentacleBlastTwo();
            for (int i = 0; i < m_tentacleBlast.Length; i++)
            {
                m_tentacleBlast[i].HasDamageTarget -= tentacleBlast_HasDamageTarget;
            }
            Debug.Log("skip part?");
            m_removeTentacleBlastAttacks = true;
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
        }
        private IEnumerator TentacleBlastTwoAttack(float cooldown)
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            //m_animation.SetAnimation(0, m_info.idleAnimation, true);

            var monolithPlatformsPresent = m_obstacleChecker.monolithSlamObstacleList.Count;

            if (monolithPlatformsPresent > 0)
            {
                m_attackDecider.hasDecidedOnAttack = false;
                m_stateHandle.ApplyQueuedState();
            }
            else
            {
                for (int i = 0; i < m_tentacleBlast.Length; i++)
                {
                    m_tentacleBlast[i].HasDamageTarget += tentacleBlast_HasDamageTarget;
                }
                yield return m_theOneThirdFormAttacks.TentacleBlastTwo();
                yield return new WaitForSeconds(5f);
                Debug.Log("skip part?");
                m_removeTentacleBlastAttacks = true;
                for (int i = 0; i < m_tentacleBlast.Length; i++)
                {
                    m_tentacleBlast[i].HasDamageTarget -= tentacleBlast_HasDamageTarget;
                }
                m_attackDecider.hasDecidedOnAttack = false;
                m_stateHandle.ApplyQueuedState();

            }

        }
        private IEnumerator MonolithSlamPhase3Attack(float cooldown)
        {

            m_stateHandle.Wait(State.ReevaluateSituation);
          //  m_animation.SetAnimation(0, m_info.idleAnimation, true);
            var blackBloodFloodPresent = m_obstacleChecker.isFloodingBlackBlood;

            if (blackBloodFloodPresent)
            {
                m_attackDecider.hasDecidedOnAttack = false;
                m_stateHandle.ApplyQueuedState();
                yield break;
            }
            yield return m_theOneThirdFormAttacks.MonolithSlam();
            yield return new WaitForSeconds(cooldown);
            yield return m_theOneThirdFormAttacks.RemovalMonolithSlamPhaseOne();
            yield return new WaitForSeconds(cooldown);
            yield return m_theOneThirdFormAttacks.MonolithSlamPhaseTwo();
            yield return new WaitForSeconds(3f);
            yield return m_theOneThirdFormAttacks.RemovalMonolithSlamPhaseTwo();
            yield return new WaitForSeconds(1f);
            yield return m_theOneThirdFormAttacks.MonolithSlamPhaseTwo();
            yield return new WaitForSeconds(3f);
            yield return m_theOneThirdFormAttacks.RemovalMonolithSlamPhaseTwo();
            yield return new WaitForSeconds(1f);
            //for (int i = 0; i < 2; i++)
            //{
            //    //yield return m_theOneThirdFormAttacks.MonolithSlam(m_targetInfo);
            //    yield return new WaitForSeconds(cooldown);
            //}
            var randomShit = RandomShit(0, 3);
            if (randomShit == 0)
            {
                yield return m_theOneThirdFormAttacks.SlidingStoneWallAttack(m_targetInfo);
            }
            else
            {
                yield return m_theOneThirdFormAttacks.TentacleGroundStabTwo();
            }

            Debug.Log("monolith done");
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
        }
        private IEnumerator MonolithSlamPhase2Attack(float cooldown)
        {
            //pattern 4-1 ni ssob 
            m_stateHandle.Wait(State.ReevaluateSituation);
           // m_animation.SetAnimation(0, m_info.idleAnimation, true);
            var blackBloodFloodPresent = m_obstacleChecker.isFloodingBlackBlood;

            if (blackBloodFloodPresent)
            {
                m_attackDecider.hasDecidedOnAttack = false;
                m_stateHandle.ApplyQueuedState();
                yield break;
            }
            yield return m_theOneThirdFormAttacks.MonolithSlam();
            yield return new WaitForSeconds(cooldown);
            yield return m_theOneThirdFormAttacks.RemovalMonolithSlamPhaseOne();
            yield return new WaitForSeconds(cooldown);
            yield return m_theOneThirdFormAttacks.MonolithSlamPhaseTwo();
            yield return new WaitForSeconds(5f);
            yield return m_theOneThirdFormAttacks.RemovalMonolithSlamPhaseTwo();
            yield return new WaitForSeconds(1f);
            //for (int i = 0; i < 2; i++)
            //{
            //    //yield return m_theOneThirdFormAttacks.MonolithSlam(m_targetInfo);
            //    yield return new WaitForSeconds(cooldown);
            //}
            yield return m_theOneThirdFormAttacks.TentacleGroundStabTwo();
            Debug.Log("monolith done");
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
        }
        private IEnumerator MonolithSlamPhase1Attack(float cooldown)
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
           // m_animation.SetAnimation(0, m_info.idleAnimation, true);
            var blackBloodFloodPresent = m_obstacleChecker.isFloodingBlackBlood;

            if (blackBloodFloodPresent)
            {
                m_attackDecider.hasDecidedOnAttack = false;
                m_stateHandle.ApplyQueuedState();
                yield break;
            }

            yield return m_theOneThirdFormAttacks.MonolithSlam();
            yield return new WaitForSeconds(cooldown);
            yield return m_theOneThirdFormAttacks.RemovalMonolithSlamPhaseOne();
            yield return new WaitForSeconds(1f);
            yield return m_theOneThirdFormAttacks.TentacleGroundStab(m_targetInfo);
            yield return new WaitForSeconds(5f);
            Debug.Log("monolith done");
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
        }
        private IEnumerator MonolithSlam(float cooldown)
        {
            var blackBloodFloodPresent = FindObjectOfType<ObstacleChecker>().isFloodingBlackBlood;

            if (blackBloodFloodPresent)
            {
                yield return null;
            }

            yield return m_theOneThirdFormAttacks.MonolithSlam();
            yield return new WaitForSeconds(cooldown);
           // m_animation.SetAnimation(0, m_info.idleAnimation, true);
            Debug.Log("monolith done");
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();

        }
        private IEnumerator MouthBlastOneAndTwo()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            // var animationState = m_animation.animationState;
            // m_animation.SetAnimation(0, m_info.idleAnimation, true);
            for (int i = 0; i < m_mouthBlastTwo.Length; i++)
            {
                m_mouthBlastTwo[i].HasDamageTarget += mouthBlastTwo_HasDamageTarget;
            }
            m_mouthBlastOne.HasDamageTarget += mouthBlastOne_HasDamageTarget;
            StartCoroutine(MouthBlastWall(1f));
            yield return m_theOneThirdFormAttacks.MouthBlastOneCeiling();
            for (int i = 0; i < m_mouthBlastTwo.Length; i++)
            {
                m_mouthBlastTwo[i].HasDamageTarget -= mouthBlastTwo_HasDamageTarget;
            }
            m_mouthBlastOne.HasDamageTarget -= mouthBlastOne_HasDamageTarget;
            yield return new WaitForSeconds(1f);
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
        }   
        private IEnumerator MouthBlastTwoWallAttack(float cooldown)
        {
            for (int i = 0; i < m_mouthBlastTwo.Length; i++)
            {
                m_mouthBlastTwo[i].HasDamageTarget += mouthBlastTwo_HasDamageTarget;
            }    
            m_stateHandle.Wait(State.ReevaluateSituation);
            //m_animation.SetAnimation(0, m_info.idleAnimation, true);
            yield return MouthBlastWall(1f);
            yield return new WaitForSeconds(cooldown);
            Debug.Log("Wall mouthblast done");
            for (int i = 0; i < m_mouthBlastTwo.Length; i++)
            {
                m_mouthBlastTwo[i].HasDamageTarget -= mouthBlastTwo_HasDamageTarget;
            }
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();

        }

        private void mouthBlastTwo_HasDamageTarget(object sender, EventActionArgs eventArgs)
        {
            LockPlayerQuickItem?.Invoke(this, EventActionArgs.Empty);
            Debug.Log("Hit by mouth blast");
        }



        private IEnumerator MouthBlastWall(float cooldown)
        {
            var monolithPlatformsPresent = m_obstacleChecker.monolithSlamObstacleList;

            if (monolithPlatformsPresent != null)
                yield return null;

            //m_animation.SetAnimation(0, m_info.idleAnimation, true);

            yield return m_theOneThirdFormAttacks.MouthBlastWall();
            yield return new WaitForSeconds(cooldown);
        }
        private IEnumerator TentacleGroundStabCeilingAttackPhase3()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            //m_animation.SetAnimation(0, m_info.idleAnimation, true);
            yield return m_theOneThirdFormAttacks.TentacleCeilingAttack();
            for (int i = 0; i < 2; i++)
            {
                yield return m_theOneThirdFormAttacks.TentacleGroundStab(m_targetInfo);
                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(2f);
            yield return m_theOneThirdFormAttacks.TentacleGroundStabTwo();
            yield return new WaitForSeconds(3f);
            for (int i = 0; i < 2; i++)
            {
                yield return m_theOneThirdFormAttacks.TentacleGroundStab(m_targetInfo);
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(2f);
            yield return m_theOneThirdFormAttacks.TentacleGroundStabTwo();
            Debug.Log("Done chasing ground tentakel");
            yield return m_theOneThirdFormAttacks.TentacleCeilingAttackRetract();
            yield return MouthBlastWallCombo(3f);
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
        }
        private IEnumerator MouthBlastWallCombo(float cooldown)
        {
           // m_animation.SetAnimation(0, m_info.idleAnimation, true);
            yield return MouthBlastWall(1f);
            yield return new WaitForSeconds(cooldown);
            Debug.Log("Wall mouthblast done");
        }
        private IEnumerator TentacleGroundStabCeilingAttack()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            //m_animation.SetAnimation(0, m_info.idleAnimation, true);
            yield return m_theOneThirdFormAttacks.TentacleCeilingAttack();
            yield return m_theOneThirdFormAttacks.TentacleGroundStab(m_targetInfo);
            yield return new WaitForSeconds(3f);
            yield return m_theOneThirdFormAttacks.TentacleGroundStab(m_targetInfo);
            yield return new WaitForSeconds(3f);
            yield return m_theOneThirdFormAttacks.TentacleGroundStabTwo();
            yield return m_theOneThirdFormAttacks.TentacleCeilingAttackRetract();
            yield return MouthBlastWall(1f);
            // next is mouth blast      
            Debug.Log("TentacleGroundStabCeilingAttack");
            //mouthblast
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();

        }
        private IEnumerator ChasingGroundBlastMouthBlast2AndMouthBlast1()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            yield return m_theOneThirdFormAttacks.TentacleCeilingAttack();
            m_chasingGroundAttack.HasDamageTarget += chasingGroundAttack_HasDamageTarget;
            yield return m_theOneThirdFormAttacks.ChasingGroundBlast();
            yield return new WaitForSeconds(3f);
            for (int i = 0; i < m_mouthBlastTwo.Length; i++)
            {
                m_mouthBlastTwo[i].HasDamageTarget += mouthBlastTwo_HasDamageTarget;
            }
            yield return m_theOneThirdFormAttacks.TentacleCeilingAttackRetract();
            yield return m_theOneThirdFormAttacks.MouthBlastWall();
            yield return m_theOneThirdFormAttacks.ChasingGroundBlast();
            yield return new WaitForSeconds(3f);
            m_mouthBlastOne.HasDamageTarget += mouthBlastOne_HasDamageTarget;
            yield return m_theOneThirdFormAttacks.MouthBlastOneCeiling();
            m_mouthBlastOne.HasDamageTarget -= mouthBlastOne_HasDamageTarget;
            m_chasingGroundAttack.HasDamageTarget -= chasingGroundAttack_HasDamageTarget;
            for (int i = 0; i < m_mouthBlastTwo.Length; i++)
            {
                m_mouthBlastTwo[i].HasDamageTarget -= mouthBlastTwo_HasDamageTarget;
            }
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
        }
        private IEnumerator ChasingGroundBlast(float cooldown)
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            m_chasingGroundAttack.HasDamageTarget += chasingGroundAttack_HasDamageTarget;
            yield return m_theOneThirdFormAttacks.ChasingGroundBlast();
            yield return new WaitForSeconds(cooldown);
            m_chasingGroundAttack.HasDamageTarget -= chasingGroundAttack_HasDamageTarget;
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
        }

        private void chasingGroundAttack_HasDamageTarget(object sender, EventActionArgs eventArgs)
        {
            LockPlayerQuickItem?.Invoke(this, EventActionArgs.Empty);
            Debug.Log(" Event is eventing sir in the one AI scipt");
        }

        private IEnumerator ChasingGroundBlastPhaseThree(float cooldown)
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            var obstacleCheck = m_obstacleChecker.monolithSlamObstacleList.Count;
            if (obstacleCheck > 0)
            {
                m_attackDecider.hasDecidedOnAttack = false;
                m_stateHandle.ApplyQueuedState();
            }
            else
            {
                var choice = RandomShit(1, 3);
                Debug.Log(choice);
                for (int i = 0; i < m_mouthBlastTwo.Length; i++)
                {
                    m_mouthBlastTwo[i].HasDamageTarget += mouthBlastTwo_HasDamageTarget;
                }
                m_chasingGroundAttack.HasDamageTarget += chasingGroundAttack_HasDamageTarget;
                StartCoroutine(m_theOneThirdFormAttacks.ChasingGroundBlast());
                yield return m_theOneThirdFormAttacks.MouthBlastWall();
                if (choice == 1)
                {
                    yield return m_theOneThirdFormAttacks.MouthBlastWall();
                    yield return new WaitForSeconds(cooldown);
                    m_attackDecider.hasDecidedOnAttack = false;
                    m_stateHandle.ApplyQueuedState();
                }
                else
                {
                    m_attackDecider.hasDecidedOnAttack = false;
                    m_stateHandle.ApplyQueuedState();
                }
                m_chasingGroundAttack.HasDamageTarget -= chasingGroundAttack_HasDamageTarget;
                for (int i = 0; i < m_mouthBlastTwo.Length; i++)
                {
                    m_mouthBlastTwo[i].HasDamageTarget -= mouthBlastTwo_HasDamageTarget;
                }
            }



        }
        private IEnumerator ChasingGroundBlastWithMouthBlastTwo()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            //m_animation.SetAnimation(0, m_info.idleAnimation, true);
            m_chasingGroundAttack.HasDamageTarget += chasingGroundAttack_HasDamageTarget;
            for (int i = 0; i < m_mouthBlastTwo.Length; i++)
            {
                m_mouthBlastTwo[i].HasDamageTarget += mouthBlastTwo_HasDamageTarget;
            }
            yield return m_theOneThirdFormAttacks.TentacleCeilingAttack();
            yield return m_theOneThirdFormAttacks.ChasingGroundBlast();
            yield return new WaitForSeconds(3f);
            yield return m_theOneThirdFormAttacks.TentacleCeilingAttackRetract();
            yield return MouthBlastWall(1f);
            // yield return m_theOneThirdFormAttacks            
            yield return new WaitForSeconds(3f);
            var randomShit = RandomShit(0, 2);
            if (randomShit == 0)
            {
                yield return MouthBlastWall(1f);
            }
            else
            {
                yield return m_theOneThirdFormAttacks.ChasingGroundBlast();
            }
            for (int i = 0; i < m_mouthBlastTwo.Length; i++)
            {
                m_mouthBlastTwo[i].HasDamageTarget -= mouthBlastTwo_HasDamageTarget;
            }
            m_chasingGroundAttack.HasDamageTarget -= chasingGroundAttack_HasDamageTarget;
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
        }
        [SerializeField]
        private MouthBlastIIAttack m_mouthBlastOne;
        private IEnumerator MouthblastOneAttack(float cooldown)
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            var randomShit = RandomShit(1, 3);
            m_mouthBlastOne.HasDamageTarget += mouthBlastOne_HasDamageTarget;
            m_sphereBombAttack.HasDamageTarget += sphereBombAttack_HasDamageTarget;
            m_sphereBombAttack.HasDamageTargetSmallBomb += sphereBombAttackSmall_HasDamageTarget;
            yield return m_theOneThirdFormAttacks.MouthBlastOneCeiling();
            if (randomShit == 1)
            {
                yield return m_theOneThirdFormAttacks.SphereBombOneAttack();
            }
            else
            {
                yield return ScriptedTentacleGrab(cooldown);
            }
            m_sphereBombAttack.HasDamageTarget -= sphereBombAttack_HasDamageTarget;
            m_sphereBombAttack.HasDamageTargetSmallBomb -= sphereBombAttackSmall_HasDamageTarget;
            m_mouthBlastOne.HasDamageTarget -= mouthBlastOne_HasDamageTarget;
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
        }
           private void mouthBlastOne_HasDamageTarget(object sender, EventActionArgs eventArgs)
        {
            LockPlayerQuickItem?.Invoke(this, EventActionArgs.Empty);
            Debug.Log("Hit by ceiling mouth blast");
        }

        private IEnumerator SlidingWallAttack()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            //m_animation.SetAnimation(0, m_info.idleAnimation, true);
            yield return m_theOneThirdFormAttacks.SlidingStoneWallAttack(m_targetInfo);
            Debug.Log("wew");
            yield return new WaitForSeconds(3f);
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
        }

        private IEnumerator TentacleGrabberSwipeAndWallSlam(float cooldown)
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            yield return ScriptedTentacleGrab(cooldown);
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();

        }

        private IEnumerator SphereBombTwoPhaseFive()
        {

            m_stateHandle.Wait(State.ReevaluateSituation);
            m_sphereBombList.Clear();
            for (int i = 0; i < 2; i++)
            {
                Debug.Log(i + " number of re iteration");
                yield return SphereBombSetSpawning();
            }
            yield return new WaitForSeconds(3f);
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
        }


        private IEnumerator SphereBombSetSpawning()
        {
            for (int i = 0; i < m_projectilePoint.Length; i++)
            {
                var instance = GameSystem.poolManager.GetPool<ProjectilePool>().GetOrCreateItem(m_info.sphereBomb.projectile);
                instance.SpawnAt(new Vector2(m_projectilePoint[i].position.x, m_projectilePoint[i].position.y), Quaternion.identity);
                m_sphereBombList.Add(instance);
                instance.GetComponent<Attacker>().TargetDamaged -= TheOneThirdFormAI_TargetDamaged;
            }
            yield return new WaitForSeconds(9f);
            List<Projectile> shuffledProjectiles = new List<Projectile>(m_sphereBombList);
            shuffledProjectiles = shuffledProjectiles.OrderBy(x => Random.value).ToList();

            for (int i = 0; i < shuffledProjectiles.Count; i++)
            {
                if (i >= shuffledProjectiles.Count) break;
                var projectile = shuffledProjectiles[i];
                projectile.GetComponent<Attacker>().TargetDamaged += TheOneThirdFormAI_TargetDamaged;
                Vector2 launchPosition = projectile.transform.position;
                Vector2 toTarget = (m_targetInfo.position - launchPosition).normalized;
                projectile.Launch(toTarget, m_info.sphereBomb.speed);
                projectile.GetComponent<Collider2D>().enabled = true;
                
                yield return new WaitForSeconds(1f);
            }
            m_sphereBombList.Clear();
        }

        private void TheOneThirdFormAI_TargetDamaged(object sender, CombatConclusionEventArgs eventArgs)
        {
            LockPlayerQuickItem?.Invoke(this, EventActionArgs.Empty);
            Debug.Log("Got hit by sphere bomb two phase five");
        }

        [SerializeField]
        private SphereBombAttack m_sphereBombAttack;
        [SerializeField]
        private SphereBomb m_sphereBombAttackSmall;
        private IEnumerator SphereBombPhaseFour()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            // m_animation.SetAnimation(0, m_info.idleAnimation, true);
            m_sphereBombAttack.HasDamageTarget += sphereBombAttack_HasDamageTarget;
            m_sphereBombAttack.HasDamageTargetSmallBomb += sphereBombAttackSmall_HasDamageTarget;
            for (int i = 0; i < m_tentacleBlast.Length; i++)
            {
                m_tentacleBlast[i].HasDamageTarget += tentacleBlast_HasDamageTarget;
            }
            yield return m_theOneThirdFormAttacks.SphereBombOneAttack();
            yield return new WaitForSeconds(3f);
            var randomAttack = RandomShit(1, 3);
            if(randomAttack == 1)
            {
                yield return m_theOneThirdFormAttacks.TentacleBlastTwo();
            }
            for (int i = 0; i < m_tentacleBlast.Length; i++)
            {
                m_tentacleBlast[i].HasDamageTarget -= tentacleBlast_HasDamageTarget;
            }
            m_sphereBombAttack.HasDamageTarget -= sphereBombAttack_HasDamageTarget;
            m_sphereBombAttack.HasDamageTargetSmallBomb -= sphereBombAttackSmall_HasDamageTarget;
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
        }

        private void sphereBombAttackSmall_HasDamageTarget(object sender, EventActionArgs eventArgs)
        {
            LockPlayerQuickItem?.Invoke(this, EventActionArgs.Empty);
            Debug.Log("Event is eventing in the one ai script for sphere bomb small attack");
        }

        private void sphereBombAttack_HasDamageTarget(object sender, EventActionArgs eventArgs)
        {
           LockPlayerQuickItem?.Invoke(this, EventActionArgs.Empty);
            Debug.Log("Event is eventing in the one ai script for sphere bomb attack");
        }

        private IEnumerator SphereBombOnePhaseFive()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            //m_animation.SetAnimation(0, m_info.idleAnimation, true);
            var randomShit = RandomShit(1,3);
            m_sphereBombAttack.HasDamageTarget += sphereBombAttack_HasDamageTarget;
            m_sphereBombAttack.HasDamageTargetSmallBomb += sphereBombAttackSmall_HasDamageTarget;
            yield return m_theOneThirdFormAttacks.SphereBombOneAttack();
            yield return new WaitForSeconds(6f);
            if (randomShit == 1)
            {
                for (int i = 0; i < m_tentacleBlast.Length; i++)
                {
                    m_tentacleBlast[i].HasDamageTarget += tentacleBlast_HasDamageTarget;
                }
                yield return m_theOneThirdFormAttacks.TentacleBlastTwo();
                yield return new WaitForSeconds(3f);
                Debug.Log("skip part?");
                m_removeTentacleBlastAttacks = true;
                for (int i = 0; i < m_tentacleBlast.Length; i++)
                {
                    m_tentacleBlast[i].HasDamageTarget -= tentacleBlast_HasDamageTarget;
                }
                m_attackDecider.hasDecidedOnAttack = false;
                m_stateHandle.ApplyQueuedState();
            }
            else
            {

                m_attackDecider.hasDecidedOnAttack = false;
                m_stateHandle.ApplyQueuedState();
            }

        }
        private IEnumerator ScriptedTentacleGrab(float cooldown)
        {
            //if (!m_targetInfo.isCharacterGrounded)
            //{
            //    yield return null;  
            //}

            yield return m_theOneThirdFormAttacks.TentacleGrab();
            yield return new WaitForSeconds(cooldown);
            //Temporary
        }

        private IEnumerator BubbleImprisonmentAttackPhaseFour()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
           // m_animation.SetAnimation(0, m_info.idleAnimation, true);
            var obstacleCheck = m_obstacleChecker.monolithSlamObstacleList.Count;
            if (obstacleCheck > 0)
            {
                m_attackDecider.hasDecidedOnAttack = false;
                m_stateHandle.ApplyQueuedState();
                yield break;
            }
            else
            {
                var randomAttack = RandomShit(1, 2);
                yield return m_theOneThirdFormAttacks.BubbleImprisonment(m_targetInfo);
                yield return new WaitForSeconds(0.5f);
                if (randomAttack == 1)
                {
                    yield return m_theOneThirdFormAttacks.BubbleImprisonment(m_targetInfo);
                }
                else
                {
                    m_attackDecider.hasDecidedOnAttack = false;
                    m_stateHandle.ApplyQueuedState();
                }
                m_attackDecider.hasDecidedOnAttack = false;
                m_stateHandle.ApplyQueuedState();
            }

            
        }

        private IEnumerator BubbleImprisonmentAttack(float cooldown)
        {

            m_stateHandle.Wait(State.ReevaluateSituation);
           // m_animation.SetAnimation(0, m_info.idleAnimation, true);
            yield return m_theOneThirdFormAttacks.BubbleImprisonment(m_targetInfo);
            yield return new WaitForSeconds(cooldown);
            m_attackDecider.hasDecidedOnAttack = false;
            m_stateHandle.ApplyQueuedState();
        }
        private IEnumerator BubbleImprisonment(float cooldown)
        {
            m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.BubbleImprisonment(m_targetInfo));
            yield return new WaitForSeconds(cooldown);
            //Temporary
            m_attackDecider.hasDecidedOnAttack = false;
            m_currentAttackCoroutine = null;
            m_stateHandle.ApplyQueuedState();
        }

        private int RandomShit(int minValue, int maxValue)
        {
            // Ensure minValue is less than maxValue
            if (minValue >= maxValue)
            {
                throw new System.ArgumentException("minValue must be less than maxValue");
            }

            return Random.Range(minValue, maxValue); // Range in Unity is [minValue, maxValue-1] for integers
        }

 
        private void EyeTracker()
        {
            if (m_targetInfo == null || m_eyeTheOne == null) return;
            Vector2 direction = (m_targetInfo.position - m_eyeCenter).normalized;
            Vector2 targetPosition = m_eyeCenter + (direction * Mathf.Min(Vector2.Distance(m_targetInfo.position, m_eyeCenter), m_maxDistance));
            m_eyeTheOne.position = Vector2.Lerp(m_eyeTheOne.position, targetPosition, Time.deltaTime * 5f);
        }
        #endregion


        private IEnumerator ExhaustedState()
        {
            m_animation.SetAnimation(0, m_info.exhaustedAnimation, true);
            yield return new WaitForSeconds(5f);
            m_animation.SetAnimation(0, m_info.exhaustedToIdleAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState,m_info.exhaustedToIdleAnimation);
            Debug.Log("ExhaustedState");
            yield return new WaitForSeconds(2f);
        }
        private IEnumerator SquintState()
        {
            m_inSquintStateAlready = true;
            m_theOneHitbox.SetActive(false);
            m_animation.SetAnimation(0, m_info.eyeSquintAnimation, true);
            yield return new WaitForSeconds(m_eyeTimerToOpenFromSquint);
            m_animation.SetAnimation(0, m_info.unsquintAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.unsquintAnimation);
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            m_damageable.DamageTaken += M_damageable_DamageTaken;
            m_theOneHitbox.SetActive(true);
            m_maxDistance = m_storeMaxDistance;
            m_inSquintStateAlready = false;
           
            
           
        }

        void Update()
        {
            m_phaseHandle.MonitorPhase();
            EyeTracker();
            switch (m_stateHandle.currentState)
            {
                case State.Idle:
                    if (m_inSquintStateAlready == false)
                    {
                        m_animation.SetAnimation(0, m_info.idleAnimation, true);
                    }
                    break;
                case State.Intro:
                    StartCoroutine(IntroRoutine());
                    break;
                case State.Phasing:
                    //StopAllCoroutines();
                    Debug.Log("State Changing Phase");
                    StartCoroutine(ChangePhaseRoutine());
                    break;
                case State.Attacking:
                    //StopAllCoroutines();
                    m_stateHandle.Wait(State.ReevaluateSituation);
                    m_lastTargetPos = m_targetInfo.position;       
                    if (m_inSquintStateAlready == false)
                    {
                        m_animation.SetAnimation(0, m_info.idleAnimation, true);
                    }
                    
                    if (m_removeTentacleBlastAttacks == true)
                    {
                        UpdateAttackDeciderListTentacleBlast();
                    }
                    else
                    {
                        UpdateAttackDeciderList();
                    }

                    if (m_attackDecider.hasDecidedOnAttack == false)
                    {

                        m_attackDecider.DecideOnAttack();
                    }

                    switch (m_attackDecider.chosenAttack.attack)
                    {
                        case Attack.TentacleGroundStab1:
                            StartCoroutine(TentacleGroundStabAttack1());
                            Debug.Log("is in tentaclegroundstab");
                            break;
                        case Attack.TentacleGroundStab2:
                            StartCoroutine(TentacleGroundStabAttack2());
                            Debug.Log("is in tentaclegroundstab2");
                            break;
                        case Attack.TentacleBlast1:
                            StartCoroutine(TentacleBlastOne(1f));
                            break;
                        case Attack.MonolithSlamPhase1:
                            StartCoroutine(MonolithSlamPhase1Attack(5f));
                            break;
                        case Attack.BubbleImprisonment:
                            StartCoroutine(BubbleImprisonmentAttack(5f));
                            break;
                        case Attack.TentacleGroundStab1AndCeiling:
                            StartCoroutine(TentacleGroundStabCeilingAttack());
                            break;
                        case Attack.ChasingGroundBlast:
                            StartCoroutine(ChasingGroundBlast(2f));
                            //to be added
                            break;
                        case Attack.TentacleBlast2:
                            StartCoroutine(TentacleBlastTwoAttack(1f));
                            break;
                        case Attack.MonolithSlamPhase2:
                            StartCoroutine(MonolithSlamPhase2Attack(1f));
                            break;
                        case Attack.MouthBlast2:
                            StartCoroutine(MouthBlastTwoWallAttack(2f));
                            break;
                        case Attack.TentacleStab1AndCeilingPhase3:
                            StartCoroutine(TentacleGroundStabCeilingAttackPhase3());
                            break;
                        case Attack.ChasingGroundBlastPhaseTree:
                            StartCoroutine(ChasingGroundBlastPhaseThree(1));
                            break;
                        case Attack.MonolithSlamPhase3:
                            StartCoroutine(MonolithSlamPhase3Attack(1f));
                            break;
                        case Attack.GrabberSwipeAndWallSlam:
                            StartCoroutine(TentacleGrabberSwipeAndWallSlam(3f));
                            break;
                        case Attack.SlidingStoneWall:
                            StartCoroutine(SlidingWallAttack());
                            break;
                        case Attack.ChasingGroundBlastAndMouthBlast2:
                            StartCoroutine(ChasingGroundBlastWithMouthBlastTwo());
                            break;
                        case Attack.TentacleBlast2PhaseFour:
                            StartCoroutine(TentacleBlastTwoPhase4());
                            break;
                        case Attack.MouthBlastCeiling1:
                            StartCoroutine(MouthblastOneAttack(3f));
                            break;
                        case Attack.SphereBomb:
                            StartCoroutine(SphereBombPhaseFour());
                            break;
                        case Attack.BubbleImprisonmentPhaseFour:
                            StartCoroutine(BubbleImprisonmentAttackPhaseFour());
                            break;
                        case Attack.ChasingGroundBlastMouthBlast2AndMouthBlast1:
                            StartCoroutine(ChasingGroundBlastMouthBlast2AndMouthBlast1());
                            break;
                        case Attack.MouthBlast1And2:
                            StartCoroutine(MouthBlastOneAndTwo());
                            break;
                        case Attack.SphereBomb1:
                            StartCoroutine(SphereBombOnePhaseFive());
                            break;
                        case Attack.SphereBomb2:
                            StartCoroutine(SphereBombTwoPhaseFive());
                            break;
                        case Attack.WaitAttackEnd:
                            break;
                    }
                    #region OldAttackSwitchCase
                    //Debug.Log("CURRENT ATTACK PATTERN " + m_currentAttack);
                    //switch (m_currentAttack)
                    //{
                    //    case Attack.Phase1Pattern1:

                    //        Debug.Log("Tentacle Stab Attack");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.TentacleGroundStab(m_targetInfo));

                    //        break;
                    //    case Attack.Phase1Pattern2:

                    //        Debug.Log("Chasing Ground Tentacle");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.ChasingGroundTentacle());

                    //        break;
                    //    case Attack.Phase1Pattern3:

                    //        Debug.Log("TENTACLE BLAST I ATTACK");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.TentacleBlastOne(m_targetInfo));

                    //        break;
                    //    case Attack.Phase1Pattern4:
                    //        m_pickedCooldown = m_currentFullCooldown[3];

                    //        Debug.Log("MONILITH SLAM ATTACK");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.MonolithSlam(m_targetInfo));

                    //        break;
                    //    case Attack.Phase1Pattern5:

                    //        Debug.Log("MOUTH BLAST WALL");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.MouthBlastWall());

                    //        break;
                    //    case Attack.Phase1Pattern6:

                    //        Debug.Log("Sliding Stone Wall");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.SlidingStoneWallAttack(m_targetInfo));

                    //        break;
                    //    case Attack.Phase2Pattern1:

                    //        Debug.Log("TENTACLE GROUND STAB");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.TentacleGroundStab(m_targetInfo));

                    //        break;
                    //    case Attack.Phase2Pattern2:

                    //        Debug.Log("CHASING GROUND TENTACLE");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.ChasingGroundTentacle());

                    //        break;
                    //    case Attack.Phase2Pattern3:

                    //        Debug.Log("TENTACLE BLAST II ATTACK");

                    //        m_currentAttackCoroutine = m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.TentacleBlastTwo());

                    //        break;
                    //    case Attack.Phase2Pattern4:

                    //        Debug.Log("MONOLITH SLAM ATTACK");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.MonolithSlam(m_targetInfo));

                    //        break;
                    //    case Attack.Phase2Pattern5:

                    //        Debug.Log("Ground Tentacle Attack");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.MovingTentacleGround());

                    //        break;
                    //    case Attack.Phase2Pattern6:

                    //        Debug.Log("TENTACLE CEILING");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.TentacleCeilingAttack());

                    //        break;
                    //    case Attack.Phase2Pattern7:

                    //        Debug.Log("MOUTH BLAST WALL");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.MouthBlastWall());

                    //        break;
                    //    case Attack.Phase3Pattern1:

                    //        Debug.Log("TENTACLE GROUND STAB");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.TentacleGroundStab(m_targetInfo));

                    //        break;
                    //    case Attack.Phase3Pattern2:

                    //        Debug.Log("Ground Tentacle Attack");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.ChasingGroundTentacle());

                    //        break;
                    //    case Attack.Phase3Pattern3:

                    //        Debug.Log("Sliding Stone Wall");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.SlidingStoneWallAttack(m_targetInfo));

                    //        break;
                    //    case Attack.Phase3Pattern4:
                    //        Debug.Log("Mouth Blast I");

                    //        m_currentAttackCoroutine = StartCoroutine(FullMouthBlastOneSequence());

                    //        break;
                    //    case Attack.Phase3Pattern5:

                    //        Debug.Log("MOUTH BLAST WALL");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.MouthBlastWall());

                    //        break;
                    //    case Attack.Phase3Pattern6:

                    //        Debug.Log("TENTACLE CEILING");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.TentacleCeilingAttack());

                    //        break;
                    //    case Attack.Phase3Pattern7:

                    //        Debug.Log("BUBBLE IMPRISONMENT ATTACK");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.BubbleImprisonment(m_targetInfo));

                    //        break;
                    //    case Attack.Phase4Pattern1:

                    //        Debug.Log("TENTACLE GROUND STAB ATTACK");

                    //        //m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.TentacleGroundStab(m_targetInfo));

                    //        m_attackDecider.hasDecidedOnAttack = false;
                    //        m_currentAttackCoroutine = null;
                    //        m_stateHandle.ApplyQueuedState();
                    //        break;
                    //    case Attack.Phase4Pattern2:

                    //        Debug.Log("Ground Tentacle Attack");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.ChasingGroundTentacle());

                    //        break;
                    //    case Attack.Phase4Pattern3:

                    //        Debug.Log("TENTACLE BLAST II ATTACK");

                    //        m_currentAttackCoroutine = m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.TentacleBlastTwo());

                    //        break;
                    //    case Attack.Phase4Pattern4:

                    //        Debug.Log("MONOLITH SLAM ATTACK");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.MonolithSlam(m_targetInfo));

                    //        break;
                    //    case Attack.Phase4Pattern5:

                    //        Debug.Log("MOUTH BLAST II ATTACK");

                    //        //m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.MouthBlastWall());

                    //        m_attackDecider.hasDecidedOnAttack = false;
                    //        m_currentAttackCoroutine = null;
                    //        m_stateHandle.ApplyQueuedState();
                    //        break;
                    //    case Attack.Phase4Pattern6:

                    //        Debug.Log("Tentacle Ceiling Attack");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.TentacleCeilingAttack());

                    //        break;
                    //    case Attack.Phase4Pattern7:

                    //        Debug.Log("BUBBLE IMPRISONMENT ATTACK");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.BubbleImprisonment(m_targetInfo));

                    //        break;
                    //    case Attack.Phase4Pattern8:

                    //        Debug.Log("GRABBER SWIPE + WALL SLAM ATTACK");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.TentacleGrab());

                    //        break;
                    //    case Attack.Phase4Pattern9:

                    //        Debug.Log("Sliding Stone Wall");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.SlidingStoneWallAttack(m_targetInfo));

                    //        break;
                    //    case Attack.Phase4Pattern10:

                    //        Debug.Log("Ground Tentacle Attack");

                    //        //m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.MovingTentacleGround());

                    //        m_attackDecider.hasDecidedOnAttack = false;
                    //        m_currentAttackCoroutine = null;
                    //        m_stateHandle.ApplyQueuedState();
                    //        break;
                    //    case Attack.Phase4Pattern11:

                    //        Debug.Log("Mouth Blast I");

                    //        m_currentAttackCoroutine = StartCoroutine(FullMouthBlastOneSequence());

                    //        break;
                    //    case Attack.Phase5Pattern1:

                    //        Debug.Log("Tentacle Stab Attack");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.TentacleGroundStab(m_targetInfo));

                    //        break;
                    //    case Attack.Phase5Pattern2:

                    //        Debug.Log("TENTACLE GARDEN / CHASING GROUND TENTACLE ATTACK");

                    //        //m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.ChasingGroundTentacle());

                    //        m_attackDecider.hasDecidedOnAttack = false;
                    //        m_currentAttackCoroutine = null;
                    //        m_stateHandle.ApplyQueuedState();
                    //        break;
                    //    case Attack.Phase5Pattern3:

                    //        Debug.Log("TENTACLE BLAST II ATTACK");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.TentacleBlastTwo());

                    //        break;
                    //    case Attack.Phase5Pattern4:

                    //        Debug.Log("MONOLITH SLAM ATTACK");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.MonolithSlam(m_targetInfo));

                    //        break;
                    //    case Attack.Phase5Pattern5:

                    //        Debug.Log("MOUTH BLAST ATTACK");

                    //        //m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.MouthBlastWall());

                    //        m_attackDecider.hasDecidedOnAttack = false;
                    //        m_currentAttackCoroutine = null;
                    //        m_stateHandle.ApplyQueuedState();
                    //        break;
                    //    case Attack.Phase5Pattern6:

                    //        Debug.Log("TENTACLE CEILING");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.TentacleCeilingAttack());

                    //        break;
                    //    case Attack.Phase5Pattern7:

                    //        Debug.Log("BUBBLE IMPRISONMENT ATTACK");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.BubbleImprisonment(m_targetInfo));

                    //        break;
                    //    case Attack.Phase5Pattern8:

                    //        Debug.Log("GRABBER SWIPE + WALL SLAM ATTACK");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.TentacleGrab());

                    //        break;
                    //    case Attack.Phase5Pattern9:

                    //        Debug.Log("Sliding Stone Wall");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.SlidingStoneWallAttack(m_targetInfo));

                    //        //m_attackDecider.hasDecidedOnAttack = false;
                    //        //m_currentAttackCoroutine = null;
                    //        //m_stateHandle.ApplyQueuedState();
                    //        break;
                    //    case Attack.Phase5Pattern10:

                    //        Debug.Log("Ground Tentacle Attack");

                    //        m_currentAttackCoroutine = StartCoroutine(m_theOneThirdFormAttacks.MovingTentacleGround());

                    //        //m_attackDecider.hasDecidedOnAttack = false;
                    //        //m_currentAttackCoroutine = null;
                    //        //m_stateHandle.ApplyQueuedState();
                    //        break;
                    //    case Attack.Phase5Pattern11:

                    //        Debug.Log("Mouth Blast I");

                    //        m_currentAttackCoroutine = StartCoroutine(FullMouthBlastOneSequence());

                    //        //m_attackDecider.hasDecidedOnAttack = false;
                    //        //m_currentAttackCoroutine = null;
                    //        //m_stateHandle.ApplyQueuedState();
                    //        break;
                    //}

                    #endregion

                    break;


                #region gayniggas
                //case State.Cooldown:
                //    //m_animation.SetAnimation(0, m_idleAnimation, true).TimeScale = 1;
                //    StopAllCoroutines();
                //    StartCoroutine(ExhaustedState());

                //    break;
                #endregion


                case State.Chasing:
                    m_stateHandle.SetState(State.Attacking);
                    //if (!m_hitbox.canBlockDamage)
                    //{
                    //    ChooseAttack();   
                    //    if (/*IsTargetInRange(m_currentAttackRange) &&*/ m_currentAttackCoroutine == null)
                    //    {
                    //        m_stateHandle.SetState(State.Attacking);
                    //    }
                    //}
                    break;

                case State.ReevaluateSituation:
                    if (m_targetInfo.isValid)
                    {
                        m_stateHandle.SetState(State.Chasing);
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

     
        protected override void OnForbidFromAttackTarget()
        {

        }

        protected override void OnTargetDisappeared()
        {

        }

        public override void ReturnToSpawnPoint()
        {

        }
    }
}

