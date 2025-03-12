using NUnit.Framework;
using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Codex.Quest.UI
{
    [Serializable]
    public class SampleDummyQuestData
    {
        [SerializeField] private string m_questName;
        [SerializeField] private bool m_isMainQuest;
        [SerializeField] private QuestState m_status;

        [SerializeField] private List<QuestProgressData> m_subQuests;

        public string questName => m_questName;
        public QuestState status => m_status;
        public bool isMainQuest => m_isMainQuest;

        public List<QuestProgressData> subQuests => m_subQuests;
    }

    [Serializable]
    public class QuestProgressData
    {
        [SerializeField] private string m_sectionName;
        [SerializeField] private string m_description;
        [SerializeField] private QuestState m_status;

        public string sectionName => m_sectionName;
        public string description => m_description;
        public QuestState status => m_status;
    }

    [Serializable]
    public class ConversationData
    {
        public string objective;

        [SerializeField] private List<DialogueData> m_dialogues;
        public List<DialogueData> dialogues => m_dialogues;
    }

    [Serializable]
    public class DialogueData
    {
        [SerializeField] private string m_characterName;
        [SerializeField] private string m_dialogue;

        public string characterName => m_characterName;
        public string dialogue => m_dialogue;
    }
}