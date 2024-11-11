using DChild.Gameplay.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Systems
{
    public class OverworldGameplaySubsystem : MonoBehaviour
    {

        #region Modules
        private static DChild.Gameplay.Systems.PlayerManager m_playerManager;
        private static OverworldGameplayUIHandle m_uiHandler;

        public static IPlayerManager playerManager => m_playerManager;
        public static OverworldGameplayUIHandle uiHandler => m_uiHandler;
        #endregion

        private void AssignModule<T>(out T module) where T : MonoBehaviour, IGameplaySystemModule => module = GetComponentInChildren<T>();

        private void AssignModules()
        {
            AssignModule(out m_playerManager);
            AssignModule(out m_uiHandler);
        }

        private void Awake()
        {
            AssignModules();
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

