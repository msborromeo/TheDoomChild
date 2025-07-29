using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EarthShakerStatsInfo
{
    [SerializeField, MinValue(0.1f)]
    private float m_fallSpeed;
    public float fallSpeed => m_fallSpeed;
    [SerializeField, MinValue(0)]
    private float m_fallDamageModifier;
    public float fallDamageModifier => m_fallDamageModifier;
    [SerializeField, MinValue(0)]
    private float m_impactDamageModifier;
    public float impactDamageModifier => m_impactDamageModifier;
    [SerializeField, Range(0f, 100f)]
    private float m_critChance;
    [SerializeField, MinValue(0), Tooltip("Multiply modifier by this value on critical hit")]
    private float m_critModifier;
    [SerializeField]
    private ParticleSystem m_critFX;

    public float critChance => m_critChance;
    public float critModifier => m_critModifier;
    public ParticleSystem critFX => m_critFX;

    public void CopyInfo(EarthShakerStatsInfo reference)
    {
        m_fallSpeed = reference.fallSpeed;
        m_fallDamageModifier = reference.fallDamageModifier;
        m_impactDamageModifier = reference.impactDamageModifier;
    }
}
