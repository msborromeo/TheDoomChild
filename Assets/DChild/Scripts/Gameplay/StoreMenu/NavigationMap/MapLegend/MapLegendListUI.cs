using Holysoft.Event;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Gameplay.NavigationMap.MapLegend
{
    public class MapLegendListUI : MonoBehaviour
    {
        private const int PAGE_LIMIT = 13;
        public int entryLimit => PAGE_LIMIT;

        [SerializeField] private List<MapLegendEntryUI> m_completeUIList;


        private List<MapLegendEntryUI> m_filteredEntries = new();
        public List<MapLegendEntryUI> legendEntries => m_filteredEntries;

        private MapIcon[] m_filteredIconData;


        public void SetFilteredEntries(MapIcon[] entries)
        {
            m_filteredIconData = entries;
        }

        public void DisplayMapLegend()
        {
            Reset();
            ShowEntries();
        }

        private void MatchEntriesToFilteredList()
        {
            m_filteredEntries.Clear();
            foreach (var entry in m_completeUIList)
            {
                var iconType = entry.legendEntry;

                if (m_filteredIconData.Contains(iconType))
                    m_filteredEntries.Add(entry);
            }
        }

        public void ShowEntries()
        {
            MatchEntriesToFilteredList();
            HandlePageChange(0);
        }

        public void HandlePageChange(int page)
        {
            Reset();

            int totalEntries = m_filteredEntries?.Count ?? 0;
            int offset = PAGE_LIMIT * page;

            if (totalEntries == 0 || offset < 0 || offset >= totalEntries)
                return;

            int endIndex = Mathf.Min(offset + PAGE_LIMIT, totalEntries);

            for (int i = offset; i < endIndex; i++)
            {
                if (m_filteredEntries[i] is MapLegendEntryUI icon && icon.gameObject != null)
                {
                    icon.gameObject.SetActive(true);
                }
            }
        }

        private void Reset()
        {
            foreach (MapLegendEntryUI item in m_completeUIList)
                item.gameObject.SetActive(false);
        }
    }
}
