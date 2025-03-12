using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

namespace PixelCrushers.DialogueSystem
{
    public class QuestLogDataRetriever : MonoBehaviour
    {
        [SerializeField]
        private DialogueDatabase m_dialogueDatabase;
        
        [SerializeField]
        private String m_questName;
        [SerializeField]
        private String m_questState;
        [SerializeField]
        private int m_entryNumber=0;

        [SerializeField]
        private String[] m_questEntries;
        [SerializeField]
        private String[] m_questEntryState;
        [SerializeField]
        private String[] m_questEntryDescription;
        [Button, HideInPrefabAssets]
        public void RetrieveQuestData()
        {
           // m_questName = m_dialogueDatabase.GetItem(m_questid).Name;

            int questnumber = m_dialogueDatabase.items.Count;
            Debug.Log("questnumber:" + questnumber);
            for (int i = 0; i <=questnumber; i++)
            {

            if (m_dialogueDatabase.items[i].IsItem == false)
            {
                   
                    m_questName = m_dialogueDatabase.items[i].Name;
                    m_questState = m_dialogueDatabase.items[i].LookupValue("State").ToString();
                    m_entryNumber = 0;
                    m_entryNumber = m_dialogueDatabase.items[i].LookupInt("Entry Count");
                    m_questEntries = new string[m_entryNumber];
                    m_questEntryState = new string[m_entryNumber];
                    m_questEntryDescription = new string[m_entryNumber];
                    for (int x = 0; x <= m_entryNumber; x++)
                    {
                        m_questEntries[x] = m_dialogueDatabase.items[i].LookupValue("Entry " + (x + 1));
                        Debug.Log("Entry " + (x + 1) + " Description");
                        m_questEntryDescription[x] = m_dialogueDatabase.items[i].LookupValue("Entry " + (x + 1)+ " Description");
                        m_questEntryState[x] = m_dialogueDatabase.items[i].LookupValue("Entry " + (x + 1) + " State");
                    }
                    
                    }


             }
        }

    }
}
