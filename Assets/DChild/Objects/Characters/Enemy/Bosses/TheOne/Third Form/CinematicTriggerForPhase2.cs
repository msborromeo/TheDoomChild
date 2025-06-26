using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CinematicTriggerForPhase2 : MonoBehaviour
{
    [SerializeField]
    private UnityEvent m_onPhaseChange;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11 || collision.CompareTag("Hitbox") && collision.gameObject.layer == 8)
        {
            m_onPhaseChange?.Invoke();
            Debug.Log("Player???");
        }
    }
}
