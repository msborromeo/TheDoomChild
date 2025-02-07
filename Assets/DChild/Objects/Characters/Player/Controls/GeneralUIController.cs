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
        [SerializeField]
        private PauseHandle m_pauseHandle;

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
            m_pauseHandle.ResumeGame();
            if(GameplaySystem.GetCurrentWorldType() == WorldType.Underworld)
            {
                m_inputReader.SetInputModeToUnderworldGameplay();
            }

            if (GameplaySystem.GetCurrentWorldType() == WorldType.Overworld)
            {
                m_inputReader.SetInputModeTOverworldGameplay();
            }

            if (GameplaySystem.GetCurrentWorldType() == WorldType.ArmyBattle)
            {
                m_inputReader.SetInputModeToArmyBattleGameplay();
            }
        }
    }
}

