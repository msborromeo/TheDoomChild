using DChild.Gameplay.Systems;
using DChild.Inputs;
using DChild.UI;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace DChild.Gameplay.UI.Controller
{
    public class UnderworldUIController : MonoBehaviour
    {
        [SerializeField]
        private InputReader m_inputReader;
        [SerializeField]
        private StoreNavigator m_storeNavigator;

        [SerializeField, MinValue(0)] private int m_necroTabIndex = 0;
        [SerializeField, MinValue(0)] private int m_fastTravelIndex = 0;

        private bool m_toggleMap = true;
        private bool m_toggleMapIcons = true;
        private bool m_toggleMainQuests = true;

        [SerializeField]
        private List<StorePage> m_necroPageOrders = new List<StorePage>();

        private void OnEnable()
        {
            m_inputReader.UICycleTabsPerformedEvent += OnUICycleTabsPerformed;
            m_inputReader.UICycleSubTabsPerformedEvent += OnUICycleSubtabsPerformed;
            m_inputReader.UINavigatePerformedEvent += OnUINavigatePerformed;
            m_inputReader.UIClickPerformedEvent += OnUIClickPerformed;
            m_inputReader.UISubmitPerformedEvent += OnUISubmitPerformed;
            m_inputReader.UICancelPerformedEvent += OnUICancelPerformed;
            m_inputReader.UIToggleMapLegendEvent += OnUIToggleMapLegendEvent;
            m_inputReader.UIHoldToSkipPerformedEvent += OnUIHoldToSkipPerformed;

            m_storeNavigator.OnStoreTabClicked += OnStoreTabClicked;
        }

        private void OnDisable()
        {
            m_inputReader.UICycleTabsPerformedEvent -= OnUICycleTabsPerformed;
            m_inputReader.UICycleSubTabsPerformedEvent -= OnUICycleSubtabsPerformed;
            m_inputReader.UINavigatePerformedEvent -= OnUINavigatePerformed;
            m_inputReader.UIClickPerformedEvent -= OnUIClickPerformed;
            m_inputReader.UISubmitPerformedEvent -= OnUISubmitPerformed;
            m_inputReader.UICancelPerformedEvent -= OnUICancelPerformed;
            m_inputReader.UIHoldToSkipPerformedEvent -= OnUIHoldToSkipPerformed;

            m_storeNavigator.OnStoreTabClicked -= OnStoreTabClicked;

        }

        #region UI Input Callbacks
        private void OnUICancelPerformed()
        {
            m_toggleMap = true;
        }

        private void OnUISubmitPerformed()
        {
            if (BaseGameplaySystem.gamplayUIHandle.gameplayUIStateObserver.isInDialogue)
            {
                BaseGameplaySystem.gamplayUIHandle.ContinueDialogue();
            }
        }

        private void OnUIClickPerformed()
        {
            //Might not be cleanest solution but should handle banter continuing on click during UI controls issue
            if (BaseGameplaySystem.gamplayUIHandle.gameplayUIStateObserver.isInDialogue)
                BaseGameplaySystem.gamplayUIHandle.ContinueDialogue();

            if (IsFastTravelOpen())
                m_fastTravelIndex = GetFastTravelActiveTab();
            
            else if (m_storeNavigator.IsStoreOpen())
                m_necroTabIndex = (int)GetActiveStorePage();
        }

        private void OnUINavigatePerformed(Vector2 vector)
        {

        }

        private void OnUICycleTabsPerformed(float obj)
        {
            int direction = obj > 0 ? 1 : (obj < 0 ? -1 : 0);
            if (direction == 0) return;

            if (IsFastTravelOpen())
                HandleFastTravelNavigation(direction);

            else if (m_storeNavigator.IsStoreOpen())
                HandleNecroNavigation(direction);
        }
        private void OnUICycleSubtabsPerformed(float obj)
        {
            var currentNecroPage = (StorePage)m_necroTabIndex;

            if (obj > 0)
            {
                switch (currentNecroPage)
                {
                    case StorePage.Map:
                        m_toggleMapIcons = !m_toggleMapIcons;
                        UnderworldGameplaySystem.gameplayUIHandle.ToggleMapIconsVisibility(m_toggleMapIcons);
                        break;
                    case StorePage.Player:
                        break;
                    case StorePage.Items:
                        break;
                    case StorePage.Equipment:
                        break;
                    case StorePage.SoulSkills:
                        break;
                    case StorePage.CombatArts:
                        break;
                    case StorePage.Codex:
                        HandleCodexCallback(obj);
                        break;
                }
            }
            else if (obj < 0)
            {
                switch (currentNecroPage)
                {
                    case StorePage.Map:
                        UnderworldGameplaySystem.gameplayUIHandle.CycleLegendPage();
                        break;
                    case StorePage.Player:
                        break;
                    case StorePage.Items:
                        break;
                    case StorePage.Equipment:
                        break;
                    case StorePage.SoulSkills:
                        break;
                    case StorePage.CombatArts:
                        break;
                    case StorePage.Codex:
                        HandleCodexCallback(obj);
                        break;
                }
            }
        }
        private void OnUIToggleMapLegendEvent()
        {
            m_toggleMap = !m_toggleMap;
            UnderworldGameplaySystem.gameplayUIHandle.ToggleMapLegend(m_toggleMap);
        }
        private void OnUIHoldToSkipPerformed()
        {

        }
        #endregion

        #region Fast Travel Handling
        private void HandleFastTravelNavigation(int direction)
        {
            var toggles = BaseGameplaySystem.gamplayUIHandle.GetFastTravelLocationTabs();

            int totalCount = toggles.Count;
            if (totalCount <= 0) return;

            int nextIndex = m_fastTravelIndex;
            int checkedCount = 0;
            bool foundValidTab = false;

            while (checkedCount < totalCount)
            {
                nextIndex = (nextIndex + direction + totalCount) % totalCount;
                checkedCount++;

                if (toggles[nextIndex] != null && toggles[nextIndex].interactable)
                {
                    foundValidTab = true;
                    break;
                }
            }

            if (foundValidTab)
            {
                m_fastTravelIndex = nextIndex;
                BaseGameplaySystem.gamplayUIHandle.OnFastTravelTabChanged(m_fastTravelIndex);
            }
        }
        public int GetFastTravelActiveTab() => BaseGameplaySystem.gamplayUIHandle.GetFastTravelActiveTab();

        private bool IsFastTravelOpen() => BaseGameplaySystem.gamplayUIHandle.IsFastTravelOpen();
        #endregion

        #region Store Tab Index Handling
        private void OnStoreTabClicked(StorePage page) => m_necroTabIndex = (int)page;
        private StorePage GetActiveStorePage() => UnderworldGameplaySystem.gameplayUIHandle.GetActiveStorePage();

        private void OpenStoreAtPage(StorePage page)
        {
            m_storeNavigator.SetPage(page);
            m_storeNavigator.OpenPage();
        }

        private void HandleNecroNavigation(int direction)
        {
            int totalPages = m_necroPageOrders.Count;
            if (totalPages <= 0) return;

            m_necroTabIndex = (m_necroTabIndex + direction + totalPages) % totalPages;

            OpenStoreAtPage(m_necroPageOrders[m_necroTabIndex]);
        }


        #endregion

        #region Codex Navigation Handling
        private void HandleCodexCallback(float obj)
        {
            var currentCodexPage = m_storeNavigator.codexHandler.currentPage;

            //input for 'Z'
            if (obj < 0)
            {
                switch (currentCodexPage)
                {
                    case CodexPage.Characters:
                        break;
                    case CodexPage.ArmyTroops:
                        break;
                    case CodexPage.Bestiary:
                        break;
                    case CodexPage.Quests:
                        m_toggleMainQuests = !m_toggleMainQuests;
                        UnderworldGameplaySystem.gameplayUIHandle.ToggleCodexQuests(m_toggleMainQuests);
                        break;
                    case CodexPage.Locations:
                        break;
                    case CodexPage.Lore:
                        break;
                    case CodexPage.Tutorials:
                        break;
                }
            }

            //input for 'X'
            else if (obj > 0)
            {
                switch (currentCodexPage)
                {
                    case CodexPage.Characters:
                        break;
                    case CodexPage.ArmyTroops:
                        break;
                    case CodexPage.Bestiary:
                        break;
                    case CodexPage.Quests:
                        break;
                    case CodexPage.Locations:
                        break;
                    case CodexPage.Lore:
                        break;
                    case CodexPage.Tutorials:
                        break;
                }
            }
        }
        #endregion
    }
}

