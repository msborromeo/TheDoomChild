using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using System.Collections;
using UnityEngine;
using USpine = Spine.Unity;

namespace DChild.Gameplay.ArmyBattle.Units
{
    [DisallowMultipleComponent]
    public abstract class ArmyUnit : MonoBehaviour
    {
        [SerializeField]
        protected SkeletonAnimation m_animation;

        [SerializeField, USpine.SpineAnimation]
        private string[] m_idleAnimations;
        [SerializeField, USpine.SpineAnimation]
        private string[] m_attackAnimations;
        [SerializeField, USpine.SpineAnimation]
        private string[] m_deathAnimations;
        [SerializeField, USpine.SpineAnimation]
        private string[] m_ressurectAnimations;

        private bool m_isAlive;

        public abstract DamageType type { get; }

        public bool isAlive => m_isAlive;

        [Button]
        public virtual void Attack()
        {
            PlayRandomAnimation(m_attackAnimations, true);
        }

        [Button]
        public virtual void Die()
        {
            m_isAlive = false;
            PlayRandomAnimation(m_deathAnimations, false);
        }

        [Button]
        public virtual void Ressurect()
        {
            m_isAlive = true;
            PlayRandomAnimation(m_ressurectAnimations, false);
            var idleAnimation = ChooseAnimation(m_idleAnimations);
            m_animation.state.AddAnimation(0, idleAnimation, true, 0);
        }

        [Button]
        public virtual void Idle()
        {
            PlayRandomAnimation(m_idleAnimations, true);
        }

        protected void PlayRandomAnimation(string[] animations, bool loop)
        {
            if (animations.Length == 0)
                return;

            var animation = ChooseAnimation(animations);


            var track = m_animation.state.SetAnimation(0, animation, false);
            if (loop)
            {
                var endAnimation = track.AnimationEnd * 0.3f;
                var startingTimeStart = Mathf.Lerp(track.AnimationStart, endAnimation, Random.Range(0f, 1f));
                track.AnimationStart = startingTimeStart;

                m_animation.state.AddAnimation(0, animation, true, 0);
            }
        }

        private string ChooseAnimation(string[] animations)
        {
            var animationIndex = Random.Range(0, animations.Length);
            return animations[animationIndex];
        }

        private void Awake()
        {
            m_isAlive = true;
        }
    }
}