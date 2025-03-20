using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Codex.Quests.UI
{
    public class QuestLogUIManager : MonoBehaviour
    {
        //[BoxGroup("Section Toggles"), SerializeField] private QuestTypeToggleUI m_mainToggle;
        //[BoxGroup("Section Toggles"), SerializeField] private QuestTypeToggleUI m_sideToggle;

        [SerializeField] private QuestLogDataList m_questList;
        [SerializeField] private QuestIndexHandle m_indexHandle;

        public QuestLogDataList questList => m_questList;
        public QuestIndexHandle indexHandle => m_indexHandle;
    }
}