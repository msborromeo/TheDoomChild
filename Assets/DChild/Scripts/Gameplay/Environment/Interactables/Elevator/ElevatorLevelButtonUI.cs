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
            this.m_elevator = elevator;
            this.m_level = level;
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
                SetPathLabel(info);
            }
        }



        public void SelectLevel()
        {
            Debug.Log($"m_elevator : {m_elevator.gameObject.name}");
            if (m_elevator == null) return;
            m_elevator.GoDestination(m_level);
        }
    }
}