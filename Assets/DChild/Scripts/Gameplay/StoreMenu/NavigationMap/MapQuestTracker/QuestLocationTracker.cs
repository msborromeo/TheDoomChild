using DChild.Gameplay.NavigationMap;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DChild.Gameplay.Environment;

namespace DChild.QuestHints
{
    public class QuestLocationTracker : MonoBehaviour
    {
        [QuestPopup(true)]
        public string quest;

        private NavMapInstantiator navMapInstantiator;

        private void Start()
        {
            navMapInstantiator = GetComponent<NavMapInstantiator>();
        }
        // Start is called before the first frame update
        public bool TrackingQuest(string Quest)
        {
            var questState = QuestLog.GetQuestState(quest);
            if (questState == QuestState.Active)
                return true;

            return false;
        }

        public bool isCurrentLocation(DChild.Gameplay.Environment.Location loc)
        {
            if (loc == navMapInstantiator.currentMap)
            {
                return true;
            }

            return false;
        }
    }
}
