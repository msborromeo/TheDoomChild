using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Scripts.Gameplay.Environment.Interactables.Elevator
{
    public class ElevatorLabelUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_label;
        [SerializeField] private Image m_background;

        public void Display(string text)
        {
            gameObject.SetActive(text != null);
            m_label.text = text;
            m_background.enabled = true;
        }
    }
}