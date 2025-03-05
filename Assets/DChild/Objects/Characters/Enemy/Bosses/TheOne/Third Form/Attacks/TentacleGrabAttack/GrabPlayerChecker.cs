using DChild.Gameplay;
using DChild.Gameplay.Characters.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class GrabPlayerChecker : MonoBehaviour
{
    [SerializeField]
    private TentacleGrab m_tentacleGrab;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Sensor") && collision.gameObject.layer == 8)
        {
            Debug.Log("eyyy ka munaaaaaaa");
            m_tentacleGrab.GrabbedPlayer();
            m_tentacleGrab.ShowDummyPlayer();
            GameplaySystem.playerManager.player.gameObject.SetActive(false);
            GameplaySystem.playerManager.player.character.gameObject.SetActive(false);
        }
        return;
    }
}
