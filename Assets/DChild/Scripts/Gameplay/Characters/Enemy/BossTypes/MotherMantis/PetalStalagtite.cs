using System;
using DChild.Gameplay;
using DChild.Gameplay.Characters;
using DChild.Gameplay.Combat;
using Holysoft.Event;
using DChild.Gameplay.Characters.AI;
using UnityEngine;
using Spine;
using Spine.Unity;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using DChild;
using DChild.Gameplay.Characters.Enemies;


namespace DChild.Gameplay.Characters.Enemies
{
    [AddComponentMenu("DChild/Gameplay/Enemies/Minion/PetalStalagmite")]
    public class PetalStalagtite : CombatAIBrain<PetalStalagtite.Info>
    {
        [System.Serializable]
        public class Info : BaseInfo
        {
            //Animations
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_growAnimation0;
            public string growAnimation0 => m_growAnimation0;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_growAnimation1;
            public string growAnimation1 => m_growAnimation1;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_growAnimation2;
            public string growAnimation2 => m_growAnimation2;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_growAnimation3;
            public string growAnimation3 => m_growAnimation3;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_sproutAnimation;
            public string sproutAnimation => m_sproutAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_idleAnimation0;
            public string idleAnimation0 => m_idleAnimation0;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_idleAnimation1;
            public string idleAnimation1 => m_idleAnimation1;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_idleAnimation2;
            public string idleAnimation2 => m_idleAnimation2;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_idleAnimation3;
            public string idleAnimation3 => m_idleAnimation3;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_deathAnimation;
            public string deathAnimation => m_deathAnimation;
            /*[SerializeField, ValueDropdown("GetAnimations")]
            private string m_death2Animation;
            public string death2Animation => m_death2Animation;*/
            /*[SerializeField, ValueDropdown("GetAnimations")]
            private string m_flinchAnimation;
            public string flinchAnimation => m_flinchAnimation;
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_flinch2Animation;
            public string flinch2Animation => m_flinch2Animation;*/
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_wiltAnimation;
            public string wiltAnimation => m_wiltAnimation;

            public override void Initialize()
            {
#if UNITY_EDITOR
                //
#endif
            }
        }

        private enum State
        {
            Sprout,
            //Grow,
            Idle,
            WaitBehaviourEnd,
        }

        //[SerializeField]
        //private Info m_info;
        /*[SerializeField]
        private SkeletonAnimation m_skeletonAnimation;

        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_growthAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_idleAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_flinchAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_flinchAnimation2;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_deathAnimation;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_deathAnimation2;
        [SerializeField, Spine.Unity.SpineAnimation(dataField = "m_skeletonAnimation")]
        private string m_wiltAnimation;*/

        /*[SerializeField]
        private GameObject m_colliders;*/
        [SerializeField, TabGroup("FX")]
        private ParticleFX m_disturbedGrass;
        [SerializeField, TabGroup("Reference")]
        private Hitbox m_hitbox;
        [SerializeField, TabGroup("Reference")]
        private Collider2D[] m_collider;
        [SerializeField, TabGroup("Reference")]
        private Damageable m_damageable;
        public GameObject m_motherMantisAI;

        [ShowInInspector]
        private StateHandle<State> m_stateHandle;
        private bool m_isPetalRain;
        public bool m_hasMantisLanded;
        public bool m_checker;

        public EventAction<EventActionArgs> Growing;

        public void GetTarget(AITargetInfo target)
        {
            m_targetInfo = target;
        }
        protected override void OnDestroyed(object sender, EventActionArgs eventArgs)
        {
            StopAllCoroutines();
            //m_motherMantisAI.OnPetalRain -= OnPetalRain;
            m_checker = true;
            this.GetComponent<Damageable>().Destroyed -= OnDestroyed;
            StartCoroutine(DeathFxRoutine());
        }
        private IEnumerator SproutRoutine()
        {
            m_checker = false;
            m_stateHandle.Wait(State.Idle);
            m_disturbedGrass.Play();
            Growing?.Invoke(this, EventActionArgs.Empty);
            m_animation.SetAnimation(0, m_info.sproutAnimation, false);
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }
        [SerializeField]
        private float delayBeforeWilt;
        private string idleAnim = "toto";
        private IEnumerator GrowthRoutine()
        {
            m_stateHandle.Wait(State.Idle);
            var growAnim = m_animation.SetEmptyAnimation(0, 0);
            var random = UnityEngine.Random.RandomRange(0, 3);
            var collider = m_collider[0];
            switch (random)
            {
                case 0:
                    growAnim = m_animation.SetAnimation(0, m_info.growAnimation0, false);
                    idleAnim = m_info.idleAnimation0;
                    collider = m_collider[0];
                    break;
                case 1:
                    growAnim = m_animation.SetAnimation(0, m_info.growAnimation2, false);
                    idleAnim = m_info.idleAnimation2;
                    collider = m_collider[1];
                    break;
                case 2:
                    growAnim = m_animation.SetAnimation(0, m_info.growAnimation3, false);
                    idleAnim = m_info.idleAnimation3;
                    collider = m_collider[2];
                    break;/*
                case 3:
                    growAnim = m_animation.SetAnimation(0, m_info.growAnimation3, false);
                    idleAnim = m_info.idleAnimation3;
                    break;*/
            }
            yield return new WaitForSeconds(1f);
            collider.enabled = true;
            yield return new WaitForSeconds(1f);
            //m_animation.SetAnimation(1, idleAnim, true);
            m_hitbox.SetInvulnerability(Invulnerability.None);
            this.GetComponent<Damageable>().Destroyed += OnDestroyed;
            yield return new WaitForSeconds(1f);
            m_hasMantisLanded = false;
            if (m_isPetalRain == false && m_checker == false)
            {
                yield return WiltFxRoutine();
            }
            m_stateHandle.ApplyQueuedState();
            yield return null;
        }
        private void OnMantisLand(object sender, EventActionArgs eventActionArgs)
        {
            m_hasMantisLanded = true;
        }

        private IEnumerator DeathFxRoutine()
        {
            m_animation.SetAnimation(0, m_info.deathAnimation, false);
            foreach (var collider in m_collider)
                collider.enabled = false;
            m_hitbox.enabled = false;
            yield return new WaitForAnimationComplete(m_animation.animationState, m_info.deathAnimation);
            //m_isPetalRain = true;
            //yield return new WaitForSeconds(1f);
            Destroy(this.gameObject);
            yield return null;

        }

        private IEnumerator WiltFxRoutine()
        {
            yield return new WaitForSeconds(delayBeforeWilt);
            var wilt = m_animation.SetAnimation(1, m_info.wiltAnimation, false);
            foreach (var collider in m_collider)
                collider.enabled = false;
            m_hitbox.enabled = false;
            m_animation.AddAnimation(1, idleAnim, true, 0);
            yield return new WaitForSeconds(0.3f);
            Destroy(this.gameObject);
            //m_motherMantisAI.OnPetalRain += OnPetalRain;
            yield return null;
        }

        protected override void Awake()
        {
            base.Awake();
            //m_motherMantisAI = GameObject.Find("MotherMantis");
            m_motherMantisAI.GetComponent<MotherMantisAI>().OnMantisLand += OnMantisLand;
            m_motherMantisAI.GetComponent<MotherMantisAI>().OnPetalRain += OnPetalRain;
            m_damageable.health.AddCurrentValue(m_damageable.health.maxValue);
            var sizeMult = UnityEngine.Random.Range(119, 120) * .01f;
            m_hitbox.SetInvulnerability(Invulnerability.MAX);
            foreach (var collider in m_collider)
                collider.enabled = false;
            transform.localScale = new Vector2(transform.localScale.x * sizeMult, transform.localScale.y * sizeMult);
            m_stateHandle = new StateHandle<State>(State.Sprout, State.WaitBehaviourEnd);

        }

        protected override void OnDisable()
        {
            base.OnDisable();
            m_motherMantisAI.GetComponent<MotherMantisAI>().OnMantisLand -= OnMantisLand;
            m_motherMantisAI.GetComponent<MotherMantisAI>().OnPetalRain -= OnPetalRain;
        }

        /*private void Start()
        {
            base.Start();
            *//*m_motherMantisAI.GetComponent<MotherMantisAI>().OnMantisLand += OnMantisLand;
            m_motherMantisAI.GetComponent<MotherMantisAI>().OnPetalRain += OnPetalRain;*//*
        }*/
        private void OnPetalRain(object sender, EventActionArgs eventActionArgs )
        {
            m_isPetalRain = false;
        }
        /*public void CallGrowthRoutine()
        {
            StartCoroutine(GrowthRoutine());
        }*/
        /*private void OnMantisLand(object sender, EventActionArgs eventActionArgs)
        {
            stalagmiteNotGrowing = false;
            m_isMantisGrounded = true;
        }*/
        private void Update()
        {
            switch (m_stateHandle.currentState)
            {
                case State.Sprout:
                    StartCoroutine(SproutRoutine());
                    break;
                case State.Idle:
                    if (m_hasMantisLanded == true)
                    {
                        StartCoroutine(GrowthRoutine());
                    }
                    break;
                case State.WaitBehaviourEnd:
                    return;
            }
        }

        public override void ReturnToSpawnPoint()
        {
            /*throw new NotImplementedException();*/
        }

        protected override void OnTargetDisappeared()
        {
            m_stateHandle.OverrideState(State.Sprout);
        }
    }

}
