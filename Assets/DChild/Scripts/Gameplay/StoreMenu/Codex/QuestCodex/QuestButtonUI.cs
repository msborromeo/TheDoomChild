using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using DChild.Localization;
using Doozy.Runtime.UIManager.Components;
using TMPro;

namespace DChild.Codex.Quests.UI
{
    public class QuestButtonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_name;

        private Quest m_questData;
        private int m_selectionIndex;

        private UIButton m_button;

        public Quest questData => m_questData;

        public virtual int selectionIndex => m_selectionIndex;

        public QuestDataLocalizer localizer;


        public void SetSelectionIndex(int index) => m_selectionIndex = index;
        private void SetQuestData(Quest data) => m_questData = data;


        private void EnsureReference()
        {
            m_button = gameObject.GetComponent<UIButton>();
        }

        public void Display(Quest questData)
        {
            EnsureReference();
            m_button.interactable = questData != null;

            if (questData == null || questData.state == QuestState.Unassigned)
                return;

            SetQuestData(localizer.LocalizeQuest(questData));
            m_name.text = m_questData.name;
        }
    }
}