using UnityEngine;

namespace DChild.Gameplay.UI
{
    public class PromptScaler : MonoBehaviour
    {
        [SerializeField, Min(0)] private float m_scaleRatio = 1f;
        [SerializeField, Min(0)] private float m_baseCameraDistance;

        [SerializeField, Min(0)] private float m_sliceInterval;

        private float m_sliceTimer;
        private Transform m_prompt;

        private void ScalePromptToCameraDistance(Camera camera)
        {
            if (camera == null)
            {
                m_prompt.localScale = Vector2.one;
                return;
            }

            var distanceDiff = Mathf.Abs(camera.transform.position.z) - m_baseCameraDistance;
            float zAxis = distanceDiff * m_scaleRatio;

            m_prompt.localScale = Vector3.one + new Vector3(zAxis, zAxis, 0);
        }

        private void LateUpdate()
        {
            m_sliceTimer -= GameplaySystem.time.deltaTime;

            if (m_sliceTimer <= 0)
            {
                ScalePromptToCameraDistance(GameSystem.mainCamera);
                m_sliceTimer = m_sliceInterval;
            }
        }

        private void OnEnable() => ScalePromptToCameraDistance(GameSystem.mainCamera);
        private void Awake() => m_prompt = GetComponent<RectTransform>();
    }
}