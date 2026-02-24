using DChild.Inputs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DChild.Gameplay.UI.Controller
{
    public class MainMenuUIController : MonoBehaviour
    {
        [SerializeField]
        private InputReader m_inputReader;
        public InputReader inputReader => m_inputReader;

        private void Awake()
        {
            m_inputReader.SetInputModeToUI();
        }

        private void OnEnable()
        {
            m_inputReader.UINavigatePerformedEvent += OnUINavigatePerformed;
            m_inputReader.UICycleTabsPerformedEvent += OnCycleTabsPerformed;
            m_inputReader.UIDeleteSaveEvent += OnDeleteSavePerformed;
        }

        private void OnDisable()
        {
            m_inputReader.UINavigatePerformedEvent -= OnUINavigatePerformed;
            m_inputReader.UICycleTabsPerformedEvent -= OnCycleTabsPerformed;
            m_inputReader.UIDeleteSaveEvent -= OnDeleteSavePerformed;
        }

        #region Controller Input Functions
        private void OnUINavigatePerformed(Vector2 vector)
        {
            
        }

        private void OnCycleTabsPerformed(float obj)
        {
            
        }


        private void OnDeleteSavePerformed()
        {
            
        }

        #endregion

        #region Utility
        public void SetCurrentSelectedButton(GameObject button)
        {
            EventSystem.current.SetSelectedGameObject(button);
        }
        #endregion
    }
}

