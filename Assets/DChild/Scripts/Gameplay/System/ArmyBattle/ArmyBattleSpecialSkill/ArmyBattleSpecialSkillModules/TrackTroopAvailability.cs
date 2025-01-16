using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    public class TrackTroopAvailability : ISpecialSkillModule, ISpecialSkillImplementor
    {
        private enum CheckBy
        {
            RandomTroop,
            TrackTroopID,
            TroopId
        }

        [SerializeField]
        private CheckBy m_checkbyID;
        [SerializeField, ShowIf("@m_checkbyID == CheckBy.TroopId")]
        private int m_id;
        [SerializeField, VariablePopup(true), ShowIf("@m_checkbyID == CheckBy.TrackTroopID")]
        private string m_trackTroopID;

        public void ApplyEffect(ArmyController owner, ArmyController target)
        {
            switch (m_checkbyID)
            {
                case CheckBy.RandomTroop:
                    var currentUnavailableTroops = owner.controlledArmy;
                    var unavailableTroop =currentUnavailableTroops.GetAllUnvailableGroups();
                    var temp = Random.Range(0,unavailableTroop.Count);
                    var unavailableTroopID = unavailableTroop[temp].id;

                    currentUnavailableTroops.SetAttackingGroupAvailability(unavailableTroopID, true);
                    break;
                case CheckBy.TrackTroopID:
                    owner.controlledArmy.SetAttackingGroupAvailability(DialogueLua.GetVariable(m_trackTroopID).asInt, true);
                    break;
                case CheckBy.TroopId:
                    owner.controlledArmy.SetAttackingGroupAvailability(m_id,true);
                    break;
            }

            
        
            
        }
        public void RemoveEffect(ArmyController owner, ArmyController target)
        {

        }

    }
}