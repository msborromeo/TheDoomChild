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

        public void Select() => m_button.Select();

        private void EnsureReference()
        {
            m_button = GetComponent<UIButton>();
        }

        public void SetInteractability(bool value)
        {
            m_button.interactable = value;
        }

        public void Display(Quest questData)
        {
            EnsureReference();
            var hasData = questData != null;

            if (!hasData)
                return;

            SetQuestData(localizer.LocalizeQuest(questData));
            m_name.text = m_questData.name;
        }

        private void Awake()
        {
            EnsureReference();
        }
    }
}