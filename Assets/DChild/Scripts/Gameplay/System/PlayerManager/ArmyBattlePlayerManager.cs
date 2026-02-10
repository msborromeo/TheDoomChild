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
    public class ArmyBattlePlayerManager : MonoBehaviour, IGameplaySystemModule, IGameplayInitializable, IPlayerManager
    {
        [SerializeField, BoxGroup("Player Data")]
        private Player m_player;

        public Player player => m_player;

        public IAutoReflexHandler autoReflex => null;

        public ArmyBattleCharacterRecruiter armyBattleCharacterRecruiter => throw new NotImplementedException();

        public void DisableControls()
        {
            Debug.Log("Theres no Player controls to actually modify");
        }

        public void DisableIntroControls()
        {
            Debug.Log("Theres no Player controls to actually modify");
        }

        public void EnableControls()
        {
            Debug.Log("Theres no Player controls to actually modify");
        }

        public void EnableIntroAction(List<IntroActions> action)
        {
            Debug.Log("Theres no Player controls to actually modify");
        }

        public void EnableIntroControls()
        {
            Debug.Log("Theres no Player controls to actually modify");
        }

        public void Initialize()
        {
            if (GameplaySystem.campaignSerializer != null)
            {
                GameplaySystem.campaignSerializer.PostDeserialization += OnPostDeserialization;
                GameplaySystem.campaignSerializer.PreSerialization += OnPreSerialization;
            }
        }

        public bool IsPartOfPlayer(GameObject gameObject)
        {
            if (gameObject.TryGetComponentInParent(out PlayerControlledObject playerObject))
            {
                return true;
            }
            return false;
        }

        public bool IsPartOfPlayer(GameObject gameObject, out IPlayer player)
        {
            var isPartOfPlayer = IsPartOfPlayer(gameObject);
            player = isPartOfPlayer ? m_player : null;
            return isPartOfPlayer;
        }

        public PlayerCharacterOverride OverrideCharacterControls()
        {
            Debug.Log("Theres no Player controls to actually modify");
            return null;
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

        public IEnumerator PlayerActionChange(Action<PlayerInput> Callback)
        {
            return null;
        }

        public void ReturnPlayerToOrginalScene()
        {
            Debug.Log("Theres no Player visuals to modify");
        }

        public void StopCharacterControlOverride()
        {
            Debug.Log("Theres no Player controls to actually modify");
        }

        public void SyncVisualsWith(SpineSyncer spineSyncer)
        {
            Debug.Log("Theres no Player visuals to modify");
        }


        public void TeleportPlayer(Vector2 position)
        {
            Debug.Log("Theres no Player visuals to modify");
        }

        private void OnDestroy()
        {
            if (GameplaySystem.campaignSerializer != null)
            {
                GameplaySystem.campaignSerializer.PostDeserialization -= OnPostDeserialization;
                GameplaySystem.campaignSerializer.PreSerialization -= OnPreSerialization;
            }
        }
    }
}