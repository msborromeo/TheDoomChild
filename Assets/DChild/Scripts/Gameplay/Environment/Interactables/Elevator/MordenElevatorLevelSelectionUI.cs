using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Scripts.Gameplay.Environment.Interactables.Elevator
{
    public class MordenElevatorLevelSelectionUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_locationLabel;
        [SerializeField]
        private List<ElevatorLevelButtonUI> m_elevatorButtons;
        [SerializeField]
        private Image m_pillar;

        [Button]
        private void AdjustPillarHeight()
        {
            var levels = 0;
            levels = m_elevatorButtons.Where(button => button.gameObject.activeSelf).Count();

            var spacing = 32;
            var pillarWidth = m_pillar.rectTransform.sizeDelta.x;
            var pillarHeight = ((40 * levels) + (spacing * levels)) - spacing;

            m_pillar.rectTransform.sizeDelta = new Vector2(pillarWidth, pillarHeight);
        }

        public void Display()
        {
            m_locationLabel.text = "received elevator location data here...";
            foreach (var button in m_elevatorButtons)
            {
                button.Display();
            }
        }
    }
}