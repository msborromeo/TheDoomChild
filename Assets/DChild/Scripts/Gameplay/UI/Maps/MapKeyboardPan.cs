using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DChild.Gameplay.UI.Map
{
    [DisallowMultipleComponent]
    public sealed class MapKeyboardPan : MonoBehaviour
    {
        private const string PAN_ACTION_PATH = "UI/Navigate";
        private const float INPUT_EPSILON = 0.001f;

        [SerializeField]
        private ScrollRect m_scrollRect;
        [SerializeField]
        private InputActionReference m_panActionReference;
        [SerializeField, Min(0f)]
        private float m_speed = 2000f;

        private InputAction m_panAction;

        public void Initialize(ScrollRect scrollRect)
        {
            m_scrollRect = scrollRect;
            ResolvePanAction();
        }

        private void Update()
        {
            if (m_scrollRect == null || m_scrollRect.content == null)
            {
                return;
            }

            if (m_panAction == null || !m_panAction.enabled)
            {
                ResolvePanAction();
            }

            if (m_panAction == null || !m_panAction.enabled)
            {
                return;
            }

            var input = m_panAction.ReadValue<Vector2>();
            if (input.sqrMagnitude < INPUT_EPSILON)
            {
                return;
            }

            var content = m_scrollRect.content;
            var viewport = m_scrollRect.viewport != null
                ? m_scrollRect.viewport
                : (RectTransform)m_scrollRect.transform;

            var scaleX = Mathf.Abs(content.lossyScale.x / viewport.lossyScale.x);
            var scaleY = Mathf.Abs(content.lossyScale.y / viewport.lossyScale.y);
            var zoomRatio = (scaleX + scaleY) * 0.5f;
            var horizontalRange = (content.rect.width * scaleX) - viewport.rect.width;
            var verticalRange = (content.rect.height * scaleY) - viewport.rect.height;
            var normalizedPosition = m_scrollRect.normalizedPosition;
            var distance = m_speed * zoomRatio * Time.unscaledDeltaTime;

            if (m_scrollRect.horizontal && horizontalRange > 0f)
            {
                normalizedPosition.x += input.x * distance / horizontalRange;
            }

            if (m_scrollRect.vertical && verticalRange > 0f)
            {
                normalizedPosition.y += input.y * distance / verticalRange;
            }

            normalizedPosition.x = Mathf.Clamp01(normalizedPosition.x);
            normalizedPosition.y = Mathf.Clamp01(normalizedPosition.y);

            m_scrollRect.StopMovement();
            m_scrollRect.normalizedPosition = normalizedPosition;
        }

        private void ResolvePanAction()
        {
            if (m_panActionReference != null)
            {
                m_panAction = m_panActionReference.action;
                return;
            }

            var playerInputs = FindObjectsOfType<PlayerInput>();
            for (var i = 0; i < playerInputs.Length; i++)
            {
                var action = playerInputs[i].actions?.FindAction(PAN_ACTION_PATH, false);
                if (action != null && action.enabled)
                {
                    m_panAction = action;
                    return;
                }
            }
        }
    }
}
