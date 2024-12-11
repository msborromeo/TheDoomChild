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
    public class ArmyBattleCharacterReward : MonoBehaviour ,IButtonToInteract
    {

        [TabGroup("Main","Reference")]
        [SerializeField, TabGroup("Main/Reference", "General References")]
        private SpriteRenderer m_Graphics;
        [SerializeField, TabGroup("Main/Reference", "General References")]
        private Vector3 m_promptOffset;
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
            [SerializeField, BoxGroup("Main/Requirements/ItemToggle/ItemRequirement")]
            private int m_ItemAmount;

        [SerializeField, TabGroup("Main", "Requirements"), HideIf("m_isFree")]
        private bool m_requiresCombatArt;

        [SerializeField, TabGroup("Main", "Requirements"), HideIf("m_isFree")]
        private bool m_requiresPrimarySkill;
            [ShowIfGroup("Main/Requirements/PrimarySkillToggle", MemberName = "m_requiresPrimarySkill")]
            [SerializeField, BoxGroup("Main/Requirements/PrimarySkillToggle/CombatArtRequirement")]
            private PrimarySkill m_PrimarySkill;

        //[SerializeField, TabGroup("Main", "Requirements"), HideIf("m_isFree")]
        private bool m_OtherConditions;

        private bool m_RequirementAchieved;


        public event EventAction<EventActionArgs> InteractionOptionChange;

        public bool showPrompt => true;

        public string promptMessage => "Army Battle Character Dispenser";

        public Vector3 promptPosition => transform.position + m_promptOffset;

        public void GiveReward()
        {
            m_CharacterGiver?.RecruitCharacter(m_CharacterReward);
            Debug.LogError("AHHHHHHHHHHHHHH pain");
        }

        public void RequirementMet(bool isAchieved)
        {
            m_RequirementAchieved = isAchieved;
        }

        public void HasOtherConditions(bool x)
        {
            m_OtherConditions = x;
        }

        public void Interact(Character character)
        {
            if(!m_isFree)
            {
                if(m_requiresSoulEssence)
                {
                    if (GameplaySystem.playerManager.player.inventory.GetCurrencyAmount(CurrencyType.SoulEssence) < m_requiredSoulEssence && m_requiredSoulEssence != 0)
                    {
                        m_RequirementFailed?.Invoke();
                        return;
                    }
                }

                if(m_requiresItem)
                {
                    int x = GameplaySystem.playerManager.player.inventory.GetCurrentAmount(m_hasItem);
                    if (x == 0||x < m_ItemAmount)
                    {
                        m_RequirementFailed?.Invoke();
                        return;
                    }
                }
                
                if(m_requiresPrimarySkill)
                {
                    if(!GameplaySystem.playerManager.player.skills.IsSkillUnlocked(m_PrimarySkill))
                    {
                        m_RequirementFailed?.Invoke();
                        return;
                    }
                }

                if(m_OtherConditions)
                {
                    if(!m_RequirementAchieved)
                    {
                        m_RequirementFailed?.Invoke();
                        return;
                    }
                }
                
            }

            m_GiveReward?.Invoke();
        }
    }
}

