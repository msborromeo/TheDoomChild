using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderColor : MonoBehaviour
{
    // The hex color string you want for the collider (e.g., "#FF5733")
    public string hexColor = "#A020F0";

    // This method is used to draw Gizmos in the Scene view
    void OnDrawGizmos()
    {
        // Convert the hex string to a Color
        Color colliderColor;
        if (ColorUtility.TryParseHtmlString(hexColor, out colliderColor))
        {
            Gizmos.color = colliderColor;
        }
        else
        {
            // If the hex is invalid, use a default color
            Gizmos.color = Color.red;
        }

        // Get the collider component (either 3D or 2D)
        Collider collider3D = GetComponent<Collider>();
        Collider2D collider2D = GetComponent<Collider2D>();

        // If it's a 3D collider, draw it manually
        if (collider3D != null)
        {
            if (collider3D is BoxCollider boxCollider)
            {
                Gizmos.DrawWireCube(boxCollider.transform.position + boxCollider.center, boxCollider.size);
            }
            else if (collider3D is SphereCollider sphereCollider)
            {
                Gizmos.DrawWireSphere(sphereCollider.transform.position + sphereCollider.center, sphereCollider.radius);
            }
            else if (collider3D is CapsuleCollider capsuleCollider)
            {
                Gizmos.DrawWireSphere(capsuleCollider.transform.position + capsuleCollider.center, capsuleCollider.radius);
            }
            else if (collider3D is MeshCollider meshCollider)
            {
                Gizmos.DrawWireMesh(meshCollider.sharedMesh, meshCollider.transform.position);
            }
        }

        // If it's a 2D collider, draw it manually
        if (collider2D != null)
        {
            if (collider2D is CircleCollider2D circleCollider)
            {
                Gizmos.DrawWireSphere((Vector2)circleCollider.transform.position + circleCollider.offset, circleCollider.radius);
            }
            else if (collider2D is PolygonCollider2D polygonCollider)
            {
                Vector2[] points = polygonCollider.points;
                for (int i = 0; i < points.Length; i++)
                {
                    Vector2 start = points[i] + (Vector2)polygonCollider.transform.position;
                    Vector2 end = points[(i + 1) % points.Length] + (Vector2)polygonCollider.transform.position;
                    Gizmos.DrawLine(start, end);
                }
            }
            else if (collider2D is EdgeCollider2D edgeCollider)
            {
                Vector2[] points = edgeCollider.points;
                for (int i = 0; i < points.Length - 1; i++)
                {
                    Gizmos.DrawLine((Vector2)edgeCollider.transform.position + points[i], (Vector2)edgeCollider.transform.position + points[i + 1]);
                }
            }
        }
    }
}
