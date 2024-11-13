using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Characters.Players.Modules;
using DChild.Gameplay.Combat;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalSwordFX : MonoBehaviour
{
    [SerializeField, Tooltip("Particle effects for basic element attacks")]
    private ParticleSystem m_physicalFX;
    public ParticleSystem physicalFX => m_physicalFX;
    [SerializeField, Tooltip("Particle effects for fire element attacks")]
    private ParticleSystem m_fireFX;
    public ParticleSystem fireFX => m_fireFX;
    [SerializeField, Tooltip("Particle effects for ice element attacks")]
    private ParticleSystem m_iceFX;
    public ParticleSystem iceFX => m_iceFX;
    [SerializeField, Tooltip("Particle effects for lightning element attacks")]
    private ParticleSystem m_lightningFX;
    public ParticleSystem lightningFX => m_lightningFX;

    [SerializeField]
    private Type m_type = Type.GroundOverhead;
    private Element m_currentElement = Element.Physical;
    public enum Type
    {
        GroundOverhead,
        Crouch,
        MidairForward,
        MidairUpward,
        Slash1,
        Slash2,
        Slash3
    }
    public enum Element
    {
        Physical,
        Fire,
        Ice,
        Lightning
    }
    public void SetElementTo(Element newElement)
    {
        m_currentElement = newElement;
    }

    public void Play()
    {
        //Stop();

        switch (m_currentElement)
        {
            case Element.Physical:
                physicalFX.Play();
                break;
            case Element.Fire:
                fireFX.Play();
                break;
            case Element.Ice:
                iceFX.Play();
                break;
            case Element.Lightning:
                lightningFX.Play();
                break;
        }
    }

    public void Stop()
    {
        if (physicalFX == null)
        {
            Debug.Log("stop physical fx is empty");
        }
        else
        {
            physicalFX.Stop();
        }
        if (fireFX == null)
        {
            Debug.Log("stop fire fx is empty");
        }
        else
        {
            fireFX.Stop();
        }
        if (iceFX == null)
        {
            Debug.Log("stop ice fx is empty");
        }
        else
        {
            iceFX.Stop();
        }
        if (lightningFX == null)
        {
            Debug.Log("stop lightning fx is empty");
        }
        else
        {
            lightningFX.Stop();
        }
    }
    public void Clear()
    {
        if (physicalFX == null)
        {
            Debug.Log("clear physical fx is empty");
        }
        else
        {
            physicalFX.Clear();
        }
        if (fireFX == null)
        {
            Debug.Log("clear fire fx is empty");
        }
        else
        {
            fireFX.Clear();
        }
        if (iceFX == null)
        {
            Debug.Log("clear ice fx is empty");
        }
        else
        {
            iceFX.Clear();
        }
        if (lightningFX == null)
        {
            Debug.Log("clear lightning fx is empty");
        }
        else
        {
            lightningFX.Clear();
        }
    }
}