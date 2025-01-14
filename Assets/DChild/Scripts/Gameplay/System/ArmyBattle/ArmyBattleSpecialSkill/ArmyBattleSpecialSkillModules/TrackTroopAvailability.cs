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
        [SerializeField]
        private int m_id;
        [SerializeField]
        private int m_randomTroop;
        [SerializeField]
        private string m_trackTroopID;

        public void ApplyEffect(ArmyController owner, ArmyController target)
        {
            switch (m_checkbyID)
            {
                case CheckBy.RandomTroop:
                    var unavailableTroop = owner.controlledArmy.GetAllUnvailableGroups();
                    var iDToPass = Random.Range(0,unavailableTroop.Count);

                    break;
                case CheckBy.TrackTroopID:
                    break;
                case CheckBy.TroopId:
                    break;
            }

            
        
            
        }
        public void RemoveEffect(ArmyController owner, ArmyController target)
        {

        }

    }
}