using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    public class DealDamage : ISpecialSkillImplementor, ISpecialSkillModule
    {
        private enum DamageType
        {
            FlatValue,
            PercentageValue
        }
        [SerializeField, ShowIf("m_damageType", DamageType.FlatValue)]
        private int m_damageDealt;
        [SerializeField, ShowIf("m_damageType", DamageType.PercentageValue), SuffixLabel("%", true)]
        private int m_damageDealtPercentage;
        [SerializeField]
        private DamageType m_damageType;
        public void ApplyEffect(ArmyController owner, ArmyController target)
        {

            switch (m_damageType)
            {
                case DamageType.FlatValue:
                    target.controlledArmy.SubtractTroopCount(m_damageDealt);
                    break;
                case DamageType.PercentageValue:
                    float damagePercentage = m_damageDealtPercentage / 100f; 
                    var damage = GetPercentageTroopCount(target.controlledArmy.troopCount, damagePercentage);
                    target.controlledArmy.SubtractTroopCount(damage);
                    break;
            }


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

