using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using I2.Loc;
using System;

namespace DChild.Localization
{
    //[RequireComponent(typeof(IQuestDataLocalize))]
    public class QuestDataLocalizer : MonoBehaviour
    {
        private int m_questID;
       
        public string LocalizedQuestName(Quest quest)
        {
            return LocalizationManager.GetTranslation("Dialogue System/Item-Quest/"+ DialogueManager.masterDatabase.GetItem(quest.name).id+"/Name");
        }

        public string LocalizedQuestDescription(Quest quest)
        {
            return LocalizationManager.GetTranslation("Dialogue System/Item-Quest/" + DialogueManager.masterDatabase.GetItem(quest.name).id + "/Description");
        }

        public string LocalizedQuestResultSuccess(Quest quest,bool isSuccessfull)
        {
            return LocalizationManager.GetTranslation("Dialogue System/Item-Quest/" + DialogueManager.masterDatabase.GetItem(quest.name).id +(isSuccessfull? "/Success Description" : "Falure Description"));
        }

        [SerializeField]
        private Localize m_localizeDescription;

        [SerializeField]
        private Localize m_localizeName;

        private IQuestDataLocalize m_Injector;

        private void Awake()
        {
            //m_Injector = GetComponent<IQuestDataLocalize>();
            //m_Injector.LocalizeEntry += onUpdate;
        }

        private void OnDestroy()
        {
            //m_Injector.LocalizeEntry -= onUpdate;
        }

        private void onUpdate(QuestEntry entry, int index)
        {
            Debug.Log(m_questID);
            //string questName = DialogueManager.
            m_localizeName.SetTerm("Dialogue System/Item-Quest/"+ m_questID + "/Entry "+(index+1));
            //DialogueManager.masterDatabase.GetItem(entry.name).LookupLocalizedValue;
        }

        public Quest LocalizeQuest(Quest q)
        {
            if(q==null)
            {
                return null;
            }
            Item ItemQuest = DialogueManager.masterDatabase.GetItem(q.name);
            if(ItemQuest==null)
            {
                Debug.LogError("AHHHHHHHHHHH"+ItemQuest+" "+q.name);
                return q;
            }
            var questName = ItemQuest.LookupLocalizedValue("Name");
            var questState = ConvertString(ItemQuest.LookupValue("State").ToString());

            var entryCount = 0;
            entryCount = ItemQuest.LookupInt("Entry Count");
            QuestEntry[] entries = null;

            if (entryCount > 0)
            {
                entries = new QuestEntry[entryCount];
                for (int x = 0; x < entryCount; x++)
                {
                    var entryNumber = (x + 1);
                    var entryName = ItemQuest.LookupLocalizedValue("Entry " + entryNumber);
                    var entryDescription = ItemQuest.LookupLocalizedValue("Entry " + entryNumber + " Description");
                    var entryState = ConvertString(ItemQuest.LookupValue("Entry " + entryNumber + " State"));

                    //Dialogue Retrieval Starts Here

                    entries[x] = new QuestEntry(entryName, entryState, entryDescription);
                }
            }

            Quest localizedQuest = new Quest(questName, questState, entries);


            return localizedQuest;
        }

        public void SetQuestID(int id)
        {
            m_questID = id;
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
