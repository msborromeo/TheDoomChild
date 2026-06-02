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

        private UIButton m_button;

        public Quest questData => m_questData;

        public QuestDataLocalizer localizer;


        private void SetQuestData(Quest data) => m_questData = data;


        private void EnsureReference()
        {
            m_button = gameObject.GetComponent<UIButton>();
        }

        public void Display(Quest questData, bool debugReveal = false)
        {
            var hasData = questData != null;

            EnsureReference();
            m_button.interactable = hasData || debugReveal;

            //if (!hasData || questData.state == QuestState.Unassigned)
            if (!hasData)
                return;

            SetQuestData(localizer.LocalizeQuest(questData));
            m_name.text = m_questData.name;
        }
    }
}