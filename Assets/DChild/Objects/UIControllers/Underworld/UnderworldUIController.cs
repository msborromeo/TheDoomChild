using DChild.Gameplay.Systems;
using DChild.Inputs;
using DChild.UI;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.UI.Controller
{
    public class UnderworldUIController : MonoBehaviour
    {
        [SerializeField]
        private InputReader m_inputReader;
        [SerializeField]
        private StoreNavigator m_storeNavigator;

        [SerializeField]
        private UnderworldUIStateObserver m_UIStateObserver;

        private void OnEnable()
        {
            m_inputReader.UICycleTabsPerformedEvent += OnUICycleTabsPerformed;
            m_inputReader.UICycleSubTabsPerformedEvent += OnUICycleSubtabsPerformed;
            m_inputReader.UINavigatePerformedEvent += OnUINavigatePerformed;
        }

        private void OnDisable()
        {
            m_inputReader.UICycleTabsPerformedEvent -= OnUICycleTabsPerformed;
            m_inputReader.UICycleSubTabsPerformedEvent -= OnUICycleSubtabsPerformed;
            m_inputReader.UINavigatePerformedEvent -= OnUINavigatePerformed;
        }

        private void OnUINavigatePerformed(Vector2 vector)
        {
            throw new NotImplementedException();
        }

        private void OnUICycleTabsPerformed(float obj)
        {
            switch (m_UIStateObserver.currentUnderworldUIState)
            {
                case UnderworldUIState.NecroMap:
                    if (obj > 0)
                    {
                        m_storeNavigator.SetPage(StorePage.Player);
                        m_storeNavigator.OpenPage();
                    }
                    else if (obj < 0)
                    {
                        m_storeNavigator.SetPage(StorePage.Codex);
                        m_storeNavigator.OpenPage();
                    }
                    break;

                case UnderworldUIState.NecroStats:
                    if (obj > 0)
                    {
                        m_storeNavigator.SetPage(StorePage.Items);
                        m_storeNavigator.OpenPage();
                    }
                    else if (obj < 0)
                    {
                        m_storeNavigator.SetPage(StorePage.Map);
                        m_storeNavigator.OpenPage();
                    }
                    break;

                case UnderworldUIState.NecroItems:
                    if (obj > 0)
                    {
                        m_storeNavigator.SetPage(StorePage.Equipment);
                        m_storeNavigator.OpenPage();
                    }
                    else if (obj < 0)
                    {
                        m_storeNavigator.SetPage(StorePage.Player);
                        m_storeNavigator.OpenPage();
                    }
                    break;

                case UnderworldUIState.NecroEquipment:
                    if (obj > 0)
                    {
                        m_storeNavigator.SetPage(StorePage.SoulSkills);
                        m_storeNavigator.OpenPage();
                    }
                    else if (obj < 0)
                    {
                        m_storeNavigator.SetPage(StorePage.Items);
                        m_storeNavigator.OpenPage();
                    }
                    break;

                case UnderworldUIState.NecroSoulSkills:
                    if (obj > 0)
                    {
                        m_storeNavigator.SetPage(StorePage.CombatArts);
                        m_storeNavigator.OpenPage();
                    }
                    else if (obj < 0)
                    {
                        m_storeNavigator.SetPage(StorePage.Equipment);
                        m_storeNavigator.OpenPage();
                    }
                    break;

                case UnderworldUIState.NecroCombatArts:
                    if (obj > 0)
                    {
                        m_storeNavigator.SetPage(StorePage.Codex);
                        m_storeNavigator.OpenPage();
                    }
                    else if (obj < 0)
                    {
                        m_storeNavigator.SetPage(StorePage.SoulSkills);
                        m_storeNavigator.OpenPage();
                    }
                    break;

                case UnderworldUIState.NecroCodex:
                    if (obj > 0)
                    {
                        m_storeNavigator.SetPage(StorePage.Map);
                        m_storeNavigator.OpenPage();
                    }
                    else if (obj < 0)
                    {
                        m_storeNavigator.SetPage(StorePage.CombatArts);
                        m_storeNavigator.OpenPage();
                    }
                    break;

                case UnderworldUIState.MordenElevator:
                    break;
                case UnderworldUIState.Shop:
                    break;
            }
        }

        private void OnUICycleSubtabsPerformed(float obj)
        {
            if(m_UIStateObserver.currentUnderworldUIState == UnderworldUIState.NecroItems)
            {
                //Handle filter in inventory
                if(obj > 0)
                {
                    
                }
                else if(obj < 0)
                {

                }
            }
        }
    }
}

