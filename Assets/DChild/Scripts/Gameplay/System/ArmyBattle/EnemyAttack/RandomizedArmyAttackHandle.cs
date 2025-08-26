using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DChild.Gameplay.ArmyBattle;
using Sirenix.OdinInspector;

[System.Serializable]
public class RandomizedArmyAttackHandle
{
    [SerializeField,SuffixLabel("%",true)]
    private float[] m_probabilities = new float[3];
    [SerializeField]
    private ArmyAIAttackInfo[] m_outcomes;
    public ArmyAIAttackInfo Randomized()
    {
        float rand = Random.Range(0f, 100f);

        switch (rand)
        {
            case float n when n <= m_probabilities[0]:
                return m_outcomes[0];
            case float n when n <= m_probabilities[0] + m_probabilities[1]:
                return m_outcomes[1];
            default:
                return m_outcomes[2];
        }
    }//change to unit type
}
