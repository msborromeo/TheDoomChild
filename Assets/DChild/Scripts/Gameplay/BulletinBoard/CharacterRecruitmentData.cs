using DChild.Gameplay.Characters.Enemies;
using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Items;
using DChild.Menu.Bestiary;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI.BulletinBoard
{
    [CreateAssetMenu(fileName = "CharacterRecruitmentData", menuName = "DChild/Gameplay/Army/Recruitment")]
    public class CharacterRecruitmentData : ScriptableObject
    {
        [SerializeField] private ArmyCharacterData m_characterData;
        public ArmyCharacterData characterData => m_characterData;
               
        [SerializeField]
        private int m_recruitmentCost;
        public int recruitmentCost => m_recruitmentCost;


        [SerializeField, TabGroup("Main", "Requirements")]
        private bool m_requiresSoulEssence;
        public bool requiresSoulEssence => m_requiresSoulEssence;

        [ShowIfGroup("Main/Requirements/SoulEssenceToggle", MemberName = "m_requiresSoulEssence")]
        [SerializeField, BoxGroup("Main/Requirements/SoulEssenceToggle/SoulEssenceRequirement")]
        private int m_requiredSoulEssence;
        public int requiredSoulEssence => m_requiredSoulEssence;


        [SerializeField, TabGroup("Main", "Requirements")]
        private bool m_requiresItem;
        public bool requiresItem => m_requiresItem;

        [ShowIfGroup("Main/Requirements/ItemToggle", MemberName = "m_requiresItem")]
        [SerializeField, BoxGroup("Main/Requirements/ItemToggle/ItemRequirement")]
        private List<ItemData> m_itemList;
        public List<ItemData> requiredItems => m_itemList;

        [SerializeField, TabGroup("Main", "Requirements")]
        private bool m_requiresCombatArt;
        public bool requiresCombatArt => m_requiresCombatArt;

        [ShowIfGroup("Main/Requirements/CombatArtToggle", MemberName = "m_requiresCombatArt")]
        [SerializeField, BoxGroup("Main/Requirements/CombatArtToggle/CombatArtRequirement")]
        private CombatArtData m_CombatArt;
        public CombatArtData combatArt => m_CombatArt;
        [ShowIfGroup("Main/Requirements/CombatArtToggle", MemberName = "m_requiresCombatArt")]
        [SerializeField, BoxGroup("Main/Requirements/CombatArtToggle/CombatArtRequirement")]
        private int m_CombatArtLevel;
        public int combatArtLevel => m_CombatArtLevel;


        [SerializeField, TabGroup("Main", "Requirements")]
        private bool m_requiresPrimarySkill;
        public bool requiresPrimarySkill => m_requiresPrimarySkill;

        [ShowIfGroup("Main/Requirements/PrimarySkillToggle", MemberName = "m_requiresPrimarySkill")]
        [SerializeField, BoxGroup("Main/Requirements/PrimarySkillToggle/PrimarySkillRequirement")]
        private PrimarySkill m_PrimarySkill;
        public PrimarySkill primarySkill => m_PrimarySkill;

        [SerializeField, TabGroup("Main", "Requirements")]
        private bool m_requiresCharacter;
        public bool requiresCharacter => m_requiresCharacter;

        [ShowIfGroup("Main/Requirements/Character", MemberName = "m_requiresCharacter")]
        [SerializeField, BoxGroup("Main/Requirements/Character/RequiredCharacter")]
        private ArmyCharacterData m_requiredCharacter;
        public ArmyCharacterData requiredCharacter => m_requiredCharacter;

        [SerializeField, TabGroup("Main", "Requirements")]
        private bool m_requiresNPCCount;
        public bool requiresNPCCount => m_requiresNPCCount;

        [ShowIfGroup("Main/Requirements/NPCsAmountToggle", MemberName = "m_requiresNPCCount")]
        [SerializeField, BoxGroup("Main/Requirements/NPCsAmountToggle/Character Count")]
        private int m_requiredNPCCount;
        public int requiredNPCCount => m_requiredNPCCount;


        [SerializeField, TabGroup("Main", "Requirements")]
        private bool m_requiresDefeatedBoss;
        public bool requiresDefeatedBoss => m_requiresDefeatedBoss;
        [ShowIfGroup("Main/Requirements/BossDefeated", MemberName = "m_requiresDefeatedBoss")]
        [SerializeField, BoxGroup("Main/Requirements/BossDefeated/Boss")]
        private BestiaryData m_defeatedBoss;
        public BestiaryData defeatedBoss => m_defeatedBoss;

        [SerializeField, TabGroup("Main", "Requirements")]
        private bool m_requiresArmyBattleWins;
        public bool requiresArmyBattleWins => m_requiresArmyBattleWins;

        [ShowIfGroup("Main/Requirements/ArmyBattle", MemberName = "m_requiresArmyBattleWins")]
        [SerializeField, BoxGroup("Main/Requirements/ArmyBattle/Battles Won")]
        private int m_battlesWon;
        public int battlesWon => m_battlesWon;
    }
}