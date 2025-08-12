using DChild.Gameplay.Cinematics;
using DChild.Gameplay.Combat;
using DChild.Gameplay.Environment;
using DChild.Gameplay.SoulSkills;
using DChild.Gameplay.Systems;
using DChild.Gameplay.VFX;
using DChild.Menu;
using DChild.Serialization;
using Holysoft.Event;
using System;
using UnityEngine;

namespace DChild.Gameplay
{
    public static class GameplaySystem
    {
        private static GameplayUIHandle uiHandle = new GameplayUIHandle();

        public static GameplayConstantsReference constantsReference => BaseGameplaySystem.constantsReference;
        public static GameplayModifiers modifiers { get => BaseGameplaySystem.modifiers; }
        public static AudioListenerPositioner audioListener { get => BaseGameplaySystem.audioListener; }
        public static IGameplayUIHandle gamplayUIHandle { get => uiHandle; }

        public static ICombatManager combatManager { get => UnderworldGameplaySystem.combatManager; }
        public static IFXManager fXManager { get => BaseGameplaySystem.fXManager; }
        public static ICinema cinema { get => BaseGameplaySystem.cinema; }
        public static IWorld world { get => BaseGameplaySystem.world; }
        public static ITime time { get => BaseGameplaySystem.time; }

        public  static VolumeMixerManager  volumeMixerManager { get => BaseGameplaySystem.volumeMixerManager; }

        public static IPlayerManager playerManager
        {
            get
            {
                switch (GetCurrentWorldType())
                {
                    case WorldType.Underworld:
                        return UnderworldGameplaySystem.playerManager;
                    case WorldType.Overworld:
                        return OverworldGameplaySystem.playerManager;
                    case WorldType.ArmyBattle:
                        return ArmyBattleGameplaySystem.playerManager;
                    default:
                        return null;
                }
            }
        }
        public static ISimulationHandler simulationHandler { get => GetCurrentWorldType() == WorldType.Underworld ? UnderworldGameplaySystem.simulationHandler : null; }
        public static ILootHandler lootHandler { get => UnderworldGameplaySystem.lootHandler; }
        public static IHealthTracker healthTracker { get => UnderworldGameplaySystem.healthTracker; }
        public static ISoulSkillManager soulSkillManager { get => UnderworldGameplaySystem.soulSkillManager; }
        public static IMinionManager minionManager { get => UnderworldGameplaySystem.minionManager; }
        public static CampaignSerializer campaignSerializer => BaseGameplaySystem.campaignSerializer;

        public static bool isGamePaused { get; private set; }
        private static Vector2 SceneTransitionPosition => new Vector2(-9999, -9999);

        public static WorldType GetCurrentWorldType() => BaseGameplaySystem.GetCurrentWorldType();

        public static void ResumeGame()
        {
            if (!isGamePaused)
                return;

            isGamePaused = false;
            BaseGameplaySystem.ResumeGame();
            if (GetCurrentWorldType() == WorldType.Underworld)
            {
                UnderworldGameplaySystem.ResumeGame();
            }
            else
            {
                OverworldGameplaySystem.ResumeGame();
            }
        }

        public static void PauseGame()
        {
            if (isGamePaused)
                return;

            isGamePaused = true;
            BaseGameplaySystem.PauseGame();
            if (GetCurrentWorldType() == WorldType.Underworld)
            {
                UnderworldGameplaySystem.PauseGame();
            }
            else
            {
                OverworldGameplaySystem.PauseGame();
            }
        }

        public static void ClearCaches()
        {
            BaseGameplaySystem.ClearCaches();
            if (BaseGameplaySystem.HasInstance)
            {
                if (GetCurrentWorldType() == WorldType.Underworld)
                {
                    UnderworldGameplaySystem.ClearCaches();
                }
                else
                {
                    OverworldGameplaySystem.LoadGame();
                }
            }
        }

        public static void LoadGame(CampaignSlot campaignSlot, LoadingHandle.LoadType loadType)
        {
            ClearCaches();
            LoadingHandle.OnLoadScreenTakeOver += OnLoadScreenTakeOver;
            BaseGameplaySystem.LoadGame(campaignSlot, loadType);
            if (GetCurrentWorldType() == WorldType.Underworld)
            {
                UnderworldGameplaySystem.LoadGame();
            }
            else
            {
                OverworldGameplaySystem.LoadGame();
            }
        }


        public static void ReloadGame()
        {
            BaseGameplaySystem.ReloadGame();
            if (GetCurrentWorldType() == WorldType.Underworld)
            {
                UnderworldGameplaySystem.LoadGame();
            }
            else
            {
                OverworldGameplaySystem.LoadGame();
            }
        }

        public static void SetCurrentCampaign(CampaignSlot campaignSlot)
        {
            BaseGameplaySystem.SetCurrentCampaign(campaignSlot);
        }

        public static void SetInputActive(bool isActive)
        {
            if (BaseGameplaySystem.HasInstance)
            {
                if (GetCurrentWorldType() == WorldType.Underworld)
                {
                    UnderworldGameplaySystem.SetInputActive(isActive);
                }
            }
        }

        public static void ForcePlayerTeleportOnSceneLoad(UnityEngine.Vector2 position)
        {
            if (GetCurrentWorldType() == WorldType.Overworld)
            {
                OverworldGameplaySystem.RequestForPlayerCharacterTeleport(position);
            }
            else if (GetCurrentWorldType() == WorldType.Underworld)
            {
                UnderworldGameplaySystem.RequestForPlayerCharacterTeleport(position);
            }
        }

        public static void ListenToNextSceneLoad()
        {
            Debug.Log("Listen To Next Scene Load");
            if (BaseGameplaySystem.HasInstance)
            {
                LoadingHandle.OnLoadScreenTakeOver += OnLoadScreenTakeOver;
                if (GetCurrentWorldType() == WorldType.Underworld)
                {
                    UnderworldGameplaySystem.ListenToNextSceneLoad();
                }
            }
            else
            {
                if (GameSystem.CurrentGameMode == GameMode.Underworld)
                {
                    UnderworldGameplaySystem.ListenToNextSceneLoad();
                }
            }
        }

        private static void OnLoadScreenTakeOver()
        {
            Debug.Log("Teleport Player to Scene Transistion Position");
            if (GetCurrentWorldType() == WorldType.Underworld)
            {
                UnderworldGameplaySystem.playerManager.TeleportPlayer(SceneTransitionPosition);
            }
            else
            {
                OverworldGameplaySystem.playerManager.TeleportPlayer(SceneTransitionPosition);
            }
            LoadingHandle.OnLoadScreenTakeOver -= OnLoadScreenTakeOver;
        }
    }
}