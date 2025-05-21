using DarkTonic.MasterAudio.Examples;
using DChild.Gameplay;
using Doozy.Runtime.Signals;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace DChild.UI
{
    public class DialogueSignalHandler : MonoBehaviour
    {
        [SerializeField]
        private SignalSender m_dialogueIntervalSignal;
        [SerializeField]
        private SignalSender m_dialogueEndSignal;

        private static bool m_checker;
        public void CheckSignal()
        {

            //m_conversation = DialogueManager.MasterDatabase.GetConversation(DialogueManager.lastConversationStarted);
            //m_currentDialogue = DialogueManager.

            //check if game is in cutscene
            if (GameplaySystem.gamplayUIHandle.isInCutsceneMode)
            {
                m_dialogueIntervalSignal.SendSignal();
                return;
            }
            
            m_dialogueEndSignal.SendSignal();
        }
    }
}