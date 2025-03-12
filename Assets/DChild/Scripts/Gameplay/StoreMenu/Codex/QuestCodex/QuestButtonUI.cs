using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Codex.Quest.UI
{
    public class QuestButtonUI : MonoBehaviour
    {
        [SerializeField] private QuestTypeBackgroundUI m_background;
        [SerializeField] private QuestNameUI m_name;
        [SerializeField] private List<QuestProgressUI> m_subQuestList;

        private SampleDummyQuestData m_questData;

        private void SetQuestData(SampleDummyQuestData data)
        {
            m_questData = data;
        }

        public void Display(SampleDummyQuestData questData)
        {
            SetQuestData(questData);

            m_background.SetBackground(m_questData.isMainQuest);
            m_name.Display(m_questData.questName, m_questData.status == QuestState.Success);
        }

        public void ShowProgress()
        {
            int count = m_questData.subQuests.Count;
            for (int i = 0; i < m_subQuestList.Count; i++)
            {
                bool isActive = i < count;
                m_subQuestList[i].gameObject.SetActive(isActive);
                if (isActive)
                    m_subQuestList[i].Display(m_questData.subQuests[i], i);
            }
        }
    }
}