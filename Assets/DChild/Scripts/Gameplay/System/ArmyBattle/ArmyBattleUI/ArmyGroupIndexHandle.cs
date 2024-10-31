using Holysoft.Collections;
using Holysoft.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI
{ 
    public class ArmyGroupIndexHandle : MonoBehaviour, IPageHandle
    {
        [SerializeField]
        private List<AttackingGroupSelectableOptionUI> m_selectableGroups;
        private List<IAttackingGroup> m_groups;

        public int currentPage => throw new System.NotImplementedException();

        public event EventAction<EventActionArgs> PageChange;

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
        }

        public int GetTotalPages()
        {
            throw new System.NotImplementedException();
        }

        public void SetPage(int pageIndex)
        {
            throw new System.NotImplementedException();
        }

        public void NextPage()
        {
            throw new System.NotImplementedException();
        }

        public void PreviousPage()
        {
            throw new System.NotImplementedException();
        }
    }
}