using DChild;
using DChild.Gameplay;
using DChild.Gameplay.Characters;
using DChild.Gameplay.Characters.AI;
using DChild.Gameplay.Combat;
using DChild.Gameplay.Pooling;
using DChild.Gameplay.Projectiles;
using DChild.Gameplay.Systems;
using DG.Tweening;
using Holysoft.Event;
using Sirenix.OdinInspector;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq.Expressions;
#if UNITY_EDITOR

using UnityEditor.Experimental.GraphView;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
#endif


public class AdranAI : CombatAIBrain<AdranAI.Info>
{
    [System.Serializable]
    public class Info : BaseInfo
    {
        [SerializeField]
        private PhaseInfo<Phase> m_phaseInfo;
        public PhaseInfo<Phase> phaseInfo => m_phaseInfo;

        [TitleGroup("Animations")]
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_flinch_1;
        public string flinch_1 => m_flinch_1;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_flinch_2;
        public string flinch_2 => m_flinch_2;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_idle;
        public string idle => m_idle;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_idleTwo;
        public string idleTwo => m_idleTwo;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_idleThree;
        public string idleThree => m_idleThree;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_idleFour;
        public string idleFour => m_idleFour;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_idleFive;
        public string idleFive => m_idleFive;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_rageQuake;
        public string rageQuake => m_rageQuake;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_TransitionSizeOneTwo;
        public string TransitionSizeOneTwo => m_TransitionSizeOneTwo;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_TransitionSizeTwoThree;
        public string TransitionSizeTwoThree => m_TransitionSizeTwoThree;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_TransitionSizeThreeFour;
        public string TransitionSizeThreeFour => m_TransitionSizeThreeFour;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_TransitionSizeFourFive;
        public string TransitionSizeFourFive => m_TransitionSizeFourFive;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_TransitionSizeTwoOne;
        public string TransitionSizeTwoOne => m_TransitionSizeTwoOne;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_TransitionSizeThreeTwo;
        public string TransitionSizeThreeTwo => m_TransitionSizeThreeTwo;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_TransitionSizeFourThree;
        public string TransitionSizeFourThree => m_TransitionSizeFourThree;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_TransitionSizeFiveFour;
        public string TransitionSizeFiveFour => m_TransitionSizeFiveFour;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_slamImpactWallLeft;
        public string slamImpactWallLeft => m_slamImpactWallLeft;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_slamImpactWallRight;
        public string slamImpactWallRight => m_slamImpactWallRight;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_slamInitial;
        public string slamInitial => m_slamInitial;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_slamInitial2;
        public string slamInitial2 => m_slamInitial2;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_slamInitial3;
        public string slamInitial3 => m_slamInitial3;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_slamFallingLoop;
        public string slamFallingLoop => m_slamFallingLoop;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_slamLand;
        public string slamLand => m_slamLand;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_slamRollLeft;
        public string slamRollLeft => m_slamRollLeft;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_slamRollRight;
        public string slamRollRight => m_slamRollRight;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_slamRollLeftLoop;
        public string slamRollLeftLoop => m_slamRollLeftLoop;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_slamRollRightLoop;
        public string slamRollRightLoop => m_slamRollRightLoop;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_slamRollRightStop;
        public string slamRollRightStop => m_slamRollRightStop;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_slamRollLeftStop;
        public string slamRollLeftStop => m_slamRollLeftStop;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_slamRollLeftToRight;
        public string slamRollLeftToRight => m_slamRollLeftToRight;
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_slamRollRightToLeft;
        public string slamRollRightToLeft => m_slamRollRightToLeft;

        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_slamRollToIdle;
        public string slamRollToIdle => m_slamRollToIdle;
        public override void Initialize()
        {
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
        Attacking,
        Chasing,
        ReevaluateSituation,
        WaitBehaviourEnd,
    }
    public enum Phase
    {
        PhaseOne,
        PhaseTwo,
        PhaseThree,
        PhaseFour,
        PhaseFive,
    }
    public enum HealthLevel
    {
        LevelOne,
        LevelTwo,
        LevelThree,
        LevelFour,
        LevelFive,
    }
    private enum Attack
    {
        HomingAttack,
        SlamAttack,
    }
    [ShowInInspector]
    private StateHandle<State> m_stateHandle;
    private List<Projectile> m_smallAdrans;
    [ShowInInspector]
    private PhaseHandle<Phase, PhaseInfo> m_phaseHandle;
    [ShowInInspector]
    private RandomAttackDecider<Attack> m_attackDecider;
    [SerializeField, TabGroup("Reference")]
    public Transform leftLimit;
    [SerializeField, TabGroup("Reference")]
    public Transform rightLimit;
    [SerializeField, TabGroup("Reference")]
    public float moveSpeed;
    [SerializeField, TabGroup("Reference")]
    public bool startMovingRight;

    private Vector3 targetPosition;
    private float fixedY;
    private float fixedZ;
    private bool isPaused = false;
    [SerializeField, TabGroup("Reference")]
    private GameObject m_deathFX; 
    [SerializeField, TabGroup("Reference")]
    private RaySensor m_groundSensor;
    [SerializeField, TabGroup("Reference")]
    private SpineEventListener m_spineListner;
    [SerializeField, TabGroup("Reference")]
    private MovementHandle2D m_movement;
    [SerializeField, TabGroup("Reference")]
    private FlinchHandler m_flinchHandler;
    [SerializeField, TabGroup("Reference")]
    private FlinchHandler m_flinchHandler_2;
    [SerializeField, TabGroup("Reference")]
    private Hitbox m_hitbox;
    [SerializeField, TabGroup("Reference")]
    private DeathHandle m_deathHandle;
    [SerializeField, TabGroup("Reference")]
    private CircleCollider2D m_hitboxCollider;
    [SerializeField, TabGroup("Reference")]
    private float dropSpeed = 2f;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private float m_offSetAbovePlayer;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private float m_dropSpeed;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private float m_maxRollSpeed;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private float m_initialSpeed;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private int m_totalRollCount;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private float m_incrementSpeed;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private float m_flightSpeedSlamRoll;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private Transform[] m_limitPoints;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private bool m_startAtPointA;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private BoxCollider2D m_movementArea;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private float m_delayBeforeToRandomArea;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private float m_moveSpeedToRandomArea;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private ParticleSystem m_slamVFX;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private ParticleSystem m_rollVFXRight;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private ParticleSystem m_rollVFXLeft;

    [SerializeField, TabGroup("Small Adran")]
    private Transform m_SummonVfxSize;
    [SerializeField, TabGroup("Small Adran")]
    private Transform m_soulOrbSize;
    [SerializeField, TabGroup("Small Adran")]
    private ParticleSystem[] m_summonFX;
    [SerializeField, TabGroup("Small Adran")]
    private CircleCollider2D m_colliderSizeAdjustment;
    [SerializeField, TabGroup("Small Adran")]
    private GameObject[] m_adranProjectiles;
    [SerializeField, TabGroup("Small Adran")]
    private Transform[] m_summonSpot;
    [SerializeField, TabGroup("Small Adran")]
    private float m_flightSpeed;
    [SerializeField, TabGroup("Small Adran")]
    private float m_flightSpeedReturn;
    [TabGroup("Reference")]
    public HealthLevel m_healthLevel { get; private set; }
    [SerializeField, TabGroup("HealthReference")]
    private float m_levelOne;
    [SerializeField, TabGroup("HealthReference")]
    private float m_levelTwo;
    [SerializeField, TabGroup("HealthReference")]
    private float m_levelThree;
    [SerializeField, TabGroup("HealthReference")]
    private float m_levelFour;
    [SerializeField, TabGroup("HealthReference")]
    private float m_levelFive;
    [SerializeField, TabGroup("HealthReference")]
    private Damage m_damageOnDeathLevel1;
    [SerializeField, TabGroup("HealthReference")]
    private Damage m_damageOnDeathLevel2;
    [SerializeField, TabGroup("HealthReference")]
    private Damage m_damageOnDeathLevel3;
    [SerializeField, TabGroup("HealthReference")]
    private Damage m_damageOnDeathLevel4;
    [SerializeField, TabGroup("Small Adran")]
    private float m_returnTimeOfAdran;
    [ShowInInspector, ReadOnly, TabGroup("Small Adran")]
    private float m_timer;
    [SerializeField, TabGroup("SlamRollPointLocation")]
    private Transform m_Area1;
    [SerializeField, TabGroup("SlamRollPointLocation")]
    private Transform m_Area1Point;
    [SerializeField, TabGroup("SlamRollPointLocation")]
    private Transform m_Area2;
    [SerializeField, TabGroup("SlamRollPointLocation")]
    private Transform m_Area2Point;
    [SerializeField, TabGroup("SlamRollPointLocation")]
    private Transform m_Area3;
    [SerializeField, TabGroup("SlamRollPointLocation")]
    private Transform m_Area3Point;
    [SerializeField, TabGroup("SlamRollPointLocation")]
    private Transform m_Area4;
    [SerializeField, TabGroup("SlamRollPointLocation")]
    private Transform m_Area4Point;


    private bool m_isReturning;
    private PlayerAreaDetection.Area m_currentPlayerArea;
    private bool hasReachedDropZone = false;
    private void OnDisable()
    {
       
    }
    protected override void Start()
    {
        m_phaseHandle = new PhaseHandle<Phase, PhaseInfo>();
        m_phaseHandle.Initialize(Phase.PhaseOne, m_info.phaseInfo, m_character, ChangeState, ApplyPhaseData);
        m_phaseHandle.ApplyChange();
        m_healthLevel = HealthLevel.LevelOne;
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
        targetPosition = GetTargetPosition(startMovingRight);
        base.Start();
    }

    private void PlayerAreaDetection_OnPlayerEnteredArea(PlayerAreaDetection.Area area)
    {
        m_currentPlayerArea = area;
        Debug.Log(m_currentPlayerArea.ToString());
       
    }

    protected override void Awake()
    {
        base.Awake();
        m_damageable.health.Death += Health_Death;
        
        m_attackDecider = new RandomAttackDecider<Attack>();
        m_stateHandle = new StateHandle<State>(State.Intro, State.WaitBehaviourEnd);
        m_smallAdrans = new List<Projectile>();
        UpdateAttackDeciderList();

    }
    //protected override void OnDestroyed(object sender, EventActionArgs eventArgs)
    //{
    //    if (m_phaseHandle.currentPhase == Phase.PhaseFour)
    //    {
    //        if (m_damageable.health.currentValue <= 0)
    //        {
    //            m_phaseHandle.MonitorPhase();
    //            Debug.Log("health 0");
    //            m_damageable.health.SetHealthPercentage(0.03f);

    //        }


    //    }
    //    else
    //    {

    //        base.OnDestroyed(sender, eventArgs);
    //        Debug.Log("Death?");
    //        StopAllCoroutines();
    //        //DeathEvent();
    //        m_deathHandle.enabled = true;
    //        //m_animation.DisableRootMotion();
    //        m_movement.Stop();
    //    }


    //}
    public void DeathEvent()
    {
        //var instance = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_deathFX, gameObject.scene);
        //instance.SpawnAt(new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }
    private void Health_Death(object sender, Holysoft.Event.EventActionArgs eventArgs)
    {
        
        var death = false;
        if (m_phaseHandle.currentPhase == Phase.PhaseFour && death == false)
        {
            if (m_damageable.health.currentValue <= 0)
            {
                m_phaseHandle.MonitorPhase();
                death = true;
                Debug.Log("health 0");
                m_damageable.health.SetHealthPercentage(0.013f);

            }
        }
        else
        {
            death = false;
            Debug.Log("Death?");
            StopAllCoroutines();
            //DeathEvent();
            m_deathHandle.gameObject.SetActive(true);
        }
    }

    public override void SetTarget(IDamageable damageable, Character m_target = null)
    {
        if (damageable != null)
        {
            base.SetTarget(damageable, m_target);
            m_stateHandle.SetState(State.Intro);
            //GameEventMessage.SendEvent("Boss Encounter");
        }
    }
    private IEnumerator IntroductionRoutine()
    {
        m_stateHandle.OverrideState(State.Attacking);
        yield return null;
    }

    private void ChangeState()
    {
        m_stateHandle.SetState(State.Phasing);
        m_phaseHandle.ApplyChange();
    }
    public override void ApplyData()
    {
        if (m_attackDecider != null)
        {
            UpdateAttackDeciderList();
        }
        base.ApplyData();
    }
    private void ApplyPhaseData(PhaseInfo obj)
    {
        if (m_attackDecider != null)
        {
            UpdateAttackDeciderList();
        }
        base.ApplyData();
    }

    private void UpdateAttackDeciderList()
    {
        switch (m_phaseHandle.currentPhase)
        {
            case Phase.PhaseOne:
                m_attackDecider.SetList(new AttackInfo<Attack>(Attack.HomingAttack, 0));
                break;
            case Phase.PhaseTwo:
                m_attackDecider.SetList(new AttackInfo<Attack>(Attack.HomingAttack, 0));
                break;
            case Phase.PhaseThree:
                m_attackDecider.SetList(new AttackInfo<Attack>(Attack.HomingAttack, 0));
                break;
            case Phase.PhaseFour:
                m_attackDecider.SetList(new AttackInfo<Attack>(Attack.HomingAttack, 0));
                break;
            case Phase.PhaseFive:
                m_attackDecider.SetList(new AttackInfo<Attack>(Attack.SlamAttack, 0));
                break;
        }
        m_attackDecider.hasDecidedOnAttack = false;
    }


    [Button]
    public void TryHommingMissiles()
    {
        StopAllCoroutines();
        StartCoroutine(HomingMissileAdranAttack());
    }
    [Button]
    public void TryRollAttack()
    {
        StartCoroutine(RollAttack());
    }
    [SerializeField]
    private float m_returnAbovePlayer;
    private IEnumerator RollAttack()
    {
        #region bitchass code
        //int rollCount = 0;
        //bool movingRight = m_startAtPointA;
        //float rollSpeed = m_initialSpeed;
        ////m_animation.SetAnimation(1, rollLoopAnim, true);
        ////var vfxRoll_1 = movingRight ? m_rollVFXRight : m_rollVFXLeft;
        ////vfxRoll_1.Play();
        //Vector2 targetStartPos = m_startAtPointA ? m_limitPoints[0].position : m_limitPoints[1].position;
        //float currentY = transform.position.y;
        //float startX = transform.position.x;
        //float globalMin = Mathf.Min(m_limitPoints[0].position.x, m_limitPoints[1].position.x);
        //float globalMax = Mathf.Max(m_limitPoints[0].position.x, m_limitPoints[1].position.x);

        //// Example: allow ±10 units from start position
        //float localMin = startX - 30f;
        //float localMax = startX + 30f;

        //// Final allowed range
        //float minX = Mathf.Max(globalMin, localMin);
        //float maxX = Mathf.Min(globalMax, localMax);
        // var rollInitializeAnim = m_startAtPointA ? m_info.slamRollLeft : m_info.slamRollRight;
        // var rollLoopAnim_2 = movingRight ? m_info.slamRollRightLoop : m_info.slamRollLeftLoop;
        #endregion
       // m_animation.EnableRootMotion(false,false);
        Debug.Log(m_currentPlayerArea.ToString());
        var returnToTop = new Vector2(0, 0);
        switch (m_currentPlayerArea)
        {
            case PlayerAreaDetection.Area.Area1NiJan: 
                Debug.Log("in: " + m_currentPlayerArea.ToString());
                yield return LoopingRoutine(m_totalRollCount, true,false,m_maxRollSpeed, m_Area1Point, m_Area2Point);
                locationDrop = m_Area1.position;
                break;

            case PlayerAreaDetection.Area.Area2NiToto:
                Debug.Log("in: " + m_currentPlayerArea.ToString());
                yield return LoopingRoutine(m_totalRollCount, true,false, m_maxRollSpeed, m_Area2Point, m_Area3Point);
                locationDrop = m_Area2.position;
                break;

            case PlayerAreaDetection.Area.Area3NiTommi:
                Debug.Log("in: " + m_currentPlayerArea.ToString());
                yield return LoopingRoutine(m_totalRollCount, false,true, m_maxRollSpeed, m_Area3Point, m_Area2Point);
                locationDrop = m_Area3.position;
                break;

            case PlayerAreaDetection.Area.Area4NiStephen:
                Debug.Log("in: " + m_currentPlayerArea.ToString());
                yield return LoopingRoutine(m_totalRollCount, false,true, m_maxRollSpeed, m_Area4Point, m_Area3Point);
                locationDrop = m_Area4.position;
                break;

        }
        Vector2 fixedDropPos = locationDrop;
        m_animation.SetAnimation(1, m_info.slamRollToIdle,false);
        while (Vector2.Distance(fixedDropPos, transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, fixedDropPos, m_returnAbovePlayer * Time.deltaTime);
            yield return null;
        }

        m_animation.SetAnimation(0, m_info.idle, true);
        m_animation.AddAnimation(1, m_info.idleFive, true, 0);
        yield return new WaitForSeconds(1f);
        Debug.Log("Finished rolling after " + m_totalRollCount + " cycles.");
    }

    private IEnumerator LoopingRoutine(int totalRollCount, bool StartMovingRight, bool BackToStartPointRight, float rollSpeed, Transform startingPoint, Transform targetPoint)
    {
        
        var InitRollCount = 0;
        var rollInitializeAnim = StartMovingRight ? m_info.slamRollRight : m_info.slamRollLeft;
        var rollLoopAnim = StartMovingRight ? m_info.slamRollRightLoop : m_info.slamRollLeftLoop;
        var rollLoopStopAnim = StartMovingRight ? m_info.slamRollRightStop : m_info.slamRollLeftStop;
        var vfxRoll = StartMovingRight ? m_rollVFXLeft   : m_rollVFXRight;

        var rollInitSpineAnim = m_animation.SetAnimation(1, rollInitializeAnim, false);
        yield return new WaitForSpineAnimationComplete(rollInitSpineAnim);
        if (!vfxRoll.isPlaying)
            vfxRoll.Play();
        m_animation.SetAnimation(1, rollLoopAnim, true);
        while (Mathf.Abs(transform.position.x - targetPoint.position.x) >= 0.1f)
        {
            float newX = Mathf.MoveTowards(transform.position.x, targetPoint.position.x, rollSpeed * Time.deltaTime);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            yield return null;

        }
        var rollStopSpineAnim = m_animation.SetAnimation(1, rollLoopStopAnim, false);
        yield return new WaitForSpineAnimationComplete(rollInitSpineAnim);
        InitRollCount++;
        var BackToStartPoint = true;

        while (InitRollCount < totalRollCount)
        {
            vfxRoll.Stop();
            var rollInitializeAnim2 = BackToStartPointRight ? m_info.slamRollRightToLeft : m_info.slamRollLeftToRight;
            var rollLoopStopAnim2 = StartMovingRight ? m_info.slamRollRightStop : m_info.slamRollLeftStop;
            var vfxRoll2 = BackToStartPointRight ? m_rollVFXLeft  : m_rollVFXRight;
            var rollInitSpineAnim2 = m_animation.SetAnimation(1, rollInitializeAnim2, false);
            yield return new WaitForSpineAnimationComplete(rollInitSpineAnim2);
            if (!vfxRoll2.isPlaying)
                vfxRoll2.Play();
            var rollLoopAnim_2 = BackToStartPointRight ? m_info.slamRollRightLoop : m_info.slamRollLeftLoop;
            m_animation.SetAnimation(1, rollLoopAnim_2, true);
    
            Vector2 targetPos = BackToStartPoint ? startingPoint.position : targetPoint.position;
            


            while (Mathf.Abs(transform.position.x - targetPos.x) > 0.1f)
            {
                float newX = Mathf.MoveTowards(transform.position.x, targetPos.x, rollSpeed * Time.deltaTime);
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
                yield return null;
            }

            var rollStopSpineAnim2 = m_animation.SetAnimation(1, rollLoopStopAnim2, false);
            yield return new WaitForSpineAnimationComplete(rollStopSpineAnim2);
            vfxRoll2.Stop();

            BackToStartPoint = !BackToStartPoint;
            BackToStartPointRight = !BackToStartPointRight;
            InitRollCount++;

            //m_animation.SetAnimation(1, rollLoopAnim, true);     

        }
        
        m_animation.AddAnimation(1, m_info.idleFive, true, 0);
        m_animation.SetAnimation(0, m_info.idle, true);
        Debug.Log("Finished rolling after " + m_totalRollCount + " cycles.");
    }

    private IEnumerator LocateRandomWithinArea()
    {
        // Wait for delay before moving
        yield return new WaitForSeconds(m_delayBeforeToRandomArea);

        Vector2 target = GetRandomPositionWithinArea();

        while (Vector2.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, m_moveSpeedToRandomArea * Time.deltaTime);
            yield return null;
        }

        Debug.Log("Arrived at random position!");
    }

    private Vector2 GetRandomPositionWithinArea()
    {
        Bounds bounds = m_movementArea.bounds;

        float randomX = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float randomY = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(randomX, randomY);
    }
    private IEnumerator SlamAttack()
    {
        Debug.Log("slam roll?");
        var AnimationFall = m_animation.SetAnimation(1, m_info.slamInitial, false);
        m_animation.AddAnimation(1, m_info.idleFive, true, 0);
        yield return new WaitForSpineAnimationComplete(AnimationFall);
        Debug.Log("slam slam falling loop??");
        m_animation.SetAnimation(1, m_info.slamFallingLoop, true);
        while (!m_groundSensor.isDetecting)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - m_dropSpeed * Time.deltaTime);
            yield return null;
        }
       // m_slamVFX.Play();
        var SlamAnimation = m_animation.SetAnimation(1, m_info.slamLand, false);
        m_animation.AddAnimation(1, m_info.idleFive, true, 0);
        yield return new WaitForSpineAnimationComplete(SlamAnimation);
        m_movement.Stop();
    }

    private IEnumerator SlamRollAttack()
    {
        Pause();
        m_stateHandle.Wait(State.ReevaluateSituation);   
        yield return SlamRollLocatePlayer();
        yield return SlamAttack();
        yield return RollAttack();
        //yield return LocateRandomWithinArea();
        m_attackDecider.hasDecidedOnAttack = false;
        m_stateHandle.ApplyQueuedState();
    }

    
    private IEnumerator GoToReachableXY(PoolableObject positiony, PoolableObject positiony1 = null, PoolableObject positiony2 = null)
    {
        if (positiony == null)
            yield break;
        while (true)
        {
            // stop immediately if destroyed or null
            if (positiony == null)
                yield break;
            var adranreachedAreaToActivate = positiony.GetComponent<SmallAdran>().m_reachedAreaToActivate;
            // stop once it reached its activation area
            if (adranreachedAreaToActivate)
                break;

            // move downward safely
            positiony.transform.position += Vector3.down * dropSpeed * Time.deltaTime;

            yield return null;
        }

        // Double-check before continuing
        if (positiony == null)
            yield break;

        yield return SpawningOfHomingMissiles(positiony);
        yield return AttackAnimationForSpawnedAdran(positiony);
        yield return ReturningToSpawnPointSummonedAdran(positiony);


        //yield return SpawningOfHomingMissiles(positiony);

    }
    private IEnumerator HomingMissileAdranAttack()
    {
        m_stateHandle.Wait(State.ReevaluateSituation);
        Pause();  
        yield return HomingMissilleAnimation();
        yield return HomingMissileProjectile();     
        m_attackDecider.hasDecidedOnAttack = false;
        m_stateHandle.ApplyQueuedState();
    }
    [Button]
    public void TrySlamRollAttackRoutine()
    {
        StartCoroutine(SlamRollAttack());
    }
    [Button]
    public void TrySlamRollAttack()
    {
        StartCoroutine(SlamRollLocatePlayer());
    }
    private Vector2 locationDrop;
    private bool m_adranDropLocation;
    [SerializeField]
    private GameObject[] m_areaDetectionCollider;
    private IEnumerator SlamRollLocatePlayer()
    {
        PlayerAreaDetection.OnPlayerEnteredArea += PlayerAreaDetection_OnPlayerEnteredArea;
        for (int i = 0; i < m_areaDetectionCollider.Length; i++)
        {
            m_areaDetectionCollider[i].gameObject.SetActive(true);
        }
        Vector2 locationDrop = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        Debug.Log(locationDrop.ToString());   
        PlayerAreaDetection.OnPlayerEnteredArea -= PlayerAreaDetection_OnPlayerEnteredArea;
        for (int i = 0; i < m_areaDetectionCollider.Length; i++)
        {
            m_areaDetectionCollider[i].gameObject.SetActive(false);
        }
        Vector2 playerPos = GameplaySystem.playerManager.player.character.transform.position;
        yield return new WaitForSeconds(0.5f);
        switch (m_currentPlayerArea)
        {
            case PlayerAreaDetection.Area.Area1NiJan:
                locationDrop = m_Area1.position;
                break;

            case PlayerAreaDetection.Area.Area2NiToto:
                locationDrop = m_Area2.position;
                break;

            case PlayerAreaDetection.Area.Area3NiTommi:
                locationDrop = m_Area3.position;
                break;

            case PlayerAreaDetection.Area.Area4NiStephen:
                locationDrop = m_Area4.position;
                break;
 
        }   
       
        while (Vector2.Distance(locationDrop, m_centerMass.transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, locationDrop, m_flightSpeedSlamRoll * Time.deltaTime);
            yield return null;
        }
        
        // Check if the object is very close to the target position
        //if (Vector2.Distance(transform.position, targetPos) <= 0.05f)
        //{
        //    Debug.Log("Now floating above player");
        //    break;
        //}




    }
    [SerializeField,ReadOnly]
    private int m_killedAdran;
    [SerializeField, ReadOnly]
    private int m_adranReturned;
    [SerializeField]
    private float m_delayAdranSpawn;

    private void UnsubscribeEvents(PoolableObject instance)
    {
        if (instance == null)
            return;


        instance.GetComponent<SmallAdran>().GotDamagedByPlayer -= AdranAI_GotDamagedByPlayer;
        instance.GetComponent<SmallAdran>().SmallAdranGotDestroyed -= SmallAdranGotDestroyed;
        instance.GetComponent<SmallAdran>().SmallAdranReachedZone -= SmallAdranReachedZoneEvent;
    }
    private IEnumerator HomingMissileProjectile()
    {
        hasReachedDropZone = false;
        
        HealthTracker();
       
        if (m_healthLevel == HealthLevel.LevelOne)
        {
            var randomInstance = UnityEngine.Random.Range(0, 4);
            var randomProjectiles = UnityEngine.Random.Range(0, 4);
            m_summonFX[randomInstance].Play();
            //yield return new WaitForSeconds(1f);
            var instance1 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[randomProjectiles], gameObject.scene);
            instance1.GetComponent<SmallAdran>().GotDamagedByPlayer += AdranAI_GotDamagedByPlayer;
            instance1.GetComponent<SmallAdran>().SmallAdranGotDestroyed += SmallAdranGotDestroyed;
            instance1.GetComponent<SmallAdran>().SmallAdranReachedZone += SmallAdranReachedZoneEvent;
            yield return new WaitForSeconds(m_delayAdranSpawn);
            instance1.SpawnAt(new Vector2(m_summonSpot[randomInstance].position.x, m_summonSpot[randomInstance].position.y), Quaternion.identity);
            instance1.transform.rotation = Quaternion.identity;
            StartCoroutine(GoToReachableXY(instance1));
            while (instance1 != null)
                yield return null;

            Debug.Log("done destroyed all 1");
            if (m_killedAdran == 1)
            {
                DamageCheck();
                UnsubscribeEvents(instance1);
                yield return new WaitForSeconds(1f);
                m_adranReturned = 0;
                m_killedAdran = 0;
                yield break;

            }
                
            yield return HomingMissileReturnAnimation();
            
            

        }
        else if (m_healthLevel == HealthLevel.LevelTwo || m_healthLevel == HealthLevel.LevelFour)
        {
            var randomInstance = UnityEngine.Random.Range(0, 4);
            int randomInstance_2;
            do
            {
                randomInstance_2 = UnityEngine.Random.Range(0, 4);
            } while (randomInstance_2 == randomInstance);
            var randomProjectiles = UnityEngine.Random.Range(0, 4);
            var instance1 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[randomProjectiles], gameObject.scene);
            instance1.GetComponent<SmallAdran>().GotDamagedByPlayer += AdranAI_GotDamagedByPlayer;
            instance1.GetComponent<SmallAdran>().SmallAdranGotDestroyed += SmallAdranGotDestroyed;
            var instance2 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[randomProjectiles], gameObject.scene);
            instance2.GetComponent<SmallAdran>().SmallAdranGotDestroyed += SmallAdranGotDestroyed;
            instance2.GetComponent<SmallAdran>().GotDamagedByPlayer += AdranAI_GotDamagedByPlayer;
            instance1.GetComponent<SmallAdran>().SmallAdranReachedZone += SmallAdranReachedZoneEvent;
            instance2.GetComponent<SmallAdran>().SmallAdranReachedZone += SmallAdranReachedZoneEvent;
            m_summonFX[randomInstance].Play();
            m_summonFX[randomInstance_2].Play();
            yield return new WaitForSeconds(m_delayAdranSpawn);
            instance1.SpawnAt(new Vector2(m_summonSpot[randomInstance].position.x, m_summonSpot[randomInstance].position.y), Quaternion.identity);         
            instance1.transform.rotation = Quaternion.identity;     
            instance2.SpawnAt(new Vector2(m_summonSpot[randomInstance_2].position.x, m_summonSpot[randomInstance_2].position.y), Quaternion.identity);
            instance2.transform.rotation = Quaternion.identity;
            StartCoroutine(GoToReachableXY(instance1));
            StartCoroutine(GoToReachableXY(instance2));

            while (instance1 != null ||
               instance2 != null)
                yield return null;
            Debug.Log("done destroyed all 2");
            if (m_killedAdran == 2)
            {
                DamageCheck();
                UnsubscribeEvents(instance1);
                UnsubscribeEvents(instance2);
                yield return new WaitForSeconds(0.5f);
                m_adranReturned = 0;
                m_killedAdran = 0;
                yield break;

            }         
            yield return HomingMissileReturnAnimation();
            
            

        }
        else if (m_healthLevel == HealthLevel.LevelThree)
        {
            var randomInstance = UnityEngine.Random.Range(0, 4);
            int randomInstance_2;
            int randomInstance_3;           
            do
            {
                randomInstance_2 = UnityEngine.Random.Range(0, 4);
            } while (randomInstance_2 == randomInstance);

            do
            {
                randomInstance_3 = UnityEngine.Random.Range(0, 4);
            } while (randomInstance_3 == randomInstance || randomInstance_3 == randomInstance_2);
            var randomProjectiles = UnityEngine.Random.Range(0, 4);
            var instance1 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[randomProjectiles], gameObject.scene);
            instance1.GetComponent<SmallAdran>().GotDamagedByPlayer += AdranAI_GotDamagedByPlayer;
            instance1.GetComponent<SmallAdran>().SmallAdranGotDestroyed += SmallAdranGotDestroyed;
            var instance2 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[randomProjectiles], gameObject.scene);
            instance2.GetComponent<SmallAdran>().SmallAdranGotDestroyed += SmallAdranGotDestroyed;
            instance2.GetComponent<SmallAdran>().GotDamagedByPlayer += AdranAI_GotDamagedByPlayer;
            var instance3 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[randomProjectiles], gameObject.scene);
            instance3.GetComponent<SmallAdran>().SmallAdranGotDestroyed += SmallAdranGotDestroyed;
            instance3.GetComponent<SmallAdran>().GotDamagedByPlayer += AdranAI_GotDamagedByPlayer;
            instance1.GetComponent<SmallAdran>().SmallAdranReachedZone += SmallAdranReachedZoneEvent;
            instance2.GetComponent<SmallAdran>().SmallAdranReachedZone += SmallAdranReachedZoneEvent;
            instance3.GetComponent<SmallAdran>().SmallAdranReachedZone += SmallAdranReachedZoneEvent;
            m_summonFX[randomInstance].Play();
            m_summonFX[randomInstance_2].Play();
            m_summonFX[randomInstance_3].Play();
            yield return new WaitForSeconds(m_delayAdranSpawn);
            instance1.SpawnAt(new Vector2(m_summonSpot[randomInstance].position.x, m_summonSpot[randomInstance].position.y), Quaternion.identity);         
            instance1.transform.rotation = Quaternion.identity;
            instance2.SpawnAt(new Vector2(m_summonSpot[randomInstance_2].position.x, m_summonSpot[randomInstance_2].position.y), Quaternion.identity);            
            instance2.transform.rotation = Quaternion.identity;          
            instance3.SpawnAt(new Vector2(m_summonSpot[randomInstance_3].position.x, m_summonSpot[randomInstance_3].position.y), Quaternion.identity);            
            instance3.transform.rotation = Quaternion.identity;
            StartCoroutine(GoToReachableXY(instance1));
            StartCoroutine(GoToReachableXY(instance2));
            StartCoroutine(GoToReachableXY(instance3));
            while (instance1 != null || instance2 != null || instance3 != null)
                yield return null;

            Debug.Log("done destroyed all 3 ");
            if (m_killedAdran == 3)
            {
                DamageCheck();
                UnsubscribeEvents(instance1);
                UnsubscribeEvents(instance2);
                UnsubscribeEvents(instance3);
                yield return new WaitForSeconds(1f);
                m_adranReturned = 0;
                m_killedAdran = 0;
                yield break;

            }

            yield return HomingMissileReturnAnimation();
           
        }
        else if (m_healthLevel == HealthLevel.LevelFour) 
        {
            var randomInstance = UnityEngine.Random.Range(0, 4);
            int randomInstance_2;
            do
            {
                randomInstance_2 = UnityEngine.Random.Range(0, 4);
            } while (randomInstance_2 == randomInstance);
            var randomProjectiles = UnityEngine.Random.Range(0, 4);
            var instance1 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[randomProjectiles], gameObject.scene);
            instance1.GetComponent<SmallAdran>().GotDamagedByPlayer += AdranAI_GotDamagedByPlayer;
            instance1.GetComponent<SmallAdran>().SmallAdranGotDestroyed += SmallAdranGotDestroyed;
            var instance2 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[randomProjectiles], gameObject.scene);
            instance2.GetComponent<SmallAdran>().SmallAdranGotDestroyed += SmallAdranGotDestroyed;
            instance2.GetComponent<SmallAdran>().GotDamagedByPlayer += AdranAI_GotDamagedByPlayer;
            instance1.GetComponent<SmallAdran>().SmallAdranReachedZone += SmallAdranReachedZoneEvent;
            instance2.GetComponent<SmallAdran>().SmallAdranReachedZone += SmallAdranReachedZoneEvent;
            m_summonFX[randomInstance].Play();
            m_summonFX[randomInstance_2].Play();
            yield return new WaitForSeconds(m_delayAdranSpawn);
            instance1.SpawnAt(new Vector2(m_summonSpot[randomInstance].position.x, m_summonSpot[randomInstance].position.y), Quaternion.identity);
            instance1.transform.rotation = Quaternion.identity;
            instance2.SpawnAt(new Vector2(m_summonSpot[randomInstance_2].position.x, m_summonSpot[randomInstance_2].position.y), Quaternion.identity);
            instance2.transform.rotation = Quaternion.identity;
               
            StartCoroutine(GoToReachableXY(instance1));
            StartCoroutine(GoToReachableXY(instance2));

            while (instance1 != null || instance2 != null)
                yield return null;

            Debug.Log("done destroyed all 2");
            if (m_killedAdran == 2)
            {
                DamageCheck();
                UnsubscribeEvents(instance1);
                UnsubscribeEvents(instance2);
                yield return new WaitForSeconds(1f);
                m_adranReturned = 0;
                m_killedAdran = 0;
                yield break;

            }
            yield return HomingMissileReturnAnimation();
           
        }


        #region old ass code
        //if (m_healthLevel == HealthLevel.LevelTwo)
        //{
        //        var randomInstance = UnityEngine.Random.Range(0, 4);
        //        int randomInstance_2;
        //        do
        //        {
        //            randomInstance_2 = UnityEngine.Random.Range(0, 4);
        //        } while (randomInstance_2 == randomInstance);
        //        var randomProjectiles = UnityEngine.Random.Range(0, 4);
        //        m_summonFX[randomInstance].Play();
        //        yield return new WaitForSeconds(1f);
        //        var instance1 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[randomProjectiles], gameObject.scene);
        //        instance1.GetComponent<SmallAdran>().GotDamagedByPlayer += AdranAI_GotDamagedByPlayer;
        //        instance1.GetComponent<SmallAdran>().SmallAdranGotDestroyed += SmallAdranGotDestroyed;
        //        instance1.SpawnAt(new Vector2(m_summonSpot[randomInstance].position.x, m_summonSpot[randomInstance].position.y), Quaternion.identity);
        //        yield return new WaitForSeconds(.3f);
        //        instance1.transform.rotation = Quaternion.identity;
        //        m_summonFX[randomInstance_2].Play();
        //        yield return new WaitForSeconds(1f);
        //        var instance2 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[randomProjectiles], gameObject.scene);               
        //        instance2.GetComponent<SmallAdran>().SmallAdranGotDestroyed += SmallAdranGotDestroyed;
        //        instance2.GetComponent<SmallAdran>().GotDamagedByPlayer += AdranAI_GotDamagedByPlayer;
        //        instance2.SpawnAt(new Vector2(m_summonSpot[randomInstance_2].position.x, m_summonSpot[randomInstance_2].position.y), Quaternion.identity);
        //        yield return new WaitForSeconds(.3f);
        //        instance2.transform.rotation = Quaternion.identity;

        //        StartCoroutine(SpawningOfHomingMissiles(instance1));
        //        yield return SpawningOfHomingMissiles(instance2);

        //        if (instance1 != null)
        //        {
        //            instance1.GetComponent<SmallAdran>().GotDamagedByPlayer -= AdranAI_GotDamagedByPlayer;
        //            instance1.GetComponent<SmallAdran>().SmallAdranGotDestroyed -= AdranAI_GotDamagedByPlayer;
        //            yield return HomingMissileReturnAnimation();
        //        }
        //        if (instance2 != null)
        //        {
        //            instance2.GetComponent<SmallAdran>().GotDamagedByPlayer -= AdranAI_GotDamagedByPlayer;
        //            instance2.GetComponent<SmallAdran>().SmallAdranGotDestroyed -= AdranAI_GotDamagedByPlayer;
        //            yield return HomingMissileReturnAnimation();
        //        }
        //}
        //else
        //{
        //    var randomProjectiles = UnityEngine.Random.Range(0, 4);
        //    var randomSummonSpot = UnityEngine.Random.Range(0, 4);
        //    m_summonFX[randomSummonSpot].Play();
        //    yield return new WaitForSeconds(1);
        //    var instance = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[randomProjectiles], gameObject.scene);
        //    instance.GetComponent<SmallAdran>().GotDamagedByPlayer += AdranAI_GotDamagedByPlayer;
        //    instance.GetComponent<SmallAdran>().SmallAdranGotDestroyed += SmallAdranGotDestroyed;
        //    instance.SpawnAt(new Vector2(m_summonSpot[randomSummonSpot].position.x, m_summonSpot[randomSummonSpot].position.y), Quaternion.identity);
        //    yield return new WaitForSeconds(.3f);
        //    instance.transform.rotation = Quaternion.identity;
        //    yield return SpawningOfHomingMissiles(instance);

        //    if (instance != null)
        //    {
        //        instance.GetComponent<SmallAdran>().GotDamagedByPlayer -= AdranAI_GotDamagedByPlayer;
        //        instance.GetComponent<SmallAdran>().SmallAdranGotDestroyed -= SmallAdranGotDestroyed;
        //        yield return HomingMissileReturnAnimation();
        //    }
        //}
        #endregion

        yield return new WaitForSeconds(0.5f);
        m_adranReturned = 0;
        m_killedAdran = 0;

    }

    private void SmallAdranReachedZoneEvent(object sender, EventActionArgs eventArgs)
    {
        //hasReachedDropZone = true;
    }
    private Damage m_FakeDamage;
    private void SmallAdranGotDestroyed(object sender, Holysoft.Event.EventActionArgs eventArgs)
    {
        Debug.Log("Damage adran");
        m_FakeDamage.value = 0;
        GameplaySystem.combatManager.Damage(m_damageable, m_FakeDamage);
        stopHomingMissile = true;       
        m_killedAdran++;
    }

    private void DamageCheck()
    {
        switch (m_healthLevel)
        {
            case HealthLevel.LevelOne:
                GameplaySystem.combatManager.Damage(m_damageable, m_damageOnDeathLevel1);
                break;
            case HealthLevel.LevelTwo:
                GameplaySystem.combatManager.Damage(m_damageable, m_damageOnDeathLevel2);
                break;
            case HealthLevel.LevelThree:
                    GameplaySystem.combatManager.Damage(m_damageable, m_damageOnDeathLevel3);
                break;
            case HealthLevel.LevelFour:
                    GameplaySystem.combatManager.Damage(m_damageable, m_damageOnDeathLevel4);
                break;
        }
      
    }

    private IEnumerator FlinchStrongAnimationRoutine()
    {
        m_hitbox.Disable();
        m_flinchHandler.SetAnimation(m_info.flinch_2);
        m_flinchHandler.Flinch();
        yield return new WaitForSeconds(0.5f);
        m_flinchHandler.SetAnimation(m_info.flinch_1);
        m_hitbox.Enable();
    }
    private void AdranAI_GotDamagedByPlayer(object sender, Holysoft.Event.EventActionArgs eventArgs)
    {

        Debug.Log("small adran got hit by player king ina mo ka");
        #region FlincnIdleSetter?
        //if (m_healthLevel == HealthLevel.LevelTwo)
        //{
        //    m_flinchHandler.SetIdleAnimation(m_info.idleTwo);
        //} else if (m_healthLevel == HealthLevel.LevelThree)
        //{
        //    m_flinchHandler.SetIdleAnimation(m_info.idleThree);
        //}
        //else if (m_healthLevel == HealthLevel.LevelFour)
        //{
        //    m_flinchHandler.SetIdleAnimation(m_info.idleThree);
        //}
        //else
        //{
        //    m_flinchHandler.SetIdleAnimation(m_info.idleFive);
        //} 
        #endregion
        m_flinchHandler.Flinch();


    }

    private IEnumerator ReturningToSpawnPointSummonedAdran(PoolableObject instance)
    {
        while (true)
        {
            

            if (instance == null)
            {
                Debug.Log("Instance destroyed, stopping coroutine.");
                //m_movement.Stop();
                yield break;
            }
            var adranDestroyed = instance.GetComponent<SmallAdran>().isDestroyed;
            if (adranDestroyed == true)
                yield break;

            var damageable = instance.GetComponent<Damageable>();
            if (damageable == null || damageable.health.currentValue <= 0)
            {
                Debug.Log("Instance dead or missing Damageable, stopping coroutine.");
                m_movement.Stop();
                yield break;
            }

            var adran = instance.GetComponent<SmallAdran>();
            if (adran == null)
            {
                Debug.Log("Missing SmallAdran component, stopping coroutine.");
                yield break;
            }

            float distance = Vector2.Distance(instance.transform.position, m_summonSpot[4].position);

            if (distance > 1f)
            {
                instance.transform.position = Vector2.MoveTowards(
                    instance.transform.position,
                    m_summonSpot[4].position,
                    m_flightSpeedReturn * Time.deltaTime
                );

                yield return null;
                continue;
            }

            // If close enough
            Debug.Log("Done?");
            m_adranReturned++;
            UnsubscribeEvents(instance);
            
            Destroy(instance.gameObject);
            yield break;
        }

    }

    private IEnumerator AttackAnimationForSpawnedAdran(PoolableObject instance)
    {

       
        if (instance == null || instance.GetComponent<Damageable>().health.currentValue <= 0)
        {
            Debug.Log("inside the instance == null || instance.GetComponent<Damageable>().health.currentValue <= 0 ");
            //Debug.Log(instance.gameObject.name);
           // Destroy(instance.gameObject);
           // m_movement.Stop();
            yield break;
        }
        //var instanceBool = instance.gameObject.GetComponent<SmallAdran>().m_stopHomingMissile;
        Debug.Log("???");
        if(instance != null)
        {
            var summonSpotSmallAdran = instance.GetComponent<SmallAdran>();
            yield return summonSpotSmallAdran.SetAttackAnimation();
            Debug.Log("hello");
        }

        yield return null;

    }
    private bool stopHomingMissile = false;
    private IEnumerator SpawningOfHomingMissiles(PoolableObject instance)
    {
        // m_isReturning = false;
        m_hitbox.Enable();
        float timer = 0f;
        bool returning = false;
        //bool reIterate = true;
       // stopHomingMissile = false;
        Resume();

        while (true)
        {
            //var adranDestroyed = instance.GetComponent<SmallAdran>().isDestroyed;
            //if (adranDestroyed == true)
            //    yield break;

            if (instance == null)
            {
                Debug.Log("Instance destroyed, exiting coroutine.");
                yield break;
            }

            var adran = instance.GetComponent<SmallAdran>();
            if (adran == null)
            {
                Debug.Log("Missing SmallAdran component, exiting coroutine.");
                yield break;
            }

            if (adran.m_stopHomingMissile)
            {
                Debug.Log("Stop signal received, exiting coroutine.");
                yield break;
            }

            if (!returning)
            {
               // adran.ColliderController(true);
                adran.isReturningToSummonSpot = false;

                Vector2 playerPos = GameplaySystem.playerManager.player.character.transform.position;
                instance.transform.position = Vector2.MoveTowards(instance.transform.position, m_targetInfo.position, m_flightSpeed * Time.deltaTime);
                float distance = Vector2.Distance(instance.transform.position, m_targetInfo.position);

                timer += Time.deltaTime;
                Debug.Log($"Timer={timer}, Dist={distance}");

                if (timer >= m_returnTimeOfAdran || distance <= 15f)
                {
                    Debug.Log($"Coroutine ending — Timer={timer:F2}, Distance={distance:F2}");
                    returning = true;
                    timer = 0f;
                    yield return null;  
                    yield break;
                }
            }
            Debug.Log("Still in loop");
            yield return null;
        }
    
        //else
        //{
        //    if (instance == null || instance.GetComponent<Damageable>().health.currentValue <= 0)
        //    {
        //        Debug.Log("inside the instance == null || instance.GetComponent<Damageable>().health.currentValue <= 0 ");
        //        Debug.Log(instance.gameObject.name);
        //        returning = true;
        //        m_movement.Stop();
        //        yield break;
        //    }
        //    else
        //    {
        //        var summonSpotSmallAdran = instance.GetComponent<SmallAdran>();
        //        summonSpotSmallAdran.startingPosition = m_summonSpot[4].position;
        //        Vector2 direction = m_summonSpot[4].position - instance.transform.position;
        //        summonSpotSmallAdran.isReturningToSummonSpot = true;
        //        yield return summonSpotSmallAdran.SetAttackAnimation();
        //        #region FlippinBurgers
        //        //var toSpotDotProduct = Vector2.Dot(Vector2.right, direction.normalized);
        //        //var toSpotDotSign = Mathf.Sign(toSpotDotProduct);
        //        //instance.transform.localScale = new Vector3(toSpotDotSign, instance.transform.localScale.y, instance.transform.localScale.z); 
        //        #endregion
        //        while (Vector2.Distance(instance.transform.position, m_summonSpot[4].position) > 10f)
        //        {
        //            instance.transform.position = Vector2.MoveTowards(instance.transform.position, m_summonSpot[4].position, m_flightSpeedReturn * Time.deltaTime);
        //            Debug.Log("else");
        //            yield return null;
        //        }


        //    }

        //    if (Vector2.Distance(instance.transform.position, m_summonSpot[4].position) <= 10f)
        //    {

        //        instance.GetComponent<SmallAdran>().isReturningToSummonSpot = true;
        //        Debug.Log("Done?");
        //        m_adranReturned++;
        //        Destroy(instance.gameObject);
        //        yield break;


        //    }
        //}

      

    }

    private IEnumerator HomingMissileReturnAnimation()
    {
        HealthTracker();
        if (m_healthLevel == HealthLevel.LevelOne)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeTwoOne, m_info.idle);
            m_colliderSizeAdjustment.radius = 16f;
            m_hitboxCollider.radius = 15f;
            m_soulOrbSize.localScale = new Vector2(1f,1f);
        }
        else if (m_healthLevel == HealthLevel.LevelTwo)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeThreeTwo, m_info.idleTwo);

            m_colliderSizeAdjustment.radius = 13f;
            m_hitboxCollider.radius = 12f;
            m_soulOrbSize.localScale = new Vector2(.9f, .9f);
        }
        else if (m_healthLevel == HealthLevel.LevelThree)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeFourThree, m_info.idleThree);
            m_colliderSizeAdjustment.radius = 9.5f;
            m_hitboxCollider.radius = 8.5f;
            m_soulOrbSize.localScale = new Vector2(0.75f, 0.75f);
        }
        else if (m_healthLevel == HealthLevel.LevelFour)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeFiveFour, m_info.idleFour);
            m_colliderSizeAdjustment.radius = 7.85f;
            m_hitboxCollider.radius = 6.85f;
            m_soulOrbSize.localScale = new Vector2(0.65f,0.65f);
        }
        yield return new WaitForSeconds(1f);
    }//end of HomingMissileReturnAnimation()
    private IEnumerator HomingMissilleAnimation()
    {
        HealthTracker();
        m_animation.SetAnimation(0, m_info.idle, true);
      
        if (m_healthLevel == HealthLevel.LevelOne)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeOneTwo, m_info.idleTwo);
            m_colliderSizeAdjustment.radius = 13f;
            m_hitboxCollider.radius = 12f;
            m_SummonVfxSize.transform.localScale = new Vector3(0.8f, 0.8f, m_SummonVfxSize.localScale.z);
            m_soulOrbSize.localScale = new Vector2(0.9f, 0.9f);
        }
        else if (m_healthLevel == HealthLevel.LevelTwo)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeTwoThree, m_info.idleThree);
            m_colliderSizeAdjustment.radius = 9.5f;
            m_hitboxCollider.radius = 8.5f;
            m_SummonVfxSize.transform.localScale = new Vector3(0.6f, 0.6f, m_SummonVfxSize.localScale.z);
            m_soulOrbSize.localScale = new Vector2(0.75f, 0.75f);
        }
        else if (m_healthLevel == HealthLevel.LevelThree)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeThreeFour, m_info.idleFour);
            m_colliderSizeAdjustment.radius = 7.85f;
            m_hitboxCollider.radius = 6.85f;
            m_SummonVfxSize.transform.localScale = new Vector3(0.5f, 0.5f, m_SummonVfxSize.localScale.z);
            m_soulOrbSize.localScale = new Vector2(0.65f, 0.65f);
        }
        else if (m_healthLevel == HealthLevel.LevelFour)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeFourFive, m_info.idleFive);
            m_colliderSizeAdjustment.radius = 6.5f;
            m_hitboxCollider.radius = 6f;
            m_SummonVfxSize.localPosition = new Vector2(m_SummonVfxSize.localPosition.x, m_SummonVfxSize.localPosition.x - .6f);
            m_SummonVfxSize.transform.localScale = new Vector3(0.5f, 0.5f, m_SummonVfxSize.localScale.z);
            m_soulOrbSize.localScale = new Vector2(0.45f, 0.45f);
            //  m_SummonVfxSize.transform.localScale = new Vector3(0.4f, 0.4f, m_SummonVfxSize.localScale.z);
        }
        //m_animation.SetAnimation(3, m_info.idle, true);
    }//end of HomingMissilleAnimation()

    private IEnumerator AnimationSetterHomingMissile(string transitionIdleAnimation, string idleAnimation)
    {
        var sizeTransition = m_animation.SetAnimation(1, transitionIdleAnimation, false);
        m_animation.AddAnimation(1, idleAnimation, true, 0);
        yield return null;
        //yield return new WaitForSpineAnimationComplete(sizeTransition);
    }
    private IEnumerator AnimationSetterForIdle(string transitionIdleAnimation, string idleAnimation)
    {
        var sizeTransition = m_animation.SetAnimation(1, transitionIdleAnimation, false);
        m_animation.AddAnimation(1, idleAnimation, true, 0);
        yield return new WaitForSpineAnimationComplete(sizeTransition);
    }
    [Button]
    private void HealthTracker()
    {
        float[] healthLevels = { m_levelFive, m_levelFour, m_levelThree, m_levelTwo };
        HealthLevel[] healthEnum = { HealthLevel.LevelFive, HealthLevel.LevelFour, HealthLevel.LevelThree, HealthLevel.LevelTwo };

        for (int i = 0; i < healthLevels.Length; i++)
        {
            if (m_damageable.health.currentValue <= healthLevels[i])
            {
                m_healthLevel = healthEnum[i];
                break;
            }
        }

        Debug.Log("Current Health Level: " + m_healthLevel);
    }
    
    private void HorizontalMovement()
    {
        if (isPaused) return;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (Mathf.Abs(transform.position.x - targetPosition.x) < 0.01f)
        {
            bool goingRight = targetPosition.x == rightLimit.position.x;
            targetPosition = GetTargetPosition(!goingRight);
        }
    }
    public void Pause()
    {
        isPaused = true;
    }

    public void Resume()
    {
        isPaused = false;
    }
    private Vector3 GetTargetPosition(bool toRight)
    {
        float targetX = toRight ? rightLimit.position.x : leftLimit.position.x;
        return new Vector3(targetX, fixedY, fixedZ);
    }
    private IEnumerator HorizontalMovementRoutine()
    {
        bool movingRight = m_startAtPointA;
        float rollSpeed = m_initialSpeed;
        Vector2 targetStartPos = m_startAtPointA ? m_limitPoints[0].position : m_limitPoints[1].position;
        float currentY = transform.position.y;

        while (Mathf.Abs(transform.position.x - targetStartPos.x) > 0.1f)
        {
            float newX_1 = Mathf.MoveTowards(transform.position.x, targetStartPos.x, m_initialSpeed * Time.deltaTime);
            transform.position = new Vector3(newX_1, currentY, transform.position.z);
            yield return null;
        }
        Debug.Log("Reached the starting point, beginning the rolling attack!");
            Vector2 targetPos = movingRight ? m_limitPoints[1].position : m_limitPoints[0].position;

            float newX = Mathf.MoveTowards(transform.position.x, targetPos.x, rollSpeed * Time.deltaTime);
            transform.position = new Vector3(newX, currentY, transform.position.z);  // Keep the Y position constant
            if (Mathf.Abs(transform.position.x - targetPos.x) <= 0.1f)
            {
                movingRight = !movingRight;
                Debug.Log("Switching direction!");
                rollSpeed = Mathf.Min(rollSpeed + m_incrementSpeed, m_maxRollSpeed);
            }
            yield return null;
        
        m_animation.SetAnimation(0, m_info.idle, true);
        m_animation.AddAnimation(1, m_info.idleFive, true, 0);
        Debug.Log("Finished rolling after " + m_totalRollCount + " cycles.");
    }
    private IEnumerator ChangePhaseRoutine()
    {
        m_stateHandle.Wait(State.ReevaluateSituation);
        m_hitbox.Disable();
        switch (m_phaseHandle.currentPhase)
        {
            //case Phase.PhaseTwo:
            //    m_colliderSizeAdjustment.radius = 13f;
            //    yield return AnimationSetterForIdle(m_info.TransitionSizeOneTwo, m_info.idleTwo);
            //    // m_SummonVfxSize.transform.localScale = new Vector3(0.8f, 0.8f, m_SummonVfxSize.localScale.z);
            //    break;
            //case Phase.PhaseThree:
            //    m_colliderSizeAdjustment.radius = 9.5f;
            //    yield return AnimationSetterForIdle(m_info.TransitionSizeTwoThree, m_info.idleThree);
            //    // m_SummonVfxSize.transform.localScale = new Vector3(0.6f, 0.6f, m_SummonVfxSize.localScale.z);
            //    break;
            //case Phase.PhaseFour:
            //    m_colliderSizeAdjustment.radius = 7.85f;
            //    yield return AnimationSetterForIdle(m_info.TransitionSizeThreeFour, m_info.idleFour);
            //    //m_SummonVfxSize.transform.localScale = new Vector3(0.5f, 0.5f, m_SummonVfxSize.localScale.z);
            //    break;
            case Phase.PhaseFive:
                Pause();
                m_colliderSizeAdjustment.radius = 6.5f;
                yield return new WaitForSeconds(0.3f);
                var sizeTransition = m_animation.SetAnimation(1, m_info.rageQuake, false);
                m_animation.AddAnimation(1, m_info.idleFive, true, 0);
                yield return new WaitForSpineAnimationComplete(sizeTransition);
                break;
        }
        m_hitbox.Enable();
        m_phaseHandle.ApplyChange();
        m_attackDecider.hasDecidedOnAttack = false;
        m_stateHandle.ApplyQueuedState();
    }
    
    private void Update()
    {
        HealthTracker();      
        m_phaseHandle.MonitorPhase();
        HorizontalMovement();
        Debug.Log(m_phaseHandle.currentPhase.ToString());
        m_animation.SetAnimation(0, m_info.idle, true);
        //StopAllCoroutines();
        switch (m_stateHandle.currentState)
        {
            case State.Phasing:
                Debug.Log("State Changing Phase");
                Pause();
                StartCoroutine(ChangePhaseRoutine());
                break;
            case State.Intro:
                StartCoroutine(IntroductionRoutine());
                break;
            case State.Idle:
                m_animation.SetAnimation(0, m_info.idle, true);
                break;
            case State.Attacking:
                if (m_attackDecider.hasDecidedOnAttack == false)
                {
                    m_attackDecider.DecideOnAttack();
                }
                switch (m_attackDecider.chosenAttack.attack)
                {
                    case Attack.HomingAttack:
                        StartCoroutine(HomingMissileAdranAttack());
                        break;
                    case Attack.SlamAttack:
                        StartCoroutine(SlamRollAttack());
                        break;
                }
                break;
            case State.ReevaluateSituation:
                m_stateHandle.SetState(State.Attacking);
                break;
            case State.WaitBehaviourEnd:
                break;
        }
    }
    public override void ReturnToSpawnPoint()
    {

    }

    protected override void OnTargetDisappeared()
    {

    }
}
