using PixelCrushers.DialogueSystem;
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
            PercentageValue,
            TrackValue
        }
        private enum TargetType
        {
            Self,
            Opponent
        }

        [SerializeField, ShowIf("m_damageType", DamageType.FlatValue)]
        private int m_damageDealt;
        [SerializeField, ShowIf("m_damageType", DamageType.PercentageValue), SuffixLabel("%", true)]
        private int m_damageDealtPercentage;
        [SerializeField, VariablePopup(true), ShowIf("m_damageType", DamageType.TrackValue)]
        private string m_damageDealtTrackValues;
        [SerializeField]
        private DamageType m_damageType;
        [SerializeField]
        private TargetType m_targetType;
        public void ApplyEffect(ArmyController owner, ArmyController target)
        {

            switch (m_damageType)
            {
                case DamageType.FlatValue:
                    if(m_targetType == TargetType.Self)
                    {
                        owner.controlledArmy.SubtractTroopCount(m_damageDealt);
                    }
                    else
                    {
                        target.controlledArmy.SubtractTroopCount(m_damageDealt);
                    }     
                    break;
                case DamageType.PercentageValue:
                    float damagePercentage = m_damageDealtPercentage / 100f;
                    if (m_targetType == TargetType.Self)
                    {
                        var damage = GetPercentageTroopCount(owner.controlledArmy.troopCount, damagePercentage);
                        owner.controlledArmy.SubtractTroopCount(damage);
                    }
                    else
                    {
                        var damage = GetPercentageTroopCount(target.controlledArmy.troopCount, damagePercentage);
                        target.controlledArmy.SubtractTroopCount(damage);
                    }
                    break;
                case DamageType.TrackValue:
                    var damageasInt = DialogueLua.GetVariable(m_damageDealtTrackValues).asInt;
                    if (m_targetType == TargetType.Self)
                    {
                        owner.controlledArmy.SubtractTroopCount(damageasInt);
                    }
                    else
                    {
                        target.controlledArmy.SubtractTroopCount(damageasInt);
                    }
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

