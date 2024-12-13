using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    public class LifeStealArmy : ISpecialSkillModule, ISpecialSkillImplementor
    {
        [SerializeField]
        private int m_stealEnemyTroopCountPercentage;
        public void ApplyEffect(ArmyController owner, ArmyController target)
        {
            float stealEnemyTroopCountPercentage = m_stealEnemyTroopCountPercentage / 100f;
            var troopsToSteal = GetPercentageTroopCount(target.controlledArmy.troopCount, stealEnemyTroopCountPercentage);

            target.controlledArmy.SubtractTroopCount(troopsToSteal);
            owner.controlledArmy.AddTroopCount(troopsToSteal);
        }

        public void RemoveEffect(ArmyController owner, ArmyController target)
        {
        }
        private int GetPercentageTroopCount(int value, float percentage)
        {
            return Mathf.RoundToInt(value * (percentage));
        }
    }
}

