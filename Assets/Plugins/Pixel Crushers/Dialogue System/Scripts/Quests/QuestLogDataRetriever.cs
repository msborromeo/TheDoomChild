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

    public class QuestLogDataRetriever
    {
        public Quest[] RetrieveQuestDatas(DialogueDatabase[] databases, bool lookForMainQuests)
        {
            List<Quest> quests = new List<Quest>();
            foreach (DialogueDatabase database in databases)
            {
                if (database.name.Contains("Auto-Backup"))
                    continue;

                var quest = RetrieveQuestData(database, lookForMainQuests);

                if (quest == null)
                    continue;

                quests.Add(quest);
            }

            return quests.ToArray();
        }

        public Quest RetrieveQuestData(DialogueDatabase dialogueDatabase, bool lookForMainQuests)
        {
            int questnumber = dialogueDatabase.items.Count;
            for (int i = 0; i < questnumber; i++)
            {
                var quest = RetrieveQuest(dialogueDatabase.items[i], lookForMainQuests);
                if (quest == null)
                    continue;

                return quest;
            }

            return null;
        }

     

        public Quest RetrieveQuest(Item item, bool lookForMainQuests =true)
        {
            if (item.IsItem || item.LookupBool("Trackable") == false)
                return null;

            if (item.LookupBool("IsMainQuest") != lookForMainQuests)
                return null;

            var questName = item.Name;
            var questState = ConvertString(item.LookupValue("State").ToString());

            var entryCount = 0;
            entryCount = item.LookupInt("Entry Count");
            QuestEntry[] entries = null;

            if (entryCount > 0)
            {
                entries = new QuestEntry[entryCount];
                for (int x = 0; x < entryCount; x++)
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
            str = str.ToLower();
            switch (str)
            {
                case "unassigned":
                    return QuestState.Unassigned;
                case "active":
                    return QuestState.Active;
                case "success":
                    return QuestState.Success;
                default:
                    throw new ArgumentException($"{str} string cannot be converted to QuestState");
            }
        }
    }
}
