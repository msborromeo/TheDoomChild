using UnityEngine;

namespace DChild.Gameplay.NavigationMap
{
    public class PointOfInterestIconGroupUI: MonoBehaviour
    {
        private RectTransform[] m_pointIcons;

        public RectTransform[] pointIcons => m_pointIcons;

        private void Awake()
        {
            m_pointIcons = GetComponentsInChildren<RectTransform>();
        }
    }
}