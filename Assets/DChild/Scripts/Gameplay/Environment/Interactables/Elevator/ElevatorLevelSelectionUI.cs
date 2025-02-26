using DChild.Gameplay.Environment;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Scripts.Gameplay.Environment.Interactables.Elevator
{
    public class ElevatorLevelSelectionUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_locationLabel;
        [SerializeField]
        private List<ElevatorLevelButtonUI> m_elevatorButtons;
        [SerializeField]
        private Image m_pillar;
        [SerializeField]
        private MovingPlatform m_elevator;

        private void AdjustPillarHeight(List<ElevatorLevelButtonUI> levelButtons)
        {
            var levels = 0;
            levels = levelButtons.Where(button => button.gameObject.activeSelf).Count();

            var spacing = 32;
            var pillarWidth = m_pillar.rectTransform.sizeDelta.x;
            var pillarHeight = ((40 * levels) + (spacing * levels)) - spacing;

            m_pillar.rectTransform.sizeDelta = new Vector2(pillarWidth, pillarHeight);
        }

        public void Display(string location)
        {
            List<ElevatorLevelButtonUI> updatedLevelButtons;
            switch (location)
            {
                case "east":
                    updatedLevelButtons = m_elevatorButtons.GetRange(0, 4);
                    break;
                case "upper west":
                    updatedLevelButtons = m_elevatorButtons.GetRange(0, 3);
                    break;
                default:
                    updatedLevelButtons = m_elevatorButtons;
                    break;
            }

            AdjustPillarHeight(updatedLevelButtons);
            SetupUI();
        }


        //setup ui based on elevator location
        private void SetupUI()
        {
            m_locationLabel.text = "received elevator location data here";

            for (int i = 0; i < m_elevatorButtons.Count; i++)
            {
                m_elevatorButtons[i].SetLevel(i);
                m_elevatorButtons[i].Display("left", "right");
            }
        }

    }
}