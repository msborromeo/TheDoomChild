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


        public void OnMapZoom(object sender, MapZoomEventActionArgs eventArgs)
        {
            AdjustCollectionScaling(m_iconGroupCollection, eventArgs, 0);
        }
        //foreach (var iconGroup in m_iconGroupCollection)
            //{
            //    iconGroup.Zoom(eventArgs.iconScaleRate);
            //}
        //}

        private void AdjustCollectionScaling(PointOfInterestIconGroupUI[] groupCollection, MapZoomEventActionArgs zoomArgs , int i)
        {
            if ( i == groupCollection.Length)
                return;

            groupCollection[i].Zoom(zoomArgs.scrollWheel, i);
            AdjustCollectionScaling(groupCollection, zoomArgs, i + 1);
        }
    }
}