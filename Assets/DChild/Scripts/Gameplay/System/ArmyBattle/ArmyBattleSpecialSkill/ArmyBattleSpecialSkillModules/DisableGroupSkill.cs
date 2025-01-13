using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    public class DisableGroupSkill : ISpecialSkillModule, ISpecialSkillImplementor
    {
        [SerializeField]
        private ArmyGroupTemplateData m_group;

        public void ApplyEffect(ArmyController owner, ArmyController target)
        {
             ISpecialSkillGroup group = owner.controlledArmy.GetSpecificGroup(m_group);
            owner.controlledArmy.SetSpecialSkillAvailability(group, false);
            

        }

        public void RemoveEffect(ArmyController owner, ArmyController target)
        {
            
        }
    }
}

