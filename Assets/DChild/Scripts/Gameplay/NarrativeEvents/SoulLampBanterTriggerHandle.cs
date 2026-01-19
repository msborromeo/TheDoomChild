using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Dialogues
{
    public class SoulLampBanterTriggerHandle : MonoBehaviour
    {
        private SoulLampBanterCooldownHandle m_soulLampBanterCooldownHandle;

        private void Start()
        {
            m_soulLampBanterCooldownHandle = FindObjectOfType<SoulLampBanterCooldownHandle>();
        }

        public void OnLampDestroyed()
        {
            if(m_soulLampBanterCooldownHandle != null)
                m_soulLampBanterCooldownHandle.TriggerBanterCooldown();
        }
    }
}

