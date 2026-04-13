using DarkTonic.MasterAudio.Examples;
using DChild.Gameplay;
using Doozy.Runtime.Signals;
using PixelCrushers.DialogueSystem;
using System.Collections;
using UnityEngine;

namespace DChild.UI
{
    public class DialogueSignalHandler : MonoBehaviour
    {
        [SerializeField]
        private SignalSender m_dialogueIntervalSignal;
        [SerializeField]
        private SignalSender m_dialogueEndSignal;

        private IEnumerator BufferSend()
        {
            yield return new WaitForEndOfFrame();

            if (GameplaySystem.gamplayUIHandle.isInCutsceneMode)
            {
                m_dialogueIntervalSignal.SendSignal();
                yield return null;
            }
            m_dialogueEndSignal.SendSignal();
        }
        public void CheckSignal()
        {
            StartCoroutine(BufferSend());
        }
    }
}