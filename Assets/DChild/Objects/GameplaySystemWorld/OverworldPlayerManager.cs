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
    public class OverworldPlayerManager : MonoBehaviour, IPlayerManager
    {
        [SerializeField, BoxGroup("Player Data")]
        private Player m_player;
        [SerializeField]
        private GameplayInput m_gameplayInput;
        [SerializeField]
        private InputTranslator m_characterInput;
        [SerializeField]
        private PlayerCharacterOverride m_overrideController;

        #region IPlayerManager Stuff
        public IPlayer player => m_player;

        public IAutoReflexHandler autoReflex => null;

        public ArmyBattleCharacterRecruiter armyBattleCharacterRecruiter { get; }

        public void DisableControls()
        {
            m_gameplayInput?.SetStoreInputActive(false);
            m_characterInput?.Disable();
            m_player.controller.Disable();
        }

        public void DisableIntroControls()
        {
            Debug.Log("Disable Intro Controls empty on Overworld Player Manager");
        }

        public void EnableControls()
        {
            m_gameplayInput?.SetStoreInputActive(true);
            m_characterInput?.Enable();
            m_player.controller.Enable();
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
            m_gameplayInput?.SetStoreInputActive(false);
            m_characterInput?.Disable();
            m_player.controller.Disable();
            m_player.controller.Enable();
            m_overrideController.enabled = true;
            m_player.state.allowExtendedIdle = false;
            return m_overrideController;
        }

        public IEnumerator PlayerActionChange(Action<PlayerInput> Callback)
        {
            throw new NotImplementedException();
        }

        public void ReturnPlayerToOrginalScene()
        {
            throw new NotImplementedException();
        }

        public void StopCharacterControlOverride()
        {
            m_overrideController.enabled = false;
            m_gameplayInput?.SetStoreInputActive(true);
            m_characterInput?.Enable();
            m_player.controller.Enable();
            m_player.state.allowExtendedIdle = true;
        }

        public void SyncVisualsWith(SpineSyncer spineSyncer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

