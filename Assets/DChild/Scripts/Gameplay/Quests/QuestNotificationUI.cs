using UnityEngine;
using PixelCrushers.DialogueSystem;
using TMPro;
using Sirenix.OdinInspector;

namespace DChild.Gameplay.Quests
{

    public class QuestNotificationUI : NotificationUI
    {
        [SerializeField] private TextMeshProUGUI m_questTitle;
        [SerializeField] private TextMeshProUGUI m_subEntry;
        [SerializeField] private TextMeshProUGUI m_objective;
        //[SerializeField] private List<TextMeshProUGUI> m_questEntries;

        [SerializeField] private QuestStateUI m_stateUI;

        /// <summary >
        /// entryNumber will be treated as entryCount
        /// </summary>
        /// <param name="questInfo"></param>
        [Button]
        public void UpdateLog(QuestEntryArgs questInfo)
        {
            m_questTitle.text = questInfo.questName;

            if (questInfo.entryNumber > 0)
            {
                var subEntry = QuestLog.GetQuestEntry(questInfo.questName, questInfo.entryNumber);
                var subEntryState = QuestLog.GetQuestEntryState(questInfo.questName, questInfo.entryNumber);
                m_subEntry.text = subEntry;
                m_stateUI.Display(subEntryState);
                var instructions = DialogueManager.MasterDatabase.GetItem(questInfo.questName).LookupValue($"Entry {questInfo.entryNumber} Description");
                m_objective.text = instructions;
            }
            else
            {
                m_subEntry.text = "";
                m_stateUI.Display(QuestLog.GetQuestState(questInfo.questName));

                var instructions = QuestLog.GetQuestDescription(questInfo.questName);
                m_objective.text = instructions;
            }
        }
    }
}