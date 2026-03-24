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
    public class UnderworldUIStateObserver : MonoBehaviour
    {
        [SerializeField]
        private UnderworldUIState m_currentUnderworldUIState;
        public UnderworldUIState currentUnderworldUIState => m_currentUnderworldUIState;

        [SerializeField]
        private InputReader m_inputReader;
        [SerializeField]
        private PlayerInput m_playerInput;

        public event Action<UnderworldUIState> UnderworldUIStateChanged;

        public void SetCurrentUnderworldUIState(int underworldUIState)
        {
            m_currentUnderworldUIState = (UnderworldUIState)underworldUIState;

            if(m_currentUnderworldUIState == UnderworldUIState.GameplayHUD)
            {
                m_inputReader.SetInputModeToUnderworldGameplay();
                m_playerInput.SwitchCurrentActionMap("Underworld");
            }
            else
            {
                m_inputReader.SetInputModeToUI();
                m_playerInput.SwitchCurrentActionMap("UI");
            }

            UnderworldUIStateChanged?.Invoke(m_currentUnderworldUIState);
        }
    }
}

