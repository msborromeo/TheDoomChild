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
        private string m_slamRollInitial;
        public string slamRollInitial => m_slamRollInitial;
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
    [SerializeField, TabGroup("Small Adran")]
    private GameObject[] m_adranProjectiles;
    [SerializeField, TabGroup("Small Adran")]
    private Transform m_summonSpot;
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
    [SerializeField, TabGroup("Small Adran")]
    private float m_returnTimeOfAdran;
    [ShowInInspector, ReadOnly,TabGroup("Small Adran")]
    private float m_timer;
    private bool m_isReturning;
    protected override void Start()
    {

        m_phaseHandle = new PhaseHandle<Phase, PhaseInfo>();
        m_phaseHandle.Initialize(Phase.PhaseOne, m_info.phaseInfo, m_character, ChangeState, ApplyPhaseData);
        m_phaseHandle.ApplyChange();
        m_healthLevel = HealthLevel.LevelOne;
        enabled = true;
        base.Start();
    }
    protected override void Awake()
    {
        base.Awake();
        m_attackDecider = new RandomAttackDecider<Attack>();
        m_stateHandle = new StateHandle<State>(State.Idle, State.WaitBehaviourEnd);
        m_smallAdrans = new List<Projectile>();
        UpdateAttackDeciderList();

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
    private IEnumerator HomingMissileAdranAttack()
    {
        m_stateHandle.Wait(State.ReevaluateSituation);
        yield return HomingMissilleAnimation();
        yield return HomingMissileProjectile();
        yield return HomingMissileReturnAnimation();
        m_attackDecider.hasDecidedOnAttack = false;
        m_stateHandle.ApplyQueuedState();
    }
    private IEnumerator HomingMissileProjectile()
    {
        HealthTracker();
        if (m_healthLevel == HealthLevel.LevelTwo)
        {
            var random = UnityEngine.Random.Range(0, 2);
            if(random == 1)
            {
                var instance1 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[0], gameObject.scene);
                instance1.SpawnAt(new Vector2(m_summonSpot.position.x, m_summonSpot.position.y), Quaternion.identity);
                var instance2 = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[1], gameObject.scene);
                instance2.SpawnAt(new Vector2(m_summonSpot.position.x + 5f  , m_summonSpot.position.y + 5f), Quaternion.identity);
                StartCoroutine(SpawningOfHomingMissiles(instance1));
                yield return SpawningOfHomingMissiles(instance2);

            }
            else
            {
                var instance = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[0], gameObject.scene);
                instance.SpawnAt(new Vector2(m_summonSpot.position.x, m_summonSpot.position.y), Quaternion.identity);

                yield return SpawningOfHomingMissiles(instance);

            }
        }
        else
        {
            var instance = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_adranProjectiles[0], gameObject.scene);
            instance.SpawnAt(new Vector2(m_summonSpot.position.x, m_summonSpot.position.y), Quaternion.identity);

            yield return SpawningOfHomingMissiles(instance);
        }
        
    }

    private IEnumerator SpawningOfHomingMissiles(PoolableObject instance)
    {
        // m_isReturning = false;
        float timer = 0f;
        bool returning = false;

        while (true)
        {
            if (!returning)
            {
                Vector2 playerPos = GameplaySystem.playerManager.player.character.transform.position;
                instance.transform.position = Vector2.MoveTowards(instance.transform.position, playerPos, m_flightSpeed * Time.deltaTime);
                timer += Time.deltaTime;
                if (timer >= m_returnTimeOfAdran)
                {
                    returning = true;
                    timer = 0f;
                }
            }
            else
            {
                instance.transform.position = Vector2.MoveTowards(instance.transform.position, m_summonSpot.position, m_flightSpeedReturn * Time.deltaTime);
                if (Vector2.Distance(instance.transform.position, m_summonSpot.position) <= 1f)
                {
                    Destroy(instance.gameObject);
                    yield break;
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
        }
        else if (m_healthLevel == HealthLevel.LevelTwo)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeThreeTwo, m_info.idleTwo);
        }
        else if (m_healthLevel == HealthLevel.LevelThree)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeFourThree, m_info.idleThree);
        }
    }//end of HomingMissileReturnAnimation()
    private IEnumerator HomingMissilleAnimation()
    {
        HealthTracker();
        m_animation.SetAnimation(0, m_info.idle, true);
        if (m_healthLevel == HealthLevel.LevelOne)
        {
           yield return  AnimationSetterHomingMissile(m_info.TransitionSizeOneTwo, m_info.idleTwo);
        }
        else if(m_healthLevel == HealthLevel.LevelTwo)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeTwoThree, m_info.idleThree);
        }else if (m_healthLevel == HealthLevel.LevelThree)
        {
            yield return AnimationSetterHomingMissile(m_info.TransitionSizeThreeFour, m_info.idleFour);
        }
        //else if (m_healthLevel == HealthLevel.LevelFour)
        //{
        //    yield return AnimationSetterHomingMissile(m_info.TransitionSizeFourFive, m_info.idleFive);
        //}
    }//end of HomingMissilleAnimation()

    private IEnumerator AnimationSetterHomingMissile(string attackAnimation, string idleAnimation)
    {
        var sizeTransition = m_animation.SetAnimation(1, attackAnimation, false);
        m_animation.AddAnimation(1, idleAnimation, true, 0);
        yield return new WaitForSpineAnimationComplete(sizeTransition);
    }

    private IEnumerator SlamLeftOrRight() 
    {  
        yield return null; 
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

    
    private void Update()
    {
        
        m_phaseHandle.MonitorPhase();
        
        switch (m_stateHandle.currentState)
        {
            case State.Phasing:
                break;
            case State.Intro:
                break;
            case State.Idle:
                m_animation.SetAnimation(0, m_info.idle, true);
                break;
            case State.Attacking:
                StopAllCoroutines();
                if (m_attackDecider.hasDecidedOnAttack == false)
                {
                    m_attackDecider.DecideOnAttack();
                }
                switch (m_attackDecider.chosenAttack.attack)
                {
                    case Attack.HomingAttack:
                        break;
                    case Attack.SlamAttack:
                        break;
                }
                break;
            case State.ReevaluateSituation:
                break;
            case State.WaitBehaviourEnd:
                break;
        }
    }
    public override void ReturnToSpawnPoint()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnTargetDisappeared()
    {
        throw new System.NotImplementedException();
    }
}
