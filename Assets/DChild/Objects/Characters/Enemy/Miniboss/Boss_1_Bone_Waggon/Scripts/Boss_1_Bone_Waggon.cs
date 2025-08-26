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
using Spine.Unity.Modules;
using Spine.Unity.Examples;
using DChild.Gameplay.Pooling;
using DChild.Gameplay.Projectiles;
using DChild.Temp;
using System.Linq;
using DChild.Gameplay;
using DChild.Gameplay.Characters;

public class Boss_1_Bone_Waggon : CombatAIBrain<Boss_1_Bone_Waggon.Info>
{
    public override void ReturnToSpawnPoint()
    {
    
    }

    protected override void OnTargetDisappeared()
    {

    }

    [System.Serializable]
    public class Info : BaseInfo
    {
        [SerializeField]
        private PhaseInfo<Phase> m_phaseInfo;
        public PhaseInfo<Phase> phaseInfo => m_phaseInfo;
        [SerializeField]
        private MovementInfo m_run = new MovementInfo();
        public MovementInfo run => m_run;

        [SerializeField]
        private GameObject m_bungo;
        public GameObject bungo => m_bungo;
        [SerializeField]
        private MovementInfo m_move = new MovementInfo();
        public MovementInfo move => m_move;

        [Title("Attack Behaviours")]
        [SerializeField, TabGroup("Slam Waggon")]
        private BasicAnimationInfo m_slamWaggonAttack = new BasicAnimationInfo();
        public BasicAnimationInfo slamWaggonAttack => m_slamWaggonAttack;

        [Title("Animations")]
        [SerializeField]
        private BasicAnimationInfo m_deathAnimation;
        public BasicAnimationInfo deathAnimation => m_deathAnimation;
        [SerializeField]
        private BasicAnimationInfo m_flinchAnimation;
        public BasicAnimationInfo flinchAnimation => m_flinchAnimation;
        [SerializeField]
        private BasicAnimationInfo m_exhaustedAnimation;
        public BasicAnimationInfo exhaustedAnimation => m_exhaustedAnimation;


        [TitleGroup("Pattern Ranges")]
        [SerializeField, BoxGroup("Phase 1")]
        private float m_phaseOneAttackRange;
        public float phaseOneAttackRange => m_phaseOneAttackRange;
        [SerializeField, BoxGroup("Phase 2")]
        private float m_phaseTwoAttackRange;
        public float phaseTwoAttackRange => m_phaseTwoAttackRange;

        [Title("Events")]
        [SerializeField, ValueDropdown("GetEvents")]
        private string m_realeaseBungoEvent;
        public string realeaseBungoEvent => m_realeaseBungoEvent;
        public override void Initialize()
        {
#if UNITY_EDITOR

            run.SetData(m_skeletonDataAsset);
            m_slamWaggonAttack.SetData(m_skeletonDataAsset);
            m_deathAnimation.SetData(m_skeletonDataAsset);
            m_flinchAnimation.SetData(m_skeletonDataAsset);
            m_exhaustedAnimation.SetData(m_skeletonDataAsset);

#endif

        }
    }


    public class PhaseInfo : IPhaseInfo
    {
        [SerializeField]
        private int m_phaseIndex;
        public int phaseIndex => m_phaseIndex;
        [SerializeField]
        private List<float> m_fullCooldown;
        public List<float> fullCooldown => m_fullCooldown;
        //[SerializeField]
        //private int m_hitCount;
        //public int hitCount => m_hitCount;
    }
    public enum Phase
    {
        PhaseOne,
        PhaseTwo,
        Wait,
    }
    
    private enum State
    {
        Idle,
        Phasing,
        Intro,
        Turning,
        Attacking,
        Cooldown,
        ReevaluateSituation,
        WaitBehaviourEnd,
    }

    private enum Attack
    {
        AttackForPhaseOneRun,
        AttackForPhaseOneSlam,
        AttackForPhaseTwoRun,
        AttackForPhaseTwoSlam,
    }
    [ShowInInspector]
    private StateHandle<State> m_stateHandle;
    State m_turnState;
    [ShowInInspector]
    private PhaseHandle<Phase, PhaseInfo> m_phaseHandle;

    [ShowInInspector]
    private RandomAttackDecider<Attack> m_attackDecider;
    private int m_currentPhaseIndex;
    [SerializeField, TabGroup("Move Points")]
    private List<Transform> m_movePoints;
    [SerializeField, TabGroup("Spawn Points")]
    private List<Transform> m_spawnPointBungo;
    [SerializeField, TabGroup("Modules")]
    private MovementHandle2D m_movement;
    [SerializeField, TabGroup("Modules")]
    private AnimatedTurnHandle m_turnHandle;
    [SerializeField, TabGroup("Reference")]
    private Boss m_boss;

    [SerializeField]
    private SpineEventListener m_spineListener;

    private bool[] m_attackUsed;
    private List<Attack> m_attackCache;
    private Attack m_currentAttack;
    private float m_currentCooldown;
    private Coroutine m_currentAttackCoroutine;
    private float m_currentAttackRange;
    private List<float> m_attackRangeCache;
    private bool moveToLeft = true;
    [SerializeField]
    private int runCounter = 0;
    [SerializeField]
    private float m_increaseSpeedPhase1;
    [SerializeField]
    private GameObject m_collider;

    private void ApplyPhaseData(PhaseInfo obj)
    {
        m_attackCache.Clear();
        switch (m_phaseHandle.currentPhase)
        {
            case Phase.PhaseOne:
                AddToAttackCache(Attack.AttackForPhaseOneRun, Attack.AttackForPhaseOneSlam);
                break;
            case Phase.PhaseTwo:
                AddToAttackCache(Attack.AttackForPhaseTwoRun,Attack.AttackForPhaseTwoSlam);
                break;
                
        }
        m_attackUsed = new bool[m_attackCache.Count];

        m_currentPhaseIndex = obj.phaseIndex;

    }
 
    private void ChangeState()
    {
        if (m_currentAttackCoroutine != null)
        {
            StopCoroutine(m_currentAttackCoroutine);
            m_currentAttackCoroutine = null;
            m_attackDecider.hasDecidedOnAttack = false;
        }
        m_phaseHandle.ApplyChange();
        StopAllCoroutines();
        m_animation.SetEmptyAnimation(0, 0);
        m_stateHandle.OverrideState(State.Phasing);
        switch (m_phaseHandle.currentPhase)
        {
            case Phase.PhaseOne:
                AddToAttackCache(Attack.AttackForPhaseOneRun, Attack.AttackForPhaseOneSlam);
                break;
            case Phase.PhaseTwo:
                AddToAttackCache(Attack.AttackForPhaseTwoRun, Attack.AttackForPhaseTwoSlam);
                break;
        }

    }
    private IEnumerator IntroRoutine()
    {
        Debug.Log("INTRO!");
        yield return new WaitForSeconds(1f);
        m_stateHandle.SetState(State.Attacking);
        yield return null;

    }

    private IEnumerator ChangePhaseRoutine()
    {
        m_movement.Stop();
        m_animation.SetAnimation(0, m_info.slamWaggonAttack, false);
        yield return new WaitForAnimationComplete(m_animation.animationState, m_info.slamWaggonAttack);
        m_stateHandle.ApplyQueuedState();
        yield return null;
    }

    private IEnumerator RunRoutine()
    {
        var runToLeft = m_movePoints[1];
        var runToRight = m_movePoints[0];
        m_animation.SetAnimation(0, m_info.run.animation, true);
        while (runCounter < 4)
        {
            if (moveToLeft == true)
            {
                yield return new WaitForSeconds(0.5f);
                while (Vector3.Distance(transform.position, runToLeft.position) > 20f)
                {
                    //var CalculatedDistanceOfPositions = (runToLeft.position - transform.position).normalized;
                    //transform.position += m_info.run.speed * Time.deltaTime * CalculatedDistanceOfPositions;

                    m_movement.MoveTowards(Vector2.one * transform.localScale.x, m_increaseSpeedPhase1);

                    Debug.Log("Moving to left ");
                    yield return null;
                }
                m_movement.Stop();
                CustomTurn();
                moveToLeft = false;
            }
            else
            {

                yield return new WaitForSeconds(0.5f);
                while (Vector3.Distance(transform.position, runToRight.position) > 20f)
                {
                    
                    //var CalculatedDistanceOfPositions = (runToLeft.position - transform.position).normalized;
                    //transform.position += m_info.run.speed * Time.deltaTime * CalculatedDistanceOfPositions;

                    m_movement.MoveTowards(Vector2.one * transform.localScale.x, m_increaseSpeedPhase1);

                    Debug.Log("Moving to Right ");
                    yield return null;
                }
                m_movement.Stop();
                CustomTurn();
                moveToLeft = true;
            }
            m_increaseSpeedPhase1 += 10f;
            runCounter++;
            yield return null;  
        }
        yield return new WaitForAnimationComplete(m_animation.animationState, m_info.run.animation);
        m_attackDecider.hasDecidedOnAttack = false;
        m_currentAttackCoroutine = null;
        m_stateHandle.ApplyQueuedState();
        Debug.Log("bunggo na");
        yield return null;

    }

    private IEnumerator SmashRoutine()
    {
        var runToMiddle = m_movePoints[2];
        yield return new WaitForSeconds(0.5f);
        m_animation.SetAnimation(0, m_info.run.animation, true);
        while (Vector3.Distance(transform.position, runToMiddle.position) > 20f)
        {

            //var CalculatedDistanceOfPositions = (runToLeft.position - transform.position).normalized;
            //transform.position += m_info.run.speed * Time.deltaTime * CalculatedDistanceOfPositions;

            m_movement.MoveTowards(Vector2.one * transform.localScale.x, m_increaseSpeedPhase1);

            Debug.Log("Moving to Right ");
            yield return null;
        }
        m_movement.Stop();
        m_collider.SetActive(true);
        m_animation.SetAnimation(0, m_info.exhaustedAnimation, false);
        yield return new WaitForAnimationComplete(m_animation.animationState, m_info.exhaustedAnimation.animation);
        m_animation.SetAnimation(0, m_info.slamWaggonAttack, false);
        yield return new WaitForAnimationComplete(m_animation.animationState, m_info.slamWaggonAttack.animation);
        m_collider.SetActive(false);
        m_increaseSpeedPhase1 = m_info.run.speed;
        m_attackDecider.hasDecidedOnAttack = false;
        m_currentAttackCoroutine = null;
        m_stateHandle.ApplyQueuedState();
        yield return null;
    }
    public float gapDistance =10f;
    public float spawnRadius = 2f;
    private void ReleaseBungo()
    {
        var spawnPoint = m_spawnPointBungo[0];
        spawnBungo(spawnPoint, m_info.bungo);

    }
    protected override void OnDestroyed(object sender, EventActionArgs eventArgs)
    {
        base.OnDestroyed(sender, eventArgs);
        StopAllCoroutines();
        m_movement.Stop();
        m_animation.DisableRootMotion();
    }
    public void spawnBungo(Transform point, GameObject objecToSpawn)
    {
        int randomNumber = Random.Range(1, 5);
        for (int i = 0; i < randomNumber; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector2 spawnPosition = point.position + (point.right * i * gapDistance);
            var spawnPoint = spawnPosition + randomOffset;
            var instance = GameSystem.poolManager.GetPool<ProjectilePool>().GetOrCreateItem(objecToSpawn);
            instance.transform.position = spawnPoint;
            var component = instance.GetComponent<Projectile>();
            //instance.GetComponent<MotherMantisSeed>().OnStalagmiteSummoned += OnStalagmiteInstantiate;
            component.ResetState();
            //Instantiate(instance, spawnPosition + randomOffset, Quaternion.identity);
        }
    }
    void AddToAttackCache(params Attack[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            m_attackCache.Add(list[i]);
        }
    }
    private void UpdateAttackDeciderList()
    {
        m_attackDecider.SetList(new AttackInfo<Attack>(Attack.AttackForPhaseOneRun, m_info.phaseOneAttackRange), 
            new AttackInfo<Attack>(Attack.AttackForPhaseOneSlam, m_info.phaseOneAttackRange),
            new AttackInfo<Attack>(Attack.AttackForPhaseTwoRun, m_info.phaseTwoAttackRange),
            new AttackInfo<Attack>(Attack.AttackForPhaseTwoSlam, m_info.phaseTwoAttackRange));
        m_attackDecider.hasDecidedOnAttack = false;
    }
    public override void ApplyData()
    {
        if (m_attackDecider != null)
        {
            UpdateAttackDeciderList();
        }
        base.ApplyData();
    }
    private void ChooseAttack()
    {
        if (!m_attackDecider.hasDecidedOnAttack)
        {
            IsAllAttackComplete();
            for (int i = 0; i < m_attackCache.Count; i++)
            {
                m_attackDecider.DecideOnAttack();
                if (m_attackCache[i] != m_currentAttack && !m_attackUsed[i])
                {
                    m_attackUsed[i] = true;
                    m_currentAttack = m_attackCache[i];
                  //  m_currentAttackRange = m_attackRangeCache[i];
                    return;
                }
            }
        }
    }
    private void IsAllAttackComplete()
    {
        for (int i = 0; i < m_attackUsed.Length; ++i)
        {
            if (!m_attackUsed[i])
            {
                return;
            }
        }
        for (int i = 0; i < m_attackUsed.Length; ++i)
        {
            m_attackUsed[i] = false;
        }
    }


    protected override void Start()
    {
        base.Start();
        m_spineListener.Subscribe(m_info.realeaseBungoEvent, ReleaseBungo);
        m_phaseHandle = new PhaseHandle<Phase, PhaseInfo>();
        m_phaseHandle.Initialize(Phase.PhaseOne, m_info.phaseInfo, m_character, ChangeState, ApplyPhaseData);
        m_phaseHandle.ApplyChange();

    }
    protected override void Awake()
    {
        base.Awake();
        m_increaseSpeedPhase1 = m_info.run.speed;
        m_attackDecider = new RandomAttackDecider<Attack>();
        m_stateHandle = new StateHandle<State>(State.Intro, State.WaitBehaviourEnd);
        UpdateAttackDeciderList();
        m_attackCache = new List<Attack>();
        AddToAttackCache(Attack.AttackForPhaseOneRun,Attack.AttackForPhaseOneSlam, Attack.AttackForPhaseTwoRun,Attack.AttackForPhaseTwoSlam);
      
    }

    private void Update()
    {
        
        m_phaseHandle.MonitorPhase();
        switch (m_stateHandle.currentState)
        {
            case State.Attacking:
               m_stateHandle.Wait(State.Cooldown);
                switch (m_currentAttack)
                {
                    case Attack.AttackForPhaseOneRun:
                   
                            m_currentAttackCoroutine = StartCoroutine(RunRoutine()); 
                        break;
                    case Attack.AttackForPhaseOneSlam:
          
                            m_currentAttackCoroutine = StartCoroutine(SmashRoutine());
                        break;
                    case Attack.AttackForPhaseTwoRun:
                        Debug.Log("phase 2");
             
                         m_currentAttackCoroutine = StartCoroutine(RunRoutine());
                        break;
                    case Attack.AttackForPhaseTwoSlam:
  
                        m_currentAttackCoroutine = StartCoroutine(SmashRoutine());
                     
                        break;
                }
                break;
            case State.Intro:
                m_currentAttackCoroutine = StartCoroutine(IntroRoutine());
                break;
            case State.Turning:

                break;
            case State.Cooldown:
                m_stateHandle.OverrideState(State.ReevaluateSituation);
                break;
            case State.ReevaluateSituation:
                runCounter = 0;
                
                ChooseAttack();
                    m_stateHandle.SetState(State.Attacking);

                break;
            case State.WaitBehaviourEnd:
                break;
            case State.Phasing:
                StartCoroutine(ChangePhaseRoutine());
                break;
            case State.Idle:
                break;
        }
    }
}

