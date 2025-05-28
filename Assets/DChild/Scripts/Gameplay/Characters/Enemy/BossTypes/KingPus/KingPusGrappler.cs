using Sirenix.OdinInspector;
using Spine.Unity;
using System.Collections;
using UnityEngine;
using static DChild.Gameplay.SpineAnimation;

namespace DChild.Gameplay.Characters.Enemies
{

    public class KingPusGrappler : MonoBehaviour
    {
        [SerializeField]
        private SpineAnimation m_animation;
        [SerializeField, MinValue(1f)]
        private int m_animationLayerIndex = 1;
        [SerializeField]
        private SkeletonUtilityBone m_boneIK;
        [SerializeField]
        private Transform m_overridePoint;
        [SerializeField]
        private SpringJoint2D m_chain;
        [SerializeField, FoldoutGroup("Animation")]
        private SkeletonDataAsset m_skeletonDataAsset;
        [SerializeField, FoldoutGroup("Animation"), Spine.Unity.SpineAnimation(dataField = "m_skeletonDataAsset", startsWith = "Wall_")]
        private string m_extendAnimation;
        [SerializeField, FoldoutGroup("Animation"), Spine.Unity.SpineAnimation(dataField = "m_skeletonDataAsset", startsWith = "Wall_")]
        private string m_loopAnimation;
        [SerializeField, FoldoutGroup("Animation"), Spine.Unity.SpineAnimation(dataField = "m_skeletonDataAsset", startsWith = "Wall_")]
        private string m_retractAnimation;

        [SerializeField, ReadOnly]
        private bool m_isExtended;

        public bool isExtended => m_isExtended;

#if UNITY_EDITOR
        [Button, FoldoutGroup("Animation")]
        private void ExtractSkeletonDataAsset()
        {
            m_skeletonDataAsset = m_animation.skeletonAnimation.SkeletonDataAsset;
        }
#endif
        [Button, ShowIf("m_isExtended")]
        public void Retract(float speed)
        {
            if (m_isExtended == false)
                return;

            StopAllCoroutines();
            StartCoroutine(RetractRoutine(speed));
        }

        [Button, HideIf("m_isExtended")]
        public void Extend(float speed, bool activatePhysicsAtEnd = false)
        {
            if (m_isExtended)
                return;

            StopAllCoroutines();
            StartCoroutine(ExtendRoutine(speed, activatePhysicsAtEnd));
        }

        [Button]
        public void OverrideIK(Transform target)
        {
            OverrideIK(target.position);
        }

        public void OverrideIK(Vector2 position)
        {
            m_boneIK.mode = SkeletonUtilityBone.Mode.Override;
            m_boneIK.transform.position = position;
            m_overridePoint.position = position;
        }

        [Button]
        public void StopIKOverride()
        {
            m_boneIK.mode = SkeletonUtilityBone.Mode.Follow;
        }

        [Button]
        public void SetPhysicsActive(bool active)
        {
            if (active)
            {
                m_chain.gameObject.SetActive(true);
            }
            else if (m_chain.gameObject.activeSelf)
            {
                m_chain.gameObject.SetActive(false);
                m_chain.transform.localPosition = Vector3.zero;
            }
        }

        private IEnumerator RetractRoutine(float speed)
        {
            var track = m_animation.SetAnimation(m_animationLayerIndex, m_retractAnimation, false);
            track.TimeScale = speed;
            SetPhysicsActive(false);
            yield return new WaitForSpineAnimationComplete(track);
            m_animation.SetEmptyAnimation(m_animationLayerIndex, 0);
            m_isExtended = false;
        }


        private IEnumerator ExtendRoutine(float speed, bool activatePhysicsAtEnd = false)
        {
            var track = m_animation.SetAnimation(m_animationLayerIndex, m_extendAnimation, false);
            track.TimeScale = speed;
            m_animation.AddAnimation(m_animationLayerIndex, m_loopAnimation, true, 0);
            yield return new WaitForSpineAnimationComplete(track);
            if (activatePhysicsAtEnd)
            {
                SetPhysicsActive(true);
            }
            m_isExtended = true;
        }

        private void Update()
        {
            if (m_isExtended)
            {
                m_boneIK.transform.position = m_overridePoint.position;
            }
        }

        public void OnDrawGizmosSelected()
        {
            if (m_isExtended == false)
                return;


            if (m_animation != null && m_overridePoint != null)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(m_animation.transform.position, m_overridePoint.position);
            }
            if (m_overridePoint != null && m_chain != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(m_overridePoint.position, m_chain.transform.position);
            }
        }
    }
}

