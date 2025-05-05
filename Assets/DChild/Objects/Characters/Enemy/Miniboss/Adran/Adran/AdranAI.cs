using AllIn1VfxToolkit;
using DChild;
using DChild.Gameplay;
using DChild.Gameplay.Characters;
using DChild.Gameplay.Characters.AI;
using DChild.Gameplay.Characters.Enemies;
using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Combat;
using DChild.Gameplay.Pooling;
using DChild.Gameplay.Projectiles;
using DG.Tweening;
using Holysoft.Event;
using Language.Lua;
using Pathfinding.Util;
using Sirenix.OdinInspector;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static AdranAI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

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
    private RaySensor m_groundSensor;
    [SerializeField, TabGroup("Reference")]
    private MovementHandle2D m_movement;
    [SerializeField, TabGroup("Reference")]
    private FlinchHandler m_flinchHandler;
    [SerializeField, TabGroup("Reference")]
    private Hitbox m_hitbox;
    [SerializeField, TabGroup("Reference")]
    private DeathHandle m_deathHandle;
    [SerializeField, TabGroup("Reference")]
    private CircleCollider2D m_hitboxCollider;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private float m_offSetAbovePlayer;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private float m_dropSpeed;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private float m_maxRollSpeed;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private float m_initialSpeed;
    [SerializeField, TabGroup("Slam Roll Attack")]
    private float m_totalRollCount;
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
    private Damage m_damageOnDeath;
    [SerializeField, TabGroup("Small Adran")]
    private float m_returnTimeOfAdran;
    [ShowInInspector, ReadOnly, TabGroup("Small Adran")]
    private float m_timer;
    private bool m_isReturning;
    protected override void Start()
    {

        m_phaseHandle = new PhaseHandle<Phase, PhaseInfo>();
        m_phaseHandle.Initialize(Phase.PhaseOne, m_info.phaseInfo, m_character, ChangeState, ApplyPhaseData);
        m_phaseHandle.ApplyChange();
        m_healthLevel = HealthLevel.LevelOne; ;
        base.Start();
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
    protected override void OnDestroyed(object sender, EventActionArgs eventArgs)
    {
        base.OnDestroyed(sender, eventArgs);
        Debug.Log("Death?");
        StopAllCoroutines();
        m_movement.Stop();
        m_animation.DisableRootMotion();

    }
    private void Health_Death(object sender, Holysoft.Event.EventActionArgs eventArgs)
    {
        if (m_phaseHandle.currentPhase == Phase.PhaseFour)
        {
            m_phaseHandle.MonitorPhase();
            Debug.Log("health 0");
            m_damageable.health.SetHealthPercentage(0.03f);
            m_damageable.health.Death -= Health_Death;
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
    private IEnumerator RollAttack()
    {
        int rollCount = 0;
        bool movingRight = m_startAtPointA;
        float rollSpeed = m_initialSpeed;
        var rollInitializeAnim = m_startAtPointA ? m_info.slamRollLeft : m_info.slamRollRight;
        var rollLoopAnim = m_startAtPointA ? m_info.slamRollLeftLoop : m_info.slamRollRightLoop;
        var rollIntitialSpineAnim = m_animation.SetAnimation(1, rollInitializeAnim, false);
        yield return new WaitForSpineAnimationComplete(rollIntitialSpineAnim);
        m_animation.SetAnimation(1, rollLoopAnim, true);
        var vfxRoll_1 = movingRight ? m_rollVFXRight : m_rollVFXLeft;
        vfxRoll_1.Play();
        Vector2 targetStartPos = m_startAtPointA ? m_limitPoints[0].position : m_limitPoints[1].position;
        float currentY = transform.position.y;

        while (Mathf.Abs(transform.position.x - targetStartPos.x) > 0.1f)
        {
            float newX = Mathf.MoveTowards(transform.position.x, targetStartPos.x, m_initialSpeed * Time.deltaTime);
            transform.position = new Vector3(newX, currentY, transform.position.z);
            yield return null;       
        }
        vfxRoll_1.Stop();
        Debug.Log("Reached the starting point, beginning the rolling attack!");
     
        while (rollCount < m_totalRollCount)
        {
            
            var rollLoopAnim_2 = movingRight ? m_info.slamRollRightLoop : m_info.slamRollLeftLoop;
            m_animation.SetAnimation(1, rollLoopAnim_2, true);
            Vector2 targetPos = movingRight ? m_limitPoints[1].position : m_limitPoints[0].position;
            var vfxRoll = movingRight ? m_rollVFXLeft : m_rollVFXRight;
            if (!vfxRoll.isPlaying) 
            {
                vfxRoll.Play();
            }
            
            float newX = Mathf.MoveTowards(transform.position.x, targetPos.x, rollSpeed * Time.deltaTime);
            transform.position = new Vector3(newX, currentY, transform.position.z);  // Keep the Y position constant


            if (Mathf.Abs(transform.position.x - targetPos.x) <= 0.1f)
            { 
                movingRight = !movingRight;
                Debug.Log("Switching direction!");
                vfxRoll.Stop();
                rollCount++;
                rollSpeed = Mathf.Min(rollSpeed + m_incrementSpeed, m_maxRollSpeed);
                Debug.Log("Roll Count: " + rollCount + ", Roll Speed: " + rollSpeed);
            }
            yield return null;
        }
        m_animation.SetAnimation(0, m_info.idle, true);
        m_animation.AddAnimation(1, m_info.idleFive, true, 0);
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
        m_slamVFX.Play();
        var SlamAnimation = m_animation.SetAnimation(1, m_info.slamLand, false);
        m_animation.AddAnimation(1, m_info.idleFive, true, 0);
        yield return new WaitForSpineAnimationComplete(SlamAnimation);
        m_movement.Stop();
    }

    private IEnumerator SlamRollAttack()
    {
        m_stateHandle.Wait(State.ReevaluateSituation);
        yield return SlamRollLocatePlayer();
        yield return SlamAttack();
        yield return RollAttack();
        yield return LocateRandomWithinArea();
        m_attackDecider.hasDecidedOnAttack = false;
        m_stateHandle.ApplyQueuedState();
    }
    private IEnumerator HomingMissileAdranAttack()
    {
        m_stateHandle.Wait(State.ReevaluateSituation);
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
    private IEnumerator SlamRollLocatePlayer()
    {
        float heightOffset = m_offSetAbovePlayer;

        while (true)
        {
            Vector2 playerPos = GameplaySystem.playerManager.player.character.transform.position;
            Vector2 targetPos = playerPos + Vector2.up * heightOffset;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, m_flightSpeedSlamRoll * Time.deltaTime);

            // Check if the object is very close to the target position
            if (Vector2.Distance(transform.position, targetPos) <= 0.05f)
            {
                Debug.Log("Now floating above player");
                break;
            }

            yield return null;
        }

    }
    private IEnumerator HomingMissileProjectile()
    {
        HealthTracker();
        if (m_healthLevel == HealthLevel.LevelTwo)
        {
            var random = UnityEngine.Random.Range(0, 2);
            if (random == 1)
            {

                var randomInstance = UnityEngine.Random.Range(0, 4);

                // Generate a second index that's guaranteed to be different
                int randomInstance_2;
                do
                {
                    randomInstance_2 = UnityEngine.Random.Range(0, 4);
                } while (randomInstance_2 == randomInstance);
                var randomProjectiles = UnityEngine.Random.Range(0, 4);
                // Spawn first instance
                m_summonFX[randomInstance].Play();
                yield return new WaitForSeconds(1f);
                var instance1 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[randomProjectiles], gameObject.scene);

                instance1.GetComponent<SmallAdran>().GotDamagedByPlayer += AdranAI_GotDamagedByPlayer;
                instance1.GetComponent<SmallAdran>().SmallAdranGotDestroyed += SmallAdranGotDestroyed;
                instance1.SpawnAt(new Vector2(m_summonSpot[randomInstance].position.x, m_summonSpot[randomInstance].position.y), m_summonSpot[randomInstance].transform.rotation);
                yield return new WaitForSeconds(.3f);
                instance1.transform.rotation = Quaternion.identity;

                // Spawn second instance
                m_summonFX[randomInstance_2].Play();
                yield return new WaitForSeconds(1f);
                var instance2 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[randomProjectiles], gameObject.scene);
                
                instance2.GetComponent<SmallAdran>().SmallAdranGotDestroyed += SmallAdranGotDestroyed;
                instance2.GetComponent<SmallAdran>().GotDamagedByPlayer += AdranAI_GotDamagedByPlayer;
                instance2.SpawnAt(new Vector2(m_summonSpot[randomInstance_2].position.x, m_summonSpot[randomInstance_2].position.y), m_summonSpot[randomInstance].transform.rotation);
                yield return new WaitForSeconds(.3f);
                instance2.transform.rotation = Quaternion.identity;

                StartCoroutine(SpawningOfHomingMissiles(instance1));
                yield return SpawningOfHomingMissiles(instance2);

                if (instance1 != null)
                {
                    instance1.GetComponent<SmallAdran>().GotDamagedByPlayer -= AdranAI_GotDamagedByPlayer;
                    instance1.GetComponent<SmallAdran>().SmallAdranGotDestroyed -= AdranAI_GotDamagedByPlayer;
                    yield return HomingMissileReturnAnimation();
                }
                if (instance2 != null)
                {
                    instance2.GetComponent<SmallAdran>().GotDamagedByPlayer -= AdranAI_GotDamagedByPlayer;
                    instance2.GetComponent<SmallAdran>().SmallAdranGotDestroyed -= AdranAI_GotDamagedByPlayer;
                    yield return HomingMissileReturnAnimation();
                }



            }
            else
            {
                var randomProjectiles = UnityEngine.Random.Range(0, 4);
                var randomSummonSpot = UnityEngine.Random.Range(0, 4);
                m_summonFX[randomSummonSpot].Play();
                yield return new WaitForSeconds(1f);
                var instance = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[randomProjectiles], gameObject.scene);
                instance.SpawnAt(new Vector2(m_summonSpot[randomSummonSpot].position.x, m_summonSpot[randomSummonSpot].position.y), m_summonSpot[randomSummonSpot].transform.rotation);
                yield return new WaitForSeconds(.3f);
                instance.transform.rotation = Quaternion.identity;
                instance.GetComponent<SmallAdran>().SmallAdranGotDestroyed += SmallAdranGotDestroyed;
                instance.GetComponent<SmallAdran>().GotDamagedByPlayer += AdranAI_GotDamagedByPlayer;
                yield return SpawningOfHomingMissiles(instance);
                if (instance != null)
                {
                    instance.GetComponent<SmallAdran>().GotDamagedByPlayer -= AdranAI_GotDamagedByPlayer;
                    instance.GetComponent<SmallAdran>().SmallAdranGotDestroyed -= AdranAI_GotDamagedByPlayer;
                    yield return HomingMissileReturnAnimation();
                }

            }
        }
        else
        {
            var randomProjectiles = UnityEngine.Random.Range(0, 4);
            var randomSummonSpot = UnityEngine.Random.Range(0, 4);
            m_summonFX[randomSummonSpot].Play();
            yield return new WaitForSeconds(1);
            var instance = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[randomProjectiles], gameObject.scene);
            instance.GetComponent<SmallAdran>().GotDamagedByPlayer += AdranAI_GotDamagedByPlayer;
            instance.GetComponent<SmallAdran>().SmallAdranGotDestroyed += SmallAdranGotDestroyed;
            instance.SpawnAt(new Vector2(m_summonSpot[randomSummonSpot].position.x, m_summonSpot[randomSummonSpot].position.y), m_summonSpot[randomSummonSpot].transform.rotation);
            yield return new WaitForSeconds(.3f);
            instance.transform.rotation = Quaternion.identity;
            yield return SpawningOfHomingMissiles(instance);
            if (instance != null)
            {
                instance.GetComponent<SmallAdran>().GotDamagedByPlayer -= AdranAI_GotDamagedByPlayer;
                instance.GetComponent<SmallAdran>().SmallAdranGotDestroyed -= SmallAdranGotDestroyed;
                yield return HomingMissileReturnAnimation();
            }
        }

    }

    private void SmallAdranGotDestroyed(object sender, Holysoft.Event.EventActionArgs eventArgs)
    {
        Debug.Log("Damage adran");
        GameplaySystem.combatManager.Damage(m_damageable, m_damageOnDeath);
        StartCoroutine(FlinchStrongAnimationRoutine());
    }

    private IEnumerator FlinchStrongAnimationRoutine()
    {
        m_hitbox.Disable();
        m_flinchHandler.SetAnimation(m_info.flinch_2);
        m_flinchHandler.Flinch();
        yield return new WaitForSeconds(0.5f);
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
    private IEnumerator SpawningOfHomingMissiles(PoolableObject instance)
    {
        // m_isReturning = false;
        m_hitbox.Enable();
        float timer = 0f;
        bool returning = false;
        bool reIterate = true;
        while (true)
        {

            if (instance == null || instance.gameObject == null)
            {
                returning = true;
                yield break;
            }
            if (!returning)
            {
                instance.GetComponent<SmallAdran>().ColliderController(true);
                instance.GetComponent<SmallAdran>().isReturningToSummonSpot = false;
                Vector2 playerPos = GameplaySystem.playerManager.player.character.transform.position;
                Vector2 direction = playerPos - (Vector2)instance.transform.position;
                #region FlippingBurgers
                //   var toPlayerDotProduct = Vector2.Dot(Vector2.right, direction.normalized);
                //   var toPlayerDotSign = Mathf.Sign(toPlayerDotProduct);
                //// instance.GetComponent<SmallAdran>().TurnAnimationSetter();
                //   instance.transform.localScale = new Vector3(toPlayerDotSign, instance.transform.localScale.y, instance.transform.localScale.z); 
                #endregion

                instance.transform.position = Vector2.MoveTowards(instance.transform.position, m_targetInfo.position, m_flightSpeed * Time.deltaTime);
                var instancePlayerDistance = Vector2.Distance(instance.transform.position, m_targetInfo.position);

                timer += Time.deltaTime;
                if (timer >= m_returnTimeOfAdran || instancePlayerDistance <= 1f)
                {

                    returning = true;
                    timer = 0f;
                }
            }
            else
            {


                if (instance == null || instance.GetComponent<Damageable>().health.currentValue <= 0)
                {
                    Debug.Log("inside the instance == null || instance.GetComponent<Damageable>().health.currentValue <= 0 ");
                    returning = true;
                    m_movement.Stop();
                    yield break;
                }
                else
                {
                    var summonSpotSmallAdran = instance.GetComponent<SmallAdran>();
                    summonSpotSmallAdran.startingPosition = m_summonSpot[4].position;
                    Vector2 direction = m_summonSpot[4].position - instance.transform.position;
                    instance.GetComponent<SmallAdran>().isReturningToSummonSpot = true;
                    m_hitbox.Disable();
                    instance.GetComponent<SmallAdran>().ColliderController(false);
                    #region FlippinBurgers
                    //var toSpotDotProduct = Vector2.Dot(Vector2.right, direction.normalized);
                    //var toSpotDotSign = Mathf.Sign(toSpotDotProduct);
                    //instance.transform.localScale = new Vector3(toSpotDotSign, instance.transform.localScale.y, instance.transform.localScale.z); 
                    #endregion
                    Debug.Log("else");
                    instance.transform.position = Vector2.MoveTowards(
                    instance.transform.position,
                    m_summonSpot[4].position,
                    m_flightSpeedReturn * Time.deltaTime);
                }



                var randomShit = UnityEngine.Random.Range(0, 2);
                
                if (Vector2.Distance(instance.transform.position, m_summonSpot[4].position) <= 1f)
                {
                    if (randomShit == 1 && reIterate == true)
                    {
                        Debug.Log("Hello im ander the water");
                        returning = false;
                        reIterate = false;
                        instance.GetComponent<SmallAdran>().isReturningToSummonSpot = false;
                        instance.GetComponent<SmallAdran>().ColliderController(true);
                    }
                    else
                    {
                        instance.GetComponent<SmallAdran>().isReturningToSummonSpot = true;
                        instance.GetComponent<SmallAdran>().ColliderController(false);
                        Destroy(instance.gameObject);
                        yield break; // Stop and let next routine start
                    }

                }
            }
            yield return null;
        }
    }

    private IEnumerator HomingMissileReturnAnimation()
    {
        HealthTracker();
        if (m_healthLevel == HealthLevel.LevelOne)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeTwoOne, m_info.idle);
            m_colliderSizeAdjustment.radius = 16f;
            m_hitboxCollider.radius = 15f;
        }
        else if (m_healthLevel == HealthLevel.LevelTwo)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeThreeTwo, m_info.idleTwo);

            m_colliderSizeAdjustment.radius = 13f;
            m_hitboxCollider.radius = 12f;
        }
        else if (m_healthLevel == HealthLevel.LevelThree)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeFourThree, m_info.idleThree);
            m_colliderSizeAdjustment.radius = 9.5f;
            m_hitboxCollider.radius = 8.5f;
        }
        else if (m_healthLevel == HealthLevel.LevelFour)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeFiveFour, m_info.idleFour);
            m_colliderSizeAdjustment.radius = 7.85f;
            m_hitboxCollider.radius = 6.85f;
        }
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
        }
        else if (m_healthLevel == HealthLevel.LevelTwo)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeTwoThree, m_info.idleThree);
            m_colliderSizeAdjustment.radius = 9.5f;
            m_hitboxCollider.radius = 8.5f;
        }
        else if (m_healthLevel == HealthLevel.LevelThree)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeThreeFour, m_info.idleFour);
            m_colliderSizeAdjustment.radius = 7.85f;
            m_hitboxCollider.radius = 6.85f;
        }
        else if (m_healthLevel == HealthLevel.LevelFour)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeFourFive, m_info.idleFive);
            m_colliderSizeAdjustment.radius = 6.5f;
            m_hitboxCollider.radius = 6f;
        }
    }//end of HomingMissilleAnimation()

    private IEnumerator AnimationSetterHomingMissile(string transitionIdleAnimation, string idleAnimation)
    {
        var sizeTransition = m_animation.SetAnimation(1, transitionIdleAnimation, false);
        m_animation.AddAnimation(1, idleAnimation, true, 0);
        yield return new WaitForSpineAnimationComplete(sizeTransition);
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

    private IEnumerator ChangePhaseRoutine()
    {
        m_stateHandle.Wait(State.Attacking);
        m_hitbox.Disable();
        switch (m_phaseHandle.currentPhase)
        {
            case Phase.PhaseTwo:
                m_colliderSizeAdjustment.radius = 13f;
                yield return AnimationSetterForIdle(m_info.TransitionSizeOneTwo, m_info.idleTwo);
                break;
            case Phase.PhaseThree:
                m_colliderSizeAdjustment.radius = 9.5f;
                yield return AnimationSetterForIdle(m_info.TransitionSizeTwoThree, m_info.idleThree);
                break;
            case Phase.PhaseFour:
                m_colliderSizeAdjustment.radius = 7.85f;
                yield return AnimationSetterForIdle(m_info.TransitionSizeThreeFour, m_info.idleFour);
                break;
            case Phase.PhaseFive:
                m_colliderSizeAdjustment.radius = 6.5f;
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
        m_animation.SetAnimation(0, m_info.idle, true);
        switch (m_stateHandle.currentState)
        {
            case State.Phasing:
                Debug.Log("State Changing Phase");
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
