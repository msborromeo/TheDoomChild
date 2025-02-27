using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Scripts.Gameplay.Environment.Interactables.Elevator
{
    public class LocationHighlighterUI: MonoBehaviour
    {
        [BoxGroup("Elevator Locations"), SerializeField] private GameObject m_west;
        [BoxGroup("Elevator Locations"), SerializeField] private GameObject m_upperWest;
        [BoxGroup("Elevator Locations"), SerializeField] private GameObject m_upperEast;
        [BoxGroup("Elevator Locations"), SerializeField] private GameObject m_east;

        private Dictionary<string, GameObject> locationMap;

        private void Awake()
        {
            locationMap = new Dictionary<string, GameObject> {
                { "west", m_west },
                { "upper west", m_upperWest },
                { "upper east", m_upperEast },
                { "east", m_east }
            };
        }

        public void HighlightLocation(string location)
        {
            ResetLocationVisibility();

            if (locationMap.TryGetValue(location, out GameObject targetLocation))
                targetLocation.SetActive(true);
        }

        private void ResetLocationVisibility()
        {
            m_west.SetActive(false);
            m_upperWest.SetActive(false);
            m_east.SetActive(false);
            m_upperEast.SetActive(false);
        }
    }
}