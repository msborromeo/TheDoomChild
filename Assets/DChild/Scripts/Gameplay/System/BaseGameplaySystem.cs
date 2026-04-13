using DarkTonic.MasterAudio;
using DChild.Configurations;
using DChild.Gameplay.Cinematics;
using DChild.Gameplay.VFX;
using DChild.Inputs;
using DChild.Menu;
using DChild.Serialization;
using Holysoft.Event;
using PixelCrushers.DialogueSystem;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DChild.Gameplay.Systems
{
    public class GameplayModifiers
    {
        public float minionSoulEssenceDrop = 1;
        public float SoulessenceAbsorption = 1;
    }

    public class BaseGameplaySystem : MonoBehaviour
    {
        [SerializeField]
        private static VolumeMixerManagerHandle m_volumeMixerManager;
        [SerializeField]
        private bool m_doNotDeserializeOnAwake;
        [SerializeField]
        private AudioListenerPositioner m_audioListener;
       

        private GameplaySettings m_settings;
        private static BaseGameplaySystem m_instance;
        private static CampaignSlot m_campaignToLoad;
        private static GameplayModifiers m_modifiers;
        private static GameplayConstantsReference m_constantsReference;
        public static GameplayModifiers modifiers => m_modifiers;
        public static GameplayConstantsReference constantsReference => m_constantsReference;
        private static CampaignSerializer m_campaignSerializer;

        public static CampaignSerializer campaignSerializer => m_campaignSerializer;
        public static VolumeMixerManagerHandle volumeMixerManager => m_volumeMixerManager;

        public static AudioListenerPositioner audioListener { get; private set; }

        [SerializeField]
        private static WorldTypeManager m_worldTypeManager;
        [SerializeField]
        private static ActiveInputHandle m_activeInputHandle;

        public static bool HasInstance => m_instance != null;

        private SkeletonAnimationManager m_skeletonManager;

        #region Modules
        private static IGameplayActivatable[] m_activatableModules;
        private static IOptionalGameplaySystemModule[] m_optionalGameplaySystemModules;
        private static IGameplayModuleManager[] m_gameplayModuleManager;
        private static FXManager m_fxManager;
        private static Cinema m_cinema;
        private static World m_world;

        private static BaseGameplayUIHandle m_baseGameplayUIHandle;

        public static bool isGamePaused { get; private set; }

        public static BaseGameplayUIHandle gamplayUIHandle => m_baseGameplayUIHandle;
        public static IFXManager fXManager => m_fxManager;
        public static ICinema cinema => m_cinema;
        public static IWorld world => m_world;
        public static ITime time
        {
            get
            {
                if (m_world == null)
                {
                    return new TimeInfo(Time.timeScale, Time.deltaTime, Time.fixedDeltaTime);
                }
                else
                {
                    return m_world;
                }
            }
        }
        #endregion

        private void AssignModules()
        {
            AssignModule(out m_fxManager);
            AssignModule(out m_cinema);
            AssignModule(out m_world);
            AssignModule(out m_campaignSerializer);
            AssignModule(out m_baseGameplayUIHandle);
            AssignModule(out m_constantsReference);
            AssignModule(out m_volumeMixerManager);

            m_skeletonManager = new SkeletonAnimationManager();
            //these two iffy - Ayan
            m_gameplayModuleManager = new IGameplayModuleManager[1];
            m_gameplayModuleManager[0] = m_skeletonManager;
        }

        private void AssignModule<T>(out T module) where T : MonoBehaviour, IGameplaySystemModule => module = GetComponentInChildren<T>();

        public static void SetInputToGameplay()
        {
            m_activeInputHandle.SetInputToGameplay();
        }

        public static void SetInputToUI()
        {
            m_activeInputHandle.SetInputToUI();
        }

        public static void SetCurrentPlayerInput(PlayerInput playerInput)
        {
            m_activeInputHandle.SetCurrentPlayerInput(playerInput);
        }

        public static WorldType GetCurrentWorldType()
        {
            return m_worldTypeManager.CurrentWorldType;
        }

        public static void SetWorldType(Environment.Location location)
        {
            m_worldTypeManager.SetCurrentWorldType(location);
        }

        public static void ResumeGame()
        {
            GameTime.UnregisterValueChange(m_instance, GameTime.Factor.Multiplication);
            isGamePaused = false;
            GameSystem.SetCursorVisibility(false);

            DialogueManager.Unpause();

            try
            {
                m_volumeMixerManager.UseSnapshot(AudioSnapshot.Gameplay);
                //MasterAudio.UnpauseEverything();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            SkeletonAnimationManager.Instance?.UnpauseAllSpines();
        }

        public static void PauseGame()
        {
            m_volumeMixerManager.UseSnapshot(AudioSnapshot.GamePause);
            GameTime.RegisterValueChange(m_instance, 0, GameTime.Factor.Multiplication);
            isGamePaused = true;
            GameSystem.SetCursorVisibility(true);
            //MasterAudio.PauseEverything();
            
            SkeletonAnimationManager.Instance.PauseAllSpines();
            DialogueManager.Pause();

        }

        public static void MuteAllSounds()
        {
            m_volumeMixerManager.UseSnapshot(AudioSnapshot.Cinematic);
        }
        public static void UnMuteAllSounds()
        {
            m_volumeMixerManager.UseSnapshot(AudioSnapshot.Gameplay);
        }
        public static void UnMuteAllSounds(float duration)
        {
            m_volumeMixerManager.UseSnapshot(AudioSnapshot.Gameplay,duration);
        }

        public static void ClearCaches()
        {
            MasterAudio.StopMixer();
            m_cinema?.ClearLists();
        }

        public static void LoadGame(CampaignSlot campaignSlot, LoadingHandle.LoadType loadType)
        {
            m_campaignToLoad = campaignSlot;
            ClearCaches();
            PersistentDataManager.ApplySaveData(campaignSlot.dialogueSaveData, DatabaseResetOptions.KeepAllLoaded);
            LoadingHandle.SetLoadType(loadType);

            m_worldTypeManager.SetCurrentWorldType(m_campaignToLoad.location);

            var gameMode = GameMode.Underworld;
            switch (m_worldTypeManager.CurrentWorldType)
            {
                case WorldType.Underworld:
                    gameMode = GameMode.Underworld;
                    break;
                case WorldType.Overworld:
                    gameMode = GameMode.Overworld;
                    break;
                case WorldType.ArmyBattle:
                    gameMode = GameMode.ArmyBattle;
                    break;
            }

            GameSystem.LoadZone(gameMode, m_campaignToLoad.sceneToLoad, true);
            //Reload Items
            LoadingHandle.SceneDone += LoadGameDone;
        }

        private static void LoadGameDone(object sender, EventActionArgs eventArgs)
        {
            LoadingHandle.SceneDone -= LoadGameDone;


            m_campaignSerializer.SetSlot(m_campaignToLoad);
            //m_baseGameplayUIHandle.ResetGameplayUI();
            m_campaignSerializer.Load(SerializationScope.Gameplay, true);
        }

        public static void ReloadGame()
        {
            LoadGame(campaignSerializer.slot, LoadingHandle.LoadType.Force);
        }

        public static void SetCurrentCampaign(CampaignSlot campaignSlot)
        {
            if (m_instance)
            {
                LoadGame(campaignSlot, LoadingHandle.LoadType.Force);
            }
            else
            {
                m_campaignToLoad = campaignSlot;
            }
        }

        protected void Awake()
        {
            if (m_instance)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Base Gameplay Awake");

                m_instance = this;
                AssignModules();
                m_activatableModules = GetComponentsInChildren<IGameplayActivatable>();
                var initializables = GetComponentsInChildren<IGameplayInitializable>();
                m_worldTypeManager = GetComponentInChildren<WorldTypeManager>();
                m_activeInputHandle = GetComponentInChildren<ActiveInputHandle>();
                //m_volumeMixerManager = GetComponentInChildren<VolumeMixerManagerHandle>();

                for (int i = 0; i < m_gameplayModuleManager.Length; i++)
                {
                    m_gameplayModuleManager[i].SetInstance(m_gameplayModuleManager[i]);
                }

                for (int i = 0; i < initializables.Length; i++)
                {
                    initializables[i].Initialize();
                }
                if (m_campaignToLoad != null)
                {
                    m_campaignSerializer.SetSlot(m_campaignToLoad);
                }

                m_worldTypeManager.SetCurrentWorldType(m_campaignToLoad.location);

                if (m_doNotDeserializeOnAwake == false)
                {
                    m_campaignSerializer.Load(SerializationScope.Gameplay, true);
                }
                Debug.Log("Base Gameplay Awake Done");
            }
        }

        private void Start()
        {
            Debug.Log("Base Gameplay Start");

            audioListener = m_audioListener;
            m_settings = GameSystem.settings?.gameplay ?? null;
            m_modifiers = new GameplayModifiers();
            isGamePaused = false;
            if (m_campaignToLoad != null)
            {
                m_campaignSerializer.SetSlot(m_campaignToLoad);

                m_campaignToLoad = null;
            }
            Debug.Log("Base Gameplay Start Done");
        }

        private void OnEnable()
        {
            for (int i = 0; i < m_activatableModules.Length; i++)
            {
                m_activatableModules[i].Enable();
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < m_activatableModules.Length; i++)
            {
                m_activatableModules[i].Disable();
            }
        }

        private void OnApplicationQuit()
        {
            Time.timeScale = 1;
        }

        private void OnDestroy()
        {
            if (this == m_instance)
            {
                m_instance = null;

                m_fxManager = null;
                m_cinema = null;
                m_world = null;
                m_activatableModules = null;
                GameTime.UnregisterValueChange(m_instance, GameTime.Factor.Multiplication);

                for (int i = 0; i < m_gameplayModuleManager.Length; i++)
                {
                    m_gameplayModuleManager[i].SetInstance(null);
                }
            }
        }
    }
}