using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace DChild.Gameplay.Systems
{
    public enum AudioSnapshot
    {
        Gameplay,
        Dialogue,
        GamePause,
        Cinematic,
        FullScreenNotif,
        BossFight
    }
    [System.Serializable]
    public class AudioSnapshotHandle
    {
        [SerializeField]
        private Dictionary<AudioSnapshot, AudioMixerSnapshot> m_snapshot;

        [SerializeField]
        private AudioMixerSnapshot m_currentSnapshot;
        private AudioMixer m_reference;

        private AudioMixerSnapshot[] transistionReference;
        private float[] transistionWeight;
        private const float DEFAULT_TRANSISTION_DURATION = 0f;

        public bool HasCurrentSnapshot => m_currentSnapshot != null;

        public void Initialize(AudioMixer reference)
        {
            m_reference = reference;
            transistionReference = new AudioMixerSnapshot[2];
            m_currentSnapshot = m_snapshot[0];
            transistionWeight = new float[] { 0, 1f };
        }

        public void ForceSnapshot(AudioSnapshot snapshot)
        {
            m_currentSnapshot = GetAudioMixerSnapshot(snapshot);
            //Find a way to manually copy Snapshot details;
        }

        public void TransistionTo(AudioSnapshot snapshot, float duration = DEFAULT_TRANSISTION_DURATION)
        {
            if (m_currentSnapshot == null)
            {
                //Debug Cannot transistion from not existent Snapshot better use Force instead
                return;
            }

            transistionReference[0] = m_currentSnapshot;
            transistionReference[1] = GetAudioMixerSnapshot(snapshot);
            m_reference.TransitionToSnapshots(transistionReference, transistionWeight, duration);
            m_currentSnapshot = transistionReference[1];
        }

        private AudioMixerSnapshot GetAudioMixerSnapshot(AudioSnapshot snapshot)
        {
            return m_snapshot[snapshot];
        }
    }
}

