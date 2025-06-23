using PixelCrushers.DialogueSystem;
using TMPro;
using UnityEngine;

namespace DChild.Gameplay.Quests
{
    public class QuestStateUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_inProgress;
        [SerializeField] private TextMeshProUGUI m_completed;

        private bool m_isComplete;
        public bool isComplete => m_isComplete;

        public void Display(QuestState quest)
        {
            if (quest != QuestState.Success)
            {
                m_completed.gameObject.SetActive(false);
                m_inProgress.gameObject.SetActive(true);

                return;
            }
            m_inProgress.gameObject.SetActive(false);
            m_completed.gameObject.SetActive(true);
        }
    }
}