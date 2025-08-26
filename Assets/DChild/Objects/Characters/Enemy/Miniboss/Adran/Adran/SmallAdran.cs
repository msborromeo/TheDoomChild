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
    private GameObject m_deathVfx;
    public Vector2 startingPosition;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_turnAnimation;
    [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
    private string m_idle;
    [SerializeField]
    private SpineEventListener m_spineListener;
    [SerializeField, TabGroup("GetEvents"),SpineEvent(dataField = "m_skeletonAnimation")]
    private string m_deathFX;
    public event EventAction<EventActionArgs> GotDamagedByPlayer;
    public event EventAction<EventActionArgs> SmallAdranGotDestroyed;
    public bool isReturningToSummonSpot;

    
    private void Start()
    {
        m_spineListener.Subscribe(m_deathFX, DeathVFX);
        m_Damageable.DamageTaken += Damageable_DamageTaken;
        m_Damageable.Destroyed += ObjectOnDestroyed;
        m_deathVfx.SetActive(false);
    }

    
    public void DeathVFX()
    {
        Debug.Log("ASD");
        m_deathVfx.SetActive(true);
    }
    
    public void InitializeField(SpineRootAnimation spineRoot)
    {
        m_spine = spineRoot;
    }
    private void ObjectOnDestroyed(object sender, EventActionArgs eventArgs)
    {
        Debug.Log("Adran is Destroyed");
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        //m_deathVfx.Play();

        SmallAdranGotDestroyed?.Invoke(this, EventActionArgs.Empty);
        
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

}