using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using DChild.Localization;

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

        public Quest questData => m_questData;

        public QuestTypeBackgroundUI background => m_background;

        public virtual int selectionIndex => m_selectionIndex;

        public QuestDataLocalizer localizer;


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

            //SetQuestData(questData);
            SetQuestData(localizer.LocalizeQuest(questData));
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

                /*if(m_subQuestList[i].TryGetComponent(out QuestDataLocalizer localize))
                {
                    Item t = DialogueManager.masterDatabase.GetItem(m_questData?.name);
                    if(t!=null)
                    {
                        localize.SetQuestID(t.id);
                    }
                }*/
            }
        }
    }
}