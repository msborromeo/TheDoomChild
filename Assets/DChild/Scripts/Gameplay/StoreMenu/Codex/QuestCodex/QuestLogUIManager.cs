using DG.Tweening;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Linq;
using TMPro;
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

        [SerializeField, BoxGroup("Quest Type")] private TextMeshProUGUI m_activeHeaderTMP;
        [SerializeField, BoxGroup("Quest Type")] private TextMeshProUGUI m_availableSubHeaderTMP;

        [SerializeField] private TextMeshProUGUI m_questTitleTMP;

        [SerializeField, BoxGroup("EDITOR ONLY")] private bool m_revealAllQuests;

        private Quest[] m_filteredQuestList;
        public void Select(QuestButtonUI button)
        {
            ResetDisplay();
            m_progressIndexHandle.Display(button.questData, m_revealAllQuests);
            m_questTitleTMP.text = button.questData.name;
        }

        public void Select(QuestProgressUI button) => m_progressContent.Display(button.entry);

        private void ResetDisplay()
        {
            m_questTitleTMP.text = "";
            m_progressContent.Reset();
            m_progressIndexHandle.ResetSubEntryUIs();
        }

        public void Initialize()
        {
            ResetDisplay();
            ToggleQuests(true);
        }

        public void ToggleQuests(bool isMain)
        {
            m_filteredQuestList = isMain ? m_questList.mainQuests : m_questList.sideQuests;
            UpdateHeaders(isMain);
            DisplayQuestList();
        }

        private void UpdateHeaders(bool isMain)
        {
            m_activeHeaderTMP.text = isMain ? "Main Quests" : "Side Quests";
            m_availableSubHeaderTMP.text = !isMain ? "Main Quests" : "Side Quests";
        }

        private void DisplayQuestList() => m_indexHandle.Initialize(m_filteredQuestList, m_revealAllQuests);


    }
}