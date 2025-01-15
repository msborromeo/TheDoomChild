using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    public class SetCurrentTurn : ISpecialSkillModule, ISpecialSkillImplementor
    {
        [SerializeField] private int m_turnNumber;
        public void ApplyEffect(ArmyController owner, ArmyController target)
        {
            CurrentTurn(m_turnNumber);
        }

        public void RemoveEffect(ArmyController owner, ArmyController target)
        {
            
        }

        private void CurrentTurn(int turnNumber)
        {
            ArmyBattleSystem.SetCurrentTurn(turnNumber);
        }
    }
}

