using DChild;
using DChild.Gameplay;
using DChild.Gameplay.Characters.AI;
using DChild.Gameplay.Characters.Enemies;
using DChild.Gameplay.Combat;
using DChild.Gameplay.Pooling;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MiniThirdFormAI : CombatAIBrain<MiniThirdFormAI.Info>
{
    [System.Serializable]
    public class Info : BaseInfo
    {

      
        [TitleGroup("Animations")]
        [SerializeField, ValueDropdown("GetAnimations")]
        private string m_miniTheOneIdle;
        public string miniTheOneIdle => m_miniTheOneIdle;

        public override void Initialize()
        {
    
        }
    }
    [SerializeField]
    private BasicHealth m_health;
    [SerializeField]
    private Damage m_damageOnDeath;
    [SerializeField]
    private Damageable m_theOneThatGotAway;
    [SerializeField]
    private Collider2D m_hitbox;
    [SerializeField]
    private Damageable m_damageableMini;
    [SerializeField]
    private GameObject m_teleportFX;
    [SerializeField]
    private Transform m_teleportLocation;
    [SerializeField, TabGroup("Eye")]
    private Transform m_eyeTheOneMini;
    [SerializeField, TabGroup("Eye")]
    private Transform m_eyeCenter;
    [SerializeField, TabGroup("Eye")]
    private float m_maxDistance;
    [SerializeField, TabGroup("Eye")]
    private float m_followSpeed;
    private int m_damageReceived;

    public EventAction<EventActionArgs> OnDeath;

   
    protected override void Awake()
    {
        base.Awake();
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
       // InstantiateTeleportVFX();
        GameplaySystem.combatManager.Damage(m_theOneThatGotAway, m_damageOnDeath);
        m_teleportFX.transform.position = m_teleportLocation.position;
        m_teleportFX.SetActive(true);
        
    }

    protected override void Start()
    {
        int maxHealth = 1200;
        m_damageableMini.health.SetMaxValue(maxHealth);
        m_damageableMini.health.SetHealthPercentage(1f);
    }
    private GameObject InstantiateTeleportVFX()
    {
        var instance = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_teleportFX, gameObject.scene);
        instance.SpawnAt(m_teleportLocation.position, Quaternion.identity);
        return instance.gameObject;
    }
    private void EyeTracker()
    {
        if (m_targetInfo == null || m_eyeTheOneMini == null) return;
        Vector2 direction = ((Vector2)GameplaySystem.playerManager.player.character.transform.position - (Vector2)m_eyeCenter.position).normalized;
        Vector2 targetPosition = (Vector2)m_eyeCenter.position + (direction * Mathf.Min(Vector2.Distance((Vector2)GameplaySystem.playerManager.player.character.transform.position, (Vector2)m_eyeCenter.position), m_maxDistance));
        m_eyeTheOneMini.position = Vector2.Lerp(m_eyeTheOneMini.position, targetPosition, Time.deltaTime * m_followSpeed);
    }

    void Update()
    {
       EyeTracker(); 
       if(m_damageableMini.health.currentValue == 700)
       {
            //animation of blood eye
       }else if(m_damageableMini.health.currentValue == 300)
       {

       }
       else
       {
            m_animation.SetAnimation(0, m_info.miniTheOneIdle, true);
       }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        m_damageable.DamageTaken -= OnDamageTaken;
        m_health.Death -= OnDeathEvent;
    }

    public override void ReturnToSpawnPoint()
    {
        
    }

    protected override void OnTargetDisappeared()
    {
        
    }
}
