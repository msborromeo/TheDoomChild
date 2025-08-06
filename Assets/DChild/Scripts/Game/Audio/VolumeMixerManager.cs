using DarkTonic.MasterAudio;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace DChild.Gameplay.Systems
{


    public class VolumeMixerManager : SerializedMonoBehaviour
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

        public void AdjustVolume(WorldType worldType, AudioType audioType,float volume)
        {
            var audioName = $"{worldType}_{audioType}";
            m_audioMixer.SetFloat(audioName, volume);
        }

        private void Awake()
        {
            m_snapshotHandle.Initialize(m_audioMixer);
        }
        void Start()
        {

        }


        void Update()
        {

        }


    }
}

