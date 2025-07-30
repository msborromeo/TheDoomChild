using DChild.Gameplay.UI.Map;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

namespace DChild.Gameplay.NavigationMap
{
    public class NavigationMapIconManager : MonoBehaviour
    {
        [SerializeField] private PointOfInterestIconGroupUI[] m_iconGroupCollection;


        public void OnMapZoom(object sender, MapZoomEventActionArgs zoomArgs)
        {
            foreach (var iconGroup in m_iconGroupCollection)
            {
                iconGroup.Zoom(zoomArgs.scrollWheel, zoomArgs.iconScaleRate);
            }
        }
    }
}