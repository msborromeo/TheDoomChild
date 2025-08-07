using DChild.Gameplay;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace DChild.Gameplay.Systems
{
    //public enum WorldType
    //{
    //    Underworld,
    //    Overworld,
    //    ArmyBattle,
    //    MainMenu
    //}
   
    public class OnSceneVolumeManager : MonoBehaviour
    {
        //[SerializeField]
        //private Dictionary<AudioType, AudioMixerGroup> m_mixerGroup;
        [SerializeField, MinMaxSlider(0f, 100f)]
        private float m_volume;
        [SerializeField]
        private AudioType m_type;
        public void SetAttenuationVolume()
        {
            switch (m_type)
            {
                case AudioType.Player:
                case AudioType.Ambience:
                case AudioType.BGM:
                case AudioType.UI:
                case AudioType.ArmyBattle:
                    //var audioName = $"{GameplaySystem.GetCurrentWorldType()}_{m_type}";
                    GameplaySystem.volumeMixerManager.AdjustVolume(GameplaySystem.GetCurrentWorldType(),m_type, m_volume);
                    break;

            }
        }
        void Start()
        {

        }


        void Update()
        {

        }
    }

}
