using DChild.Gameplay.Environment;
using Doozy.Runtime.UIManager.Components;
using PixelCrushers.DialogueSystem;
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

        public List<UIToggle> GetFastTravelLocationTabs() => m_tabGroup.toggles;
        public bool isOpen => m_isOpen;
        private int m_currentTabIndex = 0;
        public int currentTabIndex => m_currentTabIndex;

        public void ForceOpenPage(Environment.Location startingLocation, FastTravelData playerLocation)
        {
            if (playerLocation != null)
                m_locationPage.SetCurrentPlayerPosition(playerLocation);

            SetupLocationTabs(startingLocation, playerLocation);

            m_isOpen = true;
        }
        private void SetupLocationTabs(Environment.Location startingLocation, FastTravelData playerLocation)
        {
            var toggles = m_tabGroup.toggles;
            for (int i = 0; i < toggles.Count; i++)
            {
                var locationTab = toggles[i].GetComponent<FastTravelLocationTab>();
                locationTab.OnDataChange();

                CheckUnlockedTownGates(locationTab);

                var isFromOverworld = locationTab.locationList.overworldTravelData == playerLocation;
                if (locationTab.locationList.location == startingLocation || isFromOverworld)
                {
                    SelectLocationTab(i);
                    OpenLocationList(locationTab);
                }
            }
        }

        private void CheckUnlockedTownGates(FastTravelLocationTab locationTab)
        {
            if (locationTab.locationList == null)
                return;

            bool unlockedOneGate = false;

            for (int i = 0; i < locationTab.locationList.count; i++)
            {
                var travelData = locationTab.locationList.GetUnderworldTravelData(i);
                string varName = FastTravelUtility.GenerateActivationVariableName(travelData);
                bool isActivated = DialogueLua.GetVariable(varName).asBool;

                if (isActivated)
                {
                    unlockedOneGate = true;
                    break;
                }
            }

            locationTab.toggle.interactable = unlockedOneGate;
        }

        public void SelectLocationTab(int tabIndex)
        {
            var updatedLocation = m_tabGroup.toggles[tabIndex];
            updatedLocation.SetIsOn(true);
        }

        public void OpenLocationList(FastTravelLocationTab locationTab)
        {
            if (locationTab == null) return;

            m_currentTabIndex = GetTabIndexFromParentGroup(locationTab);
            m_locationPage.ShowPage(locationTab.locationList);
        }

        public void FastTravelTo(FastTravelOptionButton travelButton) => m_handle.TransferPlayerTo(travelButton.data.fastTravelPoint);

        private int GetTabIndexFromParentGroup(FastTravelLocationTab locationTab)
        {
            return m_tabGroup.toggles.IndexOf(locationTab.toggle);
        }

        public void Reset() => m_isOpen = false;
    }
}