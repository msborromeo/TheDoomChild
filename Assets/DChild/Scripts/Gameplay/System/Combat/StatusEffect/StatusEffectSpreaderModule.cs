using DChild.Gameplay;
using DChild.Gameplay.Combat;
using DChild.Gameplay.Combat.StatusAilment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectSpreaderModule : IStatusEffectModule
{
    [SerializeField]
    private StatusEffectData m_data;
    [SerializeField]
    private float m_chance;

    private StatusEffectSpreaderHandler m_spreadStatus;

    public IStatusEffectModule GetInstance() => this;

    public void Start(Character character)
    {
        var characterHitBox = character.gameObject.GetComponentInChildren<Hitbox>();
        m_spreadStatus = characterHitBox.gameObject.AddComponent<StatusEffectSpreaderHandler>();
        m_spreadStatus.statusEffectList.Add(m_data, m_chance);
    }

    public void Stop(Character character)
    {
        m_spreadStatus.statusEffectList.Clear();
        Component.Destroy(character.GetComponent<StatusEffectSpreaderHandler>());
    }
}
