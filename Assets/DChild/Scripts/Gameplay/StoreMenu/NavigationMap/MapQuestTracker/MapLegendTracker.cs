using DChild.Gameplay.NavigationMap;
using DChild.QuestHints;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.QuestHints
{
    public class MapLegendTracker : MonoBehaviour
    {
        [SerializeField]
        private RectTransform m_RevealedTransform;

        [SerializeField]
        List<GameObject> m_PointsOfInterests = new List<GameObject>();

        [SerializeField, TabGroup("Point Of Interest")]
        private MapPointOfInterestHandle m_pointOfInterest;

        private void OnEnable()
        {
            m_pointOfInterest.Initialize();
            m_pointOfInterest.LoadStates();
            //MapHintRevealer.MarkerRevealer.RevealMapHint += RevealObject;
        }
        private void OnDestroy()
        {
            //MapHintRevealer.MarkerRevealer.RevealMapHint -= RevealObject;
        }

        public void RevealObject()
        {
            foreach (GameObject POI in m_PointsOfInterests)
            {
                if (POI.GetComponent<MapPointOfInterestTracker>().isTracked)
                {
                    POI.transform.SetParent(m_RevealedTransform);
                    break;
                }
            }
        }

        public void Reveal(GameObject obj)
        {
            obj.transform.SetParent(m_RevealedTransform);
            obj.SetActive(true);
        }

    }
}

