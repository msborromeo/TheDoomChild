using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBoneForce : MonoBehaviour
{
    public float minAngle; // Minimum angle for diagonal direction
    public float maxAngle;
    public float weight;
    public float forceMagnitude;
    void Start()
    {

        float randomAngle = Random.Range(minAngle, maxAngle);

        // Calculate direction vector based on the random angle
        Vector2 forceDirection = Quaternion.Euler(0, 0, randomAngle) * Vector2.right;
        // Access the Rigidbody2D component
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(forceDirection * forceMagnitude * weight, ForceMode2D.Impulse);
        }
    }


}
