using TMPro;
using UnityEngine;

namespace DChild.Codex.Quests.UI
{
    public class QuestNameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_label;
        [SerializeField] private TextMeshProUGUI m_completedText;

        public void Display(string name, bool isCompleted)
        {
            m_label.text = name ?? "";
            m_completedText.gameObject.SetActive(isCompleted);
        }
    }
}