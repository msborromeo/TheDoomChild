using DChild.Gameplay;
using DChild.Gameplay.Characters.Enemies;
using DChild.Gameplay.Characters.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonolithSlamCollision : MonoBehaviour
{
    [SerializeField]
    private MonolithSlam m_monolithSlam;
    [SerializeField]
    private MonolithSlamAttack m_monolithSlamAttackTohide;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        var playerObject = collision.gameObject.GetComponentInParent<PlayerControlledObject>();
        if (playerObject != null && collision.tag != "Sensor" && playerObject.owner == (IPlayer)GameplaySystem.playerManager.player)
        {
           
            Debug.Log("Player hit");
            m_monolithSlam.RemoveTentacle();
            //m_monolithSlamAttackTohide.monolithsToActuallyKeep.Remove(m_monolithSlam.gameObject);
            m_monolithSlamAttackTohide.RemoveMonolithFromList(m_monolithSlam.gameObject);
            m_monolithSlam.SpawnShatterFX();
        }
    }
}
