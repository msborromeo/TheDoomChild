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
        private Quest m_quest;
        [SerializeField]
        private int m_entryCount = 0;

        [Button, HideInPrefabAssets]
        public Quest RetrieveQuestData()
        {
            // m_questName = m_dialogueDatabase.GetItem(m_questid).Name;

            int questnumber = m_dialogueDatabase.items.Count;
            Debug.Log("questnumber:" + questnumber);
            for (int i = 0; i <= questnumber; i++)
            {
                var quest = RetrieveQuest(m_dialogueDatabase.items[i]);
                if (quest == null)
                    continue;

                m_quest = quest;
                return quest;
            }

            return null;
        }

        public Quest RetrieveQuest(Item item)
        {
            if (item.IsItem)
                return null;

            var questName = item.Name;
            var questState = ConvertString(item.LookupValue("State").ToString());

            m_entryCount = 0;
            m_entryCount = item.LookupInt("Entry Count");
            QuestEntry[] entries = null;

            if (m_entryCount > 0)
            {
                entries = new QuestEntry[m_entryCount];
                for (int x = 0; x <= m_entryCount; x++)
                {
                    var entryNumber = (x + 1);
                    var entryName = item.LookupValue("Entry " + entryNumber);
                    var entryDescription = item.LookupValue("Entry " + entryNumber + " Description");
                    var entryState = ConvertString(item.LookupValue("Entry " + entryNumber + " State"));

                    //Dialogue Retrieval Starts Here

                    entries[x] = new QuestEntry(entryName, entryState, entryDescription);
                }
            }

            return new Quest(questName, questState, entries);
        }

        private QuestState ConvertString(string str)
        {
            switch (str)
            {
                case "unassigned":
                    return QuestState.Unassigned;
                case "active":
                    return QuestState.Active;
                case "success":
                    return QuestState.Success;
                default:
                    throw new ArgumentException("string cannot be converted to QuestState");
            }
        }
    }
}
