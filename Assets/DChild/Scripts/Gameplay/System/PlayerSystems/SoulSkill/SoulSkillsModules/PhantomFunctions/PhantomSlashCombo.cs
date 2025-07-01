using DChild.Gameplay.Characters.Players.Modules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Players.Module
{
    public class PhantomSlashCombo : AttackBehaviour
    {
        [SerializeField]
        private List<Info> m_slashComboInfo;

        [SerializeField]
        private int m_currentVisualSlashState;

        public void PlayFX(bool value)
        {
            m_slashComboInfo[m_currentVisualSlashState].PlayFX(value);
        }

        public void EnableCollision(bool value)
        {
            m_slashComboInfo[m_currentVisualSlashState].ShowCollider(value);
        }

        public void IterateCurrentVisualState()
        {
            if(m_currentVisualSlashState >= m_slashComboInfo.Count)
            {
                m_currentVisualSlashState = 0;
            }
            else
            {
                m_currentVisualSlashState += 1;
            }
        }

        public void ResetCurrentVisualState()
        {
            m_currentVisualSlashState = 0;
        }
    }
}

