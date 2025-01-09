using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holysoft.Event;
using Sirenix.OdinInspector;
using static MinionMaterialConfigurator;
using System;
using static DChild.Gameplay.ArmyBattle.ArmySpecialSkillVfx;
using Spine.Unity;
using DChild.Gameplay.Characters.AI;
using Spine;
using DChild.Gameplay.Characters;


namespace DChild.Gameplay.ArmyBattle
{
    public class ArmySpecialSkillVfx : MonoBehaviour
    {
        [SerializeField]
        private List<VfxParticleTurnManager> m_vfxParticleTurnManager = new List<VfxParticleTurnManager>();
        [SerializeField]
        private List<VfxSpineTurnManager> m_VfxSpineTurnManager = new List<VfxSpineTurnManager>();
        [SerializeField]
        public bool m_iseffectdone  = false;
        [Serializable]
        public class VfxParticleTurnManager
        {
            [SerializeField]
            public List<ParticleSystem> m_fxPartcileSystem;
        }
        [Serializable]
        public class VfxSpineTurnManager
        {
            [SerializeField]
            public GameObject m_SpineModel;
            [SerializeField]
            public SkeletonDataAsset m_fxSpineSystem;
            protected IEnumerable GetEvents()
            {
                ValueDropdownList<string> list = new ValueDropdownList<string>();
                var reference = m_fxSpineSystem.GetAnimationStateData().SkeletonData.Events.ToArray();
                for (int i = 0; i < reference.Length; i++)
                {
                    list.Add(reference[i].Name);
                }
                return list;
            }
            protected IEnumerable GetAnimations()
            {
                ValueDropdownList<string> list = new ValueDropdownList<string>();
                var reference = m_fxSpineSystem.GetAnimationStateData().SkeletonData.Animations.ToArray();
                for (int i = 0; i < reference.Length; i++)
                {
                    list.Add(reference[i].Name);
                }
                return list;
            }
            [SerializeField, ValueDropdown("GetAnimations")]
            private List<string> m_animation;
            [SerializeField, ValueDropdown("GetEvents")]
            public List<string> m_launchOnEvent;
            [SerializeField]
            public SpineEventListener m_spineListener;
            [SerializeField]
            public List<ParticleSystem> m_eventPartcileSystem;
            [SerializeField, Min(0f)]
            private List<float> m_delaytime;

            public List<string> animation => m_animation;
            public List<float> animationDelayTime => m_delaytime;
            public List<string> launchOnEvent => m_launchOnEvent;
        }
        [Serializable]
        public class BasicAnimationInfo : VfxSpineTurnManager, IAIAnimationInfo
        {
            [SerializeField, ValueDropdown("GetAnimations")]
            private string m_animation;
            [SerializeField, ValueDropdown("GetEvents")]
            private string m_launchOnEvent;
            [SerializeField, Min(0f)]
            private float m_timeScale = 1;
            [SerializeField, Min(0f)]
            private float m_delaytime = 1;

            public string animation => m_animation;
            public float animationDelayTime => m_delaytime;
            public float animationTimeScale => m_timeScale;
            public string launchOnEvent => m_launchOnEvent;
        }
        [SerializeField]
        private int m_currentturn=1;
        private SkeletonAnimation skeletonAnimation;

        [Button, HideInPrefabAssets]
        public void PlayEffects()
        {
            m_iseffectdone = false;

            if (m_vfxParticleTurnManager.Count != 0)
            {
                for (int i = 0; i < m_vfxParticleTurnManager.Count; i++)
                {

                    if (i == m_currentturn - 1)
                    {
                        for (int x = 0; x < m_vfxParticleTurnManager[i].m_fxPartcileSystem.Count; x++)
                        {
                            m_vfxParticleTurnManager[i].m_fxPartcileSystem[x].Play();
                        }
                    }
                    else
                    {
                        for (int x = 0; x < m_vfxParticleTurnManager[i].m_fxPartcileSystem.Count; x++)
                        {
                            m_vfxParticleTurnManager[i].m_fxPartcileSystem[x].Stop();
                        }

                    }

                    if (m_VfxSpineTurnManager.Count != 0)
                    {
                        for (int x = 0; x < m_vfxParticleTurnManager[i].m_fxPartcileSystem.Count; x++)
                        {
                            var duration = m_vfxParticleTurnManager[i].m_fxPartcileSystem[x].main.duration;
                            if (duration <= 0)
                            {
                                m_iseffectdone = true;
                            }
                        }
                    }
                    else
                    {
                        m_iseffectdone = true;
                    }


                }
            }
            for (int i = 0; i < m_VfxSpineTurnManager.Count; i++)
            {

                if (i == m_currentturn - 1)
                {
                    for (int x = 0; x < m_VfxSpineTurnManager[i].animation.Count; x++)
                    {
                        var launchEvent = " ";
                        if(m_VfxSpineTurnManager[i].m_launchOnEvent.Count <=0)
                        {
                           
                        }
                        else
                        {
                            launchEvent = m_VfxSpineTurnManager[i].m_launchOnEvent[x];
                        }
                        if(m_VfxSpineTurnManager[i].m_eventPartcileSystem.Count != 0)
                        {
                            for (int y = 0; y < m_VfxSpineTurnManager[i].m_eventPartcileSystem.Count; y++)
                            {
                                m_VfxSpineTurnManager[i].m_eventPartcileSystem[y].Stop();
                            }
                        }
                 
                        if (m_VfxSpineTurnManager[i].m_spineListener != null){
                            StartCoroutine(PlayEventRoutine(m_VfxSpineTurnManager[i].m_SpineModel, m_VfxSpineTurnManager[i].animation[x], m_VfxSpineTurnManager[i].animationDelayTime[x],
                               m_VfxSpineTurnManager[i].m_spineListener, launchEvent, m_VfxSpineTurnManager[i].m_eventPartcileSystem[x]));
                        }
                        else
                        {
                            StartCoroutine(PlayRoutine(m_VfxSpineTurnManager[i].m_SpineModel, m_VfxSpineTurnManager[i].animation[x], m_VfxSpineTurnManager[i].animationDelayTime[x]));
                        }
                           
                        
                    }
                }
                
            }
           
        }
        public void StopEffects()
        {
            m_iseffectdone = false;
            for (int i = 0; i < m_vfxParticleTurnManager.Count; i++)
            {

               
                    for (int x = 0; x < m_vfxParticleTurnManager[i].m_fxPartcileSystem.Count; x++)
                    {
                        m_vfxParticleTurnManager[i].m_fxPartcileSystem[x].Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    }



            }
            m_iseffectdone = true;

        }
        private IEnumerator PlayEventRoutine(GameObject Spineasset, String animation,float delaytime, SpineEventListener spineListener, string particleevent, ParticleSystem eventPartcileSystem)
        {

            yield return new WaitForSeconds(delaytime);

            skeletonAnimation = Spineasset.GetComponent<SkeletonAnimation>(); 
            skeletonAnimation.AnimationState.SetAnimation(0, animation, false);
            spineListener.Subscribe(particleevent, eventPartcileSystem.Play);
            yield return new WaitForAnimationComplete(skeletonAnimation.AnimationState, animation);
            m_iseffectdone = true;
            yield return null;
        }
        private IEnumerator PlayRoutine(GameObject Spineasset, String animation, float delaytime)
        {

            yield return new WaitForSeconds(delaytime);
            skeletonAnimation = Spineasset.GetComponent<SkeletonAnimation>();
            skeletonAnimation.AnimationState.SetAnimation(0, animation, false);
            yield return new WaitForAnimationComplete(skeletonAnimation.AnimationState, animation);
            m_iseffectdone = true;
            yield return null;
        }
        private void SetTurn(int turn)
        {
            m_currentturn = turn;
        }
        public bool IsEffectOver() => m_iseffectdone;
        private void Start()
        {
            for (int i = 0; i < m_vfxParticleTurnManager.Count; i++)
            {
                    for (int x = 0; x < m_vfxParticleTurnManager[i].m_fxPartcileSystem.Count; x++)
                    {
                    m_vfxParticleTurnManager[i].m_fxPartcileSystem[x].Stop();
                    }

            }

        }


    }
       
}
