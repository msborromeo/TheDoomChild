using DChild.Gameplay.FastTravel;
using Holysoft.Collections;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DChild.Gameplay
{
    public class OverworldTeleportHandle : SerializedMonoBehaviour
    {
        [SerializeField]
        private FastTravelHandle m_fastTravelHandle;
        [SerializeField]
        private Dictionary<SceneInfo, FastTravelData> overworldFastTravelDictionary = new Dictionary<SceneInfo, FastTravelData>();

        [Button]
        public void TeleportToOverworld()
        {
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
    }
}

