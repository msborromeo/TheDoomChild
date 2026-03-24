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

        [SerializeField, BoxGroup("Necro Toggles")]
        private UIToggle m_mapToggle;
        [SerializeField, BoxGroup("Necro Toggles")]
        private UIToggle m_playerToggle;
        [SerializeField, BoxGroup("Necro Toggles")]
        private UIToggle m_inventoryToggle;
        [SerializeField, BoxGroup("Necro Toggles")]
        private UIToggle m_equipmentToggle;
        [SerializeField, BoxGroup("Necro Toggles")]
        private UIToggle m_soulSkillsToggle;
        [SerializeField, BoxGroup("Necro Toggles")]
        private UIToggle m_combatArtsToggle;
        [SerializeField, BoxGroup("Necro Toggles")]
        private UIToggle m_codexToggle;

        [SerializeField]
        private UnderworldUIStateObserver m_UIStateObserver;

        private void OnEnable()
        {
            m_inputReader.UICycleTabsPerformedEvent += OnUICycleTabsPerformed;
            m_inputReader.UICycleSubTabsPerformedEvent += OnUICycleSubtabsPerformed;
            m_UIStateObserver.UnderworldUIStateChanged += OnUIStateChanged;
        }

        private void OnDisable()
        {
            m_inputReader.UICycleTabsPerformedEvent -= OnUICycleTabsPerformed;
            m_inputReader.UICycleSubTabsPerformedEvent -= OnUICycleSubtabsPerformed;
            m_UIStateObserver.UnderworldUIStateChanged -= OnUIStateChanged;
        }

        private void OnUICycleTabsPerformed(float obj)
        {
            switch (m_UIStateObserver.currentUnderworldUIState)
            {
                case UnderworldUIState.NecroMap:
                    if(obj > 0)
                    {
                        m_playerToggle.Select();
                    }
                    else if(obj < 0)
                    {
                        m_codexToggle.Select();
                    }
                    break;
                case UnderworldUIState.NecroStats:
                    if (obj > 0)
                    {
                        m_inventoryToggle.Select();
                    }
                    else if (obj < 0)
                    {
                        m_mapToggle.Select();
                    }
                    break;
                case UnderworldUIState.NecroItems:
                    if (obj > 0)
                    {
                        m_equipmentToggle.Select();
                    }
                    else if (obj < 0)
                    {
                        m_playerToggle.Select();
                    }
                    break;
                case UnderworldUIState.NecroEquipment:
                    if (obj > 0)
                    {
                        m_soulSkillsToggle.Select();
                    }
                    else if (obj < 0)
                    {
                        m_inventoryToggle.Select();
                    }
                    break;
                case UnderworldUIState.NecroSoulSkills:
                    if (obj > 0)
                    {
                        m_combatArtsToggle.Select();
                    }
                    else if (obj < 0)
                    {
                        m_equipmentToggle.Select();
                    }
                    break;
                case UnderworldUIState.NecroCombatArts:
                    if (obj > 0)
                    {
                        m_codexToggle.Select();
                    }
                    else if (obj < 0)
                    {
                        m_soulSkillsToggle.Select();
                    }
                    break;
                case UnderworldUIState.NecroCodex:
                    if (obj > 0)
                    {
                        m_mapToggle.Select();
                    }
                    else if (obj < 0)
                    {
                        m_combatArtsToggle.Select();
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

        private void OnUIStateChanged(UnderworldUIState state)
        {
            if(state == UnderworldUIState.GameplayHUD)
            {
                m_inputReader.SetInputModeToUnderworldGameplay();
            }
            else
            {
                m_inputReader.SetInputModeToUI();
            }
        }
    }
}

