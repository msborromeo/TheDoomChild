using DChild.Gameplay.Environment;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Gameplay.FastTravel
{
    public class FastTravelUIManager : MonoBehaviour
    {
        [SerializeField]
        private UIToggleGroup m_tabGroup;
        [SerializeField]
        private FastTravelHandle m_handle;
        [SerializeField]
        private FastTravelPageUI m_locationPage;
        [ReadOnly] private bool m_isOpen = false;
        public bool IsFastTravelOpen() => m_isOpen;
        public List<UIToggle> GetFastTravelLocationTabs() => m_tabGroup.toggles;

        public void ForceOpenPage(Location startingLocation, FastTravelData playerLocation)
        {
            if (playerLocation != null)
                m_locationPage.SetCurrentPlayerPosition(playerLocation);

            var toggles = m_tabGroup.toggles;
            for (int i = 0; i < toggles.Count; i++)
            {
                var tab = toggles[i].GetComponent<FastTravelLocationTab>();
                tab.OnDataChange();

                var isFromOverworld = tab.locationList.overworldTravelData == playerLocation;

                if (tab.locationList.location == startingLocation || isFromOverworld)
                {
                    SelectLocationTab(i);
                    OpenLocationList(tab);
                }
            }
            m_isOpen = true;
        }

        public void SelectLocationTab(int tabIndex)
        {
            var updatedLocation = m_tabGroup.toggles[tabIndex];
            updatedLocation.SetIsOn(true);
        }

        public void OpenLocationList(FastTravelLocationTab locationTab) => m_locationPage.ShowPage(locationTab.locationList);
        public void FastTravelTo(FastTravelOptionButton travelButton) => m_handle.TransferPlayerTo(travelButton.data.fastTravelPoint);
    }
}
