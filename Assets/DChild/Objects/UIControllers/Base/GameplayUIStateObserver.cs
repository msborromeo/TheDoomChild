using DChild.Gameplay.Systems;
using DChild.Gameplay.UI.Controller;
using DChild.Inputs;
using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager.Listeners;
using PixelCrushers;
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
        private GameplayUIState m_currentUIState;
        public GameplayUIState currentUnderworldUIState => m_currentUIState;

        public event Action<GameplayUIState> GameplayUIStateChanged;

        [SerializeField]
        private List<DoozySignalName> m_doozyUISignalNames = new List<DoozySignalName>();
        [SerializeField]
        private List<DoozySignalName> m_doozyGameplaySignalNames = new List<DoozySignalName>();

        [SerializeField, ReadOnly]
        private List<SignalReceiver> m_UISignalRecievers = new List<SignalReceiver>();
        [SerializeField, ReadOnly]
        private List<SignalReceiver> m_GameplaySignalRecievers = new List<SignalReceiver>();

        private void Awake()
        {
            //initialize number of signal recievers for UI and Gameplay Signals
            InitializeSignalReceivers(m_doozyUISignalNames, m_UISignalRecievers);
            InitializeSignalReceivers(m_doozyGameplaySignalNames, m_GameplaySignalRecievers);

        }

        private void OnEnable()
        {
            //Subscribe each signal receiver to corresponding function
            for(int i = 0; i < m_UISignalRecievers.Count; i++)
            {
                m_UISignalRecievers[i].onSignal += OnUISignalReceived;
            }

            for(int i = 0; i < m_GameplaySignalRecievers.Count; i++)
            {
                m_GameplaySignalRecievers[i].onSignal += OnGameplaySignalReceived;
            }

        }

        private void OnGameplaySignalReceived(Signal arg0)
        {
            SetCurrentUnderworldUIState(GameplayUIState.GameplayHUD);
        }

        private void OnUISignalReceived(Signal signal)
        {
            SetCurrentUnderworldUIState(GameplayUIState.InteractableUI);
        }

        private void OnDisable()
        {
            //unregister all recievers
            DisconnectSignalReceivers(m_UISignalRecievers);
            DisconnectSignalReceivers(m_GameplaySignalRecievers);
        }

        public void SetCurrentUnderworldUIState(GameplayUIState gameplayUIState)
        {
            m_currentUIState = gameplayUIState;

            var currentWorldType = BaseGameplaySystem.GetCurrentWorldType();

            GameplayUIStateChanged?.Invoke(m_currentUIState);
        }

        private void InitializeSignalReceivers(List<DoozySignalName> doozySignals, List<SignalReceiver> signalRecievers)
        {
            signalRecievers.Clear();
            for (int i = 0; i < doozySignals.Count; i++)
            {
                SignalReceiver signalReceiver = new SignalReceiver();

                //Set category and name for connection, set stream connection to stream ID to connect using category and name
                SignalStream.Get(doozySignals[i].categoryName, doozySignals[i].name).ConnectReceiver(signalReceiver);
                signalReceiver.streamConnection = StreamConnection.StreamId;
                //signalReceiver.stream.SetCategoryAndName(doozySignals[i].categoryName, doozySignals[i].name);

                //Add to reciever list
                signalRecievers.Add(signalReceiver);

                //connect reciever to stream 
                //signalReceiver.Connect();
            }
        }

        private void DisconnectSignalReceivers(List<SignalReceiver> signalReceivers)
        {
            for(int i = 0;i < signalReceivers.Count; i++)
            {
                signalReceivers[i].Disconnect();
            }
        }

        [Serializable]
        private struct DoozySignalName
        {
            [SerializeField]
            private string m_categoryName;
            [SerializeField]
            string m_name;

            public string categoryName => m_categoryName;
            public string name => m_name;
        }
    }
}

