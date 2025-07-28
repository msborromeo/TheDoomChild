using DChild.Gameplay.UI.Map;
using UnityEngine;

namespace DChild.Gameplay.NavigationMap
{
    public class PointOfInterestIconGroupUI: MonoBehaviour
    {
        private RectTransform[] m_pointIcons;

        public RectTransform[] pointIcons => m_pointIcons;

        public void Zoom(Vector2 scrollValue, float scaleRate)
        {
            var ySign = Mathf.Sign(scrollValue.y);
            foreach (var icon in m_pointIcons)
            {
                icon.localScale = new Vector2(1f, 1f);
            }
        }

        private void Awake()
        {
            m_pointIcons = GetComponentsInChildren<RectTransform>();
        }
    }
}