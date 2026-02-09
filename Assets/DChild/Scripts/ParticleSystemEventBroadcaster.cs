using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

namespace DChild
{
    enum ParticleSystemState
    {
        Started,
        Idle, //Idle means just there, not really gonna do anything
        Stopped
    }

    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleSystemEventBroadcaster : MonoBehaviour
    {
        [SerializeField, ReadOnly(true)] 
        ParticleSystemState m_state;

        [SerializeField]
        private UnityEvent m_particleStartedEvents;
        [SerializeField]
        private UnityEvent m_particleEndedEvents;

        private ParticleSystem m_particleSystem;

        private void Awake()
        {
            m_particleSystem = GetComponent<ParticleSystem>();    
        }

        private void OnEnable()
        {
            m_state = ParticleSystemState.Idle;
        }

        private void LateUpdate()
        {
            if(m_particleSystem.isPlaying && m_state != ParticleSystemState.Started)
            {
                m_state = ParticleSystemState.Started;
                m_particleStartedEvents?.Invoke();
            }

            if(m_particleSystem.isStopped &&  m_state != ParticleSystemState.Stopped)
            {
                m_state = ParticleSystemState.Stopped;
                m_particleEndedEvents?.Invoke();
            }
        }

    }
}

