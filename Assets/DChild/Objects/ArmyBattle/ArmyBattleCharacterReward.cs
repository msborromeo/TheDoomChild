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
        [SerializeField, TabGroup("Reference")]
        private SpriteRenderer m_Graphics;
        [SerializeField, TabGroup("Actions")]
        private UnityEvent m_GiveReward,m_RequirementFailed;
        [SerializeField, TabGroup("Reference")]
        private Vector3 m_promptOffset;
        [SerializeField, TabGroup("Character Reward")]
        private List<ArmyCharacterData> m_CharacterReward;
        [SerializeField, TabGroup("Reference")]
        private CharacterGiver m_CharacterGiver;
        [SerializeField, TabGroup("Requirements")]
        private bool m_isFree;
        [SerializeField, TabGroup("Requirements"), HideIf("m_isFree")]
        private bool m_requiresSoulEssence;
        [SerializeField, TabGroup("Requirements"), HideIf("m_isFree")]
        private bool m_requiresItem;
        [SerializeField, TabGroup("Requirements"), HideIf("m_isFree")]
        private bool m_requiresCombatArt;
        [SerializeField, TabGroup("Requirements"), HideIf("m_isFree")]
        private bool m_requiresPrimarySkill;
        [SerializeField, TabGroup("Requirements"), HideIf("m_isFree")]
        private bool m_Others;

        [ShowIfGroup("m_requiresSoulEssence")]
        [SerializeField,BoxGroup("m_requiresSoulEssence/SoulEssenceRequirement")]
        private int m_requiredSoulEssence;

        [ShowIfGroup("m_requiresItem")]
        [SerializeField, BoxGroup("m_requiresItem/ItemRequirement")]
        private ItemData m_hasItem;
        [SerializeField, BoxGroup("m_requiresItem/ItemRequirement")]
        private int m_ItemAmount;

        [ShowIfGroup("m_requiresPrimarySkill")]
        [SerializeField, BoxGroup("m_requiresCombatSkill/CombatArtRequirement")]
        private PrimarySkill m_PrimarySkill;

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

                if(m_Others)
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

