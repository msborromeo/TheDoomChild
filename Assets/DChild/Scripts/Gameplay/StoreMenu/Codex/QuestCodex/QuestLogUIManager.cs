using DG.Tweening;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

namespace DChild.Codex.Quests.UI
{
    public class QuestLogUIManager : MonoBehaviour
    {
        [SerializeField] private QuestLogDataList m_questList;
        public QuestLogDataList questList => m_questList;

        [SerializeField] private QuestIndexHandle m_indexHandle;
        [SerializeField] private QuestProgressIndexHandle m_progressIndexHandle;
        [SerializeField] private QuestProgressContentUI m_progressContent;

        [SerializeField, BoxGroup("EDITOR ONLY")] private bool m_revealAllQuests;

        private Quest[] m_completeList;
        public void Select(QuestButtonUI button)
        {
            ResetDisplay();
            m_progressIndexHandle.Display(button.questData);
        }

        public void Select(QuestProgressUI button) => m_progressContent.Display(button.entry);

        private void ResetDisplay() => m_progressIndexHandle.ResetSubEntryUIs();

        public void Initialize()
        {
            ResetDisplay();

            // Ensure arrays are not null before concatenating
            var mains = m_questList.mainQuests ?? new Quest[0];
            var sides = m_questList.sideQuests ?? new Quest[0];

            m_completeList = mains.Concat(sides).ToArray();
            m_indexHandle.Initialize(m_completeList, m_revealAllQuests);
        }
    }
}