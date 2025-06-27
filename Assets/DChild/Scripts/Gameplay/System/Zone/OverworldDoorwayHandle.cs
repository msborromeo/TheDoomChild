using DChild.Gameplay.Characters;
using Doozy.Runtime.Signals;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Environment
{
    [System.Serializable]
    public class OverworldDoorwayHandle : ISwitchHandle
    {
        [SerializeField]
        private SignalSender m_showTransistionSignal;
        [SerializeField]
        private SignalSender m_hideTransistionSignal;
        [SerializeField]
        private Transform m_promptSource;
        [SerializeField]
        private Vector3 m_promptOffset;

        public float transitionDelay => 1.2f;

        public bool needsButtonInteraction => true;

        public Vector3 promptPosition => m_promptSource.position + m_promptOffset;

        public string prompMessage => "Enter";

        public bool isDebugSwitchHandle => false;

        public void DoSceneTransition(Character character, TransitionType type)
        {
            switch (type)
            {
                case TransitionType.Enter:
                    OnDoorwayEnter(character);
                    break;
                case TransitionType.PostEnter:
                    OnDoorwayPostEnter(character);
                    break;
                case TransitionType.Exit:
                    OnDoorwayExit(character);
                    break;
                case TransitionType.PostExit:
                    OnDoorwayPostExit();
                    break;
            }
        }

        private void OnDoorwayEnter(Character character)
        {
            GameplaySystem.campaignSerializer.UpdateDialogueSaveData();
            m_showTransistionSignal.SendSignal();
            Debug.Log("Entered an Overworld Doorway");
        }

        private void OnDoorwayPostEnter(Character character)
        {
            Debug.Log("Post Entered Overworld Doorway");
        }

        private void OnDoorwayExit(Character character)
        {
            m_hideTransistionSignal.SendSignal();
            Debug.Log("Exit Overworld Doorway");
        }

        private void OnDoorwayPostExit()
        {
            Debug.Log("Post Exited Overworld Doorway");
        }
    }
}

