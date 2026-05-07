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
        [SerializeField]
        private bool m_enableControlsInCinematic = false;
        public bool enableControlsInCinematic { get { return m_enableControlsInCinematic;  }
                                            set { m_enableControlsInCinematic = value; }
        }

        [SerializeField, ReadOnly()]
        private string m_currentActionMap;

        private PlayerInput m_playerInput;

        [SerializeField, BoxGroup("EDITOR ONLY")]
        private bool m_isEditorOnly;
        [SerializeField, BoxGroup("EDITOR ONLY")]
        private PlayerInput m_editorOnlyPlayerInput;

        private void Start()
        {
            //Hacky fix because there is no base gameplay system to
            //handle ui states when there is no Scene_System
            // This assumes testing in an underworld scene
            if (m_isEditorOnly)
            {
                m_playerInput = m_editorOnlyPlayerInput;

                m_inputReader.SetInputModeToUnderworldGameplay();
                m_playerInput.SwitchCurrentActionMap("Underworld");
                m_currentActionMap = "Underworld";
            }
        }
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
            switch (state)
            {
                case GameplayUIState.GameplayHUD:
                    SetInputToGameplay();
                    break;
                case GameplayUIState.InteractableUI:
                    SetInputToUI();            
                    break;
                case GameplayUIState.Cinematic:
                    if (m_enableControlsInCinematic)
                        SetInputToGameplay();
                    else
                        SetInputToUI();
                    break;
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
                        m_inputReader.SetInputModeToOverworldGameplay();
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

        public void DisableInput()
        {
            m_playerInput.enabled = false;
        }

        public void EnableInput()
        {
            m_playerInput.enabled = true;
        }

        public void SetCurrentPlayerInput(PlayerInput playerInput)
        {
            m_playerInput = playerInput;
        }
    }
}

