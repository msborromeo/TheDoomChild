using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileImpactWaveSlash : MonoBehaviour
{
    [SerializeField]
    private GameObject m_projectleImpact;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(""))
        {

        }
    }
}
