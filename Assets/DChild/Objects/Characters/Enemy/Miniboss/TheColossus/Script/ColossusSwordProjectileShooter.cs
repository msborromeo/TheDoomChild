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
        private Transform m_target;

        [SerializeField]
        private float m_shootRate;
        [SerializeField]
        private float m_projectileMaxMoveSpeed;
        [SerializeField]
        private float m_projectileMaxHeight;
        private float m_shootTimer;

        [SerializeField]
        private AnimationCurve m_trajectoryAnimationCurve;
        [SerializeField]
        private AnimationCurve m_axisCorrectionAnimationCurve;
        [SerializeField]
        private AnimationCurve m_speedCurve;

        private void Update()
        {
            m_shootTimer -=  Time.deltaTime;

            if(m_shootTimer <= 0)
            {
                m_shootTimer = m_shootRate;
                ColossusSwordProjectile swordProjectile = Instantiate(m_swordProjectilePrefab, transform.position, Quaternion.identity).GetComponent<ColossusSwordProjectile>();
                //change this to pooled version eventually
                swordProjectile.InitializeProjectile(m_target, m_projectileMaxMoveSpeed, m_projectileMaxHeight);
                swordProjectile.InitializeAnimationCurve(m_trajectoryAnimationCurve, m_axisCorrectionAnimationCurve, m_speedCurve);
            }
        }
    }
}

