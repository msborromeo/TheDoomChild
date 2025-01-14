using DChild.Gameplay.ArmyBattle.SpecialSkills;
using Doozy.Runtime.UIManager.Components;
using Holysoft.Collections;
using Holysoft.Event;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public class SpecialGroupIndexHandle : MonoBehaviour, IPageHandle
    {
        [SerializeField]
        private ArmyBattleSpecialSkillSelection m_specialSkillSelection;
        [SerializeField]
        private List<SpecialSkillGroupOptionUI> m_selectableGroups;

        private List<ISpecialSkillGroup> m_groups;
        private List<ISpecialSkillGroup> m_filteredGroups;

        [SerializeField]
        private UIScrollbar m_scrollBar;
        //[SerializeField]
        //private List<UIButton> m_upperButtons;
        //[SerializeField]
        //private List<UIButton> m_lowerButtons;


        private int m_startingIndex = 0;
        private int m_page;
        private const int m_maxRows = 4;

        public int currentPage => throw new System.NotImplementedException();

        public event EventAction<EventActionArgs> PageChange;

        public void Select(SpecialSkillGroupOptionUI selectable)
        {
            Debug.Log($"received special group: {selectable.group.GetCharacterGroup()}");
            m_specialSkillSelection.SelectSpecialGroup(selectable.group);
        }

        public void Display(List<ISpecialSkillGroup> specialGroups)
        {
            for (int i = 0; i < m_maxRows; i++)
            {
                var selectableGroup = m_selectableGroups[i];

                if (i < specialGroups.Count)
                {
                    selectableGroup.Display(specialGroups[i]);
                    continue;
                }
                selectableGroup.Display(null);
            }
        }

        void IPageHandle.NextPage()
        {
            throw new System.NotImplementedException();
        }

        void IPageHandle.PreviousPage()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            Debug.Log($"total special groups: ${m_groups.Count}");
            m_scrollBar.numberOfSteps = GetTotalPages();
            m_scrollBar.size = 1f / GetTotalPages();
            m_scrollBar.value = 0;

            SetPage(0);
        }


        public void SetAvailableSpecialGroups(List<ISpecialSkillGroup> groups)
        {
            this.m_groups = groups;
        }

        public int GetTotalPages()
        {
            return Mathf.CeilToInt(m_groups.Count / (float)m_maxRows);

        }        

        public void SetPage(int pageIndex)
        {
            m_page = pageIndex;
            m_startingIndex = m_page * m_maxRows;

            int rangeCount = (m_startingIndex + m_maxRows) < m_groups.Count ? m_maxRows : (m_groups.Count - m_startingIndex);

            m_filteredGroups = m_groups.GetRange(m_startingIndex, rangeCount);

            Display(m_filteredGroups);
        }


        public void HandleScroll()
        {
            int totalPages = GetTotalPages();
            int updatedPage = Mathf.FloorToInt(m_scrollBar.value / (1f / totalPages));
            updatedPage = Mathf.Clamp(updatedPage, 0, totalPages - 1);

            if (m_page != updatedPage)
            {
                SetPage(updatedPage);
            }
        }

        
    }

}