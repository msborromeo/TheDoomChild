using Language.Lua;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DChild.Codex.Quest.UI
{
    public class QuestProgressUI : MonoBehaviour
    {

        [BoxGroup("TMP Fields"), SerializeField] private TextMeshProUGUI m_questOrder;
        [BoxGroup("TMP Fields"), SerializeField] private TextMeshProUGUI m_questName;
        [BoxGroup("TMP Fields"), SerializeField] private TextMeshProUGUI m_questStatus;

        [BoxGroup("Conversation Details"), SerializeField] private TextMeshProUGUI m_descriptionPanel;
        [BoxGroup("Conversation Details"), SerializeField] private List<ConversationData> m_conversationList;
        [BoxGroup("Conversation Details"), SerializeField] private List<QuestConversationUI> m_UIPanels;


        private string m_currentQuestDescription;

        private void SetQuestDescription(string description) => m_currentQuestDescription = description;

        public void Display(QuestProgressData quest, int index)
        {
            m_questOrder.text = $"{toRomanNumeral(index+1)}";
            m_questName.text = quest.sectionName;
            m_questStatus.text = $"{quest.status}".Replace("_", " ");
            SetQuestDescription(quest.description);
        }

        [Button(ButtonSizes.Large)]
        public void ShowDialogueHistory()
        {
            m_descriptionPanel.text = m_currentQuestDescription;
            for (int i = m_conversationList.Count - 1; i >= 0; i--)
            {
                ConversationData entry = m_conversationList[i];
                if(i < m_conversationList.Count)
                m_UIPanels[i].Display(entry);

            }
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