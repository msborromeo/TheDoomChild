using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.QuestHints
{
    public class TrackQuest : MonoBehaviour
    {
        private MapLegendTracker mapLegendTracker;
        private QuestStateListener questListener;

        private void Start()
        {
            mapLegendTracker = GetComponentInParent<MapLegendTracker>();
            questListener = GetComponent<QuestStateListener>();
        }

        private void OnEnable()
        {
            questListener.UpdateIndicator(true);
        }

        public void Reveal(GameObject target)
        {
            if (target == null)
            {
                return;
            }
            mapLegendTracker.Reveal(target);
        }
    }
}
