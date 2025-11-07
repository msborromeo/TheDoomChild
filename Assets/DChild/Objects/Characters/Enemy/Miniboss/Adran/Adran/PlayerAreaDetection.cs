using DChild.Gameplay;
using DChild.Gameplay.Characters.Players;
using Holysoft.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAreaDetection : MonoBehaviour
{
   public enum Area
   {
    Area1NiJan, 
    Area2NiToto,
    Area3NiTommi,
    Area4NiStephen
   }
    [SerializeField]
    private Area m_areaLocated;
    public delegate void PlayerEnteredArea(Area area);
    public static event PlayerEnteredArea OnPlayerEnteredArea;

    private void Start()
    {
      Debug.Log(m_areaLocated.ToString()+ " player location, PlayerAreaDetection Script");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag != "Hitbox")
            return;
        var playerObject = collision.gameObject.GetComponentInParent<PlayerControlledObject>();
        if (playerObject != null && collision.tag != "Sensor" && playerObject.owner == (IPlayer)GameplaySystem.playerManager.player)
        {
            OnPlayerEnteredArea?.Invoke(m_areaLocated);
            //Debug.Log(m_areaLocated);
        }
    }
}
