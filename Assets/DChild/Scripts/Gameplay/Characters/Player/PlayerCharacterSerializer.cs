using DChild.Gameplay.Inventories;
using DChild.Serialization;
using UnityEngine;
using Sirenix.OdinInspector;
using DChild.Gameplay.SoulSkills;
using DChild.Menu.Codex;
using Holysoft.Collections;
using DChild.Gameplay.Systems;
using DChild.Gameplay.EquipmentSystem;
using DChild.Gameplay.Characters.Player.Skins;

namespace DChild.Gameplay.Characters.Players
{

    [System.Serializable]
    public class PlayerCharacterSerializer : SerializedMonoBehaviour
    {
        [SerializeField, TabGroup("Inventory")]
        private ISerializable<TradableInventorySerialization> m_inventory;
        [SerializeField, TabGroup("Codex")]
        private ISerializable<CodexSaveData> m_codex;
        [SerializeField, TabGroup("Skill")]
        private ISerializable<PrimarySkillsData> m_playerSkills;
        [SerializeField, TabGroup("Equipped Soul Skill")]
        private ISerializable<PlayerSoulSkillData> m_soulSkillHandle;
        [SerializeField, TabGroup("Equipped Soul Equipment")]
        private ISerializable<PlayerSoulEquipmentData> m_soulEquipmentHandle;
        [SerializeField]
        private ISerializable<CombatArtsSaveData> m_combatArts;
        [SerializeField]
        private ISerializable<WeaponUpgradeSaveData> m_playerWeapon;
        [SerializeField]
        private ISerializable<SkinSaveData> m_playerSkin;

        public PlayerCharacterData SaveData()
        {
            return new PlayerCharacterData(m_inventory.SaveData(),
                                        m_codex.SaveData(),
                                        m_playerSkills.SaveData(),
                                        m_soulSkillHandle.SaveData(),
                                        m_combatArts.SaveData(),
                                        m_playerWeapon.SaveData(),
                                        m_soulEquipmentHandle.SaveData(),
                                        m_playerSkin.SaveData());
        }

        public void LoadData(PlayerCharacterData data)
        {
            m_inventory.LoadData(data.inventoryData);
            m_codex.LoadData(data.codexData);
            m_playerSkills.LoadData(data.skills);
            m_soulSkillHandle.LoadData(data.soulSkillData);
            m_combatArts.LoadData(data.combatArtsData);
            m_playerWeapon.LoadData(data.weaponUpgradeSaveData);
            m_soulEquipmentHandle.LoadData(data.soulEquipmentData);
            m_playerSkin.LoadData(data.skinSaveData);
        }
    }
}