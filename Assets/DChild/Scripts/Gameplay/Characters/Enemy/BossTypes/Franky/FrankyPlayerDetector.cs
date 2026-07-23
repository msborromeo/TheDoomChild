using DChild.Gameplay;
using DChild.Gameplay.Characters.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrankyPlayerDetector : MonoBehaviour
{
    public enum PlayerPosition
    {
        Left,
        Right
    }
    [SerializeField]
    private PlayerPosition m_areaLocated;
    public delegate void PlayerEnteredArea(PlayerPosition playerPos);
    public static event PlayerEnteredArea OnPlayerEnteredArea;
    private void Start()
    {
        Debug.Log(m_areaLocated.ToString() + " Activated, FrankyPlayerDetector Script");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Hitbox")
            return;
        var playerObject = collision.gameObject.GetComponentInParent<PlayerControlledObject>();
        if (playerObject != null && collision.tag != "Sensor" && playerObject.owner == (IPlayer)GameplaySystem.playerManager.player)
        {
            OnPlayerEnteredArea?.Invoke(m_areaLocated);
            Debug.Log(m_areaLocated.ToString() + " player location, FrankyPlayerDetector Script");
        }
    }
}
