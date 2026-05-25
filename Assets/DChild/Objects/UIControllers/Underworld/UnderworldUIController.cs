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

        [SerializeField, MinValue(0)]
        private int m_necroTabIndex = 0;
        private bool m_toggleMap = true;
        private bool m_toggleMapIcons = true;
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
        }

        private void OnDisable()
        {
            m_inputReader.UICycleTabsPerformedEvent -= OnUICycleTabsPerformed;
            m_inputReader.UICycleSubTabsPerformedEvent -= OnUICycleSubtabsPerformed;
            m_inputReader.UINavigatePerformedEvent -= OnUINavigatePerformed;
            m_inputReader.UIClickPerformedEvent -= OnUIClickPerformed;
            m_inputReader.UISubmitPerformedEvent -= OnUISubmitPerformed;
            m_inputReader.UICancelPerformedEvent -= OnUICancelPerformed;
        }

        private void OnUICancelPerformed()
        {
            m_necroTabIndex = 0;
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
            {
                BaseGameplaySystem.gamplayUIHandle.ContinueDialogue();
            }
        }

        private void OnUINavigatePerformed(Vector2 vector)
        {
            
        }

        private void OnUICycleTabsPerformed(float obj)
        {
            if (obj > 0)
            {
                //to achieve cycle back on end
                if (m_necroTabIndex == m_necroPageOrders.Count - 1)
                {
                    m_storeNavigator.SetPage(m_necroPageOrders[0]);
                    m_storeNavigator.OpenPage();
                    m_necroTabIndex = 0;
                    return;
                }

                m_necroTabIndex += 1;
                m_storeNavigator.SetPage(m_necroPageOrders[m_necroTabIndex]);
                m_storeNavigator.OpenPage();
            }
            else if (obj < 0)
            {
                if (m_necroTabIndex == 0)
                {
                    m_storeNavigator.SetPage(m_necroPageOrders[m_necroPageOrders.Count - 1]);
                    m_storeNavigator.OpenPage();
                    m_necroTabIndex = m_necroPageOrders.Count - 1;
                    return;
                }

                m_necroTabIndex -= 1;
                m_storeNavigator.SetPage(m_necroPageOrders[m_necroTabIndex]);
                m_storeNavigator.OpenPage();
            }
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
                        break;
                    case StorePage.Bestiary:
                        break;
                    default:
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
                        break;
                    case StorePage.Bestiary:
                        break;
                    default:
                        break;
                }
            }
        }
        private void OnUIToggleMapLegendEvent()
        {
            m_toggleMap = !m_toggleMap;
            UnderworldGameplaySystem.gameplayUIHandle.ToggleMapLegend(m_toggleMap);
        }
    }
}

