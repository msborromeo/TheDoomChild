using DChild.Gameplay.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Enemies
{
    public class ColossusSwordProjectile : PoolableObject
    {
        private Vector3 m_target;
        private float m_moveSpeed;
        private float m_trajectoryMaxRelativeHeight;
        private float m_maxMoveSpeed;
        [SerializeField]
        private float m_distanceToTargetToDestroyProjectile = 1f;
        [SerializeField]
        private Renderer m_renderer;
        private AnimationCurve m_trajectoryCurve;
        private AnimationCurve m_axisCorrectionCurve;
        private AnimationCurve m_speedCurve;

        private Vector3 m_trajectoryStartPoint;

        private void Start()
        {
            m_trajectoryStartPoint = transform.position;
        }
        private void Update()
        {
            UpdateProjectilePosition();

            if (Vector3.Distance(transform.position, m_target) <= m_distanceToTargetToDestroyProjectile
                || m_renderer.isVisible == false)
            {
                this.DestroyInstance();
            }
        }

        private void UpdateProjectilePosition()
        {
            Vector3 trajectoryRange = m_target - m_trajectoryStartPoint;

            if(trajectoryRange.x < 0f)
            {
                //If target is behind the target
                m_moveSpeed = -m_moveSpeed;
            }

            float nextPositionX = transform.position.x + m_moveSpeed * Time.deltaTime;
            float nextPositionXNormalized = (nextPositionX - m_trajectoryStartPoint.x) / trajectoryRange.x;

            float nextPositionYNormalized = m_trajectoryCurve.Evaluate(nextPositionXNormalized);
            float nextPositionCorrectionYNormalized = m_axisCorrectionCurve.Evaluate(nextPositionXNormalized);
            float nextPositionYCorrectionAbsolute = nextPositionCorrectionYNormalized * trajectoryRange.y;

            float nextPositionY = m_trajectoryStartPoint.y + (nextPositionYNormalized * m_trajectoryMaxRelativeHeight) + nextPositionYCorrectionAbsolute;

            Vector3 nextPosition = new Vector3(nextPositionX, nextPositionY, 0);

            CalculateProjectileSpeed(nextPositionXNormalized);

            transform.position = nextPosition;
        }

        private void CalculateProjectileSpeed(float nextNormalizedXPosition)
        {
            float nextMoveSpeedNormalized = m_speedCurve.Evaluate(nextNormalizedXPosition);

            m_moveSpeed = nextMoveSpeedNormalized * m_maxMoveSpeed;
        }

        public void InitializeProjectile(Vector3 target, float maxMoveSpeed, float trajectoryMaxHeight)
        {
            this.m_target = target;
            this.m_maxMoveSpeed = maxMoveSpeed;
            float xDistanceToTarget = target.x - transform.position.x;
            this.m_trajectoryMaxRelativeHeight = Mathf.Abs(xDistanceToTarget) * trajectoryMaxHeight;
        }

        public void InitializeAnimationCurve(AnimationCurve trajectoryAnimationCurve, AnimationCurve axisCorrectionCurve, AnimationCurve speedCurve)
        {
            this.m_trajectoryCurve = trajectoryAnimationCurve;
            this.m_axisCorrectionCurve = axisCorrectionCurve;
            this.m_speedCurve = speedCurve;
        }
    }
}

