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
    public class OverworldTeleportHandle : SerializedMonoBehaviour, IGameplaySystemModule
    {
        [SerializeField]
        private FastTravelHandle m_fastTravelHandle;
        [SerializeField]
        private CurrentLocationChecker m_currentLocationChecker;
        [SerializeField]
        private Dictionary<SceneInfo, FastTravelData> overworldFastTravelDictionary = new Dictionary<SceneInfo, FastTravelData>();

        [SerializeField]
        private Dictionary<Environment.Location, LevelCompleteVariables> m_levelCompleteDictionary = new Dictionary<Environment.Location, LevelCompleteVariables>();

        [Button]
        public void TeleportToOverworld()
        {
            //TODO: Maybe in different script but show confirmation box to travel to overworld before teleporting
            //TODO: Show text or something to signify you can't go to overworld yet if you can
            if (CanTeleportToOverworld() == false)
                return;

            var currentSceneName = SceneManager.GetActiveScene().name;
            FastTravelData fastTravelData = null;

            foreach (SceneInfo sceneInfo in overworldFastTravelDictionary.Keys)
            {
                if(sceneInfo.sceneName == currentSceneName)
                {
                    fastTravelData = overworldFastTravelDictionary[sceneInfo];
                    break;
                }
            }

            m_fastTravelHandle.TransferPlayerTo(fastTravelData.fastTravelPoint);
        }

        private bool CanTeleportToOverworld()
        {
            bool canTeleport = false;
            var currentLocation = m_currentLocationChecker.GetCurrentLocation();

            foreach (Environment.Location location in m_levelCompleteDictionary.Keys)
            {
                if (currentLocation == location)
                {
                    if (DialogueLua.GetVariable(m_levelCompleteDictionary[location].m_bossDefeatVariableName).AsBool)
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

