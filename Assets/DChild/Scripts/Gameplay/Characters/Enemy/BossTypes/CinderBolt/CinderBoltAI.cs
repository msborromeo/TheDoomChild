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
using Spine.Unity.Examples;
using DChild.Gameplay.Pooling;
using UnityEngine.Playables;
using DChild.Gameplay.Projectiles;
using Sirenix.Serialization;
using DChild.Gameplay.Cinematics;
using System.Data.SqlTypes;

namespace DChild.Gameplay.Characters.Enemies
{
    [AddComponentMenu("DChild/Gameplay/Enemies/Boss/CinderBolt")]
    public class CinderBoltAI : CombatAIBrain<CinderBoltAI.Info>
    {
        [System.Serializable]
        public class Info : BaseInfo
        {
            #region Animations
            [SerializeField]
            private PhaseInfo<Phase> m_phaseInfo;
            public PhaseInfo<Phase> phaseInfo => m_phaseInfo;

            [SerializeField, BoxGroup("Movement")]
            private MovementInfo m_move = new MovementInfo();
            public MovementInfo move => m_move;
            [SerializeField, BoxGroup("Movement")]
            private MovementInfo m_overchargedMove = new MovementInfo();
            public MovementInfo overchargedMove => m_overchargedMove;

            [SerializeField, BoxGroup("Movement"), ValueDropdown("GetAnimations")]
            private string m_moveTurnAnimation;
            public string moveTurnAnimation => m_moveTurnAnimation;

            [SerializeField, Title("Hover"), BoxGroup("Movement")]
            private MovementInfo m_hoverUpward = new MovementInfo();
            public MovementInfo hoverUpward => m_hoverUpward;
            [SerializeField, Title("Hover"), BoxGroup("Movement")]
            private MovementInfo m_overchargedHoverUpward = new MovementInfo();
            public MovementInfo overchargedHoverUpward => m_overchargedHoverUpward;
            [SerializeField, Title("Hover"), BoxGroup("Movement")]
            private MovementInfo m_hoverBackward = new MovementInfo();
            public MovementInfo hoverBackward => m_hoverBackward;
            [SerializeField, Title("Hover"), BoxGroup("Movement")]
            private MovementInfo m_overchargedHoverBackward = new MovementInfo();
            public MovementInfo overchargedHoverBackward => m_overchargedHoverBackward;
            [SerializeField, Title("Hover"), BoxGroup("Movement")]
            private MovementInfo m_hoverDownward = new MovementInfo();
            public MovementInfo hoverDownward => m_hoverDownward;
            [SerializeField, Title("Hover"), BoxGroup("Movement")]
            private MovementInfo m_overchargedHoverDownward = new MovementInfo();
            public MovementInfo overchargedHoverDownward => m_overchargedHoverDownward;
            [SerializeField, Title("Hover"), BoxGroup("Movement")]
            private MovementInfo m_hoverForward = new MovementInfo();
            public MovementInfo hoverForward => m_hoverForward;
            [SerializeField, Title("Hover"), BoxGroup("Movement")]
            private MovementInfo m_overchargedHoverForward = new MovementInfo();
            public MovementInfo overchargedHoverForward => m_overchargedHoverForward;
            [SerializeField, Title("Long Dash"), BoxGroup("Movement")]
            private MovementInfo m_longDash = new MovementInfo();
            public MovementInfo longDash => m_longDash;
            [SerializeField, Title("Long Dash"), BoxGroup("Movement")]
            private MovementInfo m_overchargedLongDash = new MovementInfo();
            public MovementInfo overchargedLongDash => m_overchargedLongDash;
            [SerializeField, Title("Long Dash"), BoxGroup("Movement"), ValueDropdown("GetAnimations")]
            private string m_longDashBoosterChargeAnimation;
            public string longDashBoosterChargeAnimation => m_longDashBoosterChargeAnimation;
            [SerializeField, Title("Long Dash"), BoxGroup("Movement"), ValueDropdown("GetAnimations")]
            private string m_overchargedLongDashBoosterChargeAnimation;
            public string overchargedLongDashBoosterChargeAnimation => m_overchargedLongDashBoosterChargeAnimation;
            [SerializeField, Title("Long Dash"), BoxGroup("Movement"), ValueDropdown("GetAnimations")]
            private string m_longDashStopAnimation;
            public string longDashStopAnimation => m_longDashStopAnimation;
            /*[SerializeField, Title("Overcharged Long Dash"), BoxGroup("Movement"), ValueDropdown("GetAnimations")]
            private string m_overchargedLongDashStopAnimation;
            public string overchargedLongDashStopAnimation => m_overchargedLongDashStopAnimation;*/
            [SerializeField, Title("Short Dash"), BoxGroup("Movement")]
            private MovementInfo m_shortDash = new MovementInfo();
            public MovementInfo shortDash => m_shortDash;
            [SerializeField, Title("Short Dash"), BoxGroup("Movement")]
            private MovementInfo m_overchargedShortDash = new MovementInfo();
            public MovementInfo overchargedShortDash => m_overchargedShortDash;

            [SerializeField, Title("Straight Left and Uppercut"), BoxGroup("Attack")]
            private SimpleAttackInfo m_straightLeftAndUppercutAttack = new SimpleAttackInfo();
            public SimpleAttackInfo straightLeftAndUppercutAttack => m_straightLeftAndUppercutAttack;
            [SerializeField, Title("Straight Left and Uppercut"), BoxGroup("Attack")]
            private SimpleAttackInfo m_overchargedStraightLeftAndUppercutAttack = new SimpleAttackInfo();
            public SimpleAttackInfo overchargedStraightLeftAndUppercutAttack => m_overchargedStraightLeftAndUppercutAttack;
            [SerializeField, Title("Flame Thrower"), BoxGroup("Attack")]
            private SimpleAttackInfo m_flameThrowerAttack = new SimpleAttackInfo();
            public SimpleAttackInfo flameThrowerAttack => m_flameThrowerAttack;
            [SerializeField, Title("Flame Thrower"), BoxGroup("Attack")]
            private SimpleAttackInfo m_overchargedFlameThrowerAttack = new SimpleAttackInfo();
            public SimpleAttackInfo overchargedFlameThrowerAttack => m_overchargedFlameThrowerAttack;
            [SerializeField, Title("Flame Beam"), BoxGroup("Attack")]
            private SimpleAttackInfo m_flameBeamAttack = new SimpleAttackInfo();
            public SimpleAttackInfo flameBeamAttack => m_flameBeamAttack;
            [SerializeField, Title("Flame Beam"), BoxGroup("Attack")]
            private SimpleAttackInfo m_overchargedFlameBeamAttack = new SimpleAttackInfo();
            [SerializeField, Title("Long Dash"), BoxGroup("Attack")]
            private SimpleAttackInfo m_longDashAttack = new SimpleAttackInfo();
            public SimpleAttackInfo longDashAttack => m_longDashAttack;
            [SerializeField, Title("Long Dash"), BoxGroup("Attack")]
            private SimpleAttackInfo m_overchargedLongDashAttack = new SimpleAttackInfo();
            public SimpleAttackInfo overchargedLongDashAttack => m_overchargedLongDashAttack;
            public SimpleAttackInfo overchargedFlameBeamAttack => m_overchargedFlameBeamAttack;
            [SerializeField, Title("Punch"), BoxGroup("Attack")]
            private SimpleAttackInfo m_punchAttack = new SimpleAttackInfo();
            public SimpleAttackInfo punchAttack => m_punchAttack;
            [SerializeField, Title("Punch"), BoxGroup("Attack")]
            private SimpleAttackInfo m_overchargedPunchAttack = new SimpleAttackInfo();
            public SimpleAttackInfo overchargedPunchAttack => m_overchargedPunchAttack;
            [SerializeField, Title("Shotgun Blast"), BoxGroup("Attack")]
            private SimpleAttackInfo m_shotgunBlastFireAttack = new SimpleAttackInfo();
            public SimpleAttackInfo shotgunBlastFireAttack => m_shotgunBlastFireAttack;
            [SerializeField, Title("Shotgun Blast"), BoxGroup("Attack")]
            private SimpleAttackInfo m_overchargedShotgunBlastFireAttack = new SimpleAttackInfo();
            public SimpleAttackInfo overchargedShotgunBlastFireAttack => m_overchargedShotgunBlastFireAttack;
            [SerializeField, Title("Shotgun Blast"), BoxGroup("Attack"), ValueDropdown("GetAnimations")]
            private string m_shotgunBlastBackToIdleAnimation;
            public string shotgunBlastBackToIdleAnimation => m_shotgunBlastBackToIdleAnimation;
            [SerializeField, Title("Shotgun Blast"), BoxGroup("Attack"), ValueDropdown("GetAnimations")]
            private string m_shotgunBlastPreAnimation;
            public string shotgunBlastPreAnimation => m_shotgunBlastPreAnimation;
            [SerializeField, Title("Shotgun Blast"), BoxGroup("Attack")]
            private SimpleAttackInfo m_shotgunBlastRapidFireAttack = new SimpleAttackInfo();
            public SimpleAttackInfo shotgunBlastRapidFireAttack => m_shotgunBlastRapidFireAttack;
            [SerializeField, Title("Shotgun Blast"), BoxGroup("Attack")]
            private SimpleAttackInfo m_overchargedShotgunBlastRapidFireAttack = new SimpleAttackInfo();
            public SimpleAttackInfo overchargedShotgunBlastRapidFireAttack => m_overchargedShotgunBlastRapidFireAttack;
            [SerializeField, Title("Uppercut"), BoxGroup("Attack")]
            private SimpleAttackInfo m_uppercutAttack = new SimpleAttackInfo();
            public SimpleAttackInfo uppercutAttack => m_uppercutAttack;
            [SerializeField, Title("Uppercut"), BoxGroup("Attack")]
            private SimpleAttackInfo m_overchargedUppercutAttack = new SimpleAttackInfo();
            public SimpleAttackInfo overchargedUppercutAttack => m_overchargedUppercutAttack;
            [SerializeField, Title("Firebeam"), BoxGroup("Attack")]
            private SimpleAttackInfo m_firebeamAttack = new SimpleAttackInfo();
            public SimpleAttackInfo firebeamAttack => m_firebeamAttack;
            [SerializeField, Title("Meteor Smash"), BoxGroup("Attack")]
            private SimpleAttackInfo m_meteorAttack = new SimpleAttackInfo();
            public SimpleAttackInfo meteorAttack => m_meteorAttack;
            [SerializeField, Title("Spin Attack"), BoxGroup("Attack")]
            private SimpleAttackInfo m_spinAttack = new SimpleAttackInfo();
            public SimpleAttackInfo spinAttack => m_spinAttack;
            [SerializeField, Title("Short Dash"), BoxGroup("Attack")]
            private SimpleAttackInfo m_shortDashAttack = new SimpleAttackInfo();
            public SimpleAttackInfo shortDashAttack => m_shortDashAttack;

            [SerializeField, Title("Overcharged Punch Uppercut"), BoxGroup("Overcharged Attack")]
            private SimpleAttackInfo m_overchargedPunchUppercutAttack = new SimpleAttackInfo();
            public SimpleAttackInfo overchargedPunchUppercutAttack => m_overchargedPunchUppercutAttack;
            [SerializeField, Title("Overcharged Flamethrower1"), BoxGroup("Overcharged Attack")]
            private SimpleAttackInfo m_overchargedFlamethrower1Attack = new SimpleAttackInfo();
            public SimpleAttackInfo overchargedFlamethrower1Attack => m_overchargedFlamethrower1Attack;
            [SerializeField, Title("Overcharged Pre-Spin"), BoxGroup("Overcharged Attack")]
            private SimpleAttackInfo m_overchargedPreSpinAttack = new SimpleAttackInfo();
            public SimpleAttackInfo overchargedPreSpinAttack => m_overchargedPreSpinAttack;
            [SerializeField, Title("Overcharged Spin"), BoxGroup("Overcharged Attack")]
            private SimpleAttackInfo m_overchargedSpinAttack = new SimpleAttackInfo();
            public SimpleAttackInfo overchargedSpinAttack => m_overchargedSpinAttack;
            [SerializeField, Title("Overcharged Post-Spin"), BoxGroup("Overcharged Attack")]
            private SimpleAttackInfo m_overchargedPostSpinAttack = new SimpleAttackInfo();
            public SimpleAttackInfo overchargedPostSpinAttack => m_overchargedPostSpinAttack;
            [SerializeField, Title("Overcharged Firebeam"), BoxGroup("Overcharged Attack")]
            private SimpleAttackInfo m_overchargedFirebeamAttack = new SimpleAttackInfo();
            public SimpleAttackInfo overchargedFirebeamAttack => m_overchargedFirebeamAttack;
            [SerializeField, Title("Shotgun Blast"), BoxGroup("Overcharged Attack")]
            private SimpleAttackInfo m_overchargedShotgunBlastPreAnimation = new SimpleAttackInfo();
            public SimpleAttackInfo overchargedShotgunBlastPreAnimation => m_overchargedShotgunBlastPreAnimation;
            [SerializeField, Title("Shotgun Blast"), BoxGroup("Overcharged Attack")]
            private SimpleAttackInfo m_overchargedShotgunBlastBackToIdleAnimation = new SimpleAttackInfo();
            public SimpleAttackInfo overchargedShotgunBlastBackToIdleAnimation => m_overchargedShotgunBlastBackToIdleAnimation;
            #endregion

            [SerializeField, TabGroup("Phase 1"), BoxGroup("Pattern Ranges")]
            private float m_phase1Pattern1Range;
            public float phase1Pattern1Range => m_phase1Pattern1Range;
            [SerializeField, TabGroup("Phase 1"), BoxGroup("Pattern Ranges")]
            private float m_phase1Pattern2Range;
            public float phase1Pattern2Range => m_phase1Pattern2Range;
            [SerializeField, TabGroup("Phase 2"), BoxGroup("Pattern Ranges")]
            private float m_phase2Pattern1Range;
            public float phase2Pattern1Range => m_phase2Pattern1Range;
            [SerializeField, TabGroup("Phase 2"), BoxGroup("Pattern Ranges")]
            private float m_phase2Pattern2Range;
            public float phase2Pattern2Range => m_phase2Pattern2Range;
            [SerializeField, TabGroup("Phase 2"), BoxGroup("Pattern Ranges")]
            private float m_phase2Pattern3Range;
            public float phase2Pattern3Range => m_phase2Pattern3Range;
            [SerializeField, TabGroup("Phase 2"), BoxGroup("Pattern Ranges")]
            private float m_phase2Pattern4Range;
            public float phase2Pattern4Range => m_phase2Pattern4Range;


            [Title("Misc")]
            [SerializeField]
            private float m_targetDistanceTolerance;
            public float targetDistanceTolerance => m_targetDistanceTolerance;

            [SerializeField]
            private SimpleProjectileAttackInfo m_bulletProjectile;
            public SimpleProjectileAttackInfo bulletProjectile => m_bulletProjectile;
            [SerializeField]
            private SimpleProjectileAttackInfo m_overchargedBulletProjectile;
            public SimpleProjectileAttackInfo overchargedBulletProjectile => m_overchargedBulletProjectile;

            [Title("Animations")]
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_punchUppercut;
            public string punchUppercut => m_punchUppercut;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_deathAnimation;
            public string deathAnimation => m_deathAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_flinchAnimation;
            public string flinchAnimation => m_flinchAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_idleAnimation;
            public string idleAnimation => m_idleAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_malfunctionStateAnimation;
            public string malfunctionStateAnimation => m_malfunctionStateAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_malfunctionStateIdleAnimation;
            public string malfunctionStateIdleAnimation => m_malfunctionStateIdleAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_malfunctionRecoveryStateAnimation;
            public string malfunctionRecoveryStateAnimation => m_malfunctionRecoveryStateAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_spinPreAnimation;
            public string spinPreAnimation => m_spinPreAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_spinEndAnimation;
            public string spinEndAnimation => m_spinEndAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_overchargedSpinPreAnimation;
            public string overchargedSpinPreAnimation => m_overchargedSpinPreAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_overchargedSpinEndAnimation;
            public string overchargedSpinEndAnimation => m_overchargedSpinEndAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_turnAnimation;
            public string turnAnimation => m_turnAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_overchargedIdle;
            public string overchargedIdle => m_overchargedIdle;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_rageQuake;
            public string rageQuake => m_rageQuake;

            [Title("Events")]
            [SerializeField, ValueDropdown("GetEvents")]
            private string m_punchUppercutEvent;
            public string punchUppercutEvent => m_punchUppercutEvent;
            [SerializeField, ValueDropdown("GetEvents")]
            private string m_flamethrower1Event;
            public string flamethrower1Event => m_flamethrower1Event;
            [SerializeField, ValueDropdown("GetEvents")]
            private string m_overchargedPunchUppercutEvent;
            public string overchargedPunchUppercutEvent => m_overchargedPunchUppercutEvent;
            [SerializeField, ValueDropdown("GetEvents")]
            private string m_overchargedFlamethrower1Event;
            public string overchargedFlamethrower1Event => m_overchargedFlamethrower1Event;
            [SerializeField]
            private CinderBoltHeatHandle.Config m_heatHandleConfiguration;
            public CinderBoltHeatHandle.Config heatHandleConfiguration => m_heatHandleConfiguration;

            public Dictionary<int, (int targetIndex, bool isGoingUp)> m_moveMap = new Dictionary<int, (int, bool)>
            {
                { 0, (2, false) },
                { 1, (3, false) },
                { 2, (0, true) },
                { 3, (1, true) }
            };

            public override void Initialize()
            {
#if UNITY_EDITOR
                m_move.SetData(m_skeletonDataAsset);
                m_hoverUpward.SetData(m_skeletonDataAsset);
                m_overchargedHoverUpward.SetData(m_skeletonDataAsset);
                m_hoverBackward.SetData(m_skeletonDataAsset);
                m_overchargedHoverBackward.SetData(m_skeletonDataAsset);
                m_hoverDownward.SetData(m_skeletonDataAsset);
                m_overchargedHoverDownward.SetData(m_skeletonDataAsset);
                m_hoverForward.SetData(m_skeletonDataAsset);
                m_overchargedHoverForward.SetData(m_skeletonDataAsset);
                m_longDash.SetData(m_skeletonDataAsset);
                m_overchargedLongDash.SetData(m_skeletonDataAsset);
                m_shortDash.SetData(m_skeletonDataAsset);
                m_straightLeftAndUppercutAttack.SetData(m_skeletonDataAsset);
                m_overchargedStraightLeftAndUppercutAttack.SetData(m_skeletonDataAsset);
                m_flameThrowerAttack.SetData(m_skeletonDataAsset);
                m_overchargedFlameThrowerAttack.SetData(m_skeletonDataAsset);
                m_flameBeamAttack.SetData(m_skeletonDataAsset);
                m_overchargedFlameBeamAttack.SetData(m_skeletonDataAsset);
                m_punchAttack.SetData(m_skeletonDataAsset);
                m_overchargedPunchAttack.SetData(m_skeletonDataAsset);
                m_shotgunBlastFireAttack.SetData(m_skeletonDataAsset);
                m_overchargedShotgunBlastFireAttack.SetData(m_skeletonDataAsset);
                m_shotgunBlastRapidFireAttack.SetData(m_skeletonDataAsset);
                m_overchargedShotgunBlastRapidFireAttack.SetData(m_skeletonDataAsset);
                m_uppercutAttack.SetData(m_skeletonDataAsset);
                m_overchargedUppercutAttack.SetData(m_skeletonDataAsset);
                m_firebeamAttack.SetData(m_skeletonDataAsset);
                m_meteorAttack.SetData(m_skeletonDataAsset);
                m_spinAttack.SetData(m_skeletonDataAsset);
                m_longDashAttack.SetData(m_skeletonDataAsset);
                m_overchargedLongDashAttack.SetData(m_skeletonDataAsset);
                m_shortDashAttack.SetData(m_skeletonDataAsset);
                m_overchargedShortDash.SetData(m_skeletonDataAsset);
                m_bulletProjectile.SetData(m_skeletonDataAsset);
                m_overchargedPunchUppercutAttack.SetData(m_skeletonDataAsset);
                m_overchargedFlamethrower1Attack.SetData(m_skeletonDataAsset);
                m_overchargedPreSpinAttack.SetData(m_skeletonDataAsset);
                m_overchargedSpinAttack.SetData(m_skeletonDataAsset);
                m_overchargedPostSpinAttack.SetData(m_skeletonDataAsset);
                m_overchargedFirebeamAttack.SetData(m_skeletonDataAsset);
                m_overchargedShotgunBlastPreAnimation.SetData(m_skeletonDataAsset);
                m_overchargedShotgunBlastFireAttack.SetData(m_skeletonDataAsset);
                m_overchargedShotgunBlastBackToIdleAnimation.SetData(m_skeletonDataAsset);
                m_overchargedBulletProjectile.SetData(m_skeletonDataAsset);
                m_overchargedMove.SetData(m_skeletonDataAsset);
#endif
            }
        }

        [System.Serializable]
        public class PhaseInfo : IPhaseInfo
        {
            [SerializeField]
            private int m_phaseIndex;
            public int phaseIndex => m_phaseIndex;
        }

        private enum State
        {
            Phasing,
            Intro,
            Idle,
            Malfunction,
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
            AttackPattern4,
            AttackPattern5,
            AttackPattern6,
            WaitAttackEnd,
        }
        private enum Attack
        {
            Pattern1Phase1,
            Pattern2Phase1,
            Pattern1Phase2,
            Pattern2Phase2,
            Pattern3Phase2,
            Pattern4Phase2,
            WaitAttackEnd,
        }

        public enum Phase
        {
            PhaseOne,
            PhaseTwo,
            Wait,
        }
        [SerializeField, TabGroup("Reference")]
        private Boss m_boss;
        [SerializeField, TabGroup("Reference")]
        private Hitbox m_hitbox;
        [SerializeField, TabGroup("Modules")]
        private AttackHandle m_attackHandle;
        [SerializeField, TabGroup("Modules")]
        private AnimatedTurnHandle m_turnHandle;
        [SerializeField, TabGroup("Modules")]
        private DeathHandle m_deathHandle;
        [SerializeField, TabGroup("Modules")]
        private Health m_health;
        [SerializeField, TabGroup("Modules")]
        private MovementHandle2D m_movement;

        [SerializeField, TabGroup("FX")]
        private ParticleFX m_flamethrower1FX;
        [SerializeField, TabGroup("FX")]
        private ParticleFX m_firebeamAnticipationFX;
        [SerializeField, TabGroup("FX")]
        private ParticleFX m_spinAttackFX;
        [SerializeField, TabGroup("FX")]
        private ParticleFX m_longDashFX;
        [SerializeField, TabGroup("FX")]
        private ParticleFX m_shortDashFX;
        [SerializeField, TabGroup("FX")]
        private ParticleFX m_flamethrower2FX;
        [SerializeField, TabGroup("FX")]
        private GameObject m_flamethrower2GroundMarksFX;
        [SerializeField, TabGroup("FX")]
        private ParticleFX m_meteorSmashFX;
        [SerializeField, TabGroup("FX")]
        private GameObject[] m_runeShieldFX;
        [SerializeField, TabGroup("FX")]
        private GameObject m_meteorSmashTrailFX;
        [SerializeField, TabGroup("FX")]
        private ParticleFX m_steamMalfAndOver;
        [SerializeField, TabGroup("FX")]
        private GameObject m_steamThrustFX;
        [SerializeField, TabGroup("FX")]
        private ParticleFX m_boosterChargeFX;
        [SerializeField, TabGroup("FX")]
        private GameObject RecoveryFX;
        [SerializeField, TabGroup("FX")]
        private ParticleFX m_firebeamFX;

        [SerializeField, TabGroup("FX Overcharged")]
        private ParticleFX m_flamethrower1OverchargedFX;
        [SerializeField, TabGroup("FX Overcharged")]
        private ParticleFX m_firebeamAnticipationOverchargedFX;
        [SerializeField, TabGroup("FX Overcharged")]
        private ParticleFX m_spinAttackOverchargedFX;
        [SerializeField, TabGroup("FX Overcharged")]
        private GameObject m_flamethrower2OverchargedFX;
        [SerializeField, TabGroup("FX Overcharged")]
        private ParticleFX m_meteorSmashOverchargedFX;
        [SerializeField, TabGroup("FX Overcharged")]
        private GameObject m_meteorSmashTrailOverchargedFX;

        [SerializeField, TabGroup("Sensors")]
        private RaySensor m_wallSensor;
        [SerializeField, TabGroup("Sensors")]
        private GameObject m_groundSensor;
        [SerializeField, TabGroup("Sensors")]
        private RaySensor m_groundSens;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_projectilePoints;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_projectilePoints2;
        [SerializeField, TabGroup("Spawn Points")]
        private Transform m_projectilePoints3;
        [SerializeField, TabGroup("Spawn Points")]
        private GameObject m_flamethrower1SpawnPoint;
        [SerializeField, TabGroup("Spawn Points")]
        private GameObject m_flamethrower2SpawnPoint;
        [SerializeField, TabGroup("Spawn Points")]
        private GameObject m_rightHandSpawnPoint;
        [SerializeField, TabGroup("Spawn Points")]
        private GameObject m_leftHandSpawnPoint;

        [SerializeField, TabGroup("Spawn Points")]
        private List<Transform> m_firebeamTransformPoints;

        [SerializeField]
        private SpineEventListener m_spineListener;

        [ShowInInspector]
        private StateHandle<State> m_stateHandle;
        State m_turnState;
        [ShowInInspector]
        private PhaseHandle<Phase, PhaseInfo> m_phaseHandle;
        [ShowInInspector]
        private RandomAttackDecider<Attack> m_attackDecider;
        private ProjectileLauncher m_projectileLauncher;
        private ProjectileLauncher m_overchargeProjectileLauncher;
        private bool m_hasPhaseChanged;
        [SerializeField, TabGroup("Attack Colliders")]
        private Collider2D m_punchAttackCollider, m_overchargedPunchAttackCollider;
        [SerializeField, TabGroup("Attack Colliders")]
        private Collider2D m_punchAttackCollider2, m_overchargedPunchAttackCollider2;
        [SerializeField, TabGroup("Attack Colliders")]
        private Collider2D m_flamethrower1Collider, m_overchargedFlamethrower1Collider;
        [SerializeField, TabGroup("Attack Colliders")]
        private Collider2D m_firebeamCollider, m_overchargedFirebeamCollider;
        [SerializeField, TabGroup("Attack Colliders")]
        private Collider2D m_longDashCollider, m_overchargedLongDashCollider;
        [SerializeField, TabGroup("Attack Colliders")]
        private Collider2D m_flamethrower2Colliders, m_overchargedFlamethrower2Colliders;
        [SerializeField, TabGroup("Attack Colliders")]
        private Collider2D m_meteorSmashCollider, m_overchargedMeteorSmashCollider;
        [SerializeField, TabGroup("Attack Colliders")]
        private List<Collider2D> m_spinAttackCollider, m_overchargedSpinAttackCollider;
        [SerializeField, TabGroup("Attack Colliders")]
        private List<Collider2D> m_recoveryDamageCollider;
        private Coroutine m_changeLocationRoutine;
        private bool m_isDetecting;
        [SerializeField, TabGroup("Laser")]
        private LineRenderer m_lineRenderer;
        [SerializeField, TabGroup("Laser")]
        private LineRenderer m_telegraphLineRenderer;
        [SerializeField, TabGroup("Laser")]
        private EdgeCollider2D m_edgeCollider;
        [SerializeField, TabGroup("Laser")]
        private EdgeCollider2D m_overchargeEdgeCollider;
        [SerializeField, TabGroup("Laser")]
        private Transform m_laserOrigin;
        [SerializeField, TabGroup("Laser")]
        private ParticleFX m_muzzleLoopFX;
        [SerializeField, TabGroup("Laser")]
        private ParticleFX m_laserOriginMuzzleFX;
        [SerializeField, TabGroup("Laser")]
        private ParticleFX m_muzzleTelegraphFX;
        [SerializeField, TabGroup("Laser")]
        private float m_laserDuration;

        [SerializeField, TabGroup("OverchargedLaser")]
        private LineRenderer m_overchargedLineRenderer;
        [SerializeField, TabGroup("OverchargedLaser")]
        private LineRenderer m_overchargedTelegraphLineRenderer;
        [SerializeField, TabGroup("OverchargedLaser")]
        private Transform m_overchargedLaserOrigin;
        [SerializeField, TabGroup("OverchargedLaser")]
        private ParticleFX m_overchargedMuzzleLoopFX;
        [SerializeField, TabGroup("OverchargedLaser")]
        private ParticleFX m_overchargedLaserOriginMuzzleFX;
        [SerializeField, TabGroup("OverchargedLaser")]
        private ParticleFX m_overchargedMuzzleTelegraphFX;
        [SerializeField, TabGroup("OverchargedLaser")]
        private float m_overchargedLaserDuration;

        private Vector2 m_laserTargetPos;
        [SerializeField]
        private List<Vector2> m_Points;
        private IEnumerator m_aimRoutine;
        [SerializeField]
        private bool m_beamOn;

        private SimpleAttackProjectile m_projectile;
        [SerializeField]
        private bool m_isRaging = false;
        private float m_runeDuration;
        [SerializeField]
        private bool m_hasMalfactioned = false;
        [SerializeField]
        private bool m_hasRune = false;
        [SerializeField]
        private CinderBoltHeatHandle m_heatHandler;
        [SerializeField]
        private int m_overOfRangeCounter;
        [SerializeField]
        private CinderBoltHeatGauge m_heatGauge;
        [SerializeField]
        private Collider2D[] m_sceneFlamethrower;

        private void ApplyPhaseData(PhaseInfo obj)
        {
            GetComponent<Damageable>().DamageTaken += CinderBoltAI_DamageTaken;
            if (m_attackDecider != null)
            {
                UpdateAttackDeciderList();
            }
            base.ApplyData();
        }

        private void ChangeState()
        {
            if (!m_hasPhaseChanged && m_changeLocationRoutine == null)
            {
                m_stateHandle.OverrideState(State.Phasing);
                m_hasPhaseChanged = true;
                //UpdateAttackDeciderList();
                m_animation.DisableRootMotion();
                m_animation.SetEmptyAnimation(0, 0);
                m_phaseHandle.ApplyChange();
            }
        }
        public override void SetTarget(IDamageable damageable, Character m_target = null)
        {
            if (damageable != null && m_stateHandle.currentState == State.Intro)
            {
                base.SetTarget(damageable, m_target);
                m_stateHandle.OverrideState(State.Intro);
            }
        }

        private void OnTurnDone(object sender, FacingEventArgs eventArgs)
        {
            if (m_stateHandle.currentState != State.Phasing)
            {
                m_animation.animationState.TimeScale = 1f;
                m_stateHandle.ApplyQueuedState();
            }
            m_phaseHandle.allowPhaseChange = true;
        }

        private IEnumerator IntroRoutine()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            m_movement.Stop();
            m_hitbox.SetInvulnerability(Invulnerability.None);
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }

        private IEnumerator ChangePhaseRoutine()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            m_hasRune = true;
            StartCoroutine(OnRuneShieldRoutine(2));
            ResetLaser();
            m_punchAttacker.SetActive(true);
            m_punchAttacker2.SetActive(true);
            m_overchargedPunchAttacker.SetActive(false);
            m_overchargedPunchAttacker2.SetActive(false);
            m_flamethrower1.SetActive(true);
            m_overchargedFlamethrower1.SetActive(false);
            m_firebeam.SetActive(true);
            m_overchargedFirebeam.SetActive(false);
            m_spinAttacker.SetActive(true);
            m_overchargedSpinAttacker.SetActive(false);
            m_longD.SetActive(true);
            m_overchargedLongD.SetActive(false);
            m_shotG.SetActive(true);
            m_overchargedShotG.SetActive(true);
            m_meteor.SetActive(true);
            m_overchargedMeteor.SetActive(false);
            m_flamethrower2.SetActive(true);
            m_overchargedFlamethrower2.SetActive(false);
            m_hasMalfactioned = false;
            m_steamMalfAndOver.Play();
            m_movement.Stop();
            m_flamethrower2Colliders.enabled = false;
            m_flamethrower2GroundMarksFX.SetActive(false);
            m_flamethrower1OverchargedFX.Stop();
            m_firebeamAnticipationOverchargedFX.Stop();
            m_meteorSmashOverchargedFX.Stop();
            m_firebeamAnticipationFX.Stop();
            m_muzzleLoopFX.Stop();
            m_laserOriginMuzzleFX.Stop();
            m_flamethrower1FX.Stop();
            m_flamethrower2FX.Stop();
            m_laserOriginMuzzleFX.Stop();
            m_muzzleTelegraphFX.Stop();
            m_longDashFX.Stop();
            m_meteorSmashFX.Stop();
            m_meteorSmashTrailFX.SetActive(false);
            m_muzzleLoopFX.Stop();
            m_overchargedMuzzleLoopFX.Stop();
            m_overchargedLaserOriginMuzzleFX.Stop();
            m_overchargedMuzzleTelegraphFX.Stop();
            m_shortDashFX.Stop();
            m_spinAttackFX.Stop();
            m_punchAttackCollider.enabled = false;
            m_punchAttackCollider2.enabled = false;
            m_flamethrower1Collider.enabled = false;
            m_overchargedPunchAttackCollider.enabled = false;
            m_overchargedPunchAttackCollider2.enabled = false;
            m_overchargedFlamethrower1Collider.enabled = false;
            m_overchargedFlamethrower2Colliders.enabled = false;
            m_firebeamCollider.enabled = false;
            m_overchargedFirebeamCollider.enabled = false;
            m_longDashCollider.enabled = false;
            m_flamethrower2Colliders.enabled = false;
            m_meteorSmashCollider.enabled = false;
            m_spinAttackCollider[0].enabled = false;
            m_spinAttackCollider[1].enabled = false;
            m_edgeCollider.enabled = false;
            m_overchargeEdgeCollider.enabled = false;
            m_malfunctioning = false;
            RecoveryFX.SetActive(false);
            m_recoveryDamageCollider[0].enabled = false;
            m_recoveryDamageCollider[1].enabled = false;
            m_animation.SetAnimation(0, m_info.rageQuake, false);
            m_hitbox.SetInvulnerability(Invulnerability.MAX);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.rageQuake);
            m_hitbox.SetInvulnerability(Invulnerability.None);
            //StartCoroutine(OnFirstRuneShieldRoutine());
            m_hasPhaseChanged = false;
            DecidedOnAttack(false);
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }
        /*private IEnumerator OnFirstRuneShieldRoutine()
        {
            m_hasRune = false;
            m_runeDuration = 10;
            m_runeShieldFX.SetActive(true);
            yield return new WaitForSeconds(m_runeDuration);
            m_runeShieldFX.SetActive(false);
            //m_runeShieldBreakFX.SetActive(true);
            yield return new WaitForSeconds(1f);
            //m_runeShieldBreakFX.SetActive(false);
            yield return null;
        }*/

        protected override void OnDestroyed(object sender, EventActionArgs eventArgs)
        {
            base.OnDestroyed(sender, eventArgs);
            m_movement.Stop();
            ResetLaser();
            m_punchAttacker.SetActive(true);
            m_punchAttacker2.SetActive(true);
            m_overchargedPunchAttacker.SetActive(false);
            m_overchargedPunchAttacker2.SetActive(false);
            m_flamethrower1.SetActive(true);
            m_overchargedFlamethrower1.SetActive(false);
            m_firebeam.SetActive(true);
            m_longD.SetActive(true);
            m_overchargedLongD.SetActive(false);
            m_shotG.SetActive(true);
            m_meteor.SetActive(true);
            m_steamMalfAndOver.Play();
            m_movement.Stop();
            m_flamethrower2Colliders.enabled = false;
            m_flamethrower2GroundMarksFX.SetActive(false);
            m_flamethrower1OverchargedFX.Stop();
            m_firebeamAnticipationOverchargedFX.Stop();
            m_meteorSmashOverchargedFX.Stop();
            m_firebeamAnticipationFX.Stop();
            m_muzzleLoopFX.Stop();
            m_laserOriginMuzzleFX.Stop();
            m_flamethrower1FX.Stop();
            m_flamethrower2FX.Stop();
            m_laserOriginMuzzleFX.Stop();
            m_muzzleTelegraphFX.Stop();
            m_longDashFX.Stop();
            m_meteorSmashFX.Stop();
            m_meteorSmashTrailFX.SetActive(false);
            m_muzzleLoopFX.Stop();
            m_overchargedMuzzleLoopFX.Stop();
            m_overchargedLaserOriginMuzzleFX.Stop();
            m_overchargedMuzzleTelegraphFX.Stop();
            m_shortDashFX.Stop();
            m_spinAttackFX.Stop();
            m_punchAttackCollider.enabled = false;
            m_punchAttackCollider2.enabled = false;
            m_flamethrower1Collider.enabled = false;
            m_overchargedPunchAttackCollider.enabled = false;
            m_overchargedPunchAttackCollider2.enabled = false;
            m_overchargedFlamethrower1Collider.enabled = false;
            m_overchargedFlamethrower2Colliders.enabled = false;
            m_firebeamCollider.enabled = false;
            m_overchargedFirebeamCollider.enabled = false;
            m_longDashCollider.enabled = false;
            m_flamethrower2Colliders.enabled = false;
            m_meteorSmashCollider.enabled = false;
            m_spinAttackCollider[0].enabled = false;
            m_spinAttackCollider[1].enabled = false;
            m_edgeCollider.enabled = false;
            m_overchargeEdgeCollider.enabled = false;
            m_overchargedMeteor.SetActive(false);
            m_flamethrower2.SetActive(true);
            m_overchargedFlamethrower2.SetActive(false);
            m_steamMalfAndOver.Play();
            m_movement.Stop();
            m_firebeamAnticipationFX.Stop();
            m_flamethrower1FX.Stop();
            m_flamethrower2FX.Stop();
            m_laserOriginMuzzleFX.Stop();
            m_longDashFX.Stop();
            m_meteorSmashFX.Stop();
            m_meteorSmashTrailFX.SetActive(false);
            m_muzzleLoopFX.Stop();
            m_shortDashFX.Stop();
            m_spinAttackFX.Stop();
            m_punchAttackCollider.enabled = false;
            m_punchAttackCollider2.enabled = false;
            m_flamethrower2Colliders.enabled = false;
            m_firebeamCollider.enabled = false;
            m_longDashCollider.enabled = false;
            m_flamethrower2Colliders.enabled = false;
            m_meteorSmashCollider.enabled = false;
            m_spinAttackCollider[0].enabled = false;
            m_spinAttackCollider[1].enabled = false;
            StopAllCoroutines();
            m_movement.Stop();
            m_isDetecting = false;
        }
        #region Overcharged Attacks
        [SerializeField, TabGroup("Attackers")]
        private GameObject m_punchAttacker;
        [SerializeField, TabGroup("Attackers")]
        private GameObject m_punchAttacker2;
        [SerializeField, TabGroup("Attackers")]
        private GameObject m_flamethrower1;
        [SerializeField, TabGroup("Attackers")]
        private GameObject m_flamethrower2;
        [SerializeField, TabGroup("Attackers")]
        private GameObject m_firebeam;
        [SerializeField, TabGroup("Attackers")]
        private GameObject m_spinAttacker;
        [SerializeField, TabGroup("Attackers")]
        private GameObject m_longD;
        [SerializeField, TabGroup("Attackers")]
        private GameObject m_meteor;
        [SerializeField, TabGroup("Attackers")]
        private GameObject m_shotG;
        [SerializeField, TabGroup("Attackers")]
        private GameObject m_overchargedPunchAttacker;
        [SerializeField, TabGroup("Attackers")]
        private GameObject m_overchargedPunchAttacker2;
        [SerializeField, TabGroup("Attackers")]
        private GameObject m_overchargedFlamethrower1;
        [SerializeField, TabGroup("Attackers")]
        private GameObject m_overchargedFlamethrower2;
        [SerializeField, TabGroup("Attackers")]
        private GameObject m_overchargedFirebeam;
        [SerializeField, TabGroup("Attackers")]
        private GameObject m_overchargedSpinAttacker;
        [SerializeField, TabGroup("Attackers")]
        private GameObject m_overchargedLongD;
        [SerializeField, TabGroup("Attackers")]
        private GameObject m_overchargedMeteor;
        [SerializeField, TabGroup("Attackers")]
        private GameObject m_overchargedShotG;

        private IEnumerator OverchargedPunchAttackRoutine()
        {
            Vector2 targetPoint = m_targetInfo.position;
            var direction = (targetPoint - (Vector2)transform.position).normalized;
            while (Vector2.Distance(transform.position, targetPoint) > m_info.punchAttack.range)
            {
                m_animation.SetAnimation(0, m_info.overchargedHoverForward, true);
                m_movement.MoveTowards(direction, m_info.move.speed * 2);
                yield return null;
            }
            m_steamThrustFX.SetActive(false);
            //yield return new WaitForSeconds(0.5f);
            m_movement.Stop();
            m_animation.SetAnimation(0, m_info.overchargedPunchUppercutAttack, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.overchargedPunchUppercutAttack);
            m_overchargedPunchAttackCollider.enabled = false;
            m_overchargedPunchAttackCollider2.enabled = false;
            yield return null;
        }
        private IEnumerator OverchargedFlamethrower1Routine()
        {
            Vector2 targetPoint = m_targetInfo.position;
            var direction = (targetPoint - (Vector2)transform.position).normalized;
            while (Vector2.Distance(transform.position, targetPoint) > m_info.punchAttack.range)
            {
                m_animation.SetAnimation(0, m_info.overchargedHoverForward, true);
                m_movement.MoveTowards(direction, m_info.move.speed * 2);
                yield return null;
            }
            m_steamThrustFX.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            m_movement.Stop();
            m_animation.SetAnimation(0, m_info.overchargedFlamethrower1Attack.animation, false);
            yield return new WaitForSeconds(0.4f);
            m_flamethrower1FX.Play();
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.overchargedFlamethrower1Attack.animation);
            m_flamethrower1FX.Stop();
            yield return null;
        }
        private IEnumerator OverchargedSpinAttackRoutine()
        {
            m_steamThrustFX.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            m_hitbox.SetInvulnerability(Invulnerability.MAX);
            m_movement.Stop();
            m_animation.SetAnimation(0, m_info.overchargedPreSpinAttack, false);
            yield return new WaitForSeconds(0.5f);
            m_animation.SetAnimation(0, m_info.overchargedSpinAttack, true);
            OverchargeSpinColliders(true);
            var m_followElapsedTime = 0f;
            var m_followDuration = 10f;
            while (m_followElapsedTime < m_followDuration)
            {
                m_movement.MoveTowards(new Vector2(m_targetInfo.position.x - transform.position.x, 0).normalized, (m_info.move.speed * 2));
                m_followElapsedTime += Time.deltaTime;
                yield return null;
            }
            m_movement.Stop();
            m_animation.SetAnimation(0, m_info.overchargedSpinEndAnimation, false);
            OverchargeSpinColliders(false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.overchargedSpinEndAnimation);
            m_hitbox.SetInvulnerability(Invulnerability.None);
            yield return null;
        }
        private IEnumerator OverchargedFirebeamRoutine(bool movingFirebeam = false)
        {
            yield return new WaitForSeconds(0.5f);
            int closestPointIndex = 0;
            float closestDistance = Vector2.Distance(m_firebeamTransformPoints[closestPointIndex].position, m_targetInfo.position);
            for (int i = 0; i < m_firebeamTransformPoints.Count; i++)
            {
                float distance = Vector2.Distance(m_firebeamTransformPoints[i].position, m_targetInfo.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPointIndex = i;
                }
            }
            Vector2 targetPoint = m_firebeamTransformPoints[closestPointIndex].position;
            var direction = (targetPoint - (Vector2)transform.position).normalized;
            while (Vector2.Distance(transform.position, targetPoint) > 1f)
            {
                // Move towards the target point
                m_animation.SetAnimation(0, m_info.overchargedMove, true);
                m_movement.MoveTowards(direction, m_info.move.speed * 2);
                if (!IsFacingTarget())
                {
                    CustomTurn();
                }
                yield return null;
            }
            m_steamThrustFX.SetActive(false);
            m_movement.Stop();
            for (int i = 1; i < m_firebeamTransformPoints.Count; i++)
            {
                if ((closestPointIndex + 1) % 2 == 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    m_character.SetFacing(HorizontalDirection.Left);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    m_character.SetFacing(HorizontalDirection.Right);
                }
            }
            m_movement.Stop();
            m_animation.SetAnimation(0, m_info.overchargedFirebeamAttack, false);
            yield return new WaitForSeconds(.8f);
            if (movingFirebeam)
            {
                //StartCoroutine(OnRuneShieldRoutine(3));
                StartCoroutine(FirebeamLaserRoutine());
                if (m_info.m_moveMap.TryGetValue(closestPointIndex, out var moveInfo))
                {
                    int targetIndex = moveInfo.targetIndex;
                    m_isGoingUp = moveInfo.isGoingUp;

                    Vector2 nextPoint = m_firebeamTransformPoints[targetIndex].position;
                    var moveDir = (nextPoint - (Vector2)transform.position).normalized;

                    while (Vector2.Distance(transform.position, nextPoint) > 1f)
                    {
                        m_movement.MoveTowards(moveDir, m_info.move.speed * 2);
                        yield return null;
                    }
                    m_movement.Stop();
                    closestPointIndex = targetIndex; // Update if needed
                }
            }
            else
            {
                yield return FirebeamLaserRoutine();
                yield return null;
            }
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.overchargedFirebeamAttack);
            m_movement.Stop();
            yield return null;
        }
        private IEnumerator OverchargedShortDash()
        {
            if (!m_wallSensor.allRaysDetecting)
            {
                var targetPos = m_targetInfo.position.x;
                m_steamThrustFX.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                m_hitbox.SetInvulnerability(Invulnerability.MAX);
                m_movement.Stop();
                m_animation.SetAnimation(0, m_info.overchargedShortDash, false);
                m_shortDashFX.Play();
                m_movement.MoveTowards(new Vector2(targetPos - transform.position.x, 0), m_info.shortDash.speed * 2);
                m_overchargedLongDashCollider.enabled = true;
                var time = 0f;
                while (time < 0.05 && !m_wallSensor.allRaysDetecting)
                {
                    time += GameplaySystem.time.deltaTime;
                    yield return null;
                }
                m_movement.Stop();
                yield return new WaitForAnimationComplete(m_animation.animationState, m_info.overchargedShortDash);
                m_shortDashFX.Stop();
                m_overchargedLongDashCollider.enabled = false;
                m_movement.Stop();
                m_hitbox.SetInvulnerability(Invulnerability.None);
                if (!IsFacingTarget())
                {
                    CustomTurn();
                }
            }
            yield return null;
        }
        private IEnumerator OverchargedLongDashRoutine()
        {
            if (!m_wallSensor.allRaysDetecting)
            {
                var targetPos = m_targetInfo.position.x;
                m_steamThrustFX.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                m_hitbox.SetInvulnerability(Invulnerability.MAX);
                m_movement.Stop();
                m_boosterChargeFX.Play();
                yield return new WaitForSeconds(2f);
                m_animation.SetAnimation(0, m_info.overchargedLongDash, false);
                m_longDashFX.Play();
                m_movement.MoveTowards(new Vector2(targetPos - transform.position.x, 0), m_info.longDash.speed * 2);
                m_overchargedLongDashCollider.enabled = true;
                var time = 0f;
                while (time < 0.08f || !m_wallSensor.allRaysDetecting)
                {
                    time += GameplaySystem.time.deltaTime;
                    yield return null;
                }
                m_movement.Stop();
                m_animation.SetAnimation(0, m_info.longDashStopAnimation, false);
                m_movement.Stop();
                yield return new WaitForAnimationComplete(m_animation.animationState, m_info.longDashStopAnimation);
                m_overchargedLongDashCollider.enabled = false;
                m_longDashFX.Stop();
                m_hitbox.SetInvulnerability(Invulnerability.None);
                if (!IsFacingTarget())
                {
                    CustomTurn();
                }
            }
            yield return null;
        }
        private IEnumerator OverchargedShotgunBlastRoutine()
        {
            yield return new WaitForSeconds(0.5f);
            Vector2 targetPoint = m_targetInfo.position;
            var direction = (targetPoint - (Vector2)transform.position).normalized;
            Vector2 spitPos = m_projectilePoints.transform.position;
            Vector3 v_diff = (targetPoint - spitPos);
            float atan2 = Mathf.Atan2(v_diff.y, v_diff.x * transform.localScale.x);
            var aimRotation = atan2 * Mathf.Rad2Deg;
            m_steamThrustFX.SetActive(true);
            while (Vector2.Distance(transform.position, targetPoint) > m_info.punchAttack.range + 40f)
            {
                m_animation.SetAnimation(0, m_info.overchargedMove, true);
                m_movement.MoveTowards(direction, m_info.move.speed * 2);
                yield return null;
            }
            m_steamThrustFX.SetActive(false);
            m_movement.Stop();
            m_animation.SetAnimation(0, m_info.overchargedShotgunBlastPreAnimation, false);
            ProjectileLaunchHandle overchargeLaunchHandle = new ProjectileLaunchHandle();
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.overchargedShotgunBlastPreAnimation);
            m_animation.SetAnimation(0, m_info.overchargedShotgunBlastFireAttack, false);
            overchargeLaunchHandle.Launch(m_info.overchargedBulletProjectile.projectileInfo.projectile, m_projectilePoints.transform.position, Vector2.right * transform.localScale.x, m_info.overchargedBulletProjectile.projectileInfo.speed);
            overchargeLaunchHandle.Launch(m_info.overchargedBulletProjectile.projectileInfo.projectile, m_projectilePoints2.transform.position, Vector2.right * transform.localScale.x, m_info.overchargedBulletProjectile.projectileInfo.speed);
            overchargeLaunchHandle.Launch(m_info.overchargedBulletProjectile.projectileInfo.projectile, m_projectilePoints3.transform.position, Vector2.right * transform.localScale.x, m_info.overchargedBulletProjectile.projectileInfo.speed);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.overchargedShotgunBlastFireAttack);
            m_animation.SetAnimation(0, m_info.overchargedShotgunBlastBackToIdleAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.overchargedShotgunBlastBackToIdleAnimation);
            yield return null;
        }
        private IEnumerator OverchargedFlamethrower2Routine()
        {
            m_steamThrustFX.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            m_movement.Stop();
            Vector2 targetPoint = new Vector2(transform.position.x + 10f, m_firebeamTransformPoints[1].position.y + 20f);
            var direction = (targetPoint - (Vector2)transform.position).normalized;
            m_animation.SetAnimation(0, m_info.overchargedMove, true);
            while (Vector2.Distance(transform.position, targetPoint) > 10f)
            {
                m_movement.MoveTowards(direction, m_info.move.speed * 2);
                yield return null;
            }
            m_movement.Stop();
            m_animation.SetAnimation(0, m_info.overchargedHoverDownward, true);
            yield return new WaitForSeconds(0.5f);
            m_animation.SetAnimation(0, m_info.overchargedMove, true);
            m_overchargedFlamethrower2Colliders.enabled = true;
            m_flamethrower2FX.Play();
            var m_followElapsedTime = 0f;
            var m_followDuration = 10f;
            while (m_followElapsedTime < m_followDuration)
            {
                m_movement.MoveTowards(new Vector2((m_targetInfo.position.x + (m_character.facing == HorizontalDirection.Left ? 10f : -10f)) - transform.position.x, 0).normalized, m_info.move.speed * 2);
                m_followElapsedTime += Time.deltaTime;
                targetground = new Vector2(transform.position.x, GroundPosition().y);
                targetgroundv3 = targetground;
                m_flamethrower2GroundMarksFX.transform.position = targetgroundv3;
                m_flamethrower2GroundMarksFX.SetActive(true);
                if (!IsFacingTarget())
                {
                    CustomTurn();
                }
                yield return null;
            }
            m_flamethrower2GroundMarksFX.SetActive(false);
            m_movement.Stop();
            m_overchargedFlamethrower2Colliders.enabled = false;
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.overchargedMove);
            m_flamethrower2FX.Stop();
            yield return null;
        }
        private IEnumerator OverchargedMeteorSmashRoutine()
        {
            m_steamThrustFX.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            m_movement.Stop();
            Vector2 targetPoint = new Vector2(m_targetInfo.position.x, m_firebeamTransformPoints[1].position.y + 20f);
            var direction = (targetPoint - (Vector2)transform.position).normalized;
            while (Vector2.Distance(transform.position, targetPoint) > 10f)
            {
                m_movement.MoveTowards(direction, m_info.move.speed * 2);
                yield return null;
            }
            m_movement.Stop();
            m_animation.SetAnimation(0, m_info.overchargedSpinPreAnimation, false);
            yield return new WaitForSeconds(0.5f);
            m_overchargedMeteorSmashCollider.enabled = true;
            m_animation.SetAnimation(0, m_info.overchargedSpinAttack, true);
            m_meteorSmashTrailFX.SetActive(true);
            m_meteorSmashFX.Play();
            Vector2 targetPointBelow = new Vector2(transform.position.x, m_firebeamTransformPoints[2].position.y - 10f);
            var directionVertical = (targetPointBelow - (Vector2)transform.position).normalized;
            while (Vector2.Distance(transform.position, targetPointBelow) > 10f)
            {
                m_movement.MoveTowards(directionVertical, m_info.move.speed * 6f);
                yield return null;
            }
            m_movement.Stop();
            m_meteorSmashTrailFX.SetActive(false);
            m_animation.SetAnimation(0, m_info.overchargedSpinEndAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.overchargedSpinEndAnimation);
            m_meteorSmashFX.Stop();
            m_overchargedMeteorSmashCollider.enabled = false;
            yield return null;
        }
        #endregion
        #region Normal Attacks
        private IEnumerator PunchAttackRoutine()
        {
            Vector2 targetPoint = m_targetInfo.position;
            var direction = (targetPoint - (Vector2)transform.position).normalized;
            while (Vector2.Distance(transform.position, targetPoint) > m_info.punchAttack.range)
            {
                m_animation.SetAnimation(0, m_info.move, true);
                m_movement.MoveTowards(direction, m_info.move.speed);
                yield return null;
            }
            m_steamThrustFX.SetActive(false);
            m_movement.Stop();
            m_animation.SetAnimation(0, m_info.punchUppercut, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.punchUppercut);
            m_punchAttackCollider.enabled = false;
            m_punchAttackCollider2.enabled = false;
            yield return null;
        }
        private IEnumerator Flamethrower1Routine()
        {
            Vector2 targetPoint = m_targetInfo.position;
            var direction = (targetPoint - (Vector2)transform.position).normalized;
            while (Vector2.Distance(transform.position, targetPoint) > m_info.punchAttack.range)
            {
                m_animation.SetAnimation(0, m_info.move, true);
                m_movement.MoveTowards(direction, m_info.move.speed);
                yield return null;
            }
            m_steamThrustFX.SetActive(false);
            m_movement.Stop();
            m_animation.SetAnimation(0, m_info.flameThrowerAttack.animation, false);
            yield return new WaitForSeconds(0.4f);
            m_flamethrower1FX.Play();
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.flameThrowerAttack.animation);
            m_flamethrower1FX.Stop();
            yield return null;
        }
        private bool SpinColliders(bool isDone)
        {
            for (int i = 0; i < m_spinAttackCollider.Count; i++)
            {
                m_spinAttackCollider[i].enabled = isDone;
            }
            return isDone;
        }
        private bool OverchargeSpinColliders(bool isDone)
        {
            for (int i = 0; i < m_overchargedSpinAttackCollider.Count; i++)
            {
                m_overchargedSpinAttackCollider[i].enabled = isDone;
            }
            return isDone;
        }
        private IEnumerator SpinAttackRoutine()
        {
            m_steamThrustFX.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            m_hitbox.SetInvulnerability(Invulnerability.MAX);
            m_movement.Stop();
            m_animation.SetAnimation(0, m_info.spinPreAnimation, false);
            yield return new WaitForSeconds(0.5f);
            m_animation.SetAnimation(0, m_info.spinAttack, true);
            SpinColliders(true);
            var m_followElapsedTime = 0f;
            var m_followDuration = 10f;
            while (m_followElapsedTime < m_followDuration)
            {
                m_movement.MoveTowards(new Vector2(m_targetInfo.position.x - transform.position.x, 0).normalized, m_info.move.speed);
                m_followElapsedTime += Time.deltaTime;
                yield return null;
            }
            m_movement.Stop();
            m_animation.SetAnimation(0, m_info.spinEndAnimation, false);
            SpinColliders(false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.spinEndAnimation);
            m_hitbox.SetInvulnerability(Invulnerability.None);
            yield return null;
        }
        #region Laser Coroutine
        private IEnumerator FirebeamLaserRoutine()
        {
            yield return new WaitForSeconds(0.1f);

            // Get the laser shot position straight ahead in facing direction
            m_laserTargetPos = ShotPosition();
            var ragingLineRenderer = m_isRaging ? m_overchargedTelegraphLineRenderer : m_telegraphLineRenderer;
            ragingLineRenderer.useWorldSpace = true;
            ragingLineRenderer.SetPosition(1, m_laserTargetPos);

            Collider2D laserCollider = null;
            EdgeCollider2D laserEdgeCollider = null;

            if (!m_isRaging)
            {
                laserCollider = m_firebeamCollider;
                laserEdgeCollider = m_edgeCollider;
            }
            else
            {
                laserCollider = m_overchargedFirebeamCollider;
                laserEdgeCollider = m_overchargeEdgeCollider;
            }
            var ragingMuzzle = m_isRaging ? m_overchargedLaserOriginMuzzleFX : m_laserOriginMuzzleFX;
            var ragingMuzzleLoop = m_isRaging ? m_overchargedMuzzleLoopFX : m_muzzleLoopFX;
            laserCollider.enabled = true;
            ragingMuzzle.Play();
            ragingMuzzleLoop.Play();
            var lineRenderer = m_isRaging ? m_overchargedLineRenderer : m_lineRenderer;
            lineRenderer.SetPosition(0, m_laserOrigin.position);

            var edgeColliderPosition = laserEdgeCollider.transform.position;
            var facing = (int)m_character.facing;
            var timer = 0f;

            do
            {
                var shotpos = ShotPosition();
                ragingMuzzleLoop.transform.position = shotpos;
                lineRenderer.SetPosition(1, shotpos);
                lineRenderer.SetPosition(0, m_laserOrigin.position);
                for (int i = 0; i < m_lineRenderer.positionCount; i++)
                {
                    Vector3 worldPos = lineRenderer.GetPosition(i);
                    Vector2 localPos = laserEdgeCollider.transform.InverseTransformPoint(worldPos);
                    m_Points.Add(localPos);
                }

                laserEdgeCollider.points = m_Points.ToArray();
                m_Points.Clear();

                yield return new WaitForSeconds(0.1f);
                timer += GameplaySystem.time.deltaTime + 0.1f;

            } while (timer <= (m_isRaging? m_overchargedLaserDuration : m_laserDuration));

            laserCollider.enabled = false;
            ragingMuzzle.Stop();
            ragingMuzzleLoop.Stop();
            ResetLaser();

            yield return null;
        }

        /*private IEnumerator LaserLookRoutine()
        {
            enabled = false;
            while (true)
            {
                m_laserTargetPos = LookPosition(m_laserOrigin);
                yield return null;
                enabled = true;
            }
        }*/
        private IEnumerator AimRoutine()
        {
            while (true)
            {
                var ragingLineRenderer = m_isRaging ? m_overchargedTelegraphLineRenderer : m_telegraphLineRenderer;
                var lineRenderer = m_isRaging ? m_overchargedLineRenderer : m_lineRenderer;
                ragingLineRenderer.SetPosition(0, m_isRaging ? m_overchargedTelegraphLineRenderer.transform.position : m_telegraphLineRenderer.transform.position);
                lineRenderer.SetPosition(0, m_isRaging ? m_overchargedLineRenderer.transform.position : m_lineRenderer.transform.position);
                lineRenderer.SetPosition(1, m_isRaging ? m_overchargedLineRenderer.transform.position : m_lineRenderer.transform.position);
                yield return null;
            }
        }
        private Vector2 ShotPosition()
        {
            Vector2 startPoint = m_isRaging ? m_overchargedLaserOrigin.position : m_laserOrigin.position;
            Vector2 direction = m_character.facing == HorizontalDirection.Right ? Vector2.right : Vector2.left;

            RaycastHit2D[] grade9 = Physics2D.RaycastAll(startPoint, direction, 1000f, DChildUtility.GetEnvironmentMask());

            foreach (var tommi in grade9)
            {
                if (tommi.collider == null)
                    continue;
                bool toto = false;
                foreach (var skiTown in m_sceneFlamethrower)
                {
                    if (tommi.collider == skiTown)
                    {
                        toto = true;
                        break;
                    }
                }
                if (!toto)
                {
                    return tommi.point;
                }
            }
            return startPoint + direction * 1000f;
        }

        private void ResetLaser()
        {
            m_telegraphLineRenderer.useWorldSpace = false;
            m_overchargedTelegraphLineRenderer.useWorldSpace = false;
            m_lineRenderer.useWorldSpace = false;
            m_overchargedLineRenderer.useWorldSpace = false;
            m_lineRenderer.SetPosition(0, Vector3.zero);
            m_lineRenderer.SetPosition(1, Vector3.zero);
            m_lineRenderer.startWidth = 30;
            m_overchargedLineRenderer.SetPosition(0, Vector3.zero);
            m_overchargedLineRenderer.SetPosition(1, Vector3.zero);
            m_overchargedLineRenderer.startWidth = 30;
            m_edgeCollider.points = m_Points.ToArray();
            m_overchargeEdgeCollider.points = m_Points.ToArray();
            m_Points.Clear();
        }
        #endregion
        private bool m_isGoingUp = false;
        private IEnumerator FirebeamRoutine(bool movingFirebeam = false)
        {
            yield return new WaitForSeconds(0.5f);
            Debug.Log("message mo lng kaugalingon");
            int closestPointIndex = 0;
            float closestDistance = Vector2.Distance(m_firebeamTransformPoints[closestPointIndex].position, m_targetInfo.position);
            for (int i = 0; i < m_firebeamTransformPoints.Count; i++)
            {
                float distance = Vector2.Distance(m_firebeamTransformPoints[i].position, m_targetInfo.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPointIndex = i;
                }
            }
            Vector2 targetPoint = m_firebeamTransformPoints[closestPointIndex].position;
            var direction = (targetPoint - (Vector2)transform.position).normalized;
            while (Vector2.Distance(transform.position, targetPoint) > 1f)
            {
                // Move towards the target point
                m_animation.SetAnimation(0, m_info.move, true);
                m_movement.MoveTowards(direction, m_info.move.speed);
                yield return null;
            }
            Debug.Log("mo kng dn ka sa yield return");
            m_steamThrustFX.SetActive(false);
            m_movement.Stop();
            for (int i = 1; i < m_firebeamTransformPoints.Count; i++)
            {
                if ((closestPointIndex + 1) % 2 == 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    m_character.SetFacing(HorizontalDirection.Left);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    m_character.SetFacing(HorizontalDirection.Right);
                }
            }
            var firebeamAttack = m_animation.SetAnimation(0, m_info.firebeamAttack, false);
            yield return new WaitForSeconds(1f);
            Debug.Log("before firebeam");
            if (movingFirebeam)
            {
                //StartCoroutine(OnRuneShieldRoutine(3));
                StartCoroutine(FirebeamLaserRoutine());
                if (m_info.m_moveMap.TryGetValue(closestPointIndex, out var moveInfo))
                {
                    int targetIndex = moveInfo.targetIndex;
                    m_isGoingUp = moveInfo.isGoingUp;

                    Vector2 nextPoint = m_firebeamTransformPoints[targetIndex].position;
                    var moveDir = (nextPoint - (Vector2)transform.position).normalized;

                    while (Vector2.Distance(transform.position, nextPoint) > 1f)
                    {
                        m_movement.MoveTowards(moveDir, m_info.move.speed);
                        yield return null;
                    }
                    
                    m_movement.Stop();
                    closestPointIndex = targetIndex; // Update if needed
                }
            }
            else
            {
                yield return FirebeamLaserRoutine();
                Debug.Log("before wait");
                //yield return new WaitForSeconds(0.5f);
                Debug.Log("after wait");
                yield return null;
            }
            yield return new WaitForSpineAnimationComplete(firebeamAttack);
            Debug.Log("im at the end lol");
            yield return null;
        }
        private IEnumerator ShortDashRoutine()
        {
            if (!m_wallSensor.allRaysDetecting)
            {
                var targetPos = m_targetInfo.position.x;
                m_steamThrustFX.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                m_hitbox.SetInvulnerability(Invulnerability.MAX);
                m_movement.Stop();
                m_animation.SetAnimation(0, m_info.shortDash, false);
                m_shortDashFX.Play();
                m_movement.MoveTowards(new Vector2(targetPos - transform.position.x, 0).normalized, m_info.shortDash.speed);
                m_longDashCollider.enabled = true;
                var time = 0f;
                while (time <= 0.5f && !m_wallSensor.allRaysDetecting)
                {
                    time += GameplaySystem.time.deltaTime;
                    yield return null;
                }
                m_movement.Stop();
                yield return new WaitForAnimationComplete(m_animation.animationState, m_info.shortDash);
                m_movement.Stop();
                m_longDashCollider.enabled = false;
                m_shortDashFX.Stop();
                m_hitbox.SetInvulnerability(Invulnerability.None);
                if (!IsFacingTarget())
                {
                    CustomTurn();
                }
            }
            yield return null;
        }
        private IEnumerator LongDashRoutine()
        {
            if (!m_wallSensor.allRaysDetecting)
            {
                var targetPos = m_targetInfo.position.x;
                m_steamThrustFX.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                m_hitbox.SetInvulnerability(Invulnerability.MAX);
                m_movement.Stop();
                m_boosterChargeFX.Play();
                yield return new WaitForSeconds(2f);
                m_animation.SetAnimation(0, m_info.longDashAttack, false);
                m_longDashFX.Play();
                m_movement.MoveTowards(new Vector2(targetPos - transform.position.x, 0).normalized, m_info.longDash.speed);
                m_longDashCollider.enabled = true;
                var time = 0f;
                while (time <= 0.8f || !m_wallSensor.allRaysDetecting)
                {
                    time += GameplaySystem.time.deltaTime;
                    yield return null;
                }
                m_movement.Stop();
                m_animation.SetAnimation(0, m_info.longDashStopAnimation, false);
                m_movement.Stop();
                yield return new WaitForAnimationComplete(m_animation.animationState, m_info.longDashStopAnimation);
                m_longDashCollider.enabled = false;
                m_longDashFX.Stop();
                m_hitbox.SetInvulnerability(Invulnerability.None);
                if (!IsFacingTarget())
                {
                    CustomTurn();
                }
            }
            yield return null;
        }
        private IEnumerator ShotgunBlastRoutine()
        {
            yield return new WaitForSeconds(0.5f);
            Vector2 targetPosition = m_targetInfo.position;
            var direction = (targetPosition - (Vector2)transform.position).normalized;
            Vector2 spitPos = m_projectilePoints.transform.position;
            Vector3 v_diff = (targetPosition - spitPos);
            float atan2 = Mathf.Atan2(v_diff.y, v_diff.x * transform.localScale.x);
            var aimRotation = atan2 * Mathf.Rad2Deg;
            m_steamThrustFX.SetActive(true);
            while (Vector2.Distance(transform.position, targetPosition) > m_info.punchAttack.range + 40f)
            {
                m_animation.SetAnimation(0, m_info.move, true);
                m_movement.MoveTowards(direction, m_info.move.speed);
                yield return null;
            }
            m_steamThrustFX.SetActive(false);
            m_movement.Stop();
            m_animation.SetAnimation(0, m_info.shotgunBlastPreAnimation, false);
            ProjectileLaunchHandle launchHandle = new ProjectileLaunchHandle();
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.shotgunBlastPreAnimation);
            m_animation.SetAnimation(0, m_info.shotgunBlastFireAttack, false);
            //yield return new WaitForSeconds(0.5f);
            launchHandle.Launch(m_info.bulletProjectile.projectileInfo.projectile, m_projectilePoints.transform.position, Vector2.right * transform.localScale.x, m_info.bulletProjectile.projectileInfo.speed);
            launchHandle.Launch(m_info.bulletProjectile.projectileInfo.projectile, m_projectilePoints2.transform.position, Vector2.right * transform.localScale.x, m_info.bulletProjectile.projectileInfo.speed);
            launchHandle.Launch(m_info.bulletProjectile.projectileInfo.projectile, m_projectilePoints3.transform.position, Vector2.right * transform.localScale.x, m_info.bulletProjectile.projectileInfo.speed);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.shotgunBlastFireAttack);
            m_animation.SetAnimation(0, m_info.shotgunBlastBackToIdleAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.shotgunBlastBackToIdleAnimation);
            yield return null;
        }
        private IEnumerator MeteorSmashRoutine()
        {
            m_steamThrustFX.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            m_movement.Stop();
            Vector2 targetPoint = new Vector2(m_targetInfo.position.x, m_firebeamTransformPoints[1].position.y + 20f);
            var direction = (targetPoint - (Vector2)transform.position).normalized;
            while (Vector2.Distance(transform.position, targetPoint) > 10f)
            {
                m_movement.MoveTowards(direction, m_info.move.speed);
                yield return null;
            }
            m_movement.Stop();
            m_animation.SetAnimation(0, m_info.spinPreAnimation, false);
            yield return new WaitForSeconds(0.5f);
            m_meteorSmashCollider.enabled = true;
            m_animation.SetAnimation(0, m_info.spinAttack, true);
            m_meteorSmashTrailFX.SetActive(true);
            m_meteorSmashFX.Play();
            Vector2 targetPointBelow = new Vector2(transform.position.x, m_firebeamTransformPoints[2].position.y - 10f);
            var directionVertical = (targetPointBelow - (Vector2)transform.position).normalized;
            while (Vector2.Distance(transform.position, targetPointBelow) > 10f)
            {
                m_movement.MoveTowards(directionVertical, m_info.move.speed * 4f);
                yield return null;
            }
            m_movement.Stop();
            m_meteorSmashTrailFX.SetActive(false);
            m_animation.SetAnimation(0, m_info.spinEndAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.spinEndAnimation);
            m_meteorSmashFX.Stop();
            /*if (m_targetInfo.isCharacterGrounded)
            {
                StartCoroutine(SpinAttackRoutine());
            }
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.spinEndAnimation);*/
            m_meteorSmashCollider.enabled = false;
            yield return null;
        }
        private Vector2 targetground;
        private Vector3 targetgroundv3;
        private Vector2 GroundPosition()
        {
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, 1000, DChildUtility.GetEnvironmentMask());
            return hit.point;
        }
        private IEnumerator Flamethrower2Routine()
        {
            m_steamThrustFX.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            m_movement.Stop();
            Vector2 targetPoint = new Vector2(transform.position.x + 10f, m_firebeamTransformPoints[1].position.y + 20f);
            var direction = (targetPoint - (Vector2)transform.position).normalized;
            while (Vector2.Distance(transform.position, targetPoint) > 10f)
            {
                m_movement.MoveTowards(direction, m_info.move.speed);
                yield return null;
            }
            m_movement.Stop();
            m_animation.SetAnimation(0, m_info.hoverDownward, true);
            yield return new WaitForSeconds(0.5f);
            m_animation.SetAnimation(0, m_info.move, true);
            m_flamethrower2Colliders.enabled = true;
            m_flamethrower2FX.Play();
            var m_followElapsedTime = 0f;
            var m_followDuration = 10f;
            while (m_followElapsedTime < m_followDuration)
            {
                m_movement.MoveTowards(new Vector2((m_targetInfo.position.x + (m_character.facing == HorizontalDirection.Left ? 10f : -10f)) - transform.position.x, 0).normalized, m_info.move.speed);
                m_followElapsedTime += Time.deltaTime;
                targetground = new Vector2(transform.position.x, GroundPosition().y);
                targetgroundv3 = targetground;
                m_flamethrower2GroundMarksFX.transform.position = targetgroundv3;
                m_flamethrower2GroundMarksFX.SetActive(true);
                if (!IsFacingTarget())
                {
                    CustomTurn();
                }
                yield return null;
            }
            m_flamethrower2GroundMarksFX.SetActive(false);
            m_movement.Stop();
            m_flamethrower2Colliders.enabled = false;
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.move);
            m_flamethrower2FX.Stop();
            yield return null;
        }
        #endregion
        #region Attacks
        private IEnumerator Pattern1Phase1Attack(bool allowEndAttackBehavior = true)
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            var m_followElapsedTime = 0f;
            var m_followDuration = 10f;
            while (Vector2.Distance(transform.position, m_targetInfo.position) > 50f)
            {
                m_animation.SetAnimation(0, m_isRaging? m_info.overchargedMove : m_info.move, true);
                m_movement.MoveTowards(new Vector2(m_targetInfo.position.x - transform.position.x, m_targetInfo.position.y - transform.position.y).normalized, m_isRaging? m_info.move.speed * 2 : m_info.move.speed);
                if (!IsFacingTarget())
                {
                    CustomTurn();
                }
                m_followElapsedTime += Time.deltaTime;
                if (m_followElapsedTime >= m_followDuration)
                {
                    m_followDuration += 10f;
                    m_overOfRangeCounter++;
                }
                /*if(Vector2.Distance(transform.position, m_targetInfo.position) <= m_info.punchAttack.range)
                {
                    yield return null;
                }*/
                yield return null;
            }
            if (!IsFacingTarget())
            {
                CustomTurn();
            }
            m_movement.Stop();
            if (m_overOfRangeCounter == 2)
            {
                Debug.Log("overOfRangePresent");
                yield return null;
            }
            else
            {
                var random = UnityEngine.Random.RandomRange(0, 2);
                if (random == 0)
                {
                    yield return m_isRaging? OverchargedPunchAttackRoutine() : PunchAttackRoutine();
                }
                else
                {
                    yield return m_isRaging ? OverchargedFlamethrower1Routine() : Flamethrower1Routine();
                }

                yield return null;
            }
            if (allowEndAttackBehavior)
            {
                m_animation.SetAnimation(0, m_isRaging ? m_info.overchargedIdle : m_info.idleAnimation, true);
                DecidedOnAttack(false);
                m_stateHandle.ApplyQueuedState();
            }
            m_overOfRangeCounter = 0;
            yield return null;
        }
        private IEnumerator Pattern2Phase1Attack()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            while (Vector2.Distance(transform.position, m_targetInfo.position) > 80f)
            {
                m_animation.SetAnimation(0, m_isRaging ? m_info.overchargedMove : m_info.move, true);
                m_movement.MoveTowards(new Vector2(m_targetInfo.position.x - transform.position.x, m_targetInfo.position.y - transform.position.y).normalized, m_isRaging? m_info.move.speed * 2 : m_info.move.speed);
                if (!IsFacingTarget())
                {
                    CustomTurn();
                }
                yield return null;
            }
            if (!IsFacingTarget())
            {
                CustomTurn();
            }
            m_movement.Stop();
            var random = UnityEngine.Random.RandomRange(0, 3);
            if (random == 0)
            {
                yield return m_isRaging? OverchargedSpinAttackRoutine() : SpinAttackRoutine();
                yield return Pattern1Phase1Attack(false);
            }
            else if (random == 1)
            {
                yield return m_isRaging? OverchargedFirebeamRoutine() : FirebeamRoutine();
            }
            else
            {
                yield return m_isRaging? OverchargedLongDashRoutine() : LongDashRoutine();
                while (Vector2.Distance(transform.position, m_targetInfo.position) > 20f)
                {
                    m_animation.SetAnimation(0, m_isRaging ? m_info.overchargedMove : m_info.move, true);
                    m_movement.MoveTowards(new Vector2(m_targetInfo.position.x - transform.position.x, m_targetInfo.position.y - transform.position.y).normalized, m_isRaging ? m_info.move.speed * 2 : m_info.move.speed);
                    if (!IsFacingTarget())
                    {
                        CustomTurn();
                    }
                    yield return null;
                }
                yield return m_isRaging? OverchargedPunchAttackRoutine() : PunchAttackRoutine();
                yield return m_isRaging? OverchargedFlamethrower1Routine() : Flamethrower1Routine();
            }
            m_animation.SetAnimation(0, m_isRaging? m_info.overchargedIdle : m_info.idleAnimation, true);
            DecidedOnAttack(false);
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }
        private IEnumerator Pattern1Phase2Attack()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            var m_followElapsedTime = 0f;
            var m_followDuration = 10f;
            while (Vector2.Distance(transform.position, m_targetInfo.position) > 50f)
            {
                m_animation.SetAnimation(0, m_isRaging ? m_info.overchargedMove : m_info.move, true);
                m_movement.MoveTowards(new Vector2(m_targetInfo.position.x - transform.position.x, m_targetInfo.position.y - transform.position.y).normalized, m_isRaging ? m_info.move.speed * 2 : m_info.move.speed);
                if (!IsFacingTarget())
                {
                    CustomTurn();
                }
                m_followElapsedTime += Time.deltaTime;
                if (m_followElapsedTime >= m_followDuration)
                {
                    m_followDuration += 10f;
                    m_overOfRangeCounter++;
                }
                yield return null;
            }
            if (!IsFacingTarget())
            {
                CustomTurn();
            }
            if (m_overOfRangeCounter == 2)
            {
                Debug.Log("overOfRangePresent");
                var randomAgain = UnityEngine.Random.RandomRange(0, 2);
                if (randomAgain == 0)
                {
                    yield return m_isRaging? OverchargedSpinAttackRoutine() : SpinAttackRoutine();
                }
                else
                {
                    yield return m_isRaging? OverchargedFlamethrower2Routine() : Flamethrower2Routine();
                }
                yield return null;
            }
            else
            {
                yield return m_isRaging? OverchargedShortDash() : ShortDashRoutine();
                var random = UnityEngine.Random.RandomRange(0, 3);
                if (random == 0)
                {
                    if (!IsFacingTarget())
                    {
                        CustomTurn();
                    }
                    yield return m_isRaging? OverchargedShotgunBlastRoutine() : ShotgunBlastRoutine();
                }
                else if (random == 1)
                {
                    yield return m_isRaging ? OverchargedPunchAttackRoutine() : PunchAttackRoutine();
                    var randomAttack = UnityEngine.Random.RandomRange(0, 2);
                    if (randomAttack == 0)
                    {
                        if (!IsFacingTarget())
                        {
                            CustomTurn();
                        }
                        yield return m_isRaging ? OverchargedShotgunBlastRoutine() : ShotgunBlastRoutine();
                    }
                    else
                    {
                        yield return m_isRaging? OverchargedFlamethrower1Routine() : Flamethrower1Routine();
                    }
                }
                else
                {
                    yield return m_isRaging ? OverchargedFlamethrower1Routine() : Flamethrower1Routine();
                }
            }
            m_overOfRangeCounter = 0;
            m_animation.SetAnimation(0, m_isRaging ? m_info.overchargedIdle : m_info.idleAnimation, true);
            DecidedOnAttack(false);
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }
        private IEnumerator Pattern2Phase2Attack()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            while (Vector2.Distance(transform.position, m_targetInfo.position) > 80f)
            {
                m_animation.SetAnimation(0, m_info.move, true);
                m_movement.MoveTowards(new Vector2(m_targetInfo.position.x - transform.position.x, m_targetInfo.position.y - transform.position.y).normalized, m_info.move.speed);
                if (!IsFacingTarget())
                {
                    CustomTurn();
                }
                yield return null;
            }
            if (!IsFacingTarget())
            {
                CustomTurn();
            }
            var random = UnityEngine.Random.RandomRange(0, 2);
            if (random == 0)
            {
                yield return m_isRaging? OverchargedSpinAttackRoutine() : SpinAttackRoutine();
            }
            else
            {
                yield return m_isRaging? OverchargedLongDashRoutine() : LongDashRoutine();
                if (Vector2.Distance(transform.position, m_targetInfo.position) > 50f)
                {
                    m_animation.SetAnimation(0, m_info.move, true);
                    m_movement.MoveTowards(new Vector2(m_targetInfo.position.x - transform.position.x, m_targetInfo.position.y - transform.position.y).normalized, m_info.move.speed);
                    if (!IsFacingTarget())
                    {
                        CustomTurn();
                    }
                    yield return m_isRaging? OverchargedPunchAttackRoutine() : PunchAttackRoutine();
                    yield return m_isRaging? OverchargedShotgunBlastRoutine() : ShotgunBlastRoutine();
                    yield return null;
                }
                if (!IsFacingTarget())
                {
                    CustomTurn();
                }
            }
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            DecidedOnAttack(false);
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }
        private IEnumerator Pattern3Phase2Attack()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            if (m_targetInfo.isCharacterGrounded)
            {
                yield return m_isRaging? OverchargedMeteorSmashRoutine() : MeteorSmashRoutine();
            }
            if (m_targetInfo.isCharacterGrounded)
            {
                yield return m_isRaging ? OverchargedSpinAttackRoutine() : SpinAttackRoutine();
            }
            if (m_targetInfo.isCharacterGrounded)
            {
                var random = UnityEngine.Random.RandomRange(0, 2);
                if(random == 0)
                {
                    m_hasRune = true;
                    StartCoroutine(OnRuneShieldRoutine(0));
                    yield return m_isRaging? OverchargedFirebeamRoutine() : FirebeamRoutine();
                }
                else
                {
                    yield return null;
                }
            }
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            DecidedOnAttack(false);
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }
        private IEnumerator Pattern4Phase2Attack()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            m_hasRune = true;
            StartCoroutine(OnRuneShieldRoutine(m_isGoingUp? 0 : 3));
            yield return m_isRaging ? OverchargedFirebeamRoutine(true) : FirebeamRoutine(true);
            if (m_isGoingUp)
            {

                yield return m_isRaging? OverchargedFlamethrower2Routine() : Flamethrower2Routine();
            }
            else
            {
                yield return null;
            }
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
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
                    m_attackDecider.SetList(new AttackInfo<Attack>(Attack.Pattern1Phase1, m_info.phase1Pattern1Range),
                        new AttackInfo<Attack>(Attack.Pattern2Phase1, m_info.phase1Pattern2Range));
                    break;
                case Phase.PhaseTwo:
                    m_attackDecider.SetList(new AttackInfo<Attack>(Attack.Pattern1Phase2, m_info.phase2Pattern1Range),
                        new AttackInfo<Attack>(Attack.Pattern2Phase2, m_info.phase2Pattern2Range),
                        new AttackInfo<Attack>(Attack.Pattern3Phase2, m_info.phase2Pattern3Range),
                        new AttackInfo<Attack>(Attack.Pattern4Phase2, m_info.phase2Pattern4Range));
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
            m_hitbox.SetInvulnerability(Invulnerability.None);
            m_turnHandle.TurnDone += OnTurnDone;
            m_deathHandle.SetAnimation(m_info.deathAnimation);
            m_heatHandler.SetConfiguration(m_info.heatHandleConfiguration);
            m_heatGauge.HeatFull += HeatGauge_HeatFull;
            m_groundSens = m_groundSensor.GetComponent<RaySensor>();
            m_projectile = GetComponent<SimpleAttackProjectile>();
            m_projectileLauncher = new ProjectileLauncher(m_info.bulletProjectile.projectileInfo, m_projectilePoints);
            m_overchargeProjectileLauncher = new ProjectileLauncher(m_info.overchargedBulletProjectile.projectileInfo, m_projectilePoints);
            m_attackDecider = new RandomAttackDecider<Attack>();
            m_stateHandle = new StateHandle<State>(State.Intro, State.WaitBehaviourEnd);
            UpdateAttackDeciderList();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            m_turnHandle.TurnDone -= OnTurnDone;
            m_heatGauge.HeatFull -= HeatGauge_HeatFull;
            GetComponent<Damageable>().DamageTaken -= CinderBoltAI_DamageTaken;
        }

        private void HeatGauge_HeatFull(object sender, EventActionArgs eventArgs)
        {
            m_isRaging = true;
            m_punchAttacker.SetActive(false);
            m_punchAttacker2.SetActive(false);
            m_overchargedPunchAttacker.SetActive(true);
            m_overchargedPunchAttacker2.SetActive(true);
            m_flamethrower1.SetActive(false);
            m_overchargedFlamethrower1.SetActive(true);
            m_spinAttacker.SetActive(false);
            m_overchargedSpinAttacker.SetActive(true);
            m_firebeam.SetActive(false);
            m_overchargedFirebeam.SetActive(true);
            m_longD.SetActive(false);
            m_overchargedLongD.SetActive(true);
            m_shotG.SetActive(true);
            m_overchargedShotG.SetActive(true);
            m_meteor.SetActive(false);
            m_overchargedMeteor.SetActive(true);
            m_flamethrower2.SetActive(false);
            m_overchargedFlamethrower2.SetActive(true);
            m_steamMalfAndOver.Play();
            StartCoroutine(OnRageCounter());
            m_basicAttackResistance.SetResistance(DamageType.Physical, AttackResistanceType.Weak);
        }
        protected override void Start()
        {
            //base.Start();
            m_aimRoutine = AimRoutine();
            m_spineListener.Subscribe(m_info.punchUppercutEvent, PunchAttack);
            m_spineListener.Subscribe(m_info.flamethrower1Event, Flamethrower1Attack);
            m_spineListener.Subscribe(m_info.overchargedPunchUppercutEvent, OvercahrgedPunchAttack);
            m_spineListener.Subscribe(m_info.overchargedFlamethrower1Event, OverchargedFlamethrower1Attack);
            //IgnoreCollision();
            m_basicAttackResistance.SetResistance(DamageType.Physical, AttackResistanceType.Strong);
            m_animation.DisableRootMotion();
            m_phaseHandle = new PhaseHandle<Phase, PhaseInfo>();
            m_phaseHandle.Initialize(Phase.PhaseOne, m_info.phaseInfo, m_character, ChangeState, ApplyPhaseData);
            m_phaseHandle.ApplyChange();
        }

        private void PunchAttack()
        {
            if (!m_isRaging)
            {
                m_punchAttackCollider.enabled = true;
                m_punchAttackCollider2.enabled = true;
            }
        }
        private void Flamethrower1Attack()
        {
            if (!m_isRaging)
            {
                m_flamethrower1Collider.enabled = true;
            }
        }
        private void OvercahrgedPunchAttack()
        {
            if (m_isRaging)
            {
                m_overchargedPunchAttackCollider.enabled = true;
                m_overchargedPunchAttackCollider2.enabled = true;
            }
        }
        private void OverchargedFlamethrower1Attack()
        {
            if (m_isRaging)
            {
                m_overchargedFlamethrower1Collider.enabled = true;
            }
        }
        private int counter;
        [SerializeField]
        private float m_duration = 0;
        private IEnumerator CounterForRuneRoutine()
        {
            var timeLeft = 0f;
            while (timeLeft < m_duration && !m_hasRune)
            {
                timeLeft += Time.deltaTime;
                if (timeLeft >= m_duration)
                {
                    timeLeft = 0f;
                }
                yield return null;
            }
            yield return null;
        }
        private IEnumerator OnRageCounter()
        {
            m_hitbox.SetInvulnerability(Invulnerability.None);
            var elapsedTime = 0f;
            var rageDuration = 10f;
            while (m_isRaging && rageDuration > elapsedTime)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= rageDuration)
                {
                    elapsedTime = 0f;
                    m_isRaging = false;
                    m_hasMalfactioned = true;
                    m_heatHandler.ResetHeat();
                }
                yield return null;
            }
            if (m_hasMalfactioned)
            {
                m_beamOn = false;
                m_hasRune = true;
                StartCoroutine(OnRuneShieldRoutine(0));
                //StopCoroutine(Convert.ToString(m_attackDecider.chosenAttack.attack));
                m_stateHandle.OverrideState(State.Malfunction);
                //yield return OnMlfunctionedRoutine();
            }
            yield return null;
        }
        private IsolatedObjectPhysics2D m_gravity;
        private IEnumerator OnMlfunctionedRoutine()
        {
            m_stateHandle.Wait(State.ReevaluateSituation);
            yield return new WaitForSeconds(0.5f);
            m_malfunctioning = true;
            m_basicAttackResistance.ClearResistance();
            m_hitbox.SetInvulnerability(Invulnerability.None);
            m_gravity = GetComponent<IsolatedObjectPhysics2D>();
            //m_firebeamFX.Stop();
            ResetLaser();
            m_runeShieldFX[0].SetActive(false);
            m_runeShieldFX[1].SetActive(false);
            m_runeShieldFX[2].SetActive(false);
            m_runeShieldFX[3].SetActive(false);
            m_steamMalfAndOver.Stop();
            m_punchAttacker.SetActive(true);
            m_punchAttacker2.SetActive(true);
            m_overchargedPunchAttacker.SetActive(false);
            m_overchargedPunchAttacker2.SetActive(false);
            m_flamethrower1.SetActive(true);
            m_overchargedFlamethrower1.SetActive(false);
            m_firebeam.SetActive(true);
            m_overchargedFirebeam.SetActive(false);
            m_spinAttacker.SetActive(true);
            m_overchargedSpinAttacker.SetActive(false);
            m_longD.SetActive(true);
            m_overchargedLongD.SetActive(false);
            m_shotG.SetActive(true);
            m_overchargedShotG.SetActive(true);
            m_meteor.SetActive(true);
            m_overchargedMeteor.SetActive(false);
            m_flamethrower2.SetActive(true);
            m_overchargedFlamethrower2.SetActive(false);
            m_hasMalfactioned = false;
            m_movement.Stop();
            m_flamethrower2Colliders.enabled = false;
            m_flamethrower2GroundMarksFX.SetActive(false);
            m_flamethrower1OverchargedFX.Stop();
            m_firebeamAnticipationOverchargedFX.Stop();
            m_meteorSmashOverchargedFX.Stop();
            m_firebeamAnticipationFX.Stop();
            m_muzzleLoopFX.Stop();
            m_laserOriginMuzzleFX.Stop();
            m_flamethrower1FX.Stop();
            m_flamethrower2FX.Stop();
            m_laserOriginMuzzleFX.Stop();
            m_muzzleTelegraphFX.Stop();
            m_longDashFX.Stop();
            m_meteorSmashFX.Stop();
            m_meteorSmashTrailFX.SetActive(false);
            m_muzzleLoopFX.Stop();
            m_overchargedMuzzleLoopFX.Stop();
            m_overchargedLaserOriginMuzzleFX.Stop();
            m_overchargedMuzzleTelegraphFX.Stop();
            m_shortDashFX.Stop();
            m_spinAttackFX.Stop();
            m_punchAttackCollider.enabled = false;
            m_punchAttackCollider2.enabled = false;
            m_flamethrower1Collider.enabled = false;
            m_overchargedPunchAttackCollider.enabled = false;
            m_overchargedPunchAttackCollider2.enabled = false;
            m_overchargedFlamethrower1Collider.enabled = false;
            m_overchargedFlamethrower2Colliders.enabled = false;
            m_firebeamCollider.enabled = false;
            m_overchargedFirebeamCollider.enabled = false;
            m_longDashCollider.enabled = false;
            m_flamethrower2Colliders.enabled = false;
            m_meteorSmashCollider.enabled = false;
            m_spinAttackCollider[0].enabled = false;
            m_spinAttackCollider[1].enabled = false;
            m_edgeCollider.enabled = false;
            m_overchargeEdgeCollider.enabled = false;
            m_groundSensor.SetActive(true);
            m_animation.SetAnimation(0, m_info.malfunctionStateAnimation, false);
            m_gravity.simulateGravity = true;
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.malfunctionStateAnimation);
            m_animation.SetAnimation(0, m_info.malfunctionStateIdleAnimation, true);
            yield return new WaitForSeconds(5f);
            m_animation.SetAnimation(0, m_info.malfunctionRecoveryStateAnimation, false);
            yield return new WaitForSeconds(1f);
            RecoveryFX.SetActive(true);
            m_recoveryDamageCollider[0].enabled = true;
            m_recoveryDamageCollider[1].enabled = true;
            yield return new WaitForSeconds(1f);
            RecoveryFX.SetActive(false);
            m_recoveryDamageCollider[0].enabled = false;
            m_recoveryDamageCollider[1].enabled = false;
            m_steamMalfAndOver.Stop();
            m_animation.SetAnimation(0, m_info.idleAnimation, true);
            m_heatHandler.ResetHeat();
            m_malfunctioning = false;
            m_basicAttackResistance.SetResistance(DamageType.Physical, AttackResistanceType.Strong);
            DecidedOnAttack(false);
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }
        private IEnumerator OnRuneShieldRoutine(int cooldownType = 0, bool hasCounter = false)
        {
            if (m_hasRune && !m_malfunctioning)
            {
                switch (cooldownType)
                {
                    case 0:
                        m_runeDuration = 5;
                        break;
                    case 1:
                        m_runeDuration = 8;
                        break;
                    case 2:
                        m_runeDuration = 10;
                        break;
                    case 3:
                        m_runeDuration = 15;
                        break;
                }
                m_runeShieldFX[cooldownType].SetActive(true);
                m_basicAttackResistance.SetData(m_attackResistanceData);
                yield return new WaitForSeconds(m_runeDuration);
                m_runeShieldFX[cooldownType].SetActive(false);
                m_basicAttackResistance.ClearResistance();
                m_hasRune = false;
            }
            if(hasCounter)
                StartCoroutine(CounterForRuneRoutine());
            yield return null;
        }
        public GameObject ligthVisuals;
        public bool checker = false;
        public bool secondChecker = true;
        public bool thirdChecker = false;
        [SerializeField]
        private BasicAttackResistance m_basicAttackResistance;
        [SerializeField]
        private AttackResistanceData m_attackResistanceData;
        private void CinderBoltAI_DamageTaken(object sender, Damageable.DamageEventArgs eventArgs)
        {
            //Debug.Log("Heat Gauge value is: " + GetComponent<CinderBoltHeatGauge>().currentValue);
            var hitTwice = GetComponent<CinderBoltHeatGauge>().currentValue == 60;
            if (hitTwice)
            {
                m_hasRune = true;
                StartCoroutine(OnRuneShieldRoutine(0, true));
            }
            if (eventArgs.type == DamageType.Fire)
            {
                /*if (secondChecker)
                {
                    counter += 1;
                    if (counter == 2)
                    {
                        thirdChecker = true;
                        checker = true;
                        m_hasRune = true;
                        StartCoroutine(OnRuneShieldRoutine());
                        counter = 0;
                    }
                }*/
                if (!thirdChecker && !m_malfunctioning)
                {
                    m_heatHandler.HandleDamageTaken(DamageType.Fire);
                }
            }
        }
        private bool m_malfunctioning = false;
        private void Update()
        {
            ligthVisuals.GetComponent<CinderBoltHeatLightsReaction>().HandleReaction(GetComponent<CinderBoltHeatGauge>().currentValue);
            if (m_groundSens.allRaysDetecting && m_malfunctioning)
            {
                m_gravity.simulateGravity = false;
                m_groundSensor.SetActive(false);
                m_movement.Stop();
            }
            m_phaseHandle.MonitorPhase();
            switch (m_stateHandle.currentState)
            {
                case State.Idle:
                    m_movement.Stop();
                    break;
                case State.Intro:
                    StartCoroutine(IntroRoutine());
                    break;
                case State.Phasing:
                    StopAllCoroutines();
                    StartCoroutine(ChangePhaseRoutine());
                    break;
                case State.Turning:
                    m_phaseHandle.allowPhaseChange = false;
                    m_stateHandle.Wait(m_turnState);
                    if (!m_isRaging)
                    {
                        m_turnHandle.Execute(m_info.turnAnimation, m_info.idleAnimation);
                    }
                    else
                    {
                        m_turnHandle.Execute(m_info.turnAnimation, m_info.overchargedIdle);
                    }
                    m_movement.Stop();
                    break;
                case State.Attacking:
                    m_hitbox.SetInvulnerability(Invulnerability.None);
                    m_stateHandle.Wait(State.ReevaluateSituation);
                    if (m_attackDecider.hasDecidedOnAttack == false)
                    {

                        m_attackDecider.DecideOnAttack();
                    }
                    switch (m_attackDecider.chosenAttack.attack)
                    {
                        case Attack.Pattern1Phase1:
                            StartCoroutine(Pattern1Phase1Attack());
                            break;
                        case Attack.Pattern2Phase1:
                            StartCoroutine(Pattern2Phase1Attack());
                            break;
                        case Attack.Pattern1Phase2:
                            StartCoroutine(Pattern1Phase2Attack());
                            break;
                        case Attack.Pattern2Phase2:
                            StartCoroutine(Pattern2Phase2Attack());
                            break;
                        case Attack.Pattern3Phase2:
                            StartCoroutine(Pattern3Phase2Attack());
                            break;
                        case Attack.Pattern4Phase2:
                            StartCoroutine(Pattern4Phase2Attack());
                            break;
                    }
                    break;

                case State.Chasing:
                    m_stateHandle.SetState(State.Attacking);
                    break;

                case State.Malfunction:
                    StopAllCoroutines();
                    StartCoroutine(OnMlfunctionedRoutine());
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
        }

        public override void ReturnToSpawnPoint()
        {
        }

        protected override void OnForbidFromAttackTarget()
        {
        }
    }
}