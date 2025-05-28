using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI.BulletinBoard
{
    [CreateAssetMenu(fileName = "CharacterRecruitmentData", menuName = "DChild/Gameplay/Army/Recruitment")]
    public class CharacterRecruitmentData : ScriptableObject
    {
        [SerializeField] private ArmyCharacterData m_characterData;
        public ArmyCharacterData characterData => m_characterData;
               
        [SerializeField, TabGroup("Main", "Requirements")]
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
        private ItemData m_hasItem;
        public ItemData requiredItem => m_hasItem;
        
        [SerializeField, BoxGroup("Main/Requirements/ItemToggle/ItemRequirement")]
        private int m_ItemAmount;
        public int itemAmount => m_ItemAmount;

        [SerializeField, TabGroup("Main", "Requirements")]
        private bool m_requiresCombatArt;
        public bool requiresCombatArt => m_requiresCombatArt;

        [ShowIfGroup("Main/Requirements/CombatArtToggle", MemberName = "m_requiresCombatArt")]
        [SerializeField, BoxGroup("Main/Requirements/CombatArtToggle/CombatArtRequirement")]
        private CombatArt m_CombatArt;
        public CombatArt combatArt => m_CombatArt;


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
        [SerializeField, BoxGroup("Main/Requirements/NPCsAmountToggle/NPCsAmount")]
        private int m_requiredNPCCount;
        public int requiredNPCCount => m_requiredNPCCount;
    }
}