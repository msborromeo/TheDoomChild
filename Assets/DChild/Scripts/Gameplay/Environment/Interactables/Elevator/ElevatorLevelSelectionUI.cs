using DChild.Gameplay.Environment;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Scripts.Gameplay.Environment.Interactables.Elevator
{
    public enum ElevatorLocation
    {
        West,
        UpperWest,
        UpperEast,
        East,
        [HideInInspector]
        _COUNT
    }


    public class ElevatorLevelSelectionUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_locationLabel;
        [SerializeField] private LocationHighlighterUI m_locationHighlight;
        [SerializeField] private List<ElevatorLevelButtonUI> m_elevatorButtons;
        [SerializeField] private Image m_pillar;


        //[SerializeField] private MovingPlatform m_elevator;
        
       
        private void AdjustPillarHeight(List<ElevatorLevelButtonUI> levelButtons)
        {
            var levels = 0;
            levels = levelButtons.Where(button => button.gameObject.activeSelf).Count();

            var spacing = 32;
            var pillarWidth = m_pillar.rectTransform.sizeDelta.x;
            var pillarHeight = ((40 * levels) + (spacing * levels)) - spacing;

            m_pillar.rectTransform.sizeDelta = new Vector2(pillarWidth, pillarHeight);
        }

        public void Display(ElevatorLocation location)
        {
            m_locationLabel.text = location.ToString();
            m_locationHighlight.HighlightLocation(location);
            SetupFloorLevels(location);
        }

        private void SetupFloorLevels(ElevatorLocation location)
        {
            int levelCount;

            switch (location)
            {
                case ElevatorLocation.East:
                    levelCount = 4;
                    break;
                case ElevatorLocation.UpperWest:
                    levelCount = 3;
                    break;
                default:
                    levelCount = m_elevatorButtons.Count;
                    break;
            }

            for (int i = 0; i < m_elevatorButtons.Count; i++)
            {
                m_elevatorButtons[i].SetLevel(i);
                m_elevatorButtons[i].Display("left", "right");
            }

            AdjustPillarHeight(m_elevatorButtons.GetRange(0, levelCount));
        }

    }
}