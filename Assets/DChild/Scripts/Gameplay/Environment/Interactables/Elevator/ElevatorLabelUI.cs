using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Scripts.Gameplay.Environment.Interactables.Elevator
{
    public class ElevatorLabelUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_label;
        [SerializeField] private Image m_background;


        [SerializeField, OptionalField] private Image m_exitPortal;

        private void ShowExit()
        {
            m_exitPortal.gameObject.SetActive(m_label.text.Contains("exit"));
        }

        public void Display(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                gameObject.SetActive(false);
                return;
            }

            gameObject.SetActive(true);
            m_label.text = text;
            m_background.enabled = true;

            if (m_exitPortal != null)
                ShowExit();
        }
    }
}