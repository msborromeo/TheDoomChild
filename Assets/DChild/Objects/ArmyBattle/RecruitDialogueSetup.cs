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
        bool m_hasAfterRecruitDialogue = false;
        [SerializeField]
        private bool m_ShowSetup;
        [SerializeField, ShowIf("m_ShowSetup")]
        GameObject m_RecruitmentEssentials;
        [SerializeField, ShowIf("m_ShowSetup")]
        ArmyRecruitInteract m_RecruitInteract;
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

        private string NPCname;

        [Button]
        private void InitializeDatabase()
        {
            NPCname = database.name.Replace("DialogueDatabase_", "");
            NPCname = NPCname.Replace("Recruit", "");
            Template temp = new Template();
            Variable FirsttimeTalk = temp.CreateVariable(temp.GetNextVariableID(database), (NPCname + "_FirstTimeTalk"),"");
            FirsttimeTalk.InitialBoolValue = true;
            if(!database.variables.Contains(FirsttimeTalk))
            {
                database.variables.Add(FirsttimeTalk);
            }
            
            var RequirementsMet = temp.CreateConversation(temp.GetNextConversationID(database),"Recruit/Requirement" + NPCname + "/Met");
            database.AddConversation(RequirementsMet);
            var RequirementsUnMet = temp.CreateConversation(temp.GetNextConversationID(database), "Recruit/Requirement" + NPCname + "/UnMet");
            database.AddConversation(RequirementsUnMet);
            
            SelfSetup();

            GameObject x = Instantiate(m_RecruitmentEssentials, m_RecruitableNPC.transform.GetChild(0).transform.GetChild(0));
            x.name = x.name.Replace("(Clone)", "");


            m_RecruitInteract = x.GetComponent<ArmyRecruitInteract>();
            m_RecruitInteract.SetCollider(m_RecruitableNPC.GetComponentInChildren<Collider2D>());
            m_RecruitInteract.HasAfterRecruitDialogue(m_hasAfterRecruitDialogue);
        }
        [Button, ShowIf("m_ShowSetup")]
        private void SelfSetup()
        {
            
            string FirstTalkVariableName = (String)NPCname + "_FirstTimeTalk";
            m_FirstTimeTalk.selectedDatabase = database;
            m_FirstTimeTalk.luaCode = "Variable[\""+FirstTalkVariableName+"\"] = false";
            m_FirstTimeTalk.conversation = "Recruit/"+ NPCname;

            m_FirstTimeTalkAfterConversation.selectedDatabase = database;

            m_RequirementsMet.selectedDatabase = database;
            m_RequirementsMet.luaCode = "Variable[\"HasRecruited_"+ NPCname + "\"]=true";
            m_RequirementsMet.conversation = "Recruit/Requirement" + NPCname + "/Met";

            m_RequirementsMetAfter.selectedDatabase = database;

            m_RequirementsUnMet.selectedDatabase= database;
            m_RequirementsUnMet.conversation = "Recruit/Requirement" + NPCname + "/UnMet";

            m_Recruited.selectedDatabase = database;

            m_interact.SetDatabase(database);
            m_interact.SetVariable(FirstTalkVariableName);

            
        }

    }
}

