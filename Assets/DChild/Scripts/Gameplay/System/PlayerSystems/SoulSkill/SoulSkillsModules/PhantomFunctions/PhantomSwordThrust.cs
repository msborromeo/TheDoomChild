using DChild.Gameplay.Characters.Players.Modules;
using Holysoft.Event;
using PlayerNew;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DChild.Gameplay.Cinematics.Cameras.SpineCameraShake;

namespace DChild.Gameplay.Characters.Players.Module
{
    public class PhantomSwordThrust : AttackBehaviour
    {
        [SerializeField]
        private Info m_thrust;
        [SerializeField]
        private ParticleSystem m_chargeFX;
        [SerializeField]
        private ParticleSystem m_finishedChargeFX;
        [SerializeField]
        private ParticleSystem m_dustFX;
        [SerializeField]
        private ParticleSystem m_impactFX;

        public void Execute()
        {
            m_chargeFX?.Stop(true);
            m_finishedChargeFX?.Stop(true);
            m_thrust.PlayFX(true);
            m_thrust.ShowCollider(true);
            m_impactFX?.Play(true);
            m_dustFX?.Play(true);
        }

        public void EndSwordThrust()
        {
            m_thrust.ShowCollider(false);
            m_chargeFX?.Stop(true);
            m_thrust.PlayFX(false);
            m_finishedChargeFX?.Stop(true);
        }

        public void EndExecution()
        {
            m_thrust.PlayFX(false);
            m_thrust.ShowCollider(false);
        }
    }
}

