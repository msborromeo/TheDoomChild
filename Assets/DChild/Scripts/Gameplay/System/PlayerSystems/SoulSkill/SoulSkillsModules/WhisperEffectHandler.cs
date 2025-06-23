using DChild.Gameplay;
using DChild.Gameplay.Characters.Players.SoulSkills;
using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WhisperEffectHandler : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem m_particleeffects;
    [SerializeField]
    private VisualEffect m_visualeffects;
    [SerializeField]
    private bool m_isparticle = false;
    [SerializeField]
    private GameObject m_noWhispererDialogue;
    [SerializeField]
    private GameObject m_whispererDialogue;
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

        FurryWhisperer.Onstatechange += StateChange;        
    }

    private void StateChange(object sender, FurryWhisperer.StateChangeEvent eventArgs)
    {
        
      if (eventArgs.isactive == true)
        {
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
            m_noWhispererDialogue.SetActive(false);
            m_whispererDialogue.SetActive(true);
        }
      else
        {
            if (m_isparticle == true)
            {
                m_particleeffects.Stop();
            }
            else
            {
                m_visualeffects.Stop();
            }
            m_noWhispererDialogue.SetActive(true);
            m_whispererDialogue.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        FurryWhisperer.Onstatechange -= StateChange;
    }
}
