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
using System.Numerics;

namespace DChild.Gameplay
{
    public static class GameplaySystem
    {
        private static GameplayUIHandle uiHandle = new GameplayUIHandle();

        public static GameplayModifiers modifiers { get => BaseGameplaySystem.modifiers; }
        public static AudioListenerPositioner audioListener { get => BaseGameplaySystem.audioListener; }
        public static IGameplayUIHandle gamplayUIHandle { get => uiHandle; }

        public static ICombatManager combatManager { get => UnderworldGameplaySystem.combatManager; }
        public static IFXManager fXManager { get => BaseGameplaySystem.fXManager; }
        public static ICinema cinema { get => BaseGameplaySystem.cinema; }
        public static IWorld world { get => BaseGameplaySystem.world; }
        public static ITime time { get => BaseGameplaySystem.time; }

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

        public static WorldType GetCurrentWorldType() => BaseGameplaySystem.GetCurrentWorldType();

        public static void ResumeGame()
        {
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
        }

        public static void ListenToNextSceneLoad()
        {
            if (BaseGameplaySystem.HasInstance)
            {
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
    }
}