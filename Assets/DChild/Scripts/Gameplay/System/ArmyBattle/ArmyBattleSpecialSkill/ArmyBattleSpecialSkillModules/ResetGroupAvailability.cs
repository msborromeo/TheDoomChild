using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    public class ResetGroupAvailability : ISpecialSkillModule, ISpecialSkillImplementor
    {
        public void ApplyEffect(ArmyController owner, ArmyController target)
        {
            owner.controlledArmy.ResetGroupAvailability();
            target.controlledArmy.ResetGroupAvailability();
        }

        public void RemoveEffect(ArmyController owner, ArmyController target)
        {
            
        }
    }
}

