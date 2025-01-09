using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    public class TroopHeal : ISpecialSkillModule, ISpecialSkillImplementor
    {
        private enum Type
        {
            FlatValue,
            TrackedValue
        }

        [SerializeField]
        private Type m_type;
        [SerializeField, ShowIf("@m_type == Type.FlatValue")]
        private int m_troopCount;
        [SerializeField, ShowIf("@m_type == Type.TrackedValue")]
        private string m_trackedVariable;

        public void ApplyEffect(ArmyController owner, ArmyController target)
        {
            owner.controlledArmy.AddTroopCount(GetValueToHeal());
        }

        public void RemoveEffect(ArmyController owner, ArmyController target)
        {
            owner.controlledArmy.SubtractTroopCount(GetValueToHeal());
        }

        private int GetValueToHeal()
        {
            switch (m_type)
            {
                case Type.FlatValue:
                    return m_troopCount;

                case Type.TrackedValue:
                    return DialogueLua.GetVariable(m_trackedVariable).AsInt;
                default:
                    return 0;
            }
        }
    }
}

