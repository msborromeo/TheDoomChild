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

        public QuestEntry(DialogueDatabase associatedDatabase, string assiciatedQuest, int entryNumber)
        {
            m_associatedDatabase = associatedDatabase;
            m_associatedQuest = assiciatedQuest;
            m_entryNumber = entryNumber;
        }

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
