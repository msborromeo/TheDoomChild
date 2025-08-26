using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreBonewagonCollision : MonoBehaviour
{
    [SerializeField]
    private List<Collider2D> m_bonewagonColliders;
    private Collider2D m_collider;

    void Start()
    {
        m_collider = GetComponent<Collider2D>();
        foreach (Collider2D bonewagonCollider in m_bonewagonColliders)
        {
            if (bonewagonCollider != null)
            {
                Physics2D.IgnoreCollision(m_collider, bonewagonCollider, true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
