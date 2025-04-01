using DChild.Codex.Characters;
using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Characters.Players.SoulSkills;
using DChild.Gameplay.SoulSkills;
using DChild.Menu.Bestiary;
using Sirenix.OdinInspector;
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
            public int primarySkillAlerts;
            public int[] inventoryRecordedItems;
            public int[] inventoryAlerts;
            public int[] soulSkillAlerts;
            public int[] bestiaryAlerts;
            public int[] charactersAlerts;
        }

        [SerializeField, TabGroup("General")]
        private PrimarySkillUIAlertRecorder m_primarySkillAlerts;
        [SerializeField, TabGroup("General")]
        private InventoryUIAlertRecorder m_inventoryAlerts;
        [SerializeField, TabGroup("General")]
        private DatabaseUIAlertRecorder<SoulSkill> m_soulSkillAlerts;
        [SerializeField,TabGroup("Codex")]
        private DatabaseUIAlertRecorder<BestiaryData> m_bestiaryAlerts;
        [SerializeField, TabGroup("Codex")]
        private DatabaseUIAlertRecorder<CharacterCodexData> m_charactersAlerts;


        public PrimarySkillUIAlertRecorder primarySkillAlerts => m_primarySkillAlerts;
        public InventoryUIAlertRecorder inventoryAlerts => m_inventoryAlerts;
        public DatabaseUIAlertRecorder<SoulSkill> soulSkillAlerts => m_soulSkillAlerts;
        public DatabaseUIAlertRecorder<BestiaryData> bestiaryAlerts => m_bestiaryAlerts;
        public DatabaseUIAlertRecorder<CharacterCodexData> charactersAlerts => m_charactersAlerts;

        public SaveData Save()
        {
            return new SaveData()
            {
                primarySkillAlerts = m_primarySkillAlerts.GetAlertSaveData(),
                inventoryRecordedItems = m_inventoryAlerts.GetRecordedItems(),
                inventoryAlerts = m_inventoryAlerts.GetAlertSaveData(),
                soulSkillAlerts = m_soulSkillAlerts.GetAlertSaveData(),
                bestiaryAlerts = m_bestiaryAlerts.GetAlertSaveData(),
                charactersAlerts = m_charactersAlerts.GetAlertSaveData()
            };
        }

        public void Load(SaveData data)
        {
            if(data == null)
            {
                m_primarySkillAlerts = new PrimarySkillUIAlertRecorder(PrimarySkill.None);
                m_inventoryAlerts = new InventoryUIAlertRecorder(null, null);
                m_soulSkillAlerts = new DatabaseUIAlertRecorder<SoulSkill>(null);
                m_bestiaryAlerts = new DatabaseUIAlertRecorder<BestiaryData>(null);
                m_charactersAlerts = new DatabaseUIAlertRecorder<CharacterCodexData>(null);
                return;
            }

            m_primarySkillAlerts = new PrimarySkillUIAlertRecorder((PrimarySkill)data.primarySkillAlerts);
            m_inventoryAlerts = new InventoryUIAlertRecorder(data.inventoryRecordedItems, data.inventoryAlerts);
            m_soulSkillAlerts = new DatabaseUIAlertRecorder<SoulSkill>(data.soulSkillAlerts);
            m_bestiaryAlerts = new DatabaseUIAlertRecorder<BestiaryData>(data.bestiaryAlerts);
            m_charactersAlerts = new DatabaseUIAlertRecorder<CharacterCodexData>(data.charactersAlerts);
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