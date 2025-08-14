using DChild.Gameplay.Environment;
using DChild.Gameplay.FastTravel;
using DChild.Gameplay.Systems;
using DChild.Gameplay.Systems.Serialization;
using Holysoft.Collections;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DChild.Gameplay
{
    public class MobileTeleportHandle : SerializedMonoBehaviour, IGameplaySystemModule
    {
        [SerializeField]
        private FastTravelHandle m_fastTravelHandle;
        [SerializeField]
        private CurrentLocationChecker m_currentLocationChecker;

        [SerializeField, BoxGroup("Throne Room Teleport Variables")]
        private FastTravelData m_throneRoomFastTravelData;
        [SerializeField, BoxGroup("Throne Room Teleport Variables")]
        private Dictionary<Environment.Location, LevelCompleteVariables> m_throneRoomTeleportLevelCompleteDictionary = new Dictionary<Environment.Location, LevelCompleteVariables>();

        [SerializeField, BoxGroup("Overworld Teleport Variables")]
        private Dictionary<SceneInfo, FastTravelData> m_overworldFastTravelDictionary = new Dictionary<SceneInfo, FastTravelData>();

        [SerializeField, BoxGroup("Overworld Teleport Variables")]
        private Dictionary<Environment.Location, LevelCompleteVariables> m_overworldTeleportLevelCompleteDictionary = new Dictionary<Environment.Location, LevelCompleteVariables>();
        
        [Button]
        public void TeleportToOverworld()
        {
            //TODO: Maybe in different script but show confirmation box to travel to overworld before teleporting
            //TODO: Show text or something to signify you can't go to overworld yet if you can
            if (CanTeleportToOverworld() == false)
                return;

            var currentSceneName = SceneManager.GetActiveScene().name;
            FastTravelData fastTravelData = null;

            foreach (SceneInfo sceneInfo in m_overworldFastTravelDictionary.Keys)
            {
                if (sceneInfo.sceneName == currentSceneName)
                {
                    fastTravelData = m_overworldFastTravelDictionary[sceneInfo];
                    break;
                }
            }

            m_fastTravelHandle.TransferPlayerTo(fastTravelData.fastTravelPoint);
        }

        public void TeleportToThroneRoom()
        {
            if (CanTeleportToThroneRoom() == false)
                return;

            m_fastTravelHandle.TransferPlayerTo(m_throneRoomFastTravelData.fastTravelPoint);
        }

        private bool CanTeleportToOverworld()
        {
            bool canTeleport = false;
            var currentLocation = m_currentLocationChecker.GetCurrentLocation();

            foreach (Environment.Location location in m_overworldTeleportLevelCompleteDictionary.Keys)
            {
                if (currentLocation == location)
                {
                    if (DialogueLua.GetVariable(m_overworldTeleportLevelCompleteDictionary[location].m_bossDefeatVariableName).AsBool)
                    {
                        canTeleport = true;
                    }
                    else
                    {
                        canTeleport = false;
                    }
                }
            }
            return canTeleport;
        }

        private bool CanTeleportToThroneRoom()
        {
            bool canTeleport = false;
            var currentLocation = m_currentLocationChecker.GetCurrentLocation();

            foreach (Environment.Location location in m_throneRoomTeleportLevelCompleteDictionary.Keys)
            {
                if (currentLocation == location)
                {
                    if (DialogueLua.GetVariable(m_throneRoomTeleportLevelCompleteDictionary[location].m_bossDefeatVariableName).AsBool)
                    {
                        canTeleport = true;
                    }
                    else
                    {
                        canTeleport = false;
                    }
                }
            }
            return canTeleport;
        }
    }

    [System.Serializable]
    public struct LevelCompleteVariables
    {
        public DialogueDatabase m_bossDialogueDatabase;
        [Tooltip("It is recommended to copy-paste the variable name since it needs to be a string.")]
        public string m_bossDefeatVariableName;
    }
}

