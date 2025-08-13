using DChild.Gameplay.Environment;
using Holysoft.Collections;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DChild.Gameplay
{
    public class CurrentLocationChecker : SerializedMonoBehaviour
    {
        [SerializeField]
        private Location m_currentLocation;

        [SerializeField]
        private Dictionary<Location, List<SceneInfo>> m_locationAndSceneRelationDictionary = new Dictionary<Location, List<SceneInfo>>();

        [Button]
        public Location GetCurrentLocation()
        {
            var currentScene = SceneManager.GetActiveScene().name;

            foreach(Location location in m_locationAndSceneRelationDictionary.Keys)
            {
                foreach(SceneInfo sceneInfo in m_locationAndSceneRelationDictionary[location])
                {
                    if(sceneInfo.sceneName == currentScene)
                    {
                        m_currentLocation = location;
                        return m_currentLocation;
                    }
                }
            }

            return m_currentLocation;
        }
    }
}

