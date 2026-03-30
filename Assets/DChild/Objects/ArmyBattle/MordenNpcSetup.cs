using DChild.ArmyBattle.Recruitment;
using DChild.Gameplay.Characters.NPC;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace DChild.ArmyBattle.Recruited {
    public class MordenNpcSetup : MonoBehaviour
    {
        [SerializeField]
        DialogueDatabase database;
        //[SerializeField,Tooltip("Toggle when using an existing one that has already set-up its quest listiners and extra database in the scene")]
        private bool m_ReplaceExisting = true;
        //[SerializeField,HideIf("m_ReplaceExisting")]
        //GameObject m_BaseNPC;
        [SerializeField, ShowIf("m_ReplaceExisting")]
        GameObject m_NpcInteractable;
        [SerializeField, Tooltip("DONT TOUCH")]
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
        private DialogueSystemTrigger m_ChatLoop;
        [SerializeField, TabGroup("DialogueSystemTriggers"), ShowIf("m_ShowSetup")]
        private ArmyRecruitInteract m_interact;

        private string NPCname;

        [Button]
        private void InitializeDatabase()
        {
            NPCname = database.name.Replace("DialogueDatabase_", "");
            NPCname = NPCname.Replace("_Base Variant", "").Replace("Recruit","");
            Template temp = new Template();
            Variable FirsttimeTalk = temp.CreateVariable(temp.GetNextVariableID(database), (NPCname + "_MordenFirstTimeTalk"), "");
            FirsttimeTalk.InitialBoolValue = true;
            if (!database.variables.Contains(FirsttimeTalk))
            {
                database.variables.Add(FirsttimeTalk);
            }

            var MordenInitalChat = temp.CreateConversation(temp.GetNextConversationID(database), NPCname + "_MordenRemarks");
            database.AddConversation(MordenInitalChat);
            var MordenChatLoop = temp.CreateConversation(temp.GetNextConversationID(database), NPCname + "_MordenLoops");
            database.AddConversation(MordenChatLoop);

            SelfSetup();
            //if(m_ReplaceExisting)
            //{
                PrefabUtility.UnpackPrefabInstance(
                m_NpcInteractable,
                PrefabUnpackMode.OutermostRoot, // Unpacks all nested prefabs as well
                InteractionMode.UserAction
                );
                DestroyImmediate(m_NpcInteractable.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).gameObject);

                GameObject x = Instantiate(m_RecruitmentEssentials, m_NpcInteractable.transform.GetChild(0).transform.GetChild(0));
                x.name = x.name.Replace("(Clone)", "");

                m_NpcInteractable.transform.GetChild(0).transform.GetChild(0).gameObject.name = m_NpcInteractable.transform.GetChild(0).transform.GetChild(0).name.Replace("Recruitable", "NpcInteractable");
            //}
            /*else
            {
                GameObject x = Instantiate(m_RecruitmentEssentials, m_BaseNPC.transform);
                x.name = x.name.Replace("(Clone)", "");


                m_RecruitInteract = x.GetComponent<ArmyRecruitInteract>();
                m_RecruitInteract.SetCollider(m_BaseNPC.GetComponentInChildren<Collider2D>());
                m_RecruitInteract.HasAfterRecruitDialogue(true);
                PrefabUtility.UnpackPrefabInstance(
                    m_BaseNPC,
                    PrefabUnpackMode.Completely, // Unpacks all nested prefabs as well
                    InteractionMode.UserAction
                    );

                m_BaseNPC.name = "M_" + NPCname;
            }*/
           
        }

        [Button, ShowIf("m_ShowSetup")]
        private void SelfSetup()
        {

            string FirstTalkVariableName = (String)NPCname + "_MordenFirstTimeTalk";
            m_FirstTimeTalk.selectedDatabase = database;
            m_FirstTimeTalk.luaCode = "Variable[\"" + FirstTalkVariableName + "\"] = false";
            m_FirstTimeTalk.conversation =  NPCname+ "_MordenRemarks";

            m_FirstTimeTalkAfterConversation.selectedDatabase = database;

            m_ChatLoop.selectedDatabase = database;
            m_ChatLoop.conversation = NPCname + "_MordenLoops";

            m_interact.SetDatabase(database);
            m_interact.SetVariable(FirstTalkVariableName);


        }

       
    }

}

