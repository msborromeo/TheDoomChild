using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerCritStatsInfo
{
    [SerializeField, Range(0f, 100f)]
    private float m_critChance;
    public float critChance => m_critChance;
    [SerializeField, MinValue(0), Tooltip("Multiply modifier by this value on critical hit")]
    private float m_critModifier;
    public float critModifier => m_critModifier;

    public void CopyInfo(PlayerCritStatsInfo reference)
    {
        m_critChance = reference.m_critChance;
        m_critModifier = reference.m_critModifier;
    }
}
