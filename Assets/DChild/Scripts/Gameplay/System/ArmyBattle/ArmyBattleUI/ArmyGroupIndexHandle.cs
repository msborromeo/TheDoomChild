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
            m_groups = groups;
        }

        public void Display()
        {        
            
            foreach(IAttackingGroup group in m_groups)
            {
                Debug.Log($"name: {group.GetCharacterGroup().name}");
                Debug.Log($"member count: {group.GetCharacterGroup().memberCount}");
            }

            /*var damageType = option.damageType;
            m_groupSelection.SetSelectionList(m_player.controlledArmy.GetAvailableGroups(damageType));
            m_groupSelection.SetSelectionIcon(damageType);*/
        }
    }
}