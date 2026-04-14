using DChild.Gameplay.Environment;
using DChild.Gameplay.FastTravel;
using DChild.Gameplay.LevelFinish.UI;
using DChild.Gameplay.Systems.Serialization;
using DChild.Gameplay.UI;
using DChild.Gameplay.UI.Alerts;
using DChild.Menu;
using DChild.UI;
using DChildDebug.Cutscene;
using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using Holysoft.Event;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;
using DLocation = DChild.Gameplay.Environment.Location;

namespace DChild.Gameplay.Systems
{
    public class BaseGameplayUIHandle : MonoBehaviour, IGameplaySystemModule, IGameplayInitializable
    {
        public static BaseGameplayUIHandle Instance { get; private set; }

        [SerializeField, FoldoutGroup("Signals")]
        private SignalSender m_cinemaSignal;
        [SerializeField, FoldoutGroup("Signals")]
        private SignalSender m_fastTravelSignal;
        [SerializeField, FoldoutGroup("Signals")]
        private SignalSender m_gameOverSignal;
        [SerializeField, FoldoutGroup("Signals/Confirmation")]
        private SignalSender m_confirmationWindowSignal;
        [SerializeField, FoldoutGroup("Signals/Confirmation")]
        private SignalSender m_requestTeleportSignal;
        [SerializeField, FoldoutGroup("Signals/Confirmation")]
        private SignalSender m_confirmRecruitmentSignal;
        [SerializeField, FoldoutGroup("Signals")]
        private SignalSender m_cinematicBarsSignal;
        [SerializeField, FoldoutGroup("Signals")]
        private SignalSender m_levelFinishedSignal;
        [SerializeField, FoldoutGroup("Signals/Game Pause")]
        private SignalSender m_gamePauseSignal;
        [SerializeField, FoldoutGroup("Signals/Game Pause")]
        private GameObject m_gamePause;


        [SerializeField]
        private ConfirmationHandler m_confirmationWindow;
        [SerializeField]
        private UIAlertManager m_uiAlertManager;


        [SerializeField]
        private UIContainer m_skippableUI;
        [SerializeField]
        private UIContainer m_fadeUI;

        [SerializeField]
        private FastTravelUIManager m_fastTravelUI;
        [SerializeField]
        private CinematicVideoHandle m_cinematicVideoHandle;
        [SerializeField]
        private UIView m_cinematicBars;
        [SerializeField]
        private LevelFinishUI m_levelFinish;
        [SerializeField]
        private SequenceSkipHandle m_skipHandle;
        [SerializeField]
        private CharacterRecruitmentUI m_characterRecruitmentUI;
        [SerializeField]
        private DChildStandardUIContinueButtonFastForward m_continueButtonFastForward;


        [SerializeField]
        private PauseGameGuard m_pauseGameGuard;
        [SerializeField]
        private SignalSender m_backSignal;
        [SerializeField]
        private GameplayUIStateObserver m_gameplayUIStateObserver;
        [SerializeField, FoldoutGroup("Signals")]
        private SignalSender m_continueDialogueSignal;

        public UIAlertManager uiAlertManager => m_uiAlertManager;

        public void ContinueDialogue()
        {
            m_continueButtonFastForward.OnFastForward();
        }

        public void ToggleCinematicMode(bool on)
        {
            if (on == true)
            {
                m_gameplayUIStateObserver.SetCurrentUnderworldUIState(16); //set UI mode to cinematic to prevent player control
                DialogueManager.StopAllConversations();
            }

            m_cinemaSignal.Payload.booleanValue = on;
            m_cinemaSignal.SendSignal();
        }

        public void ToggleCinematicBars(bool value)
        {
            m_cinematicBarsSignal.Payload.booleanValue = value;
            m_cinematicBarsSignal.SendSignal();
            if (value)
            {
                m_cinematicBars.Show();
            }
            else
            {
                m_cinematicBars.Hide();
            }
        }

        public void ToggleFadeUI(bool willshow)
        {
            if (willshow)
            {
                m_fadeUI.Show();
            }
            else
            {
                m_fadeUI.Hide();
            }
        }

        public void RequestTeleportConfirmation(LocationData destinationData)
        {
            m_levelFinish.SetupTeleportableLocation(destinationData);
            m_requestTeleportSignal.SendSignal();
        }

        public void NotifyUnlockedLocation(AvailableLocations location, InputActionConfiguration input)
        {
            m_levelFinish.NotifyAvailableLocation(location, input);
            m_levelFinishedSignal.SendSignal();
        }


        public void ShowGameOverScreen()
        {
            m_gameOverSignal.SendSignal();
        }

        public void TogglePause(bool toggle)
        {
            m_gamePause.SetActive(toggle);
            if (toggle)
            {
                m_gamePauseSignal.SendSignal();
            }
        }

        public CharacterRecruitmentUI GetRecruitmentConfirmation()
        {
            return m_characterRecruitmentUI;
            
        }
        public void SendconfirmationSignal()
        {
            m_confirmRecruitmentSignal.SendSignal();
        }

        public void ToggleSequenceSkip(bool willShow)
        {
            if (willShow)
            {
                m_skipHandle.Reset();
                m_skipHandle.SubscribeToInput();
                //m_skippableUI.Show();
            }
            else
            {
                m_skipHandle.UnsubscribeToInput();
                //m_skippableUI.Hide();
            }
        }

        public void Initialize()
        {
            m_cinematicVideoHandle.Initialize();
        }

        public void ShowCinematicVideo(VideoClip clip, Func<IEnumerator> behindTheSceneRoutine = null, Action OnVideoDone = null, bool hasEventOnVideoEnd = false, float secondsBeforeVideoEnds = 0f, float audiTansistionDuration = 0f)
        {
            m_cinematicVideoHandle.hasEventOnVideoEnd = hasEventOnVideoEnd;
            m_cinematicVideoHandle.secondsBeforeVideoEnd = secondsBeforeVideoEnds;
            m_cinematicVideoHandle.audioVolumeTransistionDuration = audiTansistionDuration;
            m_cinematicVideoHandle.ShowCinematicVideo(clip, behindTheSceneRoutine, OnVideoDone);
        }

        public void ForceStopCinematicVideo()
        {
            m_cinematicVideoHandle.ForceStopCinematicVideo();
        }

        public void OpenFastTravel(DLocation startingLocation)
        {
            m_fastTravelUI.ForceOpenPage(startingLocation);
            m_fastTravelSignal?.SendSignal();
        }

        public void OpenPauseMenu()
        {
            m_pauseGameGuard.CanPauseGame();
        }

        public void UIBack()
        {
            m_backSignal.SendSignal();
        }

        public void SetGameplayUIState(int state)
        {
            m_gameplayUIStateObserver.SetCurrentUnderworldUIState(state);
        }

        public GameplayUIState GetCurrentUIState() => m_gameplayUIStateObserver.currentUnderworldUIState;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void OnDestroy()
        {
            if (Instance != null)
            {
                Instance = null;
            }
        }

    }
}