using DChild.Gameplay.Environment;
using UnityEngine;

namespace DChild.Scripts.Gameplay.Environment.Interactables.Elevator
{
    public class ElevatorLevelButtonUI : MonoBehaviour
    {
        [SerializeField] private ElevatorLabelUI m_leftLabel;
        [SerializeField] private ElevatorLabelUI m_rightLabel;

        private MovingPlatform m_elevator;
        private int m_level;

        public void SetElevatorLevel(MovingPlatform elevator, int level)
        {
            m_elevator = elevator;
            m_level = level;
        }


        private void SetPathLabel(ElevatorLevelInfo info)
        {
            m_leftLabel.Display(info.leftLabel);
            m_rightLabel.Display(info.rightLabel);
        }

        public void Display(ElevatorLevelInfo info)
        {


            bool hasInfo = info != null;

            gameObject.SetActive(hasInfo);
            if (hasInfo)
            {
                m_level = info.destinationIndex;
                SetPathLabel(info);
            }
        }

        public void SelectLevel()
        {
            if (m_elevator == null) return;
            m_elevator.GoDestination(m_level);
        }
    }
}