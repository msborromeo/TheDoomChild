using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Scripts.Gameplay.Environment.Interactables.Elevator
{
    public class ElevatorLocationLevelsUI : MonoBehaviour
    {
        [BoxGroup("Asset Sprites"), SerializeField] private Sprite m_upperWest;
        [BoxGroup("Asset Sprites"), SerializeField] private Sprite m_lowerWest;
        [BoxGroup("Asset Sprites"), SerializeField] private Sprite m_upperEast;
        [BoxGroup("Asset Sprites"), SerializeField] private Sprite m_lowerEast;

        [SerializeField] private Image m_levelsImage;

        public void SetLevelsImage(ElevatorLocation location)
        {
            m_levelsImage.sprite = location switch
            {
                ElevatorLocation.West => m_lowerWest,
                ElevatorLocation.Upper_West => m_upperWest,
                ElevatorLocation.Upper_East => m_upperEast,
                _ => m_lowerEast,
            };
        }
    }
}