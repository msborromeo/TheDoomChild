using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayOfFrostColliderConfigurator : MonoBehaviour
{
    [SerializeField]
    private LineRenderer m_reference;
    [SerializeField]
    private EdgeCollider2D m_collider;
    [SerializeField]
    private float m_activateColliderThreshold;

    private List<Vector2> m_colliderPoints;
    [SerializeField]
    private bool m_isInverted;

    public void ReorientCollider()
    {
        m_colliderPoints.Clear();
        for (int i = 0; i < m_reference.positionCount; i++)
        {
            var position = m_reference.GetPosition(i) - transform.position;
            if (m_isInverted)
                position.x *= -1;
            m_colliderPoints.Add(position);

        }
        m_collider.SetPoints(m_colliderPoints);
    }


    private void Awake()
    {
        m_colliderPoints = new List<Vector2>();
    }
    // Update is called once per frame
    void Update()
    {
        ReorientCollider();
    }
}
