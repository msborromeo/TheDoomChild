using DChild.Gameplay.Characters.Enemies;
using DChild.Gameplay.Characters.NPC;
using DChild.Gameplay.Characters.Players.SoulSkills;
using DChild.Gameplay.Environment;
using DChild.Gameplay.FastTravel;
using DChild.Gameplay.LevelFinish.UI;
using DChild.Gameplay.Systems.Serialization;
using DChild.Gameplay.Trade;
using DChild.Gameplay.UI;
using DChild.Gameplay.UI.Alerts;
using DChild.Menu.Trade;
using DChild.Scripts.Gameplay.Environment.Interactables.Elevator;
using DChild.UI;
using Holysoft.Event;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

namespace DChild.Gameplay.Systems
{

    public interface IGameplayUIHandle
    {
        bool isInCutsceneMode { get; }

        UIAlertManager alertManager { get; }
        IUINotificationManager notificationManager { get; }

        void ShowCinematicVideo(VideoClip clip, Func<IEnumerator> behindTheSceneRoutine = null, Action OnVideoDone = null, bool hasEventOnVideoEnd = false, float secondsBeforeVideoEnds = 0f, float audiTansistionDuration = 0f);

	void ForceStopCinematicVideo();

        void ToggleCinematicMode(bool on, bool instant = false);

        void ToggleCinematicBars(bool value);

        void UpdateNavMapConfiguration(Location location, int sceneIndex, Transform inGameReference, Vector2 mapReferencePoint, Vector2 calculationOffset);
        void OpenTradeWindow(NPCProfile merchantData, ITradeInventory merchantInventory, TradeAskingPrice merchantBuyingPriceRate, CurrencyType type);
        void OpenFastTravel(Location startingLocation, FastTravelData playerLocation);
        void OpenWeaponUpgradeConfirmationWindow();
        void OpenStoreAtPage(StorePage storePage);
        void OpenStore();

        void OpenPauseMenu();
        void UIBack();

        void OpenWorldMap(Location fromLocation);
        void OpenShadowGateMap(Location fromLocation);

        void MonitorBoss(Boss boss);
        void ResetGameplayUI();

        void PromptKeystoneFragmentNotification();
        void ShowJournalNotificationPrompt(float duration);

        void ToggleBossHealth(bool willshow);

        void ToggleBossCombatUI(bool willshow);
        void ToggleFadeUI(bool willshow);
        void RevealBossName();
        void ShowInteractionPrompt(bool willshow);
        void ShowMovableObjectPrompt(bool willshow);
        void ShowGameOverScreen();
        void ShowGameplayUI(bool willshow);
        void ToggleSequenceSkip(bool willShow);
        void ShowHoldToTeleportSequence(InputAction.CallbackContext context, bool isCanceled);
        void ActivateHealthRegenEffect(PassiveRegeneration.Handle regenHandle);
        void DeactivateHealthRegenEffect();
        void ActivateShadowRegenEffect();
        void DeactivateShadowRegenEffect();
        void ShowMordenElevatorUI(ElevatorLocation location, ElevatorLevelInfo[] labels, MovingPlatform elevator);
        void TogglePause(bool toggle);
        void RequestTeleportConfirmation(LocationData destinationData);
        public void NotifyUnlockedLocation(AvailableLocations location, InputActionConfiguration input);
        void OverrideCurrentUIState(GameplayUIState state);

        UIHandlerExtraReference GetReference();
        CharacterRecruitmentUI ConfirmationRequest();
    }
}
