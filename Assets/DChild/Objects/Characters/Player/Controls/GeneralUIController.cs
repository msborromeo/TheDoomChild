using DChild.Gameplay.Systems;
using DChild.Inputs;
using DChild.Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Players.Modules
{
    public class GeneralUIController : MonoBehaviour
    {
        [SerializeField]
        private InputReader m_inputReader;

        private void OnEnable()
        {
            m_inputReader.UIResumeStartedEvent += OnResumeStartedInput;
        }

        private void OnDisable()
        {
            m_inputReader.UIResumeStartedEvent -= OnResumeStartedInput;
        }

        private void OnResumeStartedInput()
        {
           
        }

        public void SetInputModeToUI()
        {
            m_inputReader.SetInputModeToUI();
        }

        public void SetInputModeToCurrentGameplay()
        {
            switch(BaseGameplaySystem.GetCurrentWorldType())
            {
                case WorldType.Underworld:
                    m_inputReader.SetInputModeToUnderworldGameplay();
                    break;
                case WorldType.Overworld:
                    m_inputReader.SetInputModeTOverworldGameplay();
                    break;
                case WorldType.ArmyBattle:
                    m_inputReader.SetInputModeToArmyBattleGameplay();
                    break;
            }
            
        }
    }
}

