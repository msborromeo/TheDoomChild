using UnityEngine;
using PixelCrushers.DialogueSystem;
using TMPro;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using static UnityEngine.EventSystems.EventTrigger;

namespace DChild.Gameplay.Quests
{

    public class QuestNotificationUI : NotificationUI
    {
        [SerializeField] private QuestLogDataList m_dataList;

        [SerializeField] private TextMeshProUGUI m_questTitle;
        [SerializeField] private TextMeshProUGUI m_subEntry;
        [SerializeField] private TextMeshProUGUI m_objective;
        //[SerializeField] private List<TextMeshProUGUI> m_questEntries;

        [SerializeField] private QuestStateUI m_stateUI;

        /// <summary>
        /// entryNumber will be treated as entryCount
        /// </summary>
        /// <param name="questInfo"></param>
        [Button]
        public void UpdateLog(QuestEntryArgs questInfo)
        {
            var subEntry = QuestLog.GetQuestEntry(questInfo.questName, questInfo.entryNumber);
            var subEntryState = QuestLog.GetQuestEntryState(questInfo.questName, questInfo.entryNumber);

            m_questTitle.text = questInfo.questName;
            m_subEntry.text = subEntry;
            m_stateUI.Display(subEntryState);

            if (questInfo.entryNumber > 0)
            {
                //var quest = DialogueManager.databaseManager.masterDatabase.GetItem(questInfo.questName);

                //var instructions = quest.LookupLocalizedValue($"Entry {questInfo.entryNumber} Instructions")
                //    ?? "No objectives found.";


                var quest = m_dataList.GetQuest(questInfo.questName);
                var instructions = quest.LookupValue($"Entry {questInfo.entryNumber} Instructions");

                m_objective.text = instructions;
            }

            //ResetEntries();

            //var entryCount = questInfo.entryNumber;
            //for (int i = 0; i < entryCount; i++)
            //{
            //    m_questEntries[i].text = "";
            //    if (questInfo.entryNumber >= 0)
            //    {
            //        var parent = m_questEntries[i].transform.parent;
            //        parent.gameObject.SetActive(true);

            //        var entryName = QuestLog.GetQuestEntry(questInfo.questName, i + 1);
            //        m_questEntries[i].text = FormattedText.Parse(entryName).text;
            //    }
            //}
        }
        //private void ResetEntries()
        //{
        //    foreach ( var entry in m_questEntries)
        //    {
        //        entry.text = "";  
        //        var parent = entry.transform.parent;
        //        parent.gameObject.SetActive(false);
        //    }
        //}

    }
}