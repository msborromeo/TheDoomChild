using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Scripts.Gameplay.Environment.Interactables.Elevator
{
    public class ElevatorLabelUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_label;
        [SerializeField]
        private Image m_background;

        public void Display()
        {
            m_label.text = "label text";
            m_background.enabled = true;
        }
    }
}