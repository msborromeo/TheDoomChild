using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay
{
    public class LevelCompleteHandle : MonoBehaviour
    {
        [SerializeField]
        private DialogueSystemTrigger m_levelCompleteTrigger;

        public void LevelComplete()
        {
            m_levelCompleteTrigger.OnUse();
            //TODO: call UI for level complete from Underworld Gameplay System
        }
    }
}

