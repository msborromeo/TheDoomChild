using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingStoneWallPush : MonoBehaviour
{
    [SerializeField]
    private float pushStrength;
    [SerializeField]
    private Rigidbody2D m_rigidBodyWall;
    private Vector2 lastPosition;
    private void Start()
    {
    }
    void OnCollisionEnter2D (Collision2D collision)
    {
        #region rigidbody
        Rigidbody2D rb = collision.gameObject.GetComponentInParent<Rigidbody2D>(); // Get the Rigidbody of the colliding object

        if (rb != null && !rb.isKinematic) // Ensure the object can be pushed
        {
            // Calculate velocity based on position difference (only during collision)
            Vector2 currentPosition = transform.position;
            Vector2 pushDirection = (currentPosition - lastPosition) / Time.fixedDeltaTime;
            lastPosition = currentPosition;

            pushDirection.y = 0; // Prevent unwanted vertical movement
            rb.AddForce(pushDirection * pushStrength, ForceMode2D.Force); // Apply force smoothly
        }
        #endregion

    }
}
