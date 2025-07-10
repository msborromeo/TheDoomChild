using DChild.Gameplay.Environment;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace DChild.Scripts.Gameplay.Environment.Interactables.Elevator
{
    public class ElevatorLevelButtonUI : MonoBehaviour
    {
        [SerializeField] private ElevatorLabelUI m_leftLabel;
        [SerializeField] private ElevatorLabelUI m_rightLabel;
        [SerializeField] private TextMeshProUGUI m_levelNumber;

        private MovingPlatform m_elevator;
        private int m_level;

        public void SetElevatorLevel(MovingPlatform elevator, int level)
        {
            this.m_elevator = elevator;
            this.m_level = level;
        }


        private void SetPathLabel(ElevatorLevelInfo info)
        {
            m_leftLabel.Display(info.leftLabel);
            m_rightLabel.Display(info.rightLabel);
            m_levelNumber.text = $"{m_level + 1}";
        }

        public void Display(ElevatorLevelInfo info)
        {
            bool hasInfo = info != null;

            gameObject.SetActive(hasInfo);
            if (hasInfo)
            {
                SetPathLabel(info);
            }
        }

        [Button]
        public void ShowAsCurrent()
        {
            var button = gameObject.GetComponent<UIButton>();
            button.Select();
        }

        public void SelectLevel()
        {
            if (m_elevator == null) return;
            m_elevator.GoDestination(m_level);
        }
    }
}