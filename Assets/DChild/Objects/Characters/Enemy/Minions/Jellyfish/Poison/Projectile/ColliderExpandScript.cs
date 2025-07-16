using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderExpandScript : MonoBehaviour
{
    [SerializeField,InfoBox("in seconds")]
    float m_Duration;
    [SerializeField]
    CircleCollider2D m_Collider;
    [SerializeField]
    float m_spreadSpeed;

    private float m_TimeAlive;
    // Start is called before the first frame update
    void Start()
    {
        if(m_Collider == null)
        {
            m_Collider = GetComponent<CircleCollider2D>();
        }
        if(m_spreadSpeed<=0)
        {
            m_spreadSpeed = 1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_TimeAlive < m_Duration)
        {
            m_Collider.radius += Time.deltaTime*m_spreadSpeed;
            m_TimeAlive += Time.deltaTime;
        }else
        {
            m_Collider.enabled = false;
        }
    }
}
