using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptPowent : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Tommi;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Hitbox"))
        {
            m_Tommi.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox")) { 

            m_Tommi.SetActive(false);
        }
    }
}
