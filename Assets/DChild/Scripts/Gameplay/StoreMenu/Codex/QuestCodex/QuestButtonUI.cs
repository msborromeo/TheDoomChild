using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace DChild.Codex.Quest.UI
{
    public class QuestButtonUI : MonoBehaviour
    {
        [SerializeField] private TypeBackgroundUI m_background;
        [SerializeField] private QuestNameUI m_name;
        [SerializeField] private bool m_isMainQuest;
        [SerializeField] private bool m_isCompleted;

        [Button(ButtonSizes.Large)]
        private void Display(string questName)
        {
            m_background.SetBackground(m_isMainQuest);
            m_name.Display(questName, m_isCompleted);
        }
    }
}