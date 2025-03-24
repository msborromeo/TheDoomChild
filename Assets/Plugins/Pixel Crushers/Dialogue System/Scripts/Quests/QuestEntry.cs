using UnityEngine;

namespace PixelCrushers.DialogueSystem
{
    [System.Serializable]
    public class QuestEntry
    {
        [SerializeField]
        private string m_name;
        [SerializeField]
        private QuestState m_state;
        [SerializeField]
        private string m_description;

        public QuestEntry(string name, QuestState state, string description)
        {
            m_name = name;
            m_state = state;
            m_description = description;
        }

        public string name => m_name;
        public QuestState state => m_state;
        public string description => m_description;
    }
}
