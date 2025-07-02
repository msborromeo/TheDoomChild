using DChild.Gameplay.Characters.Players.Modules;
using DChild.Gameplay.Combat;
using Holysoft.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DChild.Gameplay.Cinematics.Cameras.SpineCameraShake;

namespace DChild.Gameplay.Characters.Players.Module
{
    public class PhantomEarthshaker : AttackBehaviour
    {
        [SerializeField]
        private ParticleSystem m_chargeFX;
        [SerializeField]
        private ParticleSystem m_preLoopFX;
        [SerializeField]
        private ParticleSystem m_fallLoopFX;
        [SerializeField]
        private ParticleSystem m_impactFX;
        [SerializeField]
        private Collider2D m_impactCollider;

        public void HandlePreFall()
        {
            m_chargeFX?.Stop(true);
            m_preLoopFX?.Play(true);
        }

        public void HandleFall()
        {
            if ((m_fallLoopFX?.isPlaying ?? false) == false)
            {
                m_preLoopFX?.Stop(true);
                m_fallLoopFX?.Play(true);
            }
        }

        public void Impact()
        {
            m_fallLoopFX?.Stop(true);
            m_fallLoopFX.gameObject.SetActive(false); //feels hacky but it works fine 
            m_impactFX?.Play(true);
            m_impactCollider.enabled = true;
        }

        public void EndExecution()
        {
            m_fallLoopFX?.Stop(true);
            m_impactFX?.Stop(true);
            m_impactCollider.enabled = false;
            m_state.isDoingEarthShaker = false;
        }
    }
}

