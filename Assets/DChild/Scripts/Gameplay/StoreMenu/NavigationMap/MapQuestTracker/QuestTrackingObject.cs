using DChild.Gameplay.Databases;
using DChild.Gameplay.Environment;
using NUnit.Framework;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DChild.Tracker.QuestTrackingObject
{
    public class QuestTrackingObject : MonoBehaviour
    {
        [SerializeField]
        DialogueDatabase database;
        [SerializeField,Tooltip("If on, WILL ONLY CHECK THE ENTRY QUEST OF THE FIRST QUEST")]
        bool IsAQuestEntry;
        [SerializeField, ValueDropdown("GetQuests", IsUniqueList = true, SortDropdownItems = true)]
        private string Quest;
        [SerializeField, ValueDropdown("GetQuestEntry", IsUniqueList = true, SortDropdownItems = true),ShowIf("IsAQuestEntry")]
        private string QuestEntry;
        [SerializeField]
        private QuestState ExpectedQuestState = QuestState.Success;
        [SerializeField]
        private UnityEvent IfExpectedQuestState, IfNotExpectedQuestState;
        QuestState m_queststate;
        
        private IEnumerable GetQuests()
        {
            ValueDropdownList<string> list = new ValueDropdownList<string>();

            foreach (var Quest in database.items)
            {
                list.Add(Quest.Name);
            }
            return list;
        }

        private IEnumerable GetQuestEntry()
        {
            ValueDropdownList<string> list = new ValueDropdownList<string>();
            foreach (var Q in database.items)
            {
                if(Q.Name == Quest)
                {
                    int entries = Q.LookupInt("Entry Count");
                    if (entries > 0)
                    {
                        for (int x = 0; x < entries; x++)
                        {
                            list.Add(Q.LookupValue("Entry " + (x + 1)));
                        }
                    }
                    break;
                } 
            }
            return list;
        }



        private void Start()
        {
            if(IsAQuestEntry)
            {
                foreach (var Q in database.items)
                {
                    if (Q.LookupInt("Entry Count") < 1) return;

                    if (Q.Name == Quest)
                    {
                        QuestLog.GetQuestEntryState(Quest,1);
                        break;
                    }
                }
            }else
            {
                m_queststate = QuestLog.GetQuestState(Quest);
            }

            if(m_queststate == ExpectedQuestState)
            {
                IfExpectedQuestState?.Invoke();
            }else 
            if(m_queststate!=ExpectedQuestState)
            {
                IfNotExpectedQuestState?.Invoke();
            }
           
        }
    }
}
