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
        [SerializeField] private TextMeshProUGUI m_questTitle;
        [SerializeField] private List<TextMeshProUGUI> m_questEntries;


        /// <summary>
        /// entryNumber will be treated as entryCount
        /// </summary>
        /// <param name="questInfo"></param>
        [Button]
        public void UpdateLog(QuestEntryArgs questInfo)
        {
            ResetEntries();
            m_questTitle.text = FormattedText.Parse(questInfo.questName).text;
            
            var entryCount = questInfo.entryNumber;

            for (int i = 0; i < entryCount; i++)
            {
                m_questEntries[i].text = "";
                if (questInfo.entryNumber >= 0)
                {
                    var parent = m_questEntries[i].transform.parent;
                    parent.gameObject.SetActive(true);

                    var entryName = QuestLog.GetQuestEntry(questInfo.questName, i + 1);
                    m_questEntries[i].text = FormattedText.Parse(entryName).text;
                }                
            }
        }

        [Button]
        public void Display(QuestEntryArgs questInfo)
        {
            var questTitle = FormattedText.Parse(questInfo.questName).text;
            var subEntry = QuestLog.GetQuestEntry(questInfo.questName, 0);

        }

        private void ResetEntries()
        {
            foreach ( var entry in m_questEntries)
            {
                entry.text = "";
                
                var parent = entry.transform.parent;
                parent.gameObject.SetActive(false);
            }
        }

    }
}