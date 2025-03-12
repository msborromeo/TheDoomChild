namespace PixelCrushers.DialogueSystem
{
    [System.Serializable]
    public class QuestEntry
    {
        public string m_name;
        public QuestState m_state;
        public string m_description;

        public QuestEntry(string name, QuestState state, string description)
        {
            m_name = name;
            m_state = state;
            m_description = description;
        }
    }
}
