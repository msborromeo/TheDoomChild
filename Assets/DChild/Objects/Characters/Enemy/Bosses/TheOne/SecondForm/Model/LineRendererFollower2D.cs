using UnityEngine;

public class LineRendererFollower2D : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Settings")]
    [Range(0, 1)]
    [SerializeField] private int followPointIndex = 1;

    [Tooltip("Rotation offset if sprite does not face right by default")]
    [SerializeField] private float rotationOffset = 0f;

    private void LateUpdate()
    {
        if (lineRenderer == null)
            return;

        if (lineRenderer.positionCount < 2)
            return;

        // Get positions (convert to world space if needed)
        Vector3 start = GetWorldPosition(0);
        Vector3 end = GetWorldPosition(1);

        // Follow position
        Vector3 followPos = GetWorldPosition(followPointIndex);
        transform.position = followPos;

        // Direction (2D)
        Vector2 direction = (end - start);
        if (direction.sqrMagnitude < 0.0001f)
            return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += rotationOffset;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private Vector3 GetWorldPosition(int index)
    {
        Vector3 pos = lineRenderer.GetPosition(index);

        // If NOT using world space, convert local → world
        if (!lineRenderer.useWorldSpace)
            pos = lineRenderer.transform.TransformPoint(pos);

        return pos;
    }
}
