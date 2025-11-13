using DChild.Gameplay.Characters.Players.Modules;
using DChild.Gameplay.Systems;
using DChild.Gameplay.Systems.Serialization;
using DChild.Menu;
using DChild.Serialization;
using DChild.UI;
using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.FastTravel
{


    public class FastTravelHandle : MonoBehaviour
    {
        private Vector2 m_spawnPosition;
        public void TransferPlayerTo(LocationData destination)
        {
            var zoneData = FindObjectOfType<ZoneDataHandle>();
            var WorldTypeVar = FindObjectOfType<WorldTypeManager>();
            if(zoneData != null)
            {
                zoneData.ForceUpdateZoneData();
            }

            var playerManager = GameplaySystem.playerManager;
            var character = playerManager.player.character;
            character.transform.position  = new Vector2(50000, 50000);
            m_spawnPosition = destination.position;

            if(WorldTypeVar.CurrentWorldType == WorldType.Underworld)
            {
                var controller = GameplaySystem.playerManager.OverrideCharacterControls();
                controller.moveDirectionInput = 0;
                Rigidbody2D rigidBody = character.GetComponent<Rigidbody2D>();
                rigidBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                CharacterState collisionState = character.GetComponentInChildren<CharacterState>();
                collisionState.forcedCurrentGroundedness = true;
            }

            LoadingHandle.SetLoadType(LoadingHandle.LoadType.Smart);
            Cache<LoadZoneFunctionHandle> cacheLoadZoneHandle = Cache<LoadZoneFunctionHandle>.Claim();

            if (WorldTypeVar.CurrentWorldType != WorldTypeVar.GetLocationWorldType(destination.location))
            {
                GameplaySystem.campaignSerializer.UpdateData(SerializationScope.Player);
            }
            WorldTypeVar.SetCurrentWorldType(destination.location);

            switch (WorldTypeVar.CurrentWorldType)
            {
                case WorldType.Underworld:
                    cacheLoadZoneHandle.Value.Initialize(destination, character, cacheLoadZoneHandle);
                    GameSystem.LoadZone(GameMode.Underworld, destination.sceneInfo, true, OnTransferPlayerDone);
                    break;
                case WorldType.Overworld:
                    cacheLoadZoneHandle.Value.Initialize(destination, null, cacheLoadZoneHandle);
                    GameSystem.LoadZone(GameMode.Overworld, destination.sceneInfo, true, OnTransferPlayerDone);
                    break;
                case WorldType.ArmyBattle:
                    cacheLoadZoneHandle.Value.Initialize(destination, null, cacheLoadZoneHandle);
                    GameSystem.LoadZone(GameMode.ArmyBattle, destination.sceneInfo, true, OnTransferPlayerDone);
                    break;
            }

            GameplaySystem.ClearCaches();
        }

        private void OnTransferPlayerDone()
        {
            var playerManager = GameplaySystem.playerManager;
            var character = playerManager.player.character;
            character.transform.position = m_spawnPosition;
            
            Rigidbody2D rigidBody = character.GetComponent<Rigidbody2D>();
            rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

            var WorldTypeVar = FindObjectOfType<WorldTypeManager>();
            if(WorldTypeVar.CurrentWorldType == WorldType.Underworld)
            {
                CharacterState collisionState = character.GetComponentInChildren<CharacterState>();
                collisionState.forcedCurrentGroundedness = false;
                GameplaySystem.playerManager.StopCharacterControlOverride();
            }
            
        }
    }
}
