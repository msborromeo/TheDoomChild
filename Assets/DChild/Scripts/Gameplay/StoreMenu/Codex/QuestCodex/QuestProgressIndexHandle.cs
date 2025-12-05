using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace DChild.Codex.Quests.UI
{
    public class QuestProgressIndexHandle : MonoBehaviour
    {
        [SerializeField]
        private QuestProgressUI[] m_progressUIs;

        public void Display(Quest quest)
        {
            int count = quest.entryCount;
            for (int i = 0; i < m_progressUIs.Length; i++)
            {
                bool isActive = quest.GetEntry(i).state != QuestState.Unassigned;
                m_progressUIs[i].gameObject.SetActive(isActive);
                if (isActive)
                    m_progressUIs[i].Display(quest.GetEntry(i), i);
            }
        }
    }
}