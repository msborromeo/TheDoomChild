using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DChild.Codex.Quests.UI
{
    public class QuestProgressContentUI : MonoBehaviour
    {
        [BoxGroup("Conversation Details"), SerializeField] private TextMeshProUGUI m_descriptionPanel;
        [BoxGroup("Conversation Details"), SerializeField] private List<QuestConversationUI> m_UIPanels;

        public void Display(QuestEntry entry)
        {
            m_descriptionPanel.text = entry.description;

            //Figure Out conversations;
        }
    }
}