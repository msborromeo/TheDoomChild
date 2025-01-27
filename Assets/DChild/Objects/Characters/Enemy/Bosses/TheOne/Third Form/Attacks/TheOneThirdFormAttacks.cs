using DChild.Gameplay.Characters.Enemies;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using DChild.Gameplay.Characters.AI;
using UnityEngine;
using Holysoft.Event;
using System;

public class TheOneThirdFormAttacks : MonoBehaviour
{
    [SerializeField, BoxGroup("The One Third Form Attacks")]
    private TentacleGroundStabAttack m_tentacleGroundStabAttack;
    [SerializeField, BoxGroup("The One Third Form Attacks")]
    private TentacleCeilingAttack m_tentacleCeilingAttack;
    [SerializeField, BoxGroup("The One Third Form Attacks")]
    private MovingTentacleGroundAttack m_movingTentacleGroundAttack;
    [SerializeField, BoxGroup("The One Third Form Attacks")]
    private ChasingGroundTentacleAttack m_chasingGroundTentacleAttack;
    [SerializeField, BoxGroup("The One Third Form Attacks")]
    private MouthBlastIIAttack m_mouthBlastIIAttack;
    [SerializeField, BoxGroup("The One Third Form Attacks")]
    private SlidingStoneWallAttack m_slidingWallAttack;
    [SerializeField, BoxGroup("The One Third Form Attacks")]
    private MonolithSlamAttack m_monolithSlamAttack;
    [SerializeField, BoxGroup("The One Third Form Attacks")]
    private TentacleBlastAttack m_tentacleBlastAttack;
    [SerializeField, BoxGroup("The One Third Form Attacks")]
    private TentacleGrabAttack m_tentacleGrabScriptedAttack;
    [SerializeField, BoxGroup("The One Third Form Attacks")]
    private BubbleImprisonmentAttack m_bubbleImprisonmentAttack;
    [SerializeField, BoxGroup("The One Third Form Attacks")]
    private MouthBlastIAttack m_mouthBlastOneAttack;
    public MouthBlastIAttack mouthBlastOneAttack => m_mouthBlastOneAttack;

    private TheOneThirdFormAI m_targetInfo;

    public event EventAction<EventActionArgs> AttackDone;
    //{
    //    add
    //    {
    //        m_tentacleGroundStabAttack.AttackDone += value;






    //    }

    //    remove
    //    {
    //        m_tentacleGroundStabAttack.AttackDone -= value;
    //    }
    //}

    private void Awake()
    {
        AttackDone += OnAttackDone;
        m_tentacleGrabScriptedAttack.AttackDone += OnGrabAttackDone;
        m_tentacleGroundStabAttack.AttackDone += OnGroundStabDone;
        m_mouthBlastIIAttack.AttackDone += OnMouthBlastWallDone;
        m_movingTentacleGroundAttack.AttackDone += OnMovingTentacleGroundDone;
        m_monolithSlamAttack.AttackDone += OnMonolithSlamDone;
        m_chasingGroundTentacleAttack.AttackDone += OnChasingGroundTentacleDone;
        m_slidingWallAttack.AttackDone += OnSlidingWallDone;
        m_tentacleBlastAttack.AttackDone += OnTentacleBlastDone;
        m_tentacleCeilingAttack.AttackDone += OnTentacleCeilingDone;

        m_bubbleImprisonmentAttack.AttackDone += OnBubbleImprisonmentDone;
        m_mouthBlastOneAttack.AttackDone += OnMouthBlastOneDone;
    }

    private void OnAttackDone(object sender, EventActionArgs eventArgs)
    {
        Debug.Log("Attack Done from Third Form Attacks");
    }

    private void OnMouthBlastOneDone(object sender, EventActionArgs eventArgs)
    {
        Debug.Log("Mouth Blast One Done");
        AttackDone?.Invoke(this, new EventActionArgs());
    }

    private void OnBubbleImprisonmentDone(object sender, EventActionArgs eventArgs)
    {
        Debug.Log("Bubble Imprisonment Done");
        AttackDone?.Invoke(this, new EventActionArgs());
    }

    private void OnGrabAttackDone(object sender, EventActionArgs eventArgs)
    {
        Debug.Log("Grab Attack Done from Third Form Attacks");
        AttackDone?.Invoke(this, new EventActionArgs());
    }

    private void OnTentacleCeilingDone(object sender, EventActionArgs eventArgs)
    {
        Debug.Log("Tentacle Ceiling Done");
        AttackDone?.Invoke(this, new EventActionArgs());
    }

    private void OnTentacleBlastDone(object sender, EventActionArgs eventArgs)
    {
        Debug.Log("Tentacle Blast Done");
        AttackDone?.Invoke(this, new EventActionArgs());
    }

    private void OnSlidingWallDone(object sender, EventActionArgs eventArgs)
    {
        Debug.Log("Sliding Wall Done");
        AttackDone?.Invoke(this, new EventActionArgs());
    }

    private void OnChasingGroundTentacleDone(object sender, EventActionArgs eventArgs)
    {
        Debug.Log("Chasing Ground Tentacle Done");
        AttackDone?.Invoke(this, new EventActionArgs());
    }

    private void OnMonolithSlamDone(object sender, EventActionArgs eventArgs)
    {
        Debug.Log("Monolith Slam Done");
        AttackDone?.Invoke(this, new EventActionArgs());
    }

    private void OnMovingTentacleGroundDone(object sender, EventActionArgs eventArgs)
    {
        Debug.Log("Moving Tentacle Ground Done");
        AttackDone?.Invoke(this, new EventActionArgs());
    }

    private void OnMouthBlastWallDone(object sender, EventActionArgs eventArgs)
    {
        Debug.Log("Mouth Blast Wall Done");
        AttackDone?.Invoke(this, new EventActionArgs());
    }

    private void OnGroundStabDone(object sender, EventActionArgs eventArgs)
    {
        Debug.Log("Ground Stab Done");
        AttackDone?.Invoke(this, new EventActionArgs());
    }

    //Attacks
    public IEnumerator TentacleCeilingAttack()
    {
        yield return m_tentacleCeilingAttack.ExecuteAttack();
       // StartCoroutine(m_tentacleCeilingAttack.ExecuteAttack());
        yield return null;
    }

    public IEnumerator TentacleGrab()
    {
        yield return m_tentacleGrabScriptedAttack.ExecuteAttack();
        yield return null;
    }

    public IEnumerator MouthBlastWall()
    {
        yield return m_mouthBlastIIAttack.ExecuteAttack();
        yield return null;
    }

    public IEnumerator TentacleGroundStab(AITargetInfo Target)
    {
        yield return (m_tentacleGroundStabAttack.ExecuteAttack(Target));
        while(m_tentacleGroundStabAttack.m_attackDone == false)
        {
            yield return null;
        }
    }
    public IEnumerator TentacleGroundStabTwo()
    {
        yield return (m_tentacleGroundStabAttack.ExecuteAttackGroundStabTwo());
    }

    public IEnumerator MovingTentacleGround()
    {
        StartCoroutine(m_movingTentacleGroundAttack.ExecuteAttack());
        yield return null;
    }

    public IEnumerator ChasingGroundTentacle()
    {
        yield return m_chasingGroundTentacleAttack.ExecuteAttack();
    }

    public IEnumerator MonolithSlam()
    {
        yield return m_monolithSlamAttack.PhaseOneMonolithSlam();
        
    }
    public IEnumerator RemovalMonolithSlamPhaseOne()
    {
        for (int i = 0; i < m_monolithSlamAttack.m_monolithsSpawned.Count; i++)
        {
            m_monolithSlamAttack.m_monolithsSpawned[i].GetComponent<MonolithSlam>().SpawnShatterFX();
            m_monolithSlamAttack.m_monolithsSpawned[i].GetComponent<MonolithSlam>().OffImpactCollider();
        }
        for (int i = 0; i < m_monolithSlamAttack.m_PatternOneTentacleSpawn.Count; i++)
        {
            if (m_monolithSlamAttack.m_PatternOneTentacleSpawn[i].activeInHierarchy)
            {
                m_monolithSlamAttack.m_PatternOneTentacleSpawn[i].SetActive(false);
            }
            
        }
      
        m_monolithSlamAttack.m_monolithsSpawned.Clear();
        m_monolithSlamAttack.monolithsToDestroy.Clear();
        m_monolithSlamAttack.monolithsToActuallyKeep.Clear();

        
        yield return null;
    }

    public IEnumerator TentacleBlastOne(AITargetInfo Target)
    {
       yield return m_tentacleBlastAttack.ExecuteAttack(Target);
      
    }

    public IEnumerator TentacleBlastTwo()
    {
        yield return m_tentacleBlastAttack.ExecuteAttack();
        yield return null;
    }

    public IEnumerator SlidingStoneWallAttack(AITargetInfo Target)
    {
        yield return m_slidingWallAttack.ExecuteAttack(Target);
        yield return null;
    }

    public IEnumerator BubbleImprisonment(AITargetInfo Target)
    {
        yield return m_bubbleImprisonmentAttack.ExecuteAttack(Target);
        yield return null;
    }
}
