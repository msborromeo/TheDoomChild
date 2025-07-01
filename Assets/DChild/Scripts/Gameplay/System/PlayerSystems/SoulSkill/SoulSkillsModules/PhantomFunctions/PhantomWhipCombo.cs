using DChild.Gameplay.Characters.Players.Modules;
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

        public void PlayFX(bool value)
        {
            m_whipComboInfo[m_currentVisualWhipState].PlayFX(value);
        }

        public void EnableCollision(bool value)
        {
            m_whipComboInfo[m_currentVisualWhipState].ShowCollider(value);
        }


        public void IterateCurrentVisualState()
        {
            if(m_currentVisualWhipState >= m_whipComboInfo.Count)
            {
                m_currentVisualWhipState = 0;
            }
            else
            {
                m_currentVisualWhipState += 1;
            }
        }

        public void ResetCurrentVisualState()
        {
            m_currentVisualWhipState = 0;
        }
    }
}

