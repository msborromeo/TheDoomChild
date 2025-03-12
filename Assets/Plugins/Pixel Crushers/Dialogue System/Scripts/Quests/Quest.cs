namespace PixelCrushers.DialogueSystem
{
    [System.Serializable]
    public class Quest
    {
        public string m_name;
        public QuestState m_state;
        public QuestEntry[] m_entries;

        public Quest(string name, QuestState state, QuestEntry[] entries = null)
        {
            m_name = name;
            m_state = state;
            m_entries = entries;
        }
    }
}
