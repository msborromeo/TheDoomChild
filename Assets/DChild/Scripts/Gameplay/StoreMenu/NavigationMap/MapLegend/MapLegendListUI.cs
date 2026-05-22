using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.NavigationMap.MapLegend
{
    public class MapLegendListUI: MonoBehaviour
    {

        [SerializeField] private List<MapLegendEntryUI> m_legendEntries;
        public List<MapLegendEntryUI> legendEntries => m_legendEntries;

    }
}
