using DChild.Gameplay.Environment;
using DChild.Gameplay.NavigationMap;
using DChild.Gameplay.Systems;
using Doozy.Runtime.UIManager.Containers;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.UI
{
    public class OverworldGameplayUIHandle : MonoBehaviour, IGameplaySystemModule, IGameplayInitializable
    {
        public static OverworldGameplayUIHandle Instance { get; private set; }

        [SerializeField, FoldoutGroup("Object Prompt")]
        private UIContainer m_interactablePrompt;

        [SerializeField]
        private StoreNavigator m_storeNavigator;
        [SerializeField]
        private WorldMapHandler m_worldMap;
        [SerializeField]
        private NavigationMapManager m_navMap;

        public void ShowInteractionPrompt(bool willshow)
        {
            if (willshow == true)
            {
                m_interactablePrompt.Show();
            }
            else
            {
                m_interactablePrompt.Hide();
            }
        }

        public void OpenStoreAtPage(StorePage storePage)
        {
            m_storeNavigator.SetPage(storePage);
            m_storeNavigator.OpenStore();
        }

        public void OpenStore()
        {
            m_storeNavigator.OpenStore();
        }

        public void UpdateNavMapConfiguration(Location location, int sceneIndex, Transform inGameReference, Vector2 mapReferencePoint, Vector2 calculationOffset)
        {
            m_navMap.UpdateConfiguration(location, sceneIndex, inGameReference, mapReferencePoint, calculationOffset);
        }

        public void Initialize()
        {

        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Initialize();
            }
            else
            {
                Destroy(this);
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }

}