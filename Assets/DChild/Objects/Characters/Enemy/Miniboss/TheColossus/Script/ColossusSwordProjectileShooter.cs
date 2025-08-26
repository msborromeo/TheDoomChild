using DChild.Gameplay.Pooling;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Characters.Enemies
{
    public class ColossusSwordProjectileShooter : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_swordProjectilePrefab;

        [SerializeField]
        private float m_projectileMaxMoveSpeed;
        [SerializeField]
        private float m_projectileMaxHeight;

        [SerializeField]
        private AnimationCurve m_trajectoryAnimationCurve;
        [SerializeField]
        private AnimationCurve m_axisCorrectionAnimationCurve;
        [SerializeField]
        private AnimationCurve m_speedCurve;

        [Button]
        public void ShootProjectile(Vector2 targetPosition)
        {
            var swordProjectile = GameSystem.poolManager.GetPool<PoolableObjectPool>().GetOrCreateItem(m_swordProjectilePrefab, transform.position, Quaternion.identity);
            var swordProjectileInitialize = swordProjectile.GetComponent<ColossusSwordProjectile>();
            swordProjectileInitialize.InitializeProjectile(targetPosition, m_projectileMaxMoveSpeed, m_projectileMaxHeight);
            swordProjectileInitialize.InitializeAnimationCurve(m_trajectoryAnimationCurve, m_axisCorrectionAnimationCurve, m_speedCurve);
        }
    }
}

