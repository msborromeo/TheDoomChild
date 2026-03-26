using DChild.Gameplay.Systems;
using DChild.Gameplay.UI.Controller;
using DChild.Inputs;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DChild.UI
{
    public class GameplayUIStateObserver : MonoBehaviour
    {
        [SerializeField]
        private GameplayUIState m_currentUnderworldUIState;
        public GameplayUIState currentUnderworldUIState => m_currentUnderworldUIState;

        [SerializeField]
        private InputReader m_inputReader;

        private PlayerInput m_playerInput;

        public event Action<GameplayUIState> UnderworldUIStateChanged;

        public void SetCurrentUnderworldUIState(int underworldUIState)
        {
            m_currentUnderworldUIState = (GameplayUIState)underworldUIState;

            var currentWorldType = BaseGameplaySystem.GetCurrentWorldType();

            if(m_currentUnderworldUIState == GameplayUIState.GameplayHUD)
            {
                if(currentWorldType == WorldType.Underworld)
                {
                    m_inputReader.SetInputModeToUnderworldGameplay();
                    m_playerInput.SwitchCurrentActionMap("Underworld");
                }
                else if(currentWorldType == WorldType.Overworld)
                {
                    m_inputReader.SetInputModeTOverworldGameplay();
                    m_playerInput.SwitchCurrentActionMap("Overworld");
                }
                else if(currentWorldType == WorldType.ArmyBattle)
                {
                    m_inputReader.SetInputModeToArmyBattleGameplay();
                    m_playerInput.SwitchCurrentActionMap("Army Battle");
                }

            }
            else
            {
                m_inputReader.SetInputModeToUI();
                m_playerInput.SwitchCurrentActionMap("UI");
            }

            UnderworldUIStateChanged?.Invoke(m_currentUnderworldUIState);
        }

        public void SetCurrentPlayerInput(PlayerInput playerInput)
        {
            m_playerInput = playerInput;
        }
    }
}

