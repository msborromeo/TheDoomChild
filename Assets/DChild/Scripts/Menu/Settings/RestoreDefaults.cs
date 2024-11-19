using DChild;
using DChild.Configurations;
using DChild.Configurations.Visuals;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.UI
{
    public class RestoreDefaults : MonoBehaviour
    {
        [SerializeField]
        private ResolutionDropdown m_resolutionDropdown;
        [SerializeField]
        private FullScreenField m_fullScreenField;
        [SerializeField]
        private VSyncField m_vsyncField;
        [SerializeField]
        private BrightnessSlider m_brightnessSlider;
        [SerializeField]
        private BloomField m_bloomField;
        [SerializeField]
        private AntiAliasingDropdown m_antiAliasingDropdown;
        [SerializeField]
        private MasterVolumeSlider m_masterVolumeSlder;
        [SerializeField]
        private MusicSlider m_musicSlider;
        [SerializeField]
        private SoundVolumeSlider m_soundVolumeSlder;


        public void ResetSettings()
        {
            GameSystem.settings.LoadDefaultSettings();

            m_resolutionDropdown.UpdateUI();
            m_fullScreenField.UpdateUI();
            m_vsyncField.UpdateUI();
            m_brightnessSlider.UpdateUI();
            m_bloomField.UpdateUI();
            m_antiAliasingDropdown.UpdateUI();
            m_masterVolumeSlder.UpdateUI();
            m_musicSlider.UpdateUI();
            m_soundVolumeSlder.UpdateUI();

        }
    }
}
