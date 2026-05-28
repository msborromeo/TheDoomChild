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
        [SerializeField] private QuestIndexHandle m_indexHandle;
        [SerializeField] private QuestProgressIndexHandle m_progressIndexHandle;
        [SerializeField] private QuestProgressContentUI m_progressContent;

        private Quest[] m_completeList;

        public QuestLogDataList questList => m_questList;
        public QuestIndexHandle indexHandle => m_indexHandle;

        public void Select(QuestButtonUI button)
        {
            m_progressIndexHandle.Display(button.questData);
        }

        public void Select(QuestProgressUI button) => m_progressContent.Display(button.entry);

        private void ResetDisplay() => m_progressIndexHandle.ResetButtons();

        public void Initialize()
        {
            ResetDisplay();

            m_completeList = m_questList.mainQuests.Concat(m_questList.sideQuests).ToArray();

            m_indexHandle.Initialize(m_completeList);
        }
    }
}