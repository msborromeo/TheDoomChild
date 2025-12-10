using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using DChild.Localization;
using Doozy.Runtime.UIManager.Components;

namespace DChild.Codex.Quests.UI
{
    public class QuestButtonUI : MonoBehaviour
    {
        [BoxGroup("Display State"), SerializeField] private QuestTypeBackgroundUI m_background;
        [BoxGroup("Display State"), SerializeField] private GameObject m_lockedBackground;


        [SerializeField] private QuestNameUI m_name;

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
            gameObject.GetComponent<UIButton>().interactable = questData != null;
            if (questData == null)
            {
                m_background.gameObject.SetActive(false);
                m_lockedBackground.SetActive(true);
                return;
            }

            if(questData.state == QuestState.Unassigned)
            {
                m_background.gameObject.SetActive(false);
                m_lockedBackground.SetActive(true);
                return;
            }
            m_lockedBackground.SetActive(false);
            m_background.gameObject.SetActive(true);

            SetQuestData(localizer.LocalizeQuest(questData));
            m_name.Display(m_questData.name, m_questData.state == QuestState.Success);
        }
    }
}