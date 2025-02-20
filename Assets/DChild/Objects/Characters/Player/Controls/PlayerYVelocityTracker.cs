using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Characters.Players.Modules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay
{
    public class PlayerYVelocityTracker : MonoBehaviour, IComplexCharacterModule
    {
        [SerializeField]
        private Rigidbody2D m_rigidbody;
        private CharacterState m_state;
        private Animator m_animator;
        private IPlayerModifer m_modifier;
        private int m_animationParameter;

        public void Initialize(ComplexCharacterInfo info)
        {
            m_rigidbody = info.rigidbody;
            m_state = info.state;
            m_animator = info.animator;
            m_modifier = info.modifier;
            m_animationParameter = info.animationParametersData.GetParameterLabel(AnimationParametersData.Parameter.SpeedY);
        }

        public void Update()
        {
            m_animator.SetFloat(m_animationParameter, m_rigidbody.velocity.y);
        }
    }
}

