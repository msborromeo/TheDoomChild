using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DChild.Codex.Quests.UI
{
    public class QuestProgressContentUI : MonoBehaviour
    {
        [BoxGroup("Location Details"), SerializeField] private GameObject m_locationField;
        [BoxGroup("Location Details"), SerializeField] private TextMeshProUGUI m_locationValuePanel;
        [BoxGroup("Conversation Details"), SerializeField] private TextMeshProUGUI m_descriptionPanel;
        [BoxGroup("Conversation Details"), SerializeField] private List<QuestConversationUI> m_UIPanels;

        private DChild.Gameplay.Environment.Location m_currentQuestLocation;

        public void Display(QuestEntry entry)
        {
            m_locationField.SetActive(true);
            m_locationValuePanel.text = "Integration in progress...";
            //m_locationValuePanel.text = m_currentQuestLocation.ToString().Replace("_", " ");

            m_descriptionPanel.text = entry.description;

            //Figure Out conversations;
        }

        public void Reset()
        {
            m_locationValuePanel.text = "";
            m_locationField.SetActive(false);

            m_descriptionPanel.text = "";
        }
    }
}