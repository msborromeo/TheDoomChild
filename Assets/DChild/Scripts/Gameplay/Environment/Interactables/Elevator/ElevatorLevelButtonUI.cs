using DChild.Gameplay.Environment;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Scripts.Gameplay.Environment.Interactables.Elevator
{
    public class ElevatorLevelButtonUI : MonoBehaviour
    {
        [SerializeField] private ElevatorLabelUI m_leftLabel;
        [SerializeField] private ElevatorLabelUI m_rightLabel;

        private int m_level;

        public void SetLevel(int value)
        {
            m_level = value;
        }

        public void Display(string left, string right)
        {
            //if left side is null
            m_leftLabel.Display(left ?? "left location");
            
            //if right side is null
            m_rightLabel.Display(right ?? "right location");

        }

        public void SelectElevatorLevel(MovingPlatform elevator)
        {
            if (elevator == null) return;
            elevator.GoDestination(m_level);
        }
    }
}