using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace DChild.Codex.Quest.UI
{
    public class QuestButtonUI : MonoBehaviour
    {
        [SerializeField] private QuestTypeBackgroundUI m_background;
        [SerializeField] private QuestNameUI m_name;

        //[SerializeField] private bool m_isMainQuest;
        //[SerializeField] private bool m_isCompleted;

        [Button(ButtonSizes.Large)]
        public void Display(SampleDummyQuestData questData)
        {
            m_background.SetBackground(questData.isMainQuest);
            m_name.Display(questData.questName, questData.status == QuestStatus.Completed);
        }
    }
}