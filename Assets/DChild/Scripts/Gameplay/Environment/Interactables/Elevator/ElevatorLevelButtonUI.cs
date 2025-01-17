using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Scripts.Gameplay.Environment.Interactables.Elevator
{
    public class ElevatorLevelButtonUI : MonoBehaviour
    {
        [SerializeField] private ElevatorLabelUI m_leftLabel;
        [SerializeField] private ElevatorLabelUI m_rightLabel;

        public void Display()
        {
            //if left side is null
            m_leftLabel.Display("left location");
            
            //if right side is not null
            m_rightLabel.Display("right location");
        }
    }
}