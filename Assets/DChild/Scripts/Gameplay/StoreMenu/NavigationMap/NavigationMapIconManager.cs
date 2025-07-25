using Holysoft.Event;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Gameplay.NavigationMap
{
    public class NavigationMapIconManager : MonoBehaviour
    {
        [SerializeField, AssetSelector] private PointOfInterestIconGroupUI[] m_iconGroupCollection;

        public event EventAction<EventActionArgs> OnMapZoom;

        public void Zoom()
        {
            OnMapZoom.Invoke(this, EventActionArgs.Empty);
        }
    }
}