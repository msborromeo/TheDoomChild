using Language.Lua;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DChild.Localization;
using System;

namespace DChild.Codex.Quests.UI
{

    public class QuestProgressUI : MonoBehaviour, IQuestDataLocalize
    {

        [BoxGroup("TMP Fields"), SerializeField] private TextMeshProUGUI m_questOrder;
        [BoxGroup("TMP Fields"), SerializeField] private TextMeshProUGUI m_questName;
        [BoxGroup("TMP Fields"), SerializeField] private TextMeshProUGUI m_questStatus;

        [BoxGroup("Conversation Details"), SerializeField] private TextMeshProUGUI m_descriptionPanel;
        //[BoxGroup("Conversation Details"), SerializeField] private List<ConversationData> m_conversationList;
        [BoxGroup("Conversation Details"), SerializeField] private List<QuestConversationUI> m_UIPanels;


        private string m_currentQuestDescription;
        private QuestEntry m_entry;

        public QuestEntry entry => m_entry;

        public event Action<QuestEntry, int> LocalizeEntry;
        void OnDisable()
        {
            ResetButton();
        }
        private void SetQuestDescription(string description)
        {
            m_currentQuestDescription = description;
            m_descriptionPanel.text = description;
        }

        public void Display(QuestEntry entry, int index)
        {
            m_entry = entry;
            m_questOrder.text = $"{toRomanNumeral(index + 1)}";
            m_questName.text = entry.name;
            m_questStatus.text = $"{entry.state}".Replace("_", " ");
            SetQuestDescription(entry.description);

            LocalizeEntry?.Invoke(entry, index);
        }

        [Button(ButtonSizes.Large)]
        public void ShowDialogueHistory()
        {
            m_descriptionPanel.text = m_currentQuestDescription;
            //for (int i = m_conversationList.Count - 1; i >= 0; i--)
            //{
            //    //ConversationData entry = m_conversationList[i];
            //    //if(i < m_conversationList.Count)
            //    //m_UIPanels[i].Display(entry);
            //}
        }

        void ResetButton()
        {
            m_questOrder.text = null;
            m_questName.text = null;
            m_questStatus.text = null;
            m_currentQuestDescription = null;
            m_descriptionPanel.text = null;
        }


        private static string toRomanNumeral(int number)
        {
            if (number < 1 || number > 10) return "N/A";
            var romanNumerals = new[] { (10, "X"), (9, "IX"), (5, "V"), (4, "IV"), (1, "I") };
            var result = new System.Text.StringBuilder();
            foreach (var (value, numeral) in romanNumerals)
                while (number >= value) { result.Append(numeral); number -= value; }
            return result.ToString();
        }
    }
}