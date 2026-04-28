using DChild.Gameplay.Combat;
using DChild.Gameplay.SoulSkills;
using DChild.Menu;
using Holysoft.Event;
using System.Collections;
using UnityEngine;

namespace DChild.Gameplay.Systems
{
    public class UnderworldGameplaySystem : MonoBehaviour
    {
        [SerializeField]
        private bool m_doNotTeleportPlayerOnAwake;

        [SerializeField]
        private static VolumeMixerManagerHandle m_volumeMixerManager;

        private static UnderworldGameplaySystem m_instance;

        private static bool m_hasBeenRequested;
        private static Vector2 m_requestPosition;

        public static VolumeMixerManagerHandle volumeMixerManager => m_volumeMixerManager;

        #region Modules
        private static IGameplayActivatable[] m_activatableModules;
        private static MinionManager m_minionManager;
        private static SoulSkillManager m_soulSkillManager;
        private static HealthTracker m_healthTracker;
        private static SimulationHandler m_simulation;
        private static CombatManager m_combatManager;
        private static LootHandler m_lootHandler;
        private static DChild.Gameplay.Systems.PlayerManager m_playerManager;
        private static UnderworldGameplayUIHandle m_gameplayUIHandle;
        private static MobileTeleportHandle m_overworldTeleportHandle;

        public static IMinionManager minionManager => m_minionManager;
        public static ISoulSkillManager soulSkillManager => m_soulSkillManager;
        public static IHealthTracker healthTracker => m_healthTracker;

        public static ISimulationHandler simulationHandler => m_simulation;
        public static ICombatManager combatManager => m_combatManager;
        public static ILootHandler lootHandler => m_lootHandler;

        public static IPlayerManager playerManager => m_playerManager;
        public static MobileTeleportHandle overworldTeleportHandle => m_overworldTeleportHandle;
        public static UnderworldGameplayUIHandle gameplayUIHandle => m_gameplayUIHandle;

        #endregion
        public static void ResumeGame()
        {
            //m_playerManager?.EnableInput();
            //m_volumeMixerManager.UseSnapshot(AudioSnapshot.Gameplay);
        }

        public static void ClearCaches()
        {
            m_healthTracker?.RemoveAllTrackers();
            m_playerManager?.ClearCache();
        }

        public static void PauseGame()
        {
            //m_playerManager?.DisableInput();
            //m_volumeMixerManager.UseSnapshot(AudioSnapshot.GamePause);
        }
        public static void LoadGame()
        {
            m_healthTracker?.RemoveAllTrackers();
            LoadingHandle.SceneDone += LoadGameDone;
        }

        public static void SetInputActive(bool isActive)
        {
            //Reverted comment because this is used by
            //system to force player input based on game state i.e. when loading
            if (isActive)
            {
                m_playerManager?.EnableInput();
            }
            else
            {
                m_playerManager?.DisableInput();
            }
        }

        public static void ListenToNextSceneLoad()
        {
            LoadingHandle.LoadingDone += OnLoadingSceneDone;
        }

        public static void RequestForPlayerCharacterTeleport(Vector2 position)
        {
            m_requestPosition = position;
            m_hasBeenRequested = true;
        }

        private static void OnLoadingSceneDone(object sender, EventActionArgs eventArgs)
        {
            LoadingHandle.LoadingDone -= OnLoadingSceneDone;
            m_playerManager?.FreezePlayerPosition(false);
        }

        private static void LoadGameDone(object sender, EventActionArgs eventArgs)
        {
            LoadingHandle.SceneDone -= LoadGameDone;

            m_gameplayUIHandle.ResetGameplayUI();
        }

        private static void LockPlayerToSpawnPosition()
        {
            if (GameplaySystem.campaignSerializer.slot == null)
                return;

            //m_playerManager.player.character.transform.position = GameplaySystem.campaignSerializer.slot.spawnPosition;
            m_playerManager.FreezePlayerPosition(true);
        }

        private void AssignModule<T>(out T module) where T : MonoBehaviour, IGameplaySystemModule => module = GetComponentInChildren<T>();

        private void AssignModules()
        {
            AssignModule(out m_combatManager);
            AssignModule(out m_lootHandler);
            AssignModule(out m_simulation);
            AssignModule(out m_playerManager);
            AssignModule(out m_healthTracker);
            AssignModule(out m_soulSkillManager);
            AssignModule(out m_minionManager);
            AssignModule(out m_gameplayUIHandle);
            AssignModule(out m_overworldTeleportHandle);
        }

        private IEnumerator DelayedShowGameplay()
        {
            int frameCount = 30;
            do
            {
                yield return null;
                frameCount--;
            } while (frameCount > 0);
            m_gameplayUIHandle.ShowGameplayUI(true);
        }

        private void Awake()
        {
            if (m_instance)
            {
                Destroy(gameObject);
            }
            else
            {
                m_instance = this;
                Debug.Log("Underworld Gameplay Awake");

                AssignModules();
                var initializables = GetComponentsInChildren<IGameplayInitializable>();
                for (int i = 0; i < initializables.Length; i++)
                {
                    initializables[i].Initialize();
                }

                //Just to make sure that underworld system is loaded with Base Gameplay, currently still using old way to initialize first load;
                GameplaySystem.campaignSerializer.Load(SerializationScope.Gameplay | SerializationScope.Menu, true);

                if (m_hasBeenRequested)
                {
                    m_playerManager.TeleportPlayer(m_requestPosition);
                    m_hasBeenRequested = false;
                }

                if (m_doNotTeleportPlayerOnAwake == false)
                {
                    LockPlayerToSpawnPosition();
                }

                Debug.Log("Underworld Gameplay Awake Done");
            }

            m_volumeMixerManager = GameplaySystem.volumeMixerManager;
        }

        private void Start()
        {
            Debug.Log("Underworld Gameplay Start");

            StartCoroutine(DelayedShowGameplay());

            Debug.Log("Underworld Gameplay Start Done");
        }

        private void OnDestroy()
        {
            if (m_instance == this)
            {
                m_instance = null;

                m_combatManager = null;
                m_lootHandler = null;
                m_simulation = null;
                m_playerManager = null;
                m_activatableModules = null;
            }
        }
    }
}

