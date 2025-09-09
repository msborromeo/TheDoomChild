using DChild.Gameplay;
using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Characters.Players.Modules;
using DChild.Gameplay.Characters.Players.SoulSkills;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowGaugeModifier : ISoulSkillModule
{
    [SerializeField, SuffixLabel("%", Overlay = true)]
    private float m_shadowRegenerationValue;

    private float m_defaultShadowRegenValue;
    private float m_bonusApplied;

    public void AttachTo(int soulSkillInstanceID, IPlayer player)
    {
        m_defaultShadowRegenValue = player.modifiers.Get(PlayerModifier.ShadowMagicRegeneration);
        m_bonusApplied = m_defaultShadowRegenValue * (m_shadowRegenerationValue / 100f);
        player.modifiers.Add(PlayerModifier.ShadowMagicRegeneration, m_bonusApplied);
    }

    public void DetachFrom(int soulSkillInstanceID, IPlayer player)
    {
        player.modifiers.Add(PlayerModifier.ShadowMagicRegeneration, -m_bonusApplied);
    }



    #region TestingWaters
    //to test, make this a MonoBehaviour and attach it to a GameObject, Assign Player object from scene into the IPlayer
    //[Button]
    //public void Additiocal(IPlayer player)
    //{
    //    m_defaultShadowRegenValue = player.modifiers.Get(PlayerModifier.ShadowMagicRegeneration);
    //    m_bonusApplied = m_defaultShadowRegenValue * (m_shadowRegenerationValue / 100f);
    //    player.modifiers.Add(PlayerModifier.ShadowMagicRegeneration, m_bonusApplied);
    //}
    //[Button]
    //public void ReturnToDefault(IPlayer player)
    //{
    //    player.modifiers.Add(PlayerModifier.ShadowMagicRegeneration, (float)Math.Round(-m_bonusApplied, 2));
    //}
    #endregion

}
