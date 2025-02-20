using DChild.Gameplay.Pooling;
using DChild;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SmallSphereBomb : MonoBehaviour
{

    [ReadOnly]
    public Vector2 targetPosition;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float flightTime = 1.0f;
    [SerializeField]
    private GameObject m_spherebombFX;

    void Start()
    {
        Launch(targetPosition, flightTime);
    }

    void Launch(Vector2 target, float time)
    {
        Vector2 startPosition = transform.position;
        Vector2 targetWorldPos = startPosition + target;

        Vector2 velocity = new Vector2(
            (targetWorldPos.x - startPosition.x) / time,  // Vx
            ((targetWorldPos.y - startPosition.y) + (0.5f * Mathf.Abs(Physics2D.gravity.y) * time * time)) / time  // Vy
        );

        rb.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            Debug.Log("I will make the world kaboom");
            var instance = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_spherebombFX, gameObject.scene);
            instance.SpawnAt(new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
