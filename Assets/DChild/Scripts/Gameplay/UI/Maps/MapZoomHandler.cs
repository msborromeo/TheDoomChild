using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace DChild.Gameplay.UI.Map
{
    public class MapZoomHandler : MonoBehaviour
    {
        private RectTransform m_currentMap;

        private const float DEFAULT_ZOOM = 1.0f;
        private const float MIN_ZOOM = 0.75f;
        private const float MAX_ZOOM = 1.5f;

        private float m_currentX;
        private float m_currentY;

        [SerializeField] private float m_scaleRate;

        public void SetupZoom(RectTransform receivedMap)
        {
            m_currentMap = receivedMap;
            m_currentX = DEFAULT_ZOOM;
            m_currentY = DEFAULT_ZOOM;
        }


        public void Zoom()
        {
            m_currentMap.localScale = new Vector2(m_currentX, m_currentY);

            var currentZoom = m_currentMap.localScale.y;

            var scrollWheel = Mouse.current.scroll.ReadValue();

            if (scrollWheel.y == 0) { return; }


            var ySign = Mathf.Sign(scrollWheel.y);

            m_currentX += m_scaleRate * ySign;
            m_currentY += m_scaleRate * ySign;

        }
    }
}