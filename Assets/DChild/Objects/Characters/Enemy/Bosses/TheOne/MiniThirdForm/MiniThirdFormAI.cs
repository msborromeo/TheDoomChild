using DChild.Gameplay.Combat;
using Holysoft.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniThirdFormAI : MonoBehaviour
{
    [SerializeField]
    private BasicHealth m_health;
    [SerializeField]
    private Collider2D m_hitbox;
    [SerializeField]
    private Damageable m_damageable;

    private int m_damageReceived;

    public EventAction<EventActionArgs> OnDeath;

    private void Awake()
    {
        m_damageable.DamageTaken += OnDamageTaken;
        m_health.Death += OnDeathEvent;
    }

    private void OnDamageTaken(object sender, Damageable.DamageEventArgs eventArgs)
    {

    }
    private void OnDeathEvent(object sender, EventActionArgs eventArgs)
    {
        m_hitbox.enabled = false;
        OnDeath?.Invoke(this, EventActionArgs.Empty);
        this.gameObject.SetActive(false);
    }

    void Start()
    {
        int maxHealth = 1200;
        m_damageable.health.SetMaxValue(maxHealth);
        m_damageable.health.SetHealthPercentage(1f);
    }

    void Update()
    {
        
    }
}
