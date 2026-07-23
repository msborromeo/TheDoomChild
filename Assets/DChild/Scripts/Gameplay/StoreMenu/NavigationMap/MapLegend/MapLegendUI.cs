using DChild.Codex.Tutorial;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Gameplay.NavigationMap.MapLegend
{
    public class MapLegendUI : MonoBehaviour
    {

        [SerializeField] private MapLegendListUI m_listUI;
        public MapLegendListUI listUI => m_listUI;

        [SerializeField] private MapLegendBulletUIHandle m_bulletHandle;
        public MapLegendBulletUIHandle bulletHandle => m_bulletHandle;

        [Button]
        public void Initialize()
        {
            m_listUI.DisplayMapLegend();
            m_bulletHandle.SetupBullets();
            ResubscribeEvents();
        }

        public void SetLegendList(MapLegendEntryUI[] entries)
        {
            var filteredList = RemoveRepeatingEntries(entries);
            m_listUI.SetFilteredEntries(filteredList);
        }

        private MapIcon[] RemoveRepeatingEntries(MapLegendEntryUI[] entries)
        {
            if (entries == null)
                return System.Array.Empty<MapIcon>();

            return entries
                .Select(entry => entry.legendEntry)
                .Distinct()
                .ToArray();
        }


        private void ResubscribeEvents()
        {
            m_bulletHandle.OnPageChange -= m_listUI.HandlePageChange;
            m_bulletHandle.OnPageChange += m_listUI.HandlePageChange;
        }
    }
}
