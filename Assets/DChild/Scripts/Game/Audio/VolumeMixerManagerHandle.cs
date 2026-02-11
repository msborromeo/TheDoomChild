using DarkTonic.MasterAudio;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace DChild.Gameplay.Systems
{


    public class VolumeMixerManagerHandle : SerializedMonoBehaviour, IGameplaySystemModule, IGameplayInitializable
    {
        [OdinSerialize, HideReferenceObjectPicker]
        private AudioSnapshotHandle m_snapshotHandle;

        [SerializeField]
        private AudioMixerGroup m_mixerGroup;
        [SerializeField]
        private AudioMixer m_audioMixer;

        public AudioMixerGroup mixerGroup => m_mixerGroup;

        [Button]
        public void UseSnapshot(AudioSnapshot snapshot)
        {
            if (m_snapshotHandle.HasCurrentSnapshot)
            {
                m_snapshotHandle.TransistionTo(snapshot);
            }
            else
            {
                m_snapshotHandle.ForceSnapshot(snapshot);
            }
        }

        public void UseSnapshot(AudioSnapshot snapshot, float duration)
        {
            if (m_snapshotHandle.HasCurrentSnapshot)
            {
                m_snapshotHandle.TransistionTo(snapshot,duration);
            }
            else
            {
                m_snapshotHandle.ForceSnapshot(snapshot);
            }
        }

        public void AdjustVolume(WorldType worldType, AudioType audioType,float volume)
        {
            var audioName = $"{worldType}_{audioType}";
            m_audioMixer.SetFloat(audioName, volume);
        }

        public void Initialize()
        {
            m_snapshotHandle.Initialize(m_audioMixer);
            Debug.Log($"VolumeMixerManager was Initialized {m_snapshotHandle != null} && {m_audioMixer != null}");
        }
    }
}

