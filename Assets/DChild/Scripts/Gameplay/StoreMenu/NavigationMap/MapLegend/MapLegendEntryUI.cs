using UnityEngine;

namespace DChild.Gameplay.NavigationMap.MapLegend
{
    public class MapLegendEntryUI : MonoBehaviour
    {
        [SerializeField] private MapIcon m_legendEntry;
        public MapIcon legendEntry => m_legendEntry;

    }
}