using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    public class TrackDamageToDealBaseOnVar : ISpecialSkillModule,ISpecialSkillImplementor
{
        [SerializeField, VariablePopup(true)]
        private string m_valueToTrack;
        [SerializeField, VariablePopup(true)]
        private string m_value;
        public void ApplyEffect(ArmyController owner, ArmyController target)
        {
            DamageValues();
        }

        public void RemoveEffect(ArmyController owner, ArmyController target)
        {
           
        }

        public void DamageValues()
        {
            var temp = DialogueLua.GetVariable(m_valueToTrack).asInt;
            if(temp == 0)
            {
                DialogueLua.SetVariable(m_value, 20);
            }
            if(temp == 1)
            {
                DialogueLua.SetVariable(m_value, 50);
            }
            if(temp == 2)
            {
                DialogueLua.SetVariable(m_value, 100);

            }
            else
            {
                DialogueLua.SetVariable(m_value, 200);
            }
            

        }

    }
}

