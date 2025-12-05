using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Combat
{
    public class CriticalHitVFXHandle : MonoBehaviour
    {
        [SerializeField]
        private Attacker m_attacker;

        private void OnEnable()
        {
            m_attacker.CriticalHitInflicted += OnCriticalHitInflicted;
        }

        private void OnDisable()
        {
            m_attacker.CriticalHitInflicted -= OnCriticalHitInflicted;
        }

        private void OnCriticalHitInflicted(Vector3 vector, ParticleFX vfx)
        {
            var fx = GameSystem.poolManager.GetPool<FXPool>().GetOrCreateItem(vfx.gameObject, vector, Quaternion.identity);
        }
    }
}

