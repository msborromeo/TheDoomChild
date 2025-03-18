using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Codex.Quests.UI
{
    public class QuestButtonUI : MonoBehaviour
    {
        [BoxGroup("Display State"), SerializeField] private QuestTypeBackgroundUI m_background;
        [BoxGroup("Display State"), SerializeField] private GameObject m_lockedBackground;


        [SerializeField] private QuestNameUI m_name;
        [SerializeField] private List<QuestProgressUI> m_subQuestList;

        private Quest m_questData;
        private int m_selectionIndex;

        public QuestTypeBackgroundUI background => m_background;

        public virtual int selectionIndex => m_selectionIndex;


        public void SetSelectionIndex(int index) => m_selectionIndex = index;
        private void SetQuestData(Quest data) => m_questData = data;

        public void Display(Quest questData)
        {
            if (questData == null)
            {
                m_background.gameObject.SetActive(false);
                m_lockedBackground.SetActive(true);
                return;
            }
            m_lockedBackground.SetActive(false);
            m_background.gameObject.SetActive(true);

            SetQuestData(questData);
            m_name.Display(m_questData.name, m_questData.state == QuestState.Success);
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