using PixelCrushers.DialogueSystem;
using TMPro;
using UnityEngine;

namespace DChild.Gameplay.Quests
{
    public class QuestStateUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_inProgress;
        [SerializeField] private TextMeshProUGUI m_completed;

        public TextMeshProUGUI completedLabel => m_completed;

        private bool m_isMainQuest;
        public bool isMainQuest => m_isMainQuest;

        public void SetIsMainQuest(bool value) => m_isMainQuest = value;

        public void Display(QuestState quest)
        {
            m_completed.gameObject.SetActive(m_isMainQuest);
            m_inProgress.gameObject.SetActive(!m_isMainQuest);
            
            switch (quest)
            {
                case QuestState.Active:
                    m_inProgress.text = "IN PROGRESS";
                    m_completed.text = "STARTED";

                    if (m_isMainQuest)
                        m_isMainQuest = false;
                    break;

                case QuestState.Success:
                    m_completed.text = "COMPLETE";
                    m_inProgress.text = "COMPLETE";
                    break;
            }
        }
    }
}