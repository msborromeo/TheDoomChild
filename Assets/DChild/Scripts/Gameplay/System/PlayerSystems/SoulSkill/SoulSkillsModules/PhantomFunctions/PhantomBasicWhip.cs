using DChild.Gameplay.Characters.Players.Modules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Players.Module
{
    public class PhantomBasicWhip : AttackBehaviour
    {
        public enum Type
        {
            Ground_Forward,
            Ground_Overhead,
            MidAir_Forward,
            MidAir_Overhead,
            Crouch_Forward
        }

        [SerializeField]
        private Info m_groundForward;
        [SerializeField]
        private Info m_groundOverhead;
        [SerializeField]
        private Info m_midAirForward;
        [SerializeField]
        private Info m_midAirOverhead;
        [SerializeField]
        private Info m_crouchForward;

        public void EnableCollision(Type type, bool value)
        {
            switch (type)
            {
                case Type.Ground_Forward:
                    m_groundForward.ShowCollider(value);
                    break;
                case Type.Ground_Overhead:
                    m_groundOverhead.ShowCollider(value);
                    break;
                case Type.MidAir_Forward:
                    m_midAirForward.ShowCollider(value);
                    break;
                case Type.MidAir_Overhead:
                    m_midAirOverhead.ShowCollider(value);
                    break;
                case Type.Crouch_Forward:
                    m_crouchForward.ShowCollider(value);
                    break;
            }
        }

        public void PlayFXFor(Type type, bool play)
        {
            switch (type)
            {
                case Type.Ground_Forward:
                    m_groundForward.PlayFX(play);
                    break;
                case Type.Ground_Overhead:
                    m_groundOverhead.PlayFX(play);
                    break;
                case Type.Crouch_Forward:
                    m_crouchForward.PlayFX(play);
                    break;
                case Type.MidAir_Forward:
                    m_midAirForward.PlayFX(play);
                    break;
                case Type.MidAir_Overhead:
                    m_midAirOverhead.PlayFX(play);
                    break;
            }
        }
    }
}

