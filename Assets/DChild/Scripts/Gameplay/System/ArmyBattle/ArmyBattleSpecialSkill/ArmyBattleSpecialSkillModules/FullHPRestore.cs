using DChild.Gameplay.ArmyBattle;
using DChild.Gameplay.ArmyBattle.SpecialSkills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    public class FullHPRestore : ISpecialSkillModule, ISpecialSkillImplementor
    {
        public void ApplyEffect(ArmyController owner, ArmyController target)
        {
            owner.controlledArmy.ResetTroopCount();
            target.controlledArmy.ResetTroopCount();
        }

        public void RemoveEffect(ArmyController owner, ArmyController target)
        {
            
        }
    }
}

