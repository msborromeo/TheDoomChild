using UnityEngine;
using PixelCrushers.DialogueSystem;
using TMPro;
using Sirenix.OdinInspector;

namespace DChild.Gameplay.Quests
{

    public class QuestNotificationUI : NotificationUI
    {

        [TabGroup("Main Quest"), SerializeField] private TextMeshProUGUI m_mainQuestProgressLabel;

        [TabGroup("Sub Entry"), SerializeField] private GameObject m_subEntrySection;
        [TabGroup("Sub Entry"), SerializeField] private TextMeshProUGUI m_questTitle;
        [TabGroup("Sub Entry"), SerializeField] private TextMeshProUGUI m_subEntry;

        //[SerializeField] private TextMeshProUGUI m_objective;
        //[SerializeField] private List<TextMeshProUGUI> m_questEntries;

        [SerializeField] private QuestStateUI m_stateUI;

        /// <summary >
        /// entryNumber will be treated as entryCount
        /// </summary>
        /// <param name="questInfo"></param>
        [Button]
        public void UpdateLog(QuestEntryArgs questInfo)
        {
            QuestState questState;

            m_questTitle.text = questInfo.questName;

            if (questInfo.entryNumber > 0)
            {
                m_mainQuestProgressLabel.gameObject.SetActive(false);
                m_subEntrySection.SetActive(true);

                var subEntry = QuestLog.GetQuestEntry(questInfo.questName, questInfo.entryNumber);
                questState = QuestLog.GetQuestEntryState(questInfo.questName, questInfo.entryNumber);
                m_subEntry.text = subEntry;

                m_stateUI.Display(questState);
                return;
                //var instructions = DialogueManager.MasterDatabase.GetItem(questInfo.questName).LookupValue($"Entry {questInfo.entryNumber} Description");
                //m_objective.text = instructions;
            }

            questState = QuestLog.GetQuestState(questInfo.questName);
            m_mainQuestProgressLabel.text = questInfo.questName;

            m_subEntrySection.SetActive(false);
            m_mainQuestProgressLabel.gameObject.SetActive(true);

            m_stateUI.SetIsMainQuest(true);
            m_stateUI.Display(questState);

            //var instructions = QuestLog.GetQuestDescription(questInfo.questName);
            //m_objective.text = instructions;
        }
    }
}