using DChild.Gameplay;
using DChild.Gameplay.Systems;
using Doozy.Runtime.UIManager.Containers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Scripts.Gameplay.Environment.Interactables.Elevator
{
    public class ElevatorHandleUI : MonoBehaviour
    {
        //[SerializeField] private string m_location;

        //private ElevatorLevelSelectionUI m_mordenElevatorLevelSelection;

        //private UnderworldGameplayUIHandle m_uiHandle;
        [SerializeField] private UIView m_view;

        private void Start()
        {
        }

        [Button]
        private void HandleElevatorEvent(string location)
        {
            //m_mordenElevatorLevelSelection.Display(m_location);
            GameplaySystem.gamplayUIHandle.ShowMordenElevatorUI(location);
            m_view.Show();
        }

    }
}