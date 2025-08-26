using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Characters.Players.SoulSkills;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpCostReductionModifier : MonoBehaviour
{
    [SerializeField,SuffixLabel("%",Overlay = true)]
    private float m_SpReductionValue;

    private float m_defaultShadowCostValue;
    private float m_reductionApplied;
    public void AttachTo(int soulSkillInstanceID, IPlayer player)
    {
        m_defaultShadowCostValue = player.modifiers.Get(PlayerModifier.ShadowMagic_Requirement);
        m_reductionApplied = m_defaultShadowCostValue * (m_SpReductionValue / 100f);
        player.modifiers.Add(PlayerModifier.ShadowMagic_Requirement, -m_reductionApplied);
    }

    public void DetachFrom(int soulSkillInstanceID, IPlayer player)
    {
        player.modifiers.Add(PlayerModifier.ShadowMagic_Requirement, m_reductionApplied);
    }

    #region TestingWaters
    //to test, make this a MonoBehaviour and attach it to a GameObject, Assign Player object from scene into the IPlayer
    //[Button]
    //public void Additiocal(IPlayer player)
    //{
    //    m_defaultShadowCostValue = player.modifiers.Get(PlayerModifier.ShadowMagic_Requirement);
    //    m_reductionApplied = m_defaultShadowCostValue * (m_SpReductionValue / 100f);
    //    player.modifiers.Add(PlayerModifier.ShadowMagic_Requirement, -m_reductionApplied);
    //}
    //[Button]
    //public void ReturnToDefault(IPlayer player)
    //{
    //    player.modifiers.Add(PlayerModifier.ShadowMagic_Requirement, m_reductionApplied);
    //}
    #endregion

}
