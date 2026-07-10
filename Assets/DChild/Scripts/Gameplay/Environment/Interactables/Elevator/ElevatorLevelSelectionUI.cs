using DChild.Gameplay.Environment;
using Doozy.Runtime.UIManager.Components;
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
        //[SerializeField] private RectTransform m_pillarRect;

        private MovingPlatform m_elevator;

        public void Display(ElevatorLocation location, ElevatorLevelInfo[] labels, MovingPlatform elevator)
        {
            m_elevator = elevator;
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
                case ElevatorLocation.Upper_West:
                    levelCount = 4;
                    break;
                default:
                    levelCount = m_elevatorButtons.Count;
                    break;
            }
            //AdjustPillarHeight(levelCount);

            for (int i = 0; i < labels.Length; i++)
            {
                var info = labels[i];
                m_elevatorButtons[i].SetElevatorLevel(m_elevator, info.destinationIndex);

                var isCurrent = m_elevator.currentWayPoint == info.destinationIndex;
                m_elevatorButtons[i].Display(info, isCurrent);
            }

            for (int i = levelCount; i < m_elevatorButtons.Count; i++)
            {
                m_elevatorButtons[i].Display(null);
            }
        }


        //private void AdjustPillarHeight(int levels)
        //{
        //    var spacing = 32;
        //    var pillarWidth = m_pillarRect.sizeDelta.x;
        //    var pillarHeight = ((40 * levels) + (spacing * levels)) - spacing;

        //    m_pillarRect.sizeDelta = new Vector2(pillarWidth, pillarHeight);
        //}
    }


}
