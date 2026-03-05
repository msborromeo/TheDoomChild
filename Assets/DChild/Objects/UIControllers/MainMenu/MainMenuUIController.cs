using DChild.Inputs;
using DChild.Menu;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static DChild.Gameplay.Environment.ComplexIdlingCreature;

namespace DChild.Gameplay.UI.Controller
{
    public class MainMenuUIController : SerializedMonoBehaviour
    {
        [SerializeField]
        private InputReader m_inputReader;
        public InputReader inputReader => m_inputReader;
        [SerializeField, TabGroup("Game Slot Selection")]
        private UIButton m_nextSlotButton;
        [SerializeField, TabGroup("Game Slot Selection")]
        private UIButton m_previousSlotButton;
        [SerializeField, TabGroup("Game Slot Selection")]
        private CampaignSelect m_campaignSelect;
        [SerializeField, TabGroup("Game Slot Selection")]
        private CampaignHandler m_campaignHandler;
        [SerializeField, TabGroup("Game Slot Selection")]
        private GameObject m_newGameButton;
        [SerializeField, TabGroup("Game Slot Selection")]
        private GameObject m_loadGameButton;
        [SerializeField, TabGroup("Game Slot Selection")]
        private UIView m_slotView;

        [SerializeField]
        private MainMenuStateObserver m_mainMenuStateObserver;

        private void Awake()
        {
            m_inputReader.SetInputModeToUI();
        }

        private void OnEnable()
        {
            m_inputReader.UINavigatePerformedEvent += OnUINavigatePerformed;
            m_inputReader.UICycleTabsPerformedEvent += OnCycleTabsPerformed;
            m_inputReader.UIDeleteSaveEvent += OnDeleteSavePerformed;
            m_inputReader.UIClickPerformedEvent += OnClickPerformed;

            m_mainMenuStateObserver.MainMenuStateChanged += OnMainMenuStateChange;
            m_campaignSelect.CampaignSelected += OnCampaignSlotSelected;
        }

        private void OnDisable()
        {
            m_inputReader.UINavigatePerformedEvent -= OnUINavigatePerformed;
            m_inputReader.UICycleTabsPerformedEvent -= OnCycleTabsPerformed;
            m_inputReader.UIDeleteSaveEvent -= OnDeleteSavePerformed;
            m_inputReader.UIClickPerformedEvent -= OnClickPerformed;

            m_mainMenuStateObserver.MainMenuStateChanged -= OnMainMenuStateChange;
            m_campaignSelect.CampaignSelected -= OnCampaignSlotSelected;
        }

        #region Controller Input Functions
        private void OnUINavigatePerformed(Vector2 vector)
        {
            
        }

        private void OnCycleTabsPerformed(float obj)
        {
            if (m_mainMenuStateObserver.currentMainMenuState != MainMenuState.SlotSelection)
                return;

            if(obj > 0f)
            {
                m_nextSlotButton.OnSubmit(null);
            }
            else if(obj < 0f)
            {
                m_previousSlotButton.OnSubmit(null);
            }
            m_slotView.Hide();
            m_campaignSelect.SendCampaignSelectedEvent();
        }

        private void OnClickPerformed()
        {

        }

        private void OnDeleteSavePerformed()
        {
            if (m_mainMenuStateObserver.currentMainMenuState != MainMenuState.SlotSelection)
                return;

            m_campaignHandler.RequestReset();
        }

        #endregion

        #region Utility
        public void SetCurrentSelectedButton(GameObject button)
        {
            EventSystem.current.SetSelectedGameObject(button);
        }

        private void OnMainMenuStateChange(MainMenuState menuState)
        {

        }

        private void OnCampaignSlotSelected(object sender, SelectedCampaignSlotEventArgs eventArgs)
        {
            if(eventArgs.isNewGame)
            {
                SetCurrentSelectedButton(m_newGameButton);
            }
            else
            {
                SetCurrentSelectedButton(m_loadGameButton);
            }
        }
        #endregion
    }
}

