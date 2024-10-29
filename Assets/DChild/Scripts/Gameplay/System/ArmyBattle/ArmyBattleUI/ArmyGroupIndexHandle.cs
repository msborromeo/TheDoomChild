using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI
{ 
    public class ArmyGroupIndexHandle : MonoBehaviour
    {
        [SerializeField]
        private List<AttackingGroupSelectableOptionUI> m_selectableGroups;
        private List<IAttackingGroup> m_groups;
        
        public void SetAvailableGroups(List<IAttackingGroup> groups)
        {
            this.m_groups = groups;
        }

        public void Display()
        {

            for(int i = 0; i < m_groups.Count; i++)
            {
                m_selectableGroups[i].Display(m_groups[i]);
            }

            /*var damageType = option.damageType;
            m_groupSelection.SetSelectionList(m_player.controlledArmy.GetAvailableGroups(damageType));
            m_groupSelection.SetSelectionIcon(damageType);*/
        }
    }
}