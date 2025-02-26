using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Scripts.Gameplay.Environment.Interactables.Elevator
{
    public class ElevatorHandleUI : MonoBehaviour
    {
        [SerializeField] private string m_location;

        private ElevatorLevelSelectionUI m_mordenElevatorLevelSelection;

        private void Start()
        {
            m_mordenElevatorLevelSelection = GetComponent<ElevatorLevelSelectionUI>();
        }

        [Button]
        private void HandleElevatorEvent()
        {
            m_mordenElevatorLevelSelection.Display(m_location);
        }
    }
}