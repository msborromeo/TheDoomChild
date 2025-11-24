using DChild.Gameplay.ArmyBattle;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.ArmyBattle.Recruitment
{
    public class RecruitDialogueSetup : MonoBehaviour
    {
        [SerializeField]
        DialogueDatabase database;
        [SerializeField]
        GameObject m_RecruitableNPC;
        [SerializeField]
        GameObject m_RecruitmentEssentials;
        [SerializeField]
        private bool m_ShowSetup;
        [SerializeField, TabGroup("DialogueSystemTriggers"), ShowIf("m_ShowSetup")]
        private DialogueSystemTrigger m_FirstTimeTalk;
        [SerializeField, TabGroup("DialogueSystemTriggers"), ShowIf("m_ShowSetup")]
        private DialogueSystemTrigger m_FirstTimeTalkAfterConversation;
        [SerializeField, TabGroup("DialogueSystemTriggers"), ShowIf("m_ShowSetup")]
        private DialogueSystemTrigger m_RequirementsMet;
        [SerializeField, TabGroup("DialogueSystemTriggers"), ShowIf("m_ShowSetup")]
        private DialogueSystemTrigger m_RequirementsMetAfter;
        [SerializeField, TabGroup("DialogueSystemTriggers"), ShowIf("m_ShowSetup")]
        private DialogueSystemTrigger m_RequirementsUnMet;
        [SerializeField, TabGroup("DialogueSystemTriggers"), ShowIf("m_ShowSetup")]
        private DialogueSystemTrigger m_Recruited;
        [SerializeField, TabGroup("DialogueSystemTriggers"), ShowIf("m_ShowSetup")]
        private ArmyRecruitInteract m_interact;

        [Button]
        private void InitializeDatabase()
        {
            Template temp = new Template();
            Variable FirsttimeTalk = temp.CreateVariable(temp.GetNextVariableID(database), (database.name.Replace("DialogueDatabase_", "") + "_FirstTimeTalk"),"");
            FirsttimeTalk.InitialBoolValue = true;
            if(!database.variables.Contains(FirsttimeTalk))
            {
                database.variables.Add(FirsttimeTalk);
            }
            
            var RequirementsMet = temp.CreateConversation(temp.GetNextConversationID(database),"Requirement/Met");
            database.AddConversation(RequirementsMet);
            var RequirementsUnMet = temp.CreateConversation(temp.GetNextConversationID(database), "Requirement/UnMet");
            database.AddConversation(RequirementsUnMet);
            
            SelfSetup();

            GameObject x = Instantiate(m_RecruitmentEssentials, m_RecruitableNPC.transform.GetChild(0).transform.GetChild(0));
            x.name = x.name.Replace("(Clone)", "");
        }

        private void SelfSetup()
        {
            string NPCname = database.name.Replace("DialogueDatabase_", "");
            string FirstTalkVariableName = (String)NPCname + "_FirstTimeTalk";
            m_FirstTimeTalk.selectedDatabase = database;
            m_FirstTimeTalk.luaCode = "Variable[\""+FirstTalkVariableName+"\"] = false";
            m_FirstTimeTalk.conversation = "Recruit/"+ NPCname;

            m_FirstTimeTalkAfterConversation.selectedDatabase = database;

            m_RequirementsMet.selectedDatabase = database;
            m_RequirementsMet.luaCode = "Variable[\"HasRecruited_"+ NPCname.Replace("Recruit","") + "\"]=true";
            m_RequirementsMet.conversation = "Requirement/Met";

            m_RequirementsMetAfter.selectedDatabase = database;

            m_RequirementsUnMet.selectedDatabase= database;
            m_RequirementsUnMet.conversation = "Requirement/UnMet";

            m_Recruited.selectedDatabase = database;

            m_interact.SetDatabase(database);
            m_interact.SetVariable(FirstTalkVariableName);
        }

    }
}

