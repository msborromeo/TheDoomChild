using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DChild.Gameplay.Characters;
using DChild.Gameplay.Characters.Enemies;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using DChild.Gameplay.Characters.Players;
using DChild.Gameplay;
using DChild.Gameplay.Characters.Players.Modules;
using DChild.Gameplay.Combat;
using DChild.Gameplay.Combat.StatusAilment;
using DChild.Gameplay.Characters.Players.State;
using DChild.Gameplay.Characters.AI;
using DChild;

public class CobWebTrigger : MonoBehaviour
{
    [SerializeField]
    private StatusInflictor m_statusInflictor;
    private bool m_isinshadow = false;
    public EventAction<EventActionArgs> CobWebEnterEvent;
    public EventAction<EventActionArgs> Onhit;
    public PlayerDamageable playerDamageable=null;



    public void CobwebEvent()
    {
        CobWebEnterEvent?.Invoke(this, EventActionArgs.Empty);

    }
    public void DamageTaken()
    {
        Onhit?.Invoke(this, EventActionArgs.Empty);
    }

    public void ClearStatus()
    {
        if (playerDamageable != null)
        {
            StatusEffectReciever playerstatus = playerDamageable.GetComponentInParent<StatusEffectReciever>();
            playerstatus.StopStatusEffect(StatusEffectType.Snared);
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag != "Hitbox")
        //    return;
        m_isinshadow = false;
        var playerObject = collision.gameObject.GetComponentInParent<PlayerControlledObject>();
        if (playerObject != null && collision.tag != "Sensor" && playerObject.owner == (IPlayer)GameplaySystem.playerManager.player)
        {
            playerDamageable = collision.GetComponentInParent<PlayerDamageable>();
            DamageTaken();
            Debug.Log("hit??");
            m_isinshadow = GameplaySystem.playerManager.player.character.GetComponentInChildren<IShadowModeState>().isInShadowMode;
            if (collision.tag == "Hitbox"&& m_isinshadow == false)
            {
                m_statusInflictor.InflictStatusTo(collision.GetComponentInParent<StatusEffectReciever>());
            }
           
        }
    }
    
    }
