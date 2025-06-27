using Sirenix.OdinInspector;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DChild.Visuals
{
    public class PlayAnimationLoopToggle : MonoBehaviour
    {
        [SerializeField]
        SkeletonAnimation m_skeletonanimation;
        [SerializeField, TabGroup("Appearance"), OnValueChanged("AnimationValueChanged")]
        private SkeletonDataAsset m_SkeletonReference;

        private bool m_ChangeIdle;
        [SerializeField]
        private bool m_NewIdleLooping;
        [SerializeField, Spine.Unity.SpineAnimation, TabGroup("Animation")]
        private string m_Idle;
        [SerializeField, Spine.Unity.SpineAnimation, TabGroup("Animation")]
        private string m_Interacted;
        [SerializeField, Spine.Unity.SpineAnimation, TabGroup("Animation")]
        private string m_NewIdle;

        private void Start()
        {
            if(m_ChangeIdle)
            {
                m_skeletonanimation.AnimationName = m_NewIdle;
            }else
            {
                m_skeletonanimation.AnimationName = m_Idle;
                m_skeletonanimation.loop = m_NewIdleLooping;
            }
        }
        public void InteractAction()
        {
            m_skeletonanimation.AnimationName = m_Interacted;
        }

        public void ChangeIdle()
        {
            m_ChangeIdle = true;
            m_skeletonanimation.AnimationName = m_NewIdle;
        }

        public void isLooping(bool x)
        {
            m_skeletonanimation.loop = x;
        }

        void AnimationValueChanged()
        {
            m_skeletonanimation.skeletonDataAsset = m_SkeletonReference;
            m_skeletonanimation.Initialize(true);
            m_skeletonanimation.loop = true;
#if UNITY_EDITOR
            EditorUtility.SetDirty(m_skeletonanimation);
            EditorUtility.SetDirty(m_skeletonanimation.transform);
#endif
        }
    }
}

