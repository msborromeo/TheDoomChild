using UnityEngine;

namespace PixelCrushers.DialogueSystem
{
    [System.Serializable]
    public class Quest
    {
        [SerializeField]
        private DialogueDatabase m_associatedDatabase;
        [SerializeField]
        private string m_name;
        [SerializeField]
        private QuestEntry[] m_entries;

        public Quest(DialogueDatabase associatedDatabase, string name, QuestEntry[] entries = null)
        {
            m_associatedDatabase = associatedDatabase;
            m_name = name;
            m_entries = entries;
        }

        public string name
        {
            get
            {
                var result = m_associatedDatabase.GetItem(m_name).localizedName;
                if (string.IsNullOrEmpty(result))
                {
                    return m_name;
                }
                return result;
            }
        }

        public QuestState state => QuestLog.GetQuestState(m_name);
        public string description => QuestLog.GetQuestDescription(m_name);
        public int entryCount => m_entries.Length;

        public QuestEntry GetEntry(int index) => m_entries[index];
    }
}
