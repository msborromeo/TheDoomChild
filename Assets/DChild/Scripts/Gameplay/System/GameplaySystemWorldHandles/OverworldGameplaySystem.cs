using DChild.Gameplay.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Systems
{
    public class OverworldGameplaySystem : MonoBehaviour
    {
        #region Modules
        private static OverworldPlayerManager m_playerManager;
        private static OverworldGameplayUIHandle m_uiHandler;

        public static IPlayerManager playerManager => m_playerManager;
        public static OverworldGameplayUIHandle uiHandler => m_uiHandler;
        #endregion

        private static bool m_hasBeenRequested;
        private static Vector2 m_requestPosition;

        public static void RequestForPlayerCharacterTeleport(Vector2 position)
        {
            m_requestPosition = position;
            m_hasBeenRequested = true;
        }

        private void AssignModule<T>(out T module) where T : MonoBehaviour, IGameplaySystemModule => module = GetComponentInChildren<T>();

        private void AssignModules()
        {
            AssignModule(out m_playerManager);
            AssignModule(out m_uiHandler);
        }

        private void Awake()
        {
            Debug.Log("Overworld System Awake Start");
            AssignModules();

            var initializables = GetComponentsInChildren<IGameplayInitializable>();
            for (int i = 0; i < initializables.Length; i++)
            {
                initializables[i].Initialize();
            }


            //Just to make sure that underworld system is loaded with Base Gameplay, currently still using old way to initialize first load;
            GameplaySystem.campaignSerializer.Load(SerializationScope.Gameplay | SerializationScope.Menu, true);
            Debug.Log("Overworld System Awake Done");

            if (m_hasBeenRequested)
            {
                m_playerManager.TeleportPlayer(m_requestPosition);
            }
        }


        public static void LoadGame()
        {
            //throw new NotImplementedException();
        }

        public static void PauseGame()
        {
            //throw new NotImplementedException();
        }

        public static void ResumeGame()
        {
            //throw new NotImplementedException();
        }
    }
}

