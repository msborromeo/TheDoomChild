using DChild.Gameplay.Systems;
using DChild.Gameplay.UI.Controller;
using DChild.Inputs;
using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager.Listeners;
using PixelCrushers;
using PixelCrushers.DialogueSystem.SequencerCommands;
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
        [SerializeField]
        private List<DoozySignalName> m_doozyCinematicSignalNames = new List<DoozySignalName>();

        [SerializeField, ReadOnly]
        private List<SignalReceiver> m_UISignalReceivers = new List<SignalReceiver>();
        [SerializeField, ReadOnly]
        private List<SignalReceiver> m_GameplaySignalReceivers = new List<SignalReceiver>();
        [SerializeField, ReadOnly]
        private List<SignalReceiver> m_CinematicSignalReceivers = new List<SignalReceiver>();

        //Specific conditions to check
        [SerializeField, ReadOnly]
        private bool m_isInDialogue;
        public bool isInDialogue => m_isInDialogue;

        private void Awake()
        {
            //initialize number of signal receivers for UI and Gameplay Signals
            InitializeSignalReceivers(m_doozyUISignalNames, m_UISignalReceivers);
            InitializeSignalReceivers(m_doozyGameplaySignalNames, m_GameplaySignalReceivers);
            InitializeSignalReceivers(m_doozyCinematicSignalNames, m_CinematicSignalReceivers);

        }

        private void OnEnable()
        {
            //Subscribe each signal receiver to corresponding function
            for (int i = 0; i < m_UISignalReceivers.Count; i++)
            {
                m_UISignalReceivers[i].onSignal += OnUISignalReceived;
            }

            for (int i = 0; i < m_GameplaySignalReceivers.Count; i++)
            {
                m_GameplaySignalReceivers[i].onSignal += OnGameplaySignalReceived;
            }

            for (int i = 0; i < m_CinematicSignalReceivers.Count; i++)
            {
                m_CinematicSignalReceivers[i].onSignal += OnCinematicSignalReceived;
            }
        }

        private void OnCinematicSignalReceived(Signal signal)
        {
            //guard for exiting cinematic mode in case it doesn't return to no window at cinematic end
            if (signal.stream.category == "Cinematic" && signal.stream.name == "Toggle")
            {
                signal.TryGetValue(out bool value);
                if (value == false)
                {
                    return;
                }
            }

            if (signal.stream.category == "Cinematic" && signal.stream.name == "Bars")
            {
                signal.TryGetValue(out bool value);
                if (value == false)
                {
                    return;
                }
            }

            SetCurrentUIState(GameplayUIState.Cinematic);
            Debug.Log($"Received signal: \nCategory: {signal.stream.category}\nName: {signal.stream.name}");
        }

        private void OnGameplaySignalReceived(Signal signal)
        {
            SetCurrentUIState(GameplayUIState.GameplayHUD);
            Debug.Log($"Received signal: \nCategory: {signal.stream.category}\nName: {signal.stream.name}");
        }

        private void OnUISignalReceived(Signal signal)
        {
            //guard for exiting dialogue in case it doesn't return to no window
            if (signal.stream.category == "Dialogue" && signal.stream.name == "Toggle")
            {
                signal.TryGetValue(out bool value);
                if (value == false)
                {
                    m_isInDialogue = false;
                    return;
                }
                else
                {
                    m_isInDialogue = true;
                }
            }

            SetCurrentUIState(GameplayUIState.InteractableUI);
            Debug.Log($"Received signal: \nCategory: {signal.stream.category}\nName: {signal.stream.name}");
        }

        private void OnDisable()
        {
            //unregister all recievers
            DisconnectSignalReceivers(m_UISignalReceivers);
            DisconnectSignalReceivers(m_GameplaySignalReceivers);
            DisconnectSignalReceivers(m_CinematicSignalReceivers);
        }

        public void SetCurrentUIState(GameplayUIState gameplayUIState)
        {
            m_currentUIState = gameplayUIState;

            GameplayUIStateChanged?.Invoke(m_currentUIState);
            Debug.Log("Changed UI State to: " + m_currentUIState);
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

                //Add to reciever list
                signalRecievers.Add(signalReceiver);
            }
        }

        private void DisconnectSignalReceivers(List<SignalReceiver> signalReceivers)
        {
            for (int i = 0; i < signalReceivers.Count; i++)
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

