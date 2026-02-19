using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererLengthControl : MonoBehaviour
{
    [SerializeField] private Vector2 targetPosition = new Vector2(5f, -5f);

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer.positionCount < 2)
        {
            lineRenderer.positionCount = 2;
        }
    }

    public void SetLineLength()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        Vector3 pos = lineRenderer.GetPosition(1);
        pos.x = targetPosition.x;
        pos.y = targetPosition.y;

        lineRenderer.SetPosition(1, pos);
    }

    public void ResetLine()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        Vector3 pos = lineRenderer.GetPosition(1);
        pos.x = 0f;
        pos.y = 0f;

        lineRenderer.SetPosition(1, pos);
    }
}
