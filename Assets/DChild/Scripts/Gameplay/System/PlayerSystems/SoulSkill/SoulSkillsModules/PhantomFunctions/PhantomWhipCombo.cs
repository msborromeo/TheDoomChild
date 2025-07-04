using DChild.Gameplay.Characters.Players.Modules;
using DChild.Gameplay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DChild.Gameplay.Cinematics.Cameras.SpineCameraShake;
using static DG.Tweening.DOTweenModuleUtils;

namespace DChild.Gameplay.Characters.Players.Module
{
    public class PhantomWhipCombo : AttackBehaviour
    {
        [SerializeField]
        private List<Info> m_whipComboInfo;
        private int m_currentVisualWhipState;
        [SerializeField]
        private CollisionRegistrator m_collisionRegistrator;

        public void PlayFX(bool value)
        {
            m_whipComboInfo[m_currentVisualWhipState].PlayFX(value);
        }

        public void EnableCollision(bool value)
        {
            m_whipComboInfo[m_currentVisualWhipState].ShowCollider(value);
        }


        public void PlayWhipCombo(int slashState)
        {
            m_collisionRegistrator.ClearCache();
            m_whipComboInfo[slashState].PlayFX(true);
            m_whipComboInfo[slashState].ShowCollider(true);
        }

        public void StopWhipCombo()
        {
            for (int i = 0; i < m_whipComboInfo.Count; i++)
            {
                m_whipComboInfo[i].PlayFX(false);
                m_whipComboInfo[i].ShowCollider(false);
            }
        }


        public void IterateCurrentVisualState()
        {
            if(m_currentVisualWhipState >= m_whipComboInfo.Count)
            {
                m_currentVisualWhipState = -1; //-1 because 0 is whip combo state
            }
            else
            {
                m_currentVisualWhipState += 1;
            }
        }

        public void ResetCurrentVisualState()
        {
            m_currentVisualWhipState = -1;
        }
    }
}

