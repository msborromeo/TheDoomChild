using UnityEngine;

namespace PixelCrushers.DialogueSystem
{
    [System.Serializable]
    public class Quest
    {
        [SerializeField]
        private string m_name;
        [SerializeField]
        private QuestState m_state;
        [SerializeField]
        private QuestEntry[] m_entries;

        public Quest(string name, QuestState state, QuestEntry[] entries = null)
        {
            m_name = name;
            m_state = state;
            m_entries = entries;
        }

        public string name => m_name;
        public QuestState state => m_state;
        public int entryCount => m_entries.Length;

        public QuestEntry GetEntry(int index) => m_entries[index];
    }
}
