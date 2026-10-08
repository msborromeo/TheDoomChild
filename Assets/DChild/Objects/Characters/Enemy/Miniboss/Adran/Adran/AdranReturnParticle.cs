using UnityEngine;

public class AdranReturnParticle : MonoBehaviour
{
    [Header("Return")]
    [SerializeField] private float m_returnSpeed = 8f;
    [SerializeField] private float m_targetReachDistance = 0.1f;

    [Header("Left Curve")]
    [SerializeField] private float m_leftCurveHeight = 2f;
    [SerializeField] private float m_leftCurveOffset = 1.5f;

    [Header("Right Curve")]
    [SerializeField] private float m_rightCurveHeight = 2f;
    [SerializeField] private float m_rightCurveOffset = 1.5f;

    [Header("Gizmo")]
    [SerializeField] private bool m_drawGizmo = true;
    [SerializeField] private int m_gizmoResolution = 30;
    [SerializeField] private float m_gizmoPointSize = 0.08f;

    private AdranAI m_target;

    private Vector3 m_startPosition;
    private Vector3 m_controlPoint;

    private bool m_isLeftSide;
    private bool m_isReturning;

    private float m_progress;

    public bool IsReturning => m_isReturning;
    public bool IsLeftSide => m_isLeftSide;


    // ============================================================
    // RETURN
    // ============================================================

    public void StartReturn(AdranAI target)
    {
        if (target == null)
        {
            Debug.LogWarning(
                $"{name}: Cannot return because Adran target is null."
            );

            return;
        }

        m_target = target;

        // Remember where the particle started.
        m_startPosition = transform.position;

        // Determine whether it came from the left or right.
        DetermineSide();

        m_progress = 0f;
        m_isReturning = true;
    }


    // ============================================================
    // UPDATE
    // ============================================================

    private void Update()
    {
        if (!m_isReturning)
            return;

        // Adran was destroyed.
        if (m_target == null)
        {
            StopReturn();
            return;
        }

        Vector3 targetPosition =
            m_target.transform.position;

        // Recalculate the curve toward Adran's current position.
        CalculateControlPoint(targetPosition);

        // Check whether we've reached Adran.
        if (Vector3.Distance(transform.position, targetPosition)
            <= m_targetReachDistance)
        {
            FinishReturn();
            return;
        }

        MoveAlongCurve();
    }


    // ============================================================
    // SIDE DETECTION
    // ============================================================

    private void DetermineSide()
    {
        Vector3 direction =
            m_startPosition -
            m_target.transform.position;

        float side =
            Vector3.Dot(
                m_target.transform.right,
                direction
            );

        m_isLeftSide = side < 0f;
    }


    // ============================================================
    // CURVE
    // ============================================================

    private void CalculateControlPoint(Vector3 targetPosition)
    {
        float height = m_isLeftSide
            ? m_leftCurveHeight
            : m_rightCurveHeight;

        float offset = m_isLeftSide
            ? m_leftCurveOffset
            : m_rightCurveOffset;

        // Middle between particle and Adran.
        Vector3 midpoint =
            Vector3.Lerp(
                m_startPosition,
                targetPosition,
                0.5f
            );

        // Curve goes upward.
        Vector3 heightOffset =
            m_target.transform.up * height;

        // Curve bends toward the side the particle came from.
        float sideDirection =
            m_isLeftSide ? -1f : 1f;

        Vector3 sideOffset =
            m_target.transform.right *
            offset *
            sideDirection;

        m_controlPoint =
            midpoint +
            heightOffset +
            sideOffset;
    }


    // ============================================================
    // MOVEMENT
    // ============================================================

    private void MoveAlongCurve()
    {
        /*
         * Find current position on curve.
         */
        Vector3 currentPosition =
            GetBezierPosition(m_progress);

        /*
         * Find a small distance ahead on the curve.
         */
        float nextProgress =
            Mathf.Clamp01(
                m_progress + 0.01f
            );

        Vector3 nextPosition =
            GetBezierPosition(nextProgress);

        float curveDistance =
            Vector3.Distance(
                currentPosition,
                nextPosition
            );

        if (curveDistance <= 0.0001f)
        {
            m_progress = 1f;
            FinishReturn();
            return;
        }

        /*
         * Convert speed into curve progress.
         */
        float progress =
            (m_returnSpeed * Time.deltaTime)
            / (curveDistance / 0.01f);

        m_progress += progress;

        m_progress =
            Mathf.Clamp01(m_progress);

        transform.position =
            GetBezierPosition(m_progress);
    }


    // ============================================================
    // BEZIER
    // ============================================================

    private Vector3 GetBezierPosition(float t)
    {
        Vector3 targetPosition =
            m_target != null
                ? m_target.transform.position
                : transform.position;

        float inverseT = 1f - t;

        return
            inverseT * inverseT * m_startPosition +
            2f * inverseT * t * m_controlPoint +
            t * t * targetPosition;
    }


    // ============================================================
    // FINISH
    // ============================================================

    private void FinishReturn()
    {
        if (m_target != null)
        {
            transform.position =
                m_target.transform.position;
        }

        m_isReturning = false;
        Destroy(gameObject);
        /*
         * Return to your object pool here.
         */
    }


    private void StopReturn()
    {
        m_isReturning = false;

        /*
         * Adran no longer exists.
         *
         * Return particle to pool here if needed.
         */
    }


    // ============================================================
    // GIZMO
    // ============================================================

    private void OnDrawGizmosSelected()
    {
        if (!m_drawGizmo)
            return;

        if (!Application.isPlaying)
            return;

        if (!m_isReturning)
            return;

        if (m_target == null)
            return;

        CalculateControlPoint(
            m_target.transform.position
        );

        DrawCurve();
    }


    private void DrawCurve()
    {
        Vector3 targetPosition =
            m_target.transform.position;

        // Start
        Gizmos.DrawSphere(
            m_startPosition,
            m_gizmoPointSize
        );

        // Control point
        Gizmos.DrawSphere(
            m_controlPoint,
            m_gizmoPointSize
        );

        // Target
        Gizmos.DrawSphere(
            targetPosition,
            m_gizmoPointSize
        );

        // Control lines
        Gizmos.DrawLine(
            m_startPosition,
            m_controlPoint
        );

        Gizmos.DrawLine(
            m_controlPoint,
            targetPosition
        );

        // Actual curve
        Vector3 previous =
            m_startPosition;

        for (int i = 1; i <= m_gizmoResolution; i++)
        {
            float t =
                i / (float)m_gizmoResolution;

            Vector3 current =
                GetBezierPosition(t);

            Gizmos.DrawLine(
                previous,
                current
            );

            previous = current;
        }
    }
}