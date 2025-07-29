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
        private const float MIN_ZOOM = 0.5f;
        private const float MAX_ZOOM = 1.5f;

        private float m_currentY;

        [SerializeField] private float m_scaleRate;

        public void SetupZoom(RectTransform receivedMap)
        {
            m_currentMap = receivedMap;
            m_currentY = DEFAULT_ZOOM;
        }


        public void Zoom()
        {
            m_currentMap.localScale = new Vector2(m_currentY, m_currentY);

            var currentZoom = m_currentMap.localScale.y;

            var scrollWheel = Mouse.current.scroll.ReadValue();

            Debug.Log($"scroll wheel y value: {scrollWheel.y}");
            Debug.Log($"scroll wheel normalized value: {scrollWheel.normalized}");

            if (scrollWheel.y == 0) { return; }


            var ySign = Mathf.Sign(scrollWheel.y);

            m_currentY += m_scaleRate * ySign;

            if (m_currentY < MIN_ZOOM)
            {
                m_currentY = MIN_ZOOM;
            }
            else if (m_currentY > MAX_ZOOM)
            {
                m_currentY = MAX_ZOOM;
            }

     
        }
    }
}