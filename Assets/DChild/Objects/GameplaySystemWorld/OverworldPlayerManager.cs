using DChild.Gameplay.ArmyBattle;
using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Characters.Players.Modules;
using DChild.Visuals;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DChild.Gameplay.Systems
{
    public class OverworldPlayerManager : MonoBehaviour, IPlayerManager, IGameplaySystemModule, IGameplayInitializable
    {
        [SerializeField, BoxGroup("Player Data")]
        private Player m_player;
        [SerializeField]
        private GameplayInput m_gameplayInput;
        [SerializeField]
        private InputTranslator m_characterInput;
        [SerializeField]
        private PlayerCharacterOverride m_overrideController;
        [SerializeField]
        private ArmyBattleCharacterRecruiter m_armyBattleCharacterRecruiter;
        [SerializeField]
        private PlayerInput m_playerInput;

        #region IPlayerManager Stuff
        public Player player => m_player;

        public IAutoReflexHandler autoReflex => null;

        public ArmyBattleCharacterRecruiter armyBattleCharacterRecruiter => m_armyBattleCharacterRecruiter;

        public PlayerInput PlayerInput => m_playerInput;

        public void DisableControls()
        {
           // m_gameplayInput?.SetStoreInputActive(false);
           // m_characterInput?.Disable();
           //// m_player.controller?.Disable();
        }

        public void DisableIntroControls()
        {
            Debug.Log("Disable Intro Controls empty on Overworld Player Manager");
        }

        public void EnableControls()
        {
            //m_gameplayInput?.SetStoreInputActive(true);
            //m_characterInput?.Enable();
            ////m_player.controller?.Enable();
        }

        public void EnableIntroAction(List<IntroActions> action)
        {
            Debug.Log("Enable Intro Actions empty on Overworld Player Manager");
        }

        public void EnableIntroControls()
        {
            Debug.Log("Enable Intro Controls empty on Overworld Player Manager");
        }

        public bool IsPartOfPlayer(GameObject gameObject)
        {
            throw new NotImplementedException();
        }

        public bool IsPartOfPlayer(GameObject gameObject, out IPlayer player)
        {
            throw new NotImplementedException();
        }

        public PlayerCharacterOverride OverrideCharacterControls()
        {
            //m_gameplayInput?.SetStoreInputActive(false);
            //m_characterInput?.Disable();
            ////We are not using PlayerCharacterController in Overworld
            ////m_player.controller?.Disable();
            ////m_player.controller?.Enable();
            //m_overrideController.enabled = true;
            //m_player.state.allowExtendedIdle = false;
            return m_overrideController;
        }

        public IEnumerator PlayerActionChange(Action<PlayerInput> Callback)
        {
            throw new NotImplementedException();
        }

        public void ReturnPlayerToOrginalScene()
        {
            m_player.character.transform.SetParent(transform);
            m_player.character.transform.SetParent(null);
        }

        public void StopCharacterControlOverride()
        {
            //m_overrideController.enabled = false;
            //m_gameplayInput?.SetStoreInputActive(true);
            //m_characterInput?.Enable();
            //m_player.controller?.Enable();
            //m_player.state.allowExtendedIdle = true;
        }

        public void SyncVisualsWith(SpineSyncer spineSyncer)
        {
            throw new NotImplementedException();
        }
        #endregion

        public void Initialize()
        {
            //var character = m_player.character;
            //m_collisionRegistrator = character.GetComponentInChildren<CollisionRegistrator>();
            //m_interactableDetector = character.GetComponentInChildren<InteractableDetector>();

            m_player.Initialize();
            GameplaySystem.campaignSerializer.PostDeserialization += OnPostDeserialization;
            GameplaySystem.campaignSerializer.PreSerialization += OnPreSerialization;
            // m_respawnDelay.CountdownEnd += OnRespawnPlayer;
        }

        private void OnPostDeserialization(object sender, CampaignSlotUpdateEventArgs eventArgs)
        {
            if (eventArgs.IsPartOfTheUpdate(SerializationScope.Player) && m_player)
            {
                m_player.SetPosition(eventArgs.slot.spawnPosition);
                m_player.LoadData(eventArgs.slot.characterData);
            }
        }

        private void OnPreSerialization(object sender, CampaignSlotUpdateEventArgs eventArgs)
        {
            if (eventArgs.IsPartOfTheUpdate(SerializationScope.Player) && m_player)
            {
                eventArgs.slot.UpdateCharacterData(m_player.SaveData());
            }
        }

        public void TeleportPlayer(Vector2 position)
        {
            m_player.SetPosition(position);
        }
    }
}

