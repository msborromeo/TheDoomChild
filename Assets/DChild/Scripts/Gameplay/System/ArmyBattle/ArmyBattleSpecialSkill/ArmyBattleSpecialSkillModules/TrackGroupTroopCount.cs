using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    public class TrackGroupTroopCount : ISpecialSkillModule, ISpecialSkillImplementor
    {
        [SerializeField, VariablePopup(true)]
        private string m_troopCount;
        //[SerializeField]
        //private bool m_trackEnemyTroop;
        [SerializeField]
        private int m_groupID;
        public void ApplyEffect(ArmyController owner, ArmyController target)
        {
             DialogueLua.SetVariable(m_troopCount, owner.controlledArmy.GetGroupInfo(m_groupID));
        }

        public void RemoveEffect(ArmyController owner, ArmyController target)
        {
           
        }

    }

}
