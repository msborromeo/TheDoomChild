using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DChild.Gameplay.Pooling;
using Sirenix.OdinInspector;
using Spine.Unity;
using Holysoft.Event;

namespace DChild.Gameplay.Characters.Enemies
{
    public class TentacleCeiling : MonoBehaviour
    {
        //private BoxCollider2D m_tentacleHitBox;

        [SerializeField, TabGroup("Reference")]
        protected SpineRootAnimation m_animation;
        [SerializeField, TabGroup("Reference")]
        protected GameObject m_collider;
        [SerializeField, TabGroup("Reference")]
        private GameObject m_selfObject;
        [SerializeField]
        private SkeletonAnimation m_skeletonAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_attackAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_retractAnimation;

        public event EventAction<EventActionArgs> AttackStart;
        public event EventAction<EventActionArgs> AttackDone;

        public IEnumerator Attack()
        {
            // AttackStart?.Invoke(this, EventActionArgs.Empty);
            m_selfObject.SetActive(true);
            m_animation.SetAnimation(0, m_attackAnimation, false);
            yield return new WaitForSeconds(1f);
            m_collider.SetActive(true);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_attackAnimation);
       
           // m_animation.SetAnimation(0, m_extendedAnimation, true);

        }

        public IEnumerator Extended()
        {
            yield return Retract();
        }

        public IEnumerator Retract()
        {
            m_collider.SetActive(false);
            m_animation.SetAnimation(0, m_retractAnimation, false);
            //m_tentacleHitBox.enabled = false;
            yield return new WaitForAnimationComplete(m_animation.animationState, m_retractAnimation);
            m_selfObject.SetActive(false);
            // AttackDone?.Invoke(this, EventActionArgs.Empty);
        }

        private void Start()
        {
            m_selfObject.SetActive(false);
            //m_tentacleHitBox = this.GetComponent<BoxCollider2D>();
        }

        [Button]
        private void DoAttack()
        {
           // StartCoroutine(Attack(3.5f));
            //StartCoroutine(Retract());
        }
    }
}

