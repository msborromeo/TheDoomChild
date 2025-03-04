using DChild.Gameplay;
using DChild.Gameplay.Environment;
using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager.Containers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Scripts.Gameplay.Environment.Interactables.Elevator
{
    [System.Serializable]
    public class ElevatorLevelInfo {

        [SerializeField] private string m_leftLabel;
        [SerializeField] private string m_rightLabel;

        private int m_destinationIndex;


        public bool isEmpty => string.IsNullOrEmpty(m_leftLabel) && string.IsNullOrEmpty(m_rightLabel);

        public int destinationIndex => m_destinationIndex;
        public string leftLabel => m_leftLabel;
        public string rightLabel => m_rightLabel;
    }

    public class ElevatorHandleUI : MonoBehaviour
    {
        [SerializeField] private ElevatorLevelInfo[] m_infos;

        [SerializeField] private ElevatorLocation m_location;

        [SerializeField] private SignalSender m_elevatorSignal;

        private MovingPlatform m_elevator;

        private void Start() => m_elevator = GetComponent<MovingPlatform>();

        [Button(ButtonSizes.Large)]
        private void HandleElevatorEvent()
        {
            GameplaySystem.gamplayUIHandle.ShowMordenElevatorUI(m_location, m_infos, m_elevator);
            m_elevatorSignal.SendSignal();
        }

    }
}