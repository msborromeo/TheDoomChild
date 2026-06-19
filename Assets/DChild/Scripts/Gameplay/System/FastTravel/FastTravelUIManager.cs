using DChild.Gameplay.Environment;
using Doozy.Runtime.UIManager.Components;
using System;
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

        public void OpenLocationList(FastTravelLocationTab locationTab) => m_locationPage.ShowPage(locationTab.locationList);
        public void FastTravelTo(FastTravelOptionButton travelButton) => m_handle.TransferPlayerTo(travelButton.data.fastTravelPoint);


        public void ForceOpenPage(Location startingLocation, FastTravelData playerLocation)
        {
            if (playerLocation != null)
                m_locationPage.SetCurrentPlayerPosition(playerLocation);

            //if (GameplaySystem.GetCurrentWorldType() == Systems.WorldType.Overworld)
            //return;

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
        }

        public void SelectLocationTab(int tabIndex)
        {
            var updatedLocation = m_tabGroup.toggles[tabIndex];
            updatedLocation.SetIsOn(true);
            updatedLocation.Select();
        }

        public int GetFastTravelLocationCount() => m_tabGroup.toggles.Count;
    }
}
