using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using DChild.Gameplay.Environment.Interractables;
using Holysoft.Event;
using DChild.Gameplay.Items;
using DChild.Gameplay.Trade;
using DChild.Gameplay.Characters.Players;

namespace DChild.Gameplay.ArmyBattle
{
    public class ArmyBattleCharacterReward : MonoBehaviour
    {

        [TabGroup("Main","Reference")]
        //[SerializeField, TabGroup("Main/Reference", "General References")]
        //private SpriteRenderer m_Graphics;
        //[SerializeField, TabGroup("Main/Reference", "General References")]
        //private Vector3 m_promptOffset;
        [SerializeField, TabGroup("Main/Reference", "General References")]
        private CharacterGiver m_CharacterGiver;


        [SerializeField, TabGroup("Main/Reference", "Actions")]
        private UnityEvent m_GiveReward,m_RequirementFailed;
        [SerializeField, TabGroup("Main/Reference","Character Reward")]
        private List<ArmyCharacterData> m_CharacterReward;


        [SerializeField, TabGroup("Main","Requirements")]
        private bool m_isFree;
        [SerializeField, TabGroup("Main", "Requirements"), HideIf("m_isFree")]
        private bool m_requiresSoulEssence;
            [ShowIfGroup("Main/Requirements/SoulEssenceToggle", MemberName = "m_requiresSoulEssence")]
            [SerializeField, BoxGroup("Main/Requirements/SoulEssenceToggle/SoulEssenceRequirement")]
            private int m_requiredSoulEssence;


        [SerializeField, TabGroup("Main","Requirements"), HideIf("m_isFree")]
        private bool m_requiresItem;
            [ShowIfGroup("Main/Requirements/ItemToggle", MemberName = "m_requiresItem")]
            [SerializeField, BoxGroup("Main/Requirements/ItemToggle/ItemRequirement")]
            private ItemData m_hasItem;
            [SerializeField, BoxGroup("Main/Requirements/ItemToggle/ItemRequirement"),Min(1)]
            private int m_ItemAmount = 1;

        [SerializeField, TabGroup("Main", "Requirements"), HideIf("m_isFree")]
        private bool m_requiresCombatArt;
            [ShowIfGroup("Main/Requirements/CombatArtToggle", MemberName = "m_requiresCombatArt")]
            [SerializeField, BoxGroup("Main/Requirements/CombatArtToggle/CombatArtRequirement")]
            private CombatArt m_CombatArt;

        [SerializeField, TabGroup("Main", "Requirements"), HideIf("m_isFree")]
        private bool m_requiresPrimarySkill;
            [ShowIfGroup("Main/Requirements/PrimarySkillToggle", MemberName = "m_requiresPrimarySkill")]
            [SerializeField, BoxGroup("Main/Requirements/PrimarySkillToggle/PrimarySkillRequirement")]
            private PrimarySkill m_PrimarySkill;

        [SerializeField, TabGroup("Main", "Requirements"), HideIf("m_isFree")]
        private bool m_requiresSpecificNPC;
            [ShowIfGroup("Main/Requirements/SpecificNPCToggle", MemberName = "m_requiresSpecificNPC")]
            [SerializeField, BoxGroup("Main/Requirements/SpecificNPCToggle/RequiredNPC")]
            private ArmyCharacterData armyCharacterData;

        [SerializeField, TabGroup("Main", "Requirements"), HideIf("m_isFree")]
        private bool m_requiresMinimumNPCsRecruited;
            [ShowIfGroup("Main/Requirements/NPCsAmountToggle", MemberName = "m_requiresMinimumNPCsRecruited")]
            [SerializeField, BoxGroup("Main/Requirements/NPCsAmountToggle/NPCsAmount")]
            private int neededNPCsRecruited;

        [SerializeField, TabGroup("Main", "Requirements"),Tooltip("Takes the item from the player's invintory if possible")]
        private bool m_TakeRequiredItemFromInvintory;

        private bool m_OtherConditions;

        private bool m_RequirementAchieved;


        public event EventAction<EventActionArgs> InteractionOptionChange;

        [Button]
        public void GiveReward()
        {
            m_CharacterGiver?.RecruitCharacter(m_CharacterReward);


            //Because Characters are usually recieved at isolated maps where save points do not exists
            //and Underworld data is lost upon existing due to changing into Overworld Data 
            GameplaySystem.campaignSerializer.UpdateData(SerializationScope.Quest);  
        }

        public void RequirementMet(bool isAchieved)
        {
            m_RequirementAchieved = isAchieved;
        }

        public void HasOtherConditions(bool x)
        {
            m_OtherConditions = x;
        }

        public void AttemptGiveReward()
        {
            if(!m_isFree)
            {
                if(m_requiresSoulEssence)
                {
                    if (GameplaySystem.playerManager.player.inventory.GetCurrencyAmount(CurrencyType.SoulEssence) < m_requiredSoulEssence && m_requiredSoulEssence != 0)
                    {
                        RequirementFailed();
                        return;
                    }else if(m_TakeRequiredItemFromInvintory)
                    {
                        GameplaySystem.playerManager.player.inventory.AddSoulEssence(-m_requiredSoulEssence);
                    }

                }

                if(m_requiresItem)
                {
                    int x = GameplaySystem.playerManager.player.inventory.GetCurrentAmount(m_hasItem);
                    if (x == 0||x < m_ItemAmount)
                    {
                        RequirementFailed();
                        return;
                    }else if (m_TakeRequiredItemFromInvintory)
                    {
                        GameplaySystem.playerManager.player.inventory.RemoveItem(m_hasItem,m_ItemAmount);
                    }
                }
                
                if(m_requiresPrimarySkill)
                {
                    if(!GameplaySystem.playerManager.player.skills.IsSkillUnlocked(m_PrimarySkill))
                    {
                        RequirementFailed();
                        return;
                    }
                }

                if (m_requiresCombatArt)
                {
                    if (!GameplaySystem.playerManager.player.combatArts.IsAbilityActivated(m_CombatArt))
                    {
                        RequirementFailed();
                        return;
                    }
                }

                if(m_requiresSpecificNPC)
                {
                    if (!GameplaySystem.playerManager.armyBattleCharacterRecruiter.HasRecruitedCharacter(armyCharacterData))
                    {
                        RequirementFailed();
                        return;
                    }
                }

                if(m_requiresMinimumNPCsRecruited)
                {
                    if(GameplaySystem.playerManager.armyBattleCharacterRecruiter.ArmySize()<neededNPCsRecruited)
                    {
                        RequirementFailed();
                        return;
                    }
                }

                if (m_OtherConditions)
                {
                    if(!m_RequirementAchieved)
                    {
                        RequirementFailed();
                        return;
                    }
                }
            }

            GiveReward();
            m_GiveReward?.Invoke();
        }

        private void RequirementFailed()
        {
            m_RequirementFailed?.Invoke();
        }
    }
}

