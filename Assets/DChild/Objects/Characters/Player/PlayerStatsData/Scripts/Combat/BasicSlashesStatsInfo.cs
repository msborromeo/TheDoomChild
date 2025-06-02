using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BasicSlashesStatsInfo
{
    [SerializeField]
    private Vector2 m_momentumVelocity;
    public Vector2 momentumVelocity => m_momentumVelocity;
    [SerializeField]
    private float m_defaultGravity;
    public float defaultGravity => m_defaultGravity;

    public void CopyInfo(BasicSlashesStatsInfo reference)
    {
        m_momentumVelocity = reference.momentumVelocity;
        m_defaultGravity = reference.defaultGravity;
    }
}
