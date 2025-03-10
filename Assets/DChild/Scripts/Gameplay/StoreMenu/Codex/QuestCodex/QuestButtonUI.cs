using NUnit.Framework.Interfaces;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Validation;
using System.Collections.Generic;
using TMPro;
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
            m_name.Display(m_questData.questName, m_questData.status == QuestStatus.Completed);
        }

        public void ShowProgress()
        {
            var subquestCount = m_questData.subQuests.Count;

            for (int i = 0; i < m_subQuestList.Count; i++)
            {
                if (i < subquestCount)
                {
                    QuestProgressData subquestData = m_questData.subQuests[i];
                    m_subQuestList[i].Display(subquestData, i);
                    m_subQuestList[i].gameObject.SetActive(true);
                    continue;
                }
                m_subQuestList[i].gameObject.SetActive(false);
            }
        }
    }
}