using DChild.Gameplay.Characters.Enemies;
using Doozy.Runtime.Nody;
using Holysoft.Event;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace DChild.Gameplay.UI.Map
{
    public class MapZoomEventActionArgs : IEventActionArgs
    {
        private Vector2 m_scrollWheel;
        public Vector2 scrollWheel => m_scrollWheel;
        
        private float m_iconScale;
        public float iconScaleRate => m_iconScale;

        public MapZoomEventActionArgs(Vector2 scrollWheel, float iconScaleRate)
        {
            this.m_scrollWheel = scrollWheel;
            this.m_iconScale = iconScaleRate;
        }
    }

    public class MapZoomHandler : MonoBehaviour
    {
        private RectTransform m_currentMap;

        private const float DEFAULT_ZOOM = 1.0f;
        private float m_minZoom;
        private float m_maxZoom;

        private float m_currentY;

        [SerializeField] private float m_mapScale;
        [SerializeField] private float m_iconScale;


        public EventAction<MapZoomEventActionArgs> OnMapZoom;

        public void SetupZoom(RectTransform receivedMap)
        {
            m_currentMap = receivedMap;
            m_currentY = DEFAULT_ZOOM;
        }

        public void SetZoomConstraints(float min, float max)
        {
            m_minZoom = min;
            m_maxZoom = max;
        }

        public void Zoom()
        {
            m_currentMap.localScale = new Vector2(m_currentY, m_currentY);

            var scrollWheel = Mouse.current.scroll.ReadValue();

            if (scrollWheel.y == 0) { return; }


            var ySign = Mathf.Sign(scrollWheel.y);

            m_currentY += m_mapScale * ySign;

            Debug.Log($"current y: {m_currentY}");
            OnMapZoom?.Invoke(this, new MapZoomEventActionArgs(scrollWheel, m_iconScale));

            if (m_currentY < m_minZoom)
            {
                m_currentY = m_minZoom;
            }
            else if (m_currentY > m_maxZoom)
            {
                m_currentY = m_maxZoom;
            }

        }

    }
}