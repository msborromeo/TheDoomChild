using Holysoft.Event;
using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenBeeDetector : MonoBehaviour
{

    public static event EventAction<EventActionArgs> detection;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            detection?.Invoke(this, EventActionArgs.Empty);
            Debug.Log("queen bee detected");
        }
    }

}
