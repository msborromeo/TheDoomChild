using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Codex.Quests.UI
{
    public class QuestButtonUI : MonoBehaviour
    {
        [SerializeField] private QuestTypeBackgroundUI m_background;
        [SerializeField] private QuestNameUI m_name;
        [SerializeField] private List<QuestProgressUI> m_subQuestList;

        private Quest m_questData;
        private int m_selectionIndex;

        public virtual int selectionIndex => m_selectionIndex;

        public void SetSelectionIndex(int index) => m_selectionIndex = index;
        private void SetQuestData(Quest data) => m_questData = data;

        public void Display(Quest questData)
        {
            if (questData != null)
            {
                SetQuestData(questData);
                //m_background.SetBackground(isMainQuest);
                m_name.Display(m_questData.name, m_questData.state == QuestState.Success);
                return;
            }
            gameObject.SetActive(questData != null);
        }

        public void ShowProgress()
        {
            int count = m_questData.entryCount;
            for (int i = 0; i < m_subQuestList.Count; i++)
            {
                bool isActive = i < count;
                m_subQuestList[i].gameObject.SetActive(isActive);
                if (isActive)
                    m_subQuestList[i].Display(m_questData.GetEntry(i), i);
            }
        }
    }
}