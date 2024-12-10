using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    public class AddDamage : ISpecialSkillModule, ISpecialSkillImplementor
    {

        private enum AddDamageValueType
        {
            FlatValue,
            PercentageValue
        }
        [SerializeField]
        private DamageType m_unit;
        [SerializeField]
        private AddDamageValueType m_addDamageValueType;
        [SerializeField, ShowIf("m_addDamageValueType", AddDamageValueType.FlatValue)]
        private int m_damageModiferValue;
        [SerializeField, ShowIf("m_addDamageValueType", AddDamageValueType.PercentageValue), SuffixLabel("%",true)]
        private int m_damageModiferPercentageValue;

        public void ApplyEffect(ArmyController owner, ArmyController target)
        {
            
            switch (m_addDamageValueType)
            {
                case AddDamageValueType.FlatValue:
                    owner.controlledArmy.modifiers.damageModifier.AddModifier(m_unit, m_damageModiferValue);
                    break;
                case AddDamageValueType.PercentageValue:
                    //???
                    var baseDamage = owner.controlledArmy.modifiers.damageModifier.GetModifier(m_unit);
                    var modifier = m_damageModiferPercentageValue / 100f;
                    var modifiedDamage = baseDamage + (baseDamage * modifier);
                    owner.controlledArmy.modifiers.damageModifier.SetModifier(m_unit, modifiedDamage);
                    break;
            }
                
        }
        public void RemoveEffect(ArmyController owner, ArmyController target)
        {
            switch (m_addDamageValueType)
            {
                case AddDamageValueType.FlatValue:
                    owner.controlledArmy.modifiers.damageModifier.AddModifier(m_unit, -m_damageModiferValue);
                    break;
                case AddDamageValueType.PercentageValue:
                    var baseDamage = owner.controlledArmy.modifiers.damageModifier.GetModifier(m_unit);
                    var modifier = m_damageModiferPercentageValue / 100f;
                    var modifiedDamage = (baseDamage / (1 + modifier));
                    owner.controlledArmy.modifiers.damageModifier.SetModifier(m_unit, modifiedDamage);
                    break;
            }
        }
    }
}

