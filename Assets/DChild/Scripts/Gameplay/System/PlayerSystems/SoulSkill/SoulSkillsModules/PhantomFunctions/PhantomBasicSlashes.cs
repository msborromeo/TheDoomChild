using DChild.Gameplay.Characters.Players.Modules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Players.Module
{
    public class PhantomBasicSlashes : AttackBehaviour
    {
        public enum Type
        {
            Ground_Overhead,
            Crouch,
            MidAir_Forward,
            MidAir_Overhead
        }

        [SerializeField]
        private Info m_groundOverhead;
        [SerializeField]
        private Info m_crouch;
        [SerializeField]
        private Info m_midAirForward;
        [SerializeField]
        private Info m_midAirOverhead;

        public void PlayFXFor(Type type, bool play)
        {
            switch (type)
            {
                case Type.Ground_Overhead:
                    m_groundOverhead.PlayFX(play);
                    break;
                case Type.Crouch:
                    m_crouch.PlayFX(play);
                    break;
                case Type.MidAir_Forward:
                    m_midAirForward.PlayFX(play);
                    break;
                case Type.MidAir_Overhead:
                    m_midAirOverhead.PlayFX(play);
                    break;
            }
        }

        public void EnableCollision(Type type, bool value)
        {
            switch (type)
            {
                case Type.Ground_Overhead:
                    m_groundOverhead.ShowCollider(value);
                    break;
                case Type.Crouch:
                    m_crouch.ShowCollider(value);
                    break;
                case Type.MidAir_Forward:
                    m_midAirForward.ShowCollider(value);
                    break;
                case Type.MidAir_Overhead:
                    m_midAirOverhead.ShowCollider(value);
                    break;
            }
        }
    }
}

