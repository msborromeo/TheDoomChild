using DChild.Codex.Characters;
using DChild.Codex.LocationCodex;
using DChild.Codex.Lore;
using DChild.Codex.Tutorial;
using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Characters.Players.SoulSkills;
using DChild.Gameplay.SoulSkills;
using DChild.Menu.Bestiary;
using PixelCrushers.DialogueSystem;
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
            public int[] armyTroopAlerts;
            public int[] charactersAlerts;
            public int[] loreAlerts;
            public int[] locationAlerts;
            public int[] tutorialAlerts;
            public string[] questsRecordedAlerts;
            public string[] questsAlerts;
        }

        [SerializeField, TabGroup("General")]
        private PrimarySkillUIAlertRecorder m_primarySkillAlerts;
        [SerializeField, TabGroup("General")]
        private InventoryUIAlertRecorder m_inventoryAlerts;
        [SerializeField, TabGroup("General")]
        private DatabaseUIAlertRecorder<SoulSkill> m_soulSkillAlerts;
        [SerializeField, TabGroup("Codex")]
        private DatabaseUIAlertRecorder<BestiaryData> m_bestiaryAlerts;
        [SerializeField, TabGroup("Codex")]
        private DatabaseUIAlertRecorder<CharacterCodexData> m_armyTroopAlerts;
        [SerializeField, TabGroup("Codex")]
        private DatabaseUIAlertRecorder<CharacterCodexData> m_charactersAlerts;
        [SerializeField, TabGroup("Codex")]
        private DatabaseUIAlertRecorder<LoreCodexData> m_loreAlerts;
        [SerializeField, TabGroup("Codex")]
        private DatabaseUIAlertRecorder<LocationCodexData> m_locationAlerts;
        [SerializeField, TabGroup("Codex")]
        private DatabaseUIAlertRecorder<TutorialCodexData> m_tutorialAlerts;
        [SerializeField, TabGroup("Codex")]
        private QuestUIAlertRecorder m_questAlerts;


        public PrimarySkillUIAlertRecorder primarySkillAlerts => m_primarySkillAlerts;
        public InventoryUIAlertRecorder inventoryAlerts => m_inventoryAlerts;
        public DatabaseUIAlertRecorder<SoulSkill> soulSkillAlerts => m_soulSkillAlerts;
        public DatabaseUIAlertRecorder<BestiaryData> bestiaryAlerts => m_bestiaryAlerts;
        public DatabaseUIAlertRecorder<CharacterCodexData> armyTroopAlerts => m_armyTroopAlerts;
        public DatabaseUIAlertRecorder<CharacterCodexData> charactersAlerts => m_charactersAlerts;
        public DatabaseUIAlertRecorder<LoreCodexData> loreAlerts => m_loreAlerts;
        public DatabaseUIAlertRecorder<LocationCodexData> locationAlerts => m_locationAlerts;
        public DatabaseUIAlertRecorder<TutorialCodexData> tutorialAlerts => m_tutorialAlerts;
        public QuestUIAlertRecorder questAlerts => m_questAlerts;


        public SaveData Save()
        {
            return new SaveData()
            {
                primarySkillAlerts = m_primarySkillAlerts.GetAlertSaveData(),
                inventoryRecordedItems = m_inventoryAlerts.GetRecordedItems(),
                inventoryAlerts = m_inventoryAlerts.GetAlertSaveData(),
                soulSkillAlerts = m_soulSkillAlerts.GetAlertSaveData(),
                bestiaryAlerts = m_bestiaryAlerts.GetAlertSaveData(),
                armyTroopAlerts = m_armyTroopAlerts.GetAlertSaveData(),
                charactersAlerts = m_charactersAlerts.GetAlertSaveData(),
                loreAlerts = m_loreAlerts.GetAlertSaveData(),
                locationAlerts = m_locationAlerts.GetAlertSaveData(),
                tutorialAlerts = m_tutorialAlerts.GetAlertSaveData(),
                questsRecordedAlerts = m_questAlerts.GetRecordedItems(),
                questsAlerts = m_questAlerts.GetAlertSaveData()
            };
        }

        public void Load(SaveData data)
        {
            if (data == null)
            {
                m_primarySkillAlerts = new PrimarySkillUIAlertRecorder(PrimarySkill.None);
                m_inventoryAlerts = new InventoryUIAlertRecorder(null, null);
                m_soulSkillAlerts = new DatabaseUIAlertRecorder<SoulSkill>(null);
                m_bestiaryAlerts = new DatabaseUIAlertRecorder<BestiaryData>(null);
                m_armyTroopAlerts = new DatabaseUIAlertRecorder<CharacterCodexData>(null);
                m_charactersAlerts = new DatabaseUIAlertRecorder<CharacterCodexData>(null);
                m_loreAlerts = new DatabaseUIAlertRecorder<LoreCodexData>(null);
                m_locationAlerts = new DatabaseUIAlertRecorder<LocationCodexData>(null);
                m_tutorialAlerts = new DatabaseUIAlertRecorder<TutorialCodexData>(null);
                m_questAlerts = new QuestUIAlertRecorder(null, null);
                return;
            }

            m_primarySkillAlerts = new PrimarySkillUIAlertRecorder((PrimarySkill)data.primarySkillAlerts);
            m_inventoryAlerts = new InventoryUIAlertRecorder(data.inventoryRecordedItems, data.inventoryAlerts);
            m_soulSkillAlerts = new DatabaseUIAlertRecorder<SoulSkill>(data.soulSkillAlerts);
            m_bestiaryAlerts = new DatabaseUIAlertRecorder<BestiaryData>(data.bestiaryAlerts);
            m_armyTroopAlerts = new DatabaseUIAlertRecorder<CharacterCodexData>(data.armyTroopAlerts);
            m_charactersAlerts = new DatabaseUIAlertRecorder<CharacterCodexData>(data.charactersAlerts);
            m_loreAlerts = new DatabaseUIAlertRecorder<LoreCodexData>(data.loreAlerts);
            m_locationAlerts = new DatabaseUIAlertRecorder<LocationCodexData>(data.locationAlerts);
            m_tutorialAlerts= new DatabaseUIAlertRecorder<TutorialCodexData>(data.tutorialAlerts);
            m_questAlerts = new QuestUIAlertRecorder(data.questsRecordedAlerts, data.questsAlerts);
        }
        private void OnPostDeserialization(object sender, CampaignSlotUpdateEventArgs eventArgs)
        {
            if (!eventArgs.IsPartOfTheUpdate(SerializationScope.Gameplay))
                return;

            var saveData = GameplaySystem.campaignSerializer.slot.uiAlertSaveData;

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