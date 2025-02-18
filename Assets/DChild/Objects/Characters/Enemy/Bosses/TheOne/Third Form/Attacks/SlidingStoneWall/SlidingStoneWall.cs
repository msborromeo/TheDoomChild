using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DChild.Gameplay.Pooling;
using Spine.Unity;
using Holysoft.Event;

namespace DChild.Gameplay.Characters.Enemies
{
    public class SlidingStoneWall : PoolableObject
    {
        [SerializeField, TabGroup("Reference")]
        protected SpineRootAnimation m_animation;
        [SerializeField, TabGroup("Reference")]
        private SpineEventListener m_spineEventListener;
        [SerializeField]
        private SkeletonAnimation m_skeletonAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_emergeAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_waitForInputAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_retractAnimation;

        [SerializeField, TabGroup("Sensors")]
        private RaySensor m_wallSensor;
        [SerializeField, TabGroup("Sensors")]
        private RaySensor m_floorSensor;

        [SerializeField, TabGroup("Colliders")]
        private Collider2D m_floorSlamCollider;
        [SerializeField, TabGroup("Colliders")]
        private Collider2D m_wallSlamCollider;
        [SerializeField, TabGroup("Colliders")]
        private Collider2D m_wallCollider;

        [SerializeField,SpineEvent,TabGroup("Events")]
        private string m_wallBreakOn;
        [SerializeField, SpineEvent, TabGroup("Events")]
        private string m_wallBreakOff;
        [SerializeField, SpineEvent, TabGroup("Events")]
        private string m_wallStickOn;
        [SerializeField, SpineEvent, TabGroup("Events")]
        private string m_wallStickOff;
        [SerializeField, SpineEvent, TabGroup("Events")]
        private string m_floorSmashOn;
        [SerializeField, SpineEvent, TabGroup("Events")]
        private string m_floorSmashOff;
        public event EventAction<EventActionArgs> AttackStart;
        public event EventAction<EventActionArgs> AttackDone;

        // Start is called before the first frame update
        void Start()
        {
            m_animation.SetAnimation(0, m_waitForInputAnimation, false);
            m_floorSlamCollider.enabled = false;
            m_wallSlamCollider.enabled = false;
            m_wallCollider.enabled = false;
            m_spineEventListener.Subscribe(m_wallBreakOn, ActivateWallSmashDamageOrFX);
            m_spineEventListener.Subscribe(m_wallBreakOff, DeActivateWallSmashDamageOrFX);
            m_spineEventListener.Subscribe(m_wallStickOn, ActivateWallStick);
            m_spineEventListener.Subscribe(m_wallStickOff, DeActivateWallStick);
            m_spineEventListener.Subscribe(m_floorSmashOn, ActivateFloorSlamCollider);
            m_spineEventListener.Subscribe(m_floorSmashOff, DeActivateFloorSlamCollider);
        }

        private void ActivateFloorSlamCollider()
        {
            m_floorSlamCollider.enabled = true;
        }
        private void DeActivateFloorSlamCollider()
        {
            m_floorSlamCollider.enabled = false;
        }

        private void ActivateWallStick()
        {
            m_wallCollider.enabled = true;
        }
        private void DeActivateWallStick()
        {
            m_wallCollider.enabled = false;
            ActivateWallSmashDamageOrFX();
        }
        private void ActivateWallSmashDamageOrFX()
        {
            m_wallSlamCollider.enabled = true;
        }

        private void DeActivateWallSmashDamageOrFX()
        {
            m_wallSlamCollider.enabled = false;
        }


        private void DisableColliders()
        {
            m_floorSlamCollider.enabled = false;
            m_wallSlamCollider.enabled = false;
            m_wallCollider.enabled = false;
        }

        private IEnumerator EmergeTentacle()
        {
            m_animation.SetAnimation(0, m_emergeAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_emergeAnimation);
        }
        private IEnumerator RetractTentacle()
        {
            m_animation.SetAnimation(0, m_retractAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_retractAnimation);
            m_animation.SetAnimation(0,m_waitForInputAnimation, false);
            yield return new WaitForAnimationComplete(m_animation.animationState, m_waitForInputAnimation);
        }

        private IEnumerator MonolithGroundSmashImpact()
        {
            
            yield return new WaitForSeconds(6f);
            m_floorSlamCollider.enabled = true;
            yield return new WaitForSeconds(0.5f);
            m_floorSlamCollider.enabled = false;
            m_wallCollider.enabled = true;
        }

        private IEnumerator MonolithWallSlamImpact()
        {
            m_wallCollider.enabled = false;
            m_wallSlamCollider.enabled = true;
            yield return new WaitForSeconds(0.5f);
            m_wallSlamCollider.enabled = false;
        }

        public IEnumerator CompleteSlidingWallAttackSequence()
        {
            //AttackStart?.Invoke(this, EventActionArgs.Empty);
           // StopAllCoroutines();
            yield return EmergeTentacle();
            yield return RetractTentacle();
            //yield return AttackTentacle();
            //yield return RetractTentacle();
            //AttackDone?.Invoke(this, EventActionArgs.Empty);
        }

     
    }   
}

