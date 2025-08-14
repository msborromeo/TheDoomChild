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

        public void Reveal(GameObject obj)
        {
            obj.transform.SetParent(m_RevealedTransform);
            obj.SetActive(true);
        }

    }
}

