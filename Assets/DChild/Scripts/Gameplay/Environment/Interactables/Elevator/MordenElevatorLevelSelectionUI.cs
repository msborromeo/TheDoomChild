using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Scripts.Gameplay.Environment.Interactables.Elevator
{
    public class MordenElevatorLevelSelectionUI : MonoBehaviour
    {
        [SerializeField]
        private Image m_sectionBackground;
        [SerializeField]
        private TextMeshProUGUI m_locationLabel;
        [SerializeField]
        private List<ElevatorLevelButtonUI> m_elevatorButtons;
        

        public void Display()
        {
            foreach(var button in m_elevatorButtons)
            {
                button.Display();
            }
        }
    }
}