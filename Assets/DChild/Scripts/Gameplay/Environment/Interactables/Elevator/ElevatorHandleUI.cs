using DChild.Gameplay;
using DChild.Gameplay.Environment;
using Doozy.Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using static DChild.Gameplay.Environment.MovingPlatform;

namespace DChild.Scripts.Gameplay.Environment.Interactables.Elevator
{
    [System.Serializable]
    public class ElevatorLevelInfo
    {

        [SerializeField] private string m_leftLabel;
        [SerializeField] private string m_rightLabel;

        [SerializeField] private int m_destinationIndex;


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

        [SerializeField] private MovingPlatform m_elevator;

        [SerializeField] private BoxCollider2D m_boxCollider;

        //private bool m_levelChanged = true;

        [Button(ButtonSizes.Large)]
        public void HandleElevatorEvent()
        {
            GameplaySystem.PauseGame();
            GameplaySystem.gamplayUIHandle.ShowMordenElevatorUI(m_location, m_infos, m_elevator);
            m_elevatorSignal.SendSignal();
        }

        private void Awake()
        {
            m_elevator.DestinationChanged += OnDestinationChanged;
            m_elevator.DestinationReached += OnDestinationReached;
        }

        private void OnDestinationChanged(object sender, UpdateEventArgs eventArgs) => m_boxCollider.enabled = false;

        private void OnDestinationReached(object sender, UpdateEventArgs eventArgs) => m_boxCollider.enabled = true;//m_levelChanged = false;

    }
}