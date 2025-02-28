using DChild.Gameplay.Environment;
using UnityEngine;

namespace DChild.Scripts.Gameplay.Environment.Interactables.Elevator
{
    public class ElevatorLevelButtonUI : MonoBehaviour
    {
        [SerializeField] private ElevatorLabelUI m_leftLabel;
        [SerializeField] private ElevatorLabelUI m_rightLabel;

        private int m_level;

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

        public void SelectElevatorLevel(MovingPlatform elevator)
        {
            if (elevator == null) return;
            elevator.GoDestination(m_level);
        }
    }
}