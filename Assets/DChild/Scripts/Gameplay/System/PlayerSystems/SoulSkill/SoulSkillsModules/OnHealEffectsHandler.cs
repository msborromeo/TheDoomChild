using DChild.Gameplay;
using Holysoft.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class OnHealEffectsHandler : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem m_particleeffects;
    [SerializeField]
    private VisualEffect m_visualeffects;
    [SerializeField]
    private bool m_isparticle = false;
    private void Start()
    {
        if (m_isparticle == true)
        {
            m_particleeffects.Stop();
        }
        else
        {
            m_visualeffects.Stop();
        }

        GameplaySystem.playerManager.player.health.HealthGained += OnHealthGained;
        this.transform.localPosition = new Vector3(0.0f, 8.0f, 0.0f);
    }

    private void OnHealthGained(bool obj)
    {
        if (obj == false)
            return;

        if (m_isparticle == true)
        {
            m_particleeffects.Stop();
            m_particleeffects.Play();
        }
        else
        {
            m_visualeffects.Stop();
            m_visualeffects.Play();
        }
    }

    private void OnDestroy()
    {
        GameplaySystem.playerManager.player.health.HealthGained -= OnHealthGained;
    }
}
