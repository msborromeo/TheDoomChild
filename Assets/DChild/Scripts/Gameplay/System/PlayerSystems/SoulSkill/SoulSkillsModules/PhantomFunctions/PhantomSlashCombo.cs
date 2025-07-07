using DChild.Gameplay.Characters.Players.Modules;
using DChild.Gameplay.Characters.Players.State;
using DChild.Gameplay.Combat;
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
        private CollisionRegistrator m_collisionRegistrator;

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

        public void PlaySlashCombo(int slashState)
        {
            m_collisionRegistrator.ClearCache();
            m_slashComboInfo[slashState].PlayFX(true);
            m_slashComboInfo[slashState].ShowCollider(true);
        }

        public void StopSlashCombo()
        {
            for(int i = 0; i < m_slashComboInfo.Count; i++)
            {
                m_slashComboInfo[i].PlayFX(false);
                m_slashComboInfo[i].ShowCollider(false);
            }
            m_currentVisualSlashState = -1;
        }

        public void IterateCurrentVisualState()
        {
            if(m_currentVisualSlashState < m_slashComboInfo.Count)
            {
                m_currentVisualSlashState += 1;
            }
            else
            {
                m_currentVisualSlashState = 0;
            }
        }

        public void ResetCurrentVisualState()
        {
            m_currentVisualSlashState = 0;
        }

        private void Update()
        {

        }
    }
}

