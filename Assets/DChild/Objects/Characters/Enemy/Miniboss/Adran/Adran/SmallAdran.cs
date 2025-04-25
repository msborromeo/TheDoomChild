using DChild.Gameplay.Combat;
using Holysoft.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAdran : MonoBehaviour
{
    [SerializeField]
    private Damageable m_Damageable;
    [SerializeField]
    private BasicHealth m_basicHealth;
    public event EventAction<EventActionArgs> GotDamagedByPlayer;
    public event EventAction<EventActionArgs> SmallAdranGotDestroyed;
    private void Start()
    {
        m_Damageable.DamageTaken += Damageable_DamageTaken;
        m_Damageable.Destroyed += ObjectOnDestroyed;
    }


    private void ObjectOnDestroyed(object sender, EventActionArgs eventArgs)
    {
        Debug.Log("Adran is Destroyed");
        SmallAdranGotDestroyed?.Invoke(this,EventActionArgs.Empty);
    }

    private void Damageable_DamageTaken(object sender, Damageable.DamageEventArgs eventArgs)
    {
        GotDamagedByPlayer?.Invoke(this, EventActionArgs.Empty);
        Debug.Log("Got hit by player");
    }
}
