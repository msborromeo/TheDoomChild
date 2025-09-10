using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Dialogues
{
    public class SoulLampBanterCooldownHandle : MonoBehaviour
    {
        [SerializeField, VariablePopup(true)]
        private string m_canExecuteSoulLampBanter;

        [SerializeField, VariablePopup(true)]
        private string m_soulLampBanterCooldown;

        private float m_timer;

        private void Update()
        {
            if(DialogueLua.GetVariable(m_canExecuteSoulLampBanter).asBool == true)
            {
                m_timer = DialogueLua.GetVariable(m_soulLampBanterCooldown).asFloat;
                return;
            }

            m_timer -= Time.deltaTime;

            if(m_timer <= 0)
            {
                DialogueLua.SetVariable(m_canExecuteSoulLampBanter, true);
            }
        }

        public void TriggerBanterCooldown()
        {
            DialogueLua.SetVariable(m_canExecuteSoulLampBanter, false);
        }
    }
}

