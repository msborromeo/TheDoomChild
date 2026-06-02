using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DChild.Codex.Quests.UI
{
    public class QuestProgressContentUI : MonoBehaviour
    {
        [BoxGroup("Location Details"), SerializeField] private TextMeshProUGUI m_locationValuePanel;
        [BoxGroup("Conversation Details"), SerializeField] private TextMeshProUGUI m_descriptionPanel;
        [BoxGroup("Conversation Details"), SerializeField] private List<QuestConversationUI> m_UIPanels;

        public void Display(QuestEntry entry)
        {
            m_locationValuePanel.transform.parent.gameObject.SetActive(true);
            m_descriptionPanel.text = entry.description;

            //Figure Out conversations;
        }

        public void Reset()
        {
            m_locationValuePanel.text = "";
            m_locationValuePanel.transform.parent.gameObject.SetActive(false);

            m_descriptionPanel.text = "";
        }
    }
}