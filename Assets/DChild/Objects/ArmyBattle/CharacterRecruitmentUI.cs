using DChild.Gameplay.ArmyBattle;
using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Items;
using DChild.Menu;
using Holysoft.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRecruitmentUI : MonoBehaviour
{
    [SerializeField]
    private ConfirmationHandler m_confirmationHandler;
    private EventAction<EventActionArgs> m_AcceptOffer;
    private EventAction<EventActionArgs> m_DeclineOffer;
    private string m_requirementText;
    
    public void SetAcceptOffer(EventAction<EventActionArgs> listener)
    {
        m_AcceptOffer = listener;
    }

    public void SetDeclineOffer(EventAction<EventActionArgs> listener)
    {
        m_DeclineOffer = listener;
    }

    public void SetupUI(string name)
    {
        m_confirmationHandler.RequestConfirmation(m_AcceptOffer, "Recruit", name+"\n"+m_requirementText, OnDecline:m_DeclineOffer);
        m_requirementText= string.Empty;
    }

    public void AddSoulessenceReq(int amount)
    {
        m_requirementText += "\n•<color=yellow>" + amount + "</color> Soul Essence";
    }

    public void AddItemReq(ItemData item,int itemAmount)
    {
        m_requirementText += "\n•" + itemAmount.ToString() + " <color=yellow>" + item.itemName + "</color>";
    }

    public void AddPrimarySkillReq(PrimarySkill skill)
    {
        m_requirementText += "\n•Aquired:<color=yellow>" + skill.ToString() + "</color>";
    }

    public void AddCombatArtReq(CombatArt skill)
    {
        m_requirementText += "\n•Learned:<color=yellow>" + skill.ToString() + "</color>";
    }

    public void AddNPCRecruitedReq(ArmyCharacterData character)
    {
        m_requirementText += "\n•Recruited:<color=yellow>" + character.name + "</color>";
    }

    public void AddArmySizeReq(int size)
    {
        m_requirementText += "\n•Has <color=yellow>" + size + "</color> recruited";
    }

    public void AddAdditionalText(string addedtext)
    {
        m_requirementText += addedtext;
    }
}