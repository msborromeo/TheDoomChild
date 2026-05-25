using DChild.Gameplay.NavigationMap.MapLegend;
using DChild.Gameplay.UI.Map;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Gameplay.NavigationMap
{
    public class NavigationMapIconManager : MonoBehaviour
    {
        [SerializeField] private PointOfInterestIconGroupUI[] m_iconGroupCollection;
        
        private MapLegendEntryUI[] m_legendIcons;
        public MapLegendEntryUI[] legendIcons => m_legendIcons;

        [SerializeField] private float m_minZoom;
        [SerializeField] private float m_maxZoom;


        public void OnMapZoom(object sender, MapZoomEventActionArgs zoomArgs)
        {
            foreach (var iconGroup in m_iconGroupCollection)
            {
                iconGroup.Zoom(zoomArgs.scrollWheel, zoomArgs.iconScaleRate);
            }
        }

        private void Awake()
        {
            foreach (var iconGroup in m_iconGroupCollection)
            {
                iconGroup.SetZoomConstraints(m_minZoom, m_maxZoom);
            }

            m_legendIcons = GetComponentsInChildren<MapLegendEntryUI>();
        }
    }
}