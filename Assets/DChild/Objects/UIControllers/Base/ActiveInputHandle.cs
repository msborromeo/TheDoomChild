using DChild.Gameplay.Systems;
using DChild.UI;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DChild.Inputs
{
    public class ActiveInputHandle : MonoBehaviour
    {
        [SerializeField]
        private InputReader m_inputReader;
        [SerializeField]
        private GameplayUIStateObserver m_gameplayUIStateObserver;

        [SerializeField, ReadOnly()]
        private string m_currentActionMap;

        private PlayerInput m_playerInput;

        private void OnEnable()
        {
            m_gameplayUIStateObserver.GameplayUIStateChanged += OnUIStateChanged;
        }

        private void OnDisable()
        {
            m_gameplayUIStateObserver.GameplayUIStateChanged -= OnUIStateChanged;
        }

        private void OnUIStateChanged(GameplayUIState state)
        {
            if (state == GameplayUIState.GameplayHUD)
            {
                SetInputToGameplay();
            }
            else
            {
                SetInputToUI();
            }
        }

        public void SetInputToGameplay()
        {
            var currentWorldType = BaseGameplaySystem.GetCurrentWorldType();

            switch (currentWorldType)
            {
                case WorldType.Underworld:
                    {
                        m_inputReader.SetInputModeToUnderworldGameplay();
                        m_playerInput.SwitchCurrentActionMap("Underworld");
                        m_currentActionMap = "Underworld";
                    }
                    break;
                case WorldType.Overworld:
                    {
                        m_inputReader.SetInputModeTOverworldGameplay();
                        m_playerInput.SwitchCurrentActionMap("Overworld");
                        m_currentActionMap = "Overworld";
                    }
                    break;
                case WorldType.ArmyBattle:
                    {
                        m_inputReader.SetInputModeToArmyBattleGameplay();
                        m_playerInput.SwitchCurrentActionMap("Army Battle");
                        m_currentActionMap = "Army Battle";
                    }
                    break;
            }
        }

        public void SetInputToUI()
        {
            m_inputReader.SetInputModeToUI();
            m_playerInput.SwitchCurrentActionMap("UI");
            m_currentActionMap = "UI";
        }

        public void SetCurrentPlayerInput(PlayerInput playerInput)
        {
            m_playerInput = playerInput;
        }
    }
}

