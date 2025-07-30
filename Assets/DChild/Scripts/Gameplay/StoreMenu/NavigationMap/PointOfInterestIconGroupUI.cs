using DChild.Gameplay.UI.Map;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Gameplay.NavigationMap
{
    public class PointOfInterestIconGroupUI: MonoBehaviour
    {
        private const float MIN_SIZE= 1f;
        private const float MAX_SIZE = 3f;

        private RectTransform[] m_pointIcons;

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

            if (currentY < MIN_SIZE)
            {
                currentY = MIN_SIZE;

            }
            else if (currentY > MAX_SIZE)
            {
                currentY = MAX_SIZE;
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