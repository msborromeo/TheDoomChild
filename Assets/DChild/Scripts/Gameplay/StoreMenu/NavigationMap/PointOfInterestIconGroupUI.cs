using DChild.Gameplay.UI.Map;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Gameplay.NavigationMap
{
    public class PointOfInterestIconGroupUI: MonoBehaviour
    {
        private float m_minSize= 1f;
        private float m_maxSize = 3f;

        private RectTransform[] m_pointIcons;

        public void SetZoomConstraints(float min, float max)
        {
            m_minSize = min;
            m_maxSize = max;
        }

        public void Zoom(Vector2 scrollValue, float scaleRate)
        {
            var ySign = Mathf.Sign(scrollValue.y);
            foreach (var icon in m_pointIcons)
            {
                ScaleIcon(icon, ySign, scaleRate);
            }
        }

        private void ScaleIcon(RectTransform icon, float ySign, float scaleRate)
        {
            var currentY = icon.localScale.y;
            currentY += ySign * scaleRate;

            if (currentY < m_minSize)
            {
                currentY = m_minSize;

            }
            else if (currentY > m_maxSize)
            {
                currentY = m_maxSize;
            }

            icon.localScale = new Vector2(currentY, currentY);

        }

        private void Awake()
        {
            var iconList = GetComponentsInChildren<RectTransform>().Skip(1);
            m_pointIcons = iconList.ToArray();
        }
    }
}