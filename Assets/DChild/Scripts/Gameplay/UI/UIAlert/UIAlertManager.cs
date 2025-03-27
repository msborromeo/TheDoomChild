using DChild.Codex.Characters;
using DChild.Menu.Bestiary;
using System;
using UnityEngine;

namespace DChild.Gameplay.UI.Alerts
{

    [System.Serializable]
    public class UIAlertManager : MonoBehaviour
    {

        [System.Serializable]
        public class SaveData
        {
            public int[] bestiaryAlerts;
            public int[] charactersAlerts;
        }

        [SerializeField]
        private UIAlertRecorder<BestiaryData> m_bestiaryAlerts;
        [SerializeField]
        private UIAlertRecorder<CharacterCodexData> m_charactersAlerts;

        public UIAlertRecorder<BestiaryData> bestiaryAlerts => m_bestiaryAlerts;
        public UIAlertRecorder<CharacterCodexData> charactersAlerts => m_charactersAlerts;

        public SaveData Save()
        {
            return new SaveData()
            {
                bestiaryAlerts = m_bestiaryAlerts.GetAlertSaveData(),
                charactersAlerts = m_charactersAlerts.GetAlertSaveData()
            };
        }

        public void Load(SaveData data)
        {
            m_bestiaryAlerts = new UIAlertRecorder<BestiaryData>(data.bestiaryAlerts);

            m_charactersAlerts = new UIAlertRecorder<CharacterCodexData>(data.charactersAlerts);
        }
        private void OnPostDeserialization(object sender, CampaignSlotUpdateEventArgs eventArgs)
        {
            if (!eventArgs.IsPartOfTheUpdate(SerializationScope.Gameplay))
                return;

            var saveData = GameplaySystem.campaignSerializer.slot.uiAlertSaveData;
            if (saveData == null)
                return;

            Load(saveData);
        }

        private void OnPreSerialization(object sender, CampaignSlotUpdateEventArgs eventArgs)
        {
            if (!eventArgs.IsPartOfTheUpdate(SerializationScope.Gameplay))
                return;

            GameplaySystem.campaignSerializer.slot.UpdateUIAlertSaveData(Save());
        }

        private void Awake()
        {
            GameplaySystem.campaignSerializer.PreSerialization += OnPreSerialization;
            GameplaySystem.campaignSerializer.PostDeserialization += OnPostDeserialization;
        }

        private void OnDestroy()
        {
            GameplaySystem.campaignSerializer.PreSerialization -= OnPreSerialization;
            GameplaySystem.campaignSerializer.PostDeserialization -= OnPostDeserialization;
        }
    }
}