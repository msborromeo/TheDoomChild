using PixelCrushers.DialogueSystem;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    [System.Serializable]
    public class WaitForDialogue : ISpecialSkillIEnumeratorModule
    {
        [SerializeField]
        private String m_dialoguetitle = null;
        [SerializeField]
        private int m_initialDialogueEntryID = 0;
        private bool m_activedialogue = false;
        public IEnumerator ApplyEffect(ArmyController owner, ArmyController target)
        {
            DialogueManager.instance.StartConversation(m_dialoguetitle, null, null, m_initialDialogueEntryID);
            DialogueManager.instance.conversationEnded += OnConversationEnd;
            while (m_activedialogue)
            {
                yield return null;
            }

            Debug.Log("Dialogue finish!");
    

        }
       
        private void OnConversationEnd(Transform t)
        {
            m_activedialogue = true;
        }


        public IEnumerator RemoveEffect(ArmyController owner, ArmyController target)
        {
            yield return null;
        }

    }
}
