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

        public void CheckSignal()
        {
            if (GameplaySystem.gamplayUIHandle.isInCutsceneMode)
            {
                m_dialogueIntervalSignal.SendSignal();
                return;
            }
            m_dialogueEndSignal.SendSignal();
        }
    }
}