using DChild.Gameplay.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Combat
{
    public class SkullProjectileLifetime : PoolableObject
    {
        [SerializeField]
        private Renderer m_renderer;
        [SerializeField]
        private float m_lifeTime;

        private float m_lifeTimeCounter = 0;
        private bool m_isCheckingVisibility = false;

        private void Awake()
        {
            m_lifeTimeCounter = m_lifeTime;
        }

        // Update is called once per frame
        void Update()
        {
            if (m_renderer.isVisible == false)
            {
                CountdownTimer();
            }
            else
            {
                RestartTimer();
            }
        }

        private void CountdownTimer()
        {
            m_lifeTimeCounter -= Time.deltaTime;

            if(m_lifeTimeCounter < 0)
            {
                this.DestroyInstance();
            }
        }

        private void RestartTimer()
        {
            m_lifeTimeCounter = m_lifeTime;
        }
    }

}
