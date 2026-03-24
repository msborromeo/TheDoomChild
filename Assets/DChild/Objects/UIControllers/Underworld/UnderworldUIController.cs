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
        private UnderworldUIStateObserver m_UIStateObserver;

        private void OnEnable()
        {
            m_inputReader.UICycleTabsPerformedEvent += OnUICycleTabsPerformed;
            m_inputReader.UICycleSubTabsPerformedEvent += OnUICycleSubtabsPerformed;
        }

        private void OnDisable()
        {
            m_inputReader.UICycleTabsPerformedEvent -= OnUICycleTabsPerformed;
            m_inputReader.UICycleSubTabsPerformedEvent -= OnUICycleSubtabsPerformed;
        }

        private void OnUICycleTabsPerformed(float obj)
        {
            switch (m_UIStateObserver.currentUnderworldUIState)
            {
                case UnderworldUIState.NecroMap:
                    if(obj > 0)
                    {
                        GameplayUIHandle.Instance.OpenStoreAtPage(StorePage.Player);
                    }
                    else if(obj < 0)
                    {
                        GameplayUIHandle.Instance.OpenStoreAtPage(StorePage.Codex);
                    }
                    break;
                case UnderworldUIState.NecroStats:
                    if (obj > 0)
                    {
                        GameplayUIHandle.Instance.OpenStoreAtPage(StorePage.Items);
                    }
                    else if (obj < 0)
                    {
                        GameplayUIHandle.Instance.OpenStoreAtPage(StorePage.Map);
                    }
                    break;
                case UnderworldUIState.NecroItems:
                    if (obj > 0)
                    {
                        GameplayUIHandle.Instance.OpenStoreAtPage(StorePage.Equipment);
                    }
                    else if (obj < 0)
                    {
                        GameplayUIHandle.Instance.OpenStoreAtPage(StorePage.Player);
                    }
                    break;
                case UnderworldUIState.NecroEquipment:
                    if (obj > 0)
                    {
                        GameplayUIHandle.Instance.OpenStoreAtPage(StorePage.SoulSkills);
                    }
                    else if (obj < 0)
                    {
                        GameplayUIHandle.Instance.OpenStoreAtPage(StorePage.Items);
                    }
                    break;
                case UnderworldUIState.NecroSoulSkills:
                    if (obj > 0)
                    {
                        GameplayUIHandle.Instance.OpenStoreAtPage(StorePage.CombatArts);
                    }
                    else if (obj < 0)
                    {
                        GameplayUIHandle.Instance.OpenStoreAtPage(StorePage.Equipment);
                    }
                    break;
                case UnderworldUIState.NecroCombatArts:
                    if (obj > 0)
                    {
                        GameplayUIHandle.Instance.OpenStoreAtPage(StorePage.Codex);
                    }
                    else if (obj < 0)
                    {
                        GameplayUIHandle.Instance.OpenStoreAtPage(StorePage.SoulSkills);
                    }
                    break;
                case UnderworldUIState.NecroCodex:
                    if (obj > 0)
                    {
                        GameplayUIHandle.Instance.OpenStoreAtPage(StorePage.Map);
                    }
                    else if (obj < 0)
                    {
                        GameplayUIHandle.Instance.OpenStoreAtPage(StorePage.CombatArts);
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
            throw new NotImplementedException();
        }
    }
}

