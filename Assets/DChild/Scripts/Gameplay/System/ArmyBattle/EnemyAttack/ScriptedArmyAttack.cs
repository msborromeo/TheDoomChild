using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DChild.Gameplay.ArmyBattle;

public class ScriptedArmyAttack : IArmyAIAction
{ 
    [SerializeField]
    private ArmyAIAttackInfo m_AttackGroupData;
    public bool isRandomizedAction => false;

    ArmyAIAttackInfo IArmyAIAction.GetAction() => m_AttackGroupData;
}
