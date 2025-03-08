using System.Collections.Generic;
using UnityEngine;

namespace DChild.Codex.Quest.UI
{
    [System.Serializable]
    public class SampleDummyQuestData
    {
        [SerializeField] private string m_questName;
        [SerializeField] private bool m_isMainQuest;
        [SerializeField] private QuestStatus m_status;

        [SerializeField] private List<QuestProgressData> m_subQuests;

        public string questName => m_questName;
        public QuestStatus status => m_status;
        public bool isMainQuest => m_isMainQuest;

        public List<QuestProgressData> subQuests => m_subQuests;
    }

    [System.Serializable]
    public class QuestProgressData
    {
        [SerializeField] private string m_sectionName;
        [SerializeField] private QuestStatus m_status;
        [SerializeField] private int m_sequence;

        public string sectionName => m_sectionName;
        public QuestStatus status => m_status;
        public int sequence => m_sequence;
    }
}