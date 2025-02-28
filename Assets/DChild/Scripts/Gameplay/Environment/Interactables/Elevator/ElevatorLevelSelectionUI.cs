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
        [SerializeField] private TextMeshProUGUI m_locationLabel;
        [SerializeField] private LocationHighlighterUI m_locationHighlight;
        [SerializeField] private List<ElevatorLevelButtonUI> m_elevatorButtons;
        [SerializeField] private Image m_pillar;

        public void Display(ElevatorLocation location, ElevatorLevelInfo[] labels)
        {
            m_locationLabel.text = location.ToString().Replace("_", " ");
            m_locationHighlight.HighlightLocation(location);
            SetupFloorLevels(location, labels);
        }

        private void SetupFloorLevels(ElevatorLocation location, ElevatorLevelInfo[] labels)
        {

            int levelCount;

            switch (location)
            {
                case ElevatorLocation.East:
                    levelCount = 4;
                    break;
                case ElevatorLocation.Upper_West:
                    levelCount = 3;
                    break;
                default:
                    levelCount = m_elevatorButtons.Count;
                    break;
            }
            AdjustPillarHeight(levelCount);
            for (int j = 0; j < labels.Length; j++)
            {
                m_elevatorButtons[j].Display(labels[j]);
            }

            for (int i = m_elevatorButtons.Count - 1; i >= levelCount; i--)
            {
                m_elevatorButtons[i].Display(null);
            }
        }


        private void AdjustPillarHeight(int levels)
        {
            var spacing = 32;
            var pillarWidth = m_pillar.rectTransform.sizeDelta.x;
            var pillarHeight = ((40 * levels) + (spacing * levels)) - spacing;

            m_pillar.rectTransform.sizeDelta = new Vector2(pillarWidth, pillarHeight);
        }
    }


}
