using DChild;
using DChild.Gameplay;
using DChild.Gameplay.Characters;
using DChild.Gameplay.Characters.AI;
using DChild.Gameplay.Characters.Enemies;
using DChild.Gameplay.Combat;
using DChild.Gameplay.Pooling;
using Holysoft.Event;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Random = System.Random;

public class SmallAdran : MonoBehaviour
{
    [SerializeField]
    private AnimatedTurnHandle m_animatedTurnHandle;
    [SerializeField]
    private Damageable m_Damageable;
    [SerializeField]
    private BasicHealth m_basicHealth;
    [SerializeField]
    private SpineRootAnimation m_spine;
    [SerializeField]
    private Character m_character;
    [SerializeField]
    private Collider2D[] m_collider;
    [SerializeField]
    private Collider2D m_AttackCollider;
    [SerializeField]
    private GameObject m_deathVfx;
    public Vector2 startingPosition;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_turnAnimation;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_idle;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_goingDownAnim;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_attackAnimation;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_deathAnimation;
    [SerializeField]
    private SpineEventListener m_spineListener;
    [SerializeField, TabGroup("GetEvents"),SpineEvent(dataField = "m_skeletonAnimation")]
    private string m_deathFX;
    [SerializeField, TabGroup("GetEvents"), SpineEvent(dataField = "m_skeletonAnimation")]
    private string m_onCollider;
    [SerializeField, TabGroup("GetEvents"), SpineEvent(dataField = "m_skeletonAnimation")]
    private string m_offCollider;
    private bool m_startFaceDetection = false;
    public event EventAction<EventActionArgs> GotDamagedByPlayer;
    public event EventAction<EventActionArgs> SmallAdranGotDestroyed;
    public event EventAction<EventActionArgs> SmallAdranReachedZone;
    public bool isReturningToSummonSpot;
    public bool m_stopHomingMissile;
    public bool m_reachedAreaToActivate;
    public bool isDestroyed;
    public bool returnedToSpot;
    private void Start()
    {
        enabled = true;
        returnedToSpot = false;
        isDestroyed = false;
        m_reachedAreaToActivate = false;
        m_stopHomingMissile = false;
        m_spineListener.Subscribe(m_deathFX, DeathVFX);
        m_spineListener.Subscribe(m_onCollider, OnAttackCollider);
        m_spineListener.Subscribe(m_offCollider, OffAttackCollider);
        m_Damageable.DamageTaken += Damageable_DamageTaken;
        m_Damageable.Destroyed += ObjectOnDestroyed;
      //  m_deathVfx.SetActive(false);
    }
    //private void OnDisable()
    //{
    //    GotDamagedByPlayer = null;
    //    SmallAdranGotDestroyed = null;
    //    SmallAdranReachedZone = null;
    //}

    //private void OnEnable()
    //{
    //    Vector2 playerPos = GameplaySystem.playerManager.player.character.transform.position;
    //    if (isReturningToSummonSpot == false)
    //    {
    //        FacingPlayerUpdate(playerPos);
    //    }
    //    else
    //    {
    //        FacingStartingPointUpdate(startingPosition);
    //    }
    //}
    private void OnAttackCollider()
    {
        m_AttackCollider.enabled = true;
    }
    private void OffAttackCollider()
    {
        m_AttackCollider.enabled = false;
    }
    public void DeathVFX()
    {
        var instance1 = GameSystem.poolManager.GetPool<PoolableObjectPool>().
            GetOrCreateItem(m_deathVfx, gameObject.scene);
        instance1.SpawnAt(new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Destroy(gameObject);
    }
    
    public void InitializeField(SpineRootAnimation spineRoot)
    {
        m_spine = spineRoot;
    }
    private void ObjectOnDestroyed(object sender, EventActionArgs eventArgs)
    {
        Debug.Log("Adran is Destroyed");
        StopAllCoroutines();
        m_stopHomingMissile = true;
        isDestroyed = true;
        m_startFaceDetection = false;
        ColliderController(false);  
        
        SmallAdranGotDestroyed?.Invoke(this, EventActionArgs.Empty);
        enabled = false;

    }
    private IEnumerator DeathRoutine()
    {
        m_spine.SetAnimation(0, m_deathAnimation, false); 
        yield return new WaitForAnimationComplete(m_spine.animationState, m_deathAnimation);
       // yield return new WaitForSeconds(0.3f);
       // Destroy(this.gameObject);
    }
    private void Damageable_DamageTaken(object sender, Damageable.DamageEventArgs eventArgs)
    {
        GotDamagedByPlayer?.Invoke(this, EventActionArgs.Empty);
        Debug.Log("Got hit by player");
    }

    public void TurnAnimationSetter()
    {
        m_animatedTurnHandle.Execute(m_turnAnimation, m_idle);
    }

   
    private bool hasTurned;
    private void Update()
    {
        if (!m_startFaceDetection)
            return;

        Vector2 playerPos = GameplaySystem.playerManager.player.character.transform.position;
        if (isReturningToSummonSpot == false)
        {
            FacingPlayerUpdate(playerPos);
        }
        else
        {
            FacingStartingPointUpdate(startingPosition);
        } 
    }

    public void AttackAnimationRoutine()
    {
        StartCoroutine(SetAttackAnimation());
    }

    public IEnumerator SetAttackAnimation()
    {
        StopAllCoroutines();
        m_startFaceDetection = false;
        m_spine.SetAnimation(0, m_attackAnimation, false);
        yield return new WaitForAnimationComplete(m_spine.animationState, m_attackAnimation);
        m_spine.SetAnimation(0, m_idle, true);
        yield return new WaitForSeconds(.3f);
        m_startFaceDetection = true;
    }
    public void ColliderController(bool condition)
    {
        for (int i = 0; i < m_collider.Length; i++)
        {
            m_collider[i].enabled = condition;
        }
    }
    private void FacingStartingPointUpdate(Vector2 m_startingPosition)
    {
        if (isFacingTarget(m_startingPosition) == false && !hasTurned)
        {
            Debug.Log("Turning?");
            TurnAnimationSetter();
            hasTurned = true;
        }
        else if (isFacingTarget(m_startingPosition))
        {
            hasTurned = false;
        }

    }

    private void FacingPlayerUpdate(Vector2 playerPos)
    {
        if (isFacingTarget(playerPos) == false && !hasTurned)
        {
            TurnAnimationSetter();
            Debug.Log("Turning to player poss");
            hasTurned = true;
        }
        else if (isFacingTarget(playerPos))
        {
            hasTurned = false;
        }
    }

    public bool isFacingTarget(Vector2 position)
    {
        if (position.x > m_character.transform.position.x)
        {
            return m_character.facing == HorizontalDirection.Right;
        }
        else
        {
            return m_character.facing == HorizontalDirection.Left;
        }
    }

    private IEnumerator DelayActivateReachArea()
    {
        if (m_reachedAreaToActivate)
            yield break;

        var randomNumer = UnityEngine.Random.Range(0.2f, 0.3f);
        yield return new WaitForSeconds(randomNumer);
        Debug.Log(randomNumer);
        m_reachedAreaToActivate = true;
        m_startFaceDetection = true;
        m_spine.SetAnimation(0, m_idle, true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BoxKoNiYaHa")
        {
            SmallAdranReachedZone?.Invoke(this, EventActionArgs.Empty);
            Debug.Log("Reached ZOne");
            StartCoroutine(DelayActivateReachArea());
        }
    }

}