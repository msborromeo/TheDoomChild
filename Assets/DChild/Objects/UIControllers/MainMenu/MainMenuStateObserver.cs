using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace DChild.Gameplay.UI.Controller
{
    public class MainMenuStateObserver : MonoBehaviour
    {
        [SerializeField, ReadOnly(true)]
        private MainMenuState m_currentMainMenuState;
        public MainMenuState currentMainMenuState => m_currentMainMenuState;

        public event Action<MainMenuState> MainMenuStateChanged;
         
        public void SetCurrentMainMenuState(int mainMenuState)
        {
            m_currentMainMenuState = (MainMenuState)mainMenuState;
            MainMenuStateChanged?.Invoke(m_currentMainMenuState);
        }
    }
}

