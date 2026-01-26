using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrushers.DialogueSystem
{
    [System.Serializable]
    public class QuestEntry
    {
        [SerializeField]
        private DialogueDatabase m_associatedDatabase;
        [SerializeField]
        private string m_associatedQuest;
        [SerializeField]
        private int m_entryNumber;
        [SerializeField, ValueDropdown("GetConversations", IsUniqueList = true, SortDropdownItems = true)]
        private string m_Conversation;

        public QuestEntry(DialogueDatabase associatedDatabase, string assiciatedQuest, int entryNumber)
        {
            m_associatedDatabase = associatedDatabase;
            m_associatedQuest = assiciatedQuest;
            m_entryNumber = entryNumber;
        }

        public QuestEntry(DialogueDatabase associatedDatabase, string assiciatedQuest, int entryNumber,string conversation)
        {
            m_associatedDatabase = associatedDatabase;
            m_associatedQuest = assiciatedQuest;
            m_entryNumber = entryNumber;
            m_Conversation = conversation;
        }

        private IEnumerable GetConversations()
        {
            ValueDropdownList<string> list = new ValueDropdownList<string>();

            foreach (var variable in m_associatedDatabase.conversations)
            {
                list.Add(variable.Title);
            }

            return list;
        }

        public Conversation conversation => m_associatedDatabase.GetConversation(m_Conversation); 

        public string name
        {
            get
            {
                var quest = m_associatedDatabase.GetItem(m_associatedQuest);
                var result = quest.LookupLocalizedValue(nameField);
                if (string.IsNullOrEmpty(result))
                {
                    return quest.LookupValue(nameField);
                }
                return result; ;
            }
        }

        public QuestState state => QuestLog.GetQuestEntryState(m_associatedQuest, m_entryNumber);
        public string description
        {
            get
            {
                var quest = m_associatedDatabase.GetItem(m_associatedQuest);
                var result = quest.LookupLocalizedValue(descriptionField);
                if (string.IsNullOrEmpty(result))
                {
                    return quest.LookupValue(descriptionField);
                }
                return result;
            }
        }

        private string descriptionField => $"Entry {m_entryNumber} Description";
        private string nameField => $"Entry {m_entryNumber}";
    }
}
