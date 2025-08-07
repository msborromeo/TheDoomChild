using DChild.Gameplay.NavigationMap;
using Language.Lua;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DChild.QuestHints
{
    public class MapHintRevealer : MonoBehaviour
    {
        public Action<GameObject> RevealMapHint;
        public static MapHintRevealer MarkerRevealer;
        [SerializeField]
        private DialogueDatabase m_TrackerDatabase;
        [SerializeField]
        private GameObject m_Indicator;
        [QuestPopup(true)]
        public string quest;
        public bool m_TrackSubQuest;
        [ShowIf("m_TrackSubQuest"), QuestEntryPopup,SerializeField]
        private int m_QuestEntry;

        

        // Start is called before the first frame update
        void Start()
        {
            if(MarkerRevealer == null) 
            {
               MarkerRevealer = this;
            }
        }
        [Button]
        public void GenerateQuestIndicator()
        {
            string trackingVariable = quest.ToString();
            if (m_TrackSubQuest)
                trackingVariable = trackingVariable+("_" + m_QuestEntry.ToString());

            Variable var = new Variable();
            if (m_TrackerDatabase.GetVariable(trackingVariable)== null)
            {
                
                //DialogueLua.SetVariable(trackingVariable, false);
                
            }


            
            if(!gameObject.TryGetComponent(out MapPointOfInterestTracker tracker))
            {
                tracker = m_Indicator.AddComponent<MapPointOfInterestTracker>();
            }
            tracker.SetForQuestIndicator(trackingVariable);

        }
    }
}

