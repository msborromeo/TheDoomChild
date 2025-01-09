using DChild.Gameplay.ArmyBattle.SpecialSkills;
using Doozy.Runtime.UIManager.Components;
using Holysoft.Collections;
using Holysoft.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI
{

    public class ArmyGroupIndexHandle : MonoBehaviour, IPageHandle
    {
        [SerializeField]
        private ArmyBattleAttackGroupSelection m_attackGroupSelection;
        [SerializeField]
        private List<AttackingGroupSelectableOptionUI> m_selectableGroups;

        [SerializeField]
        private ArmyBattleSpecialSkillSelection m_specialSkillSelection;

        private List<IAttackingGroup> m_groups;
        private List<IAttackingGroup> m_filteredGroups;

        private List<ISpecialSkillGroup> m_specialGroups;
        private List<ISpecialSkillGroup> m_filteredSpecialGroups;



        [SerializeField]
        private UIScrollbar m_scrollBar;

        private int m_startingIndex = 0;
        private int m_page;
        private const int m_maxRows = 8;
        public int currentPage => m_page;
        public event EventAction<EventActionArgs> PageChange;

        public void Select(AttackingGroupSelectableOptionUI selectable)
        {
            m_attackGroupSelection.SetSelection(selectable.selectionIndex);
        }

        public void SetAvailableGroups(List<IAttackingGroup> groups)
        {
            this.m_groups = groups;
        }

        public void SetAvailableSpecialGroups(List<ISpecialSkillGroup> groups)
        {
            this.m_specialGroups = groups;
        }

        public void Initialize()
        {
            m_scrollBar.numberOfSteps = GetTotalPages();
            m_scrollBar.size = 1f / GetTotalPages();
            m_scrollBar.value = 0;

            SetPage(0);
        }

        public void Display(List<IAttackingGroup> attackingGroups)
        {
            for (int i = 0; i < m_maxRows; i++)
            {
                var selectableGroup = m_selectableGroups[i];

                if (i < attackingGroups.Count)
                {
                    selectableGroup.SetSelectionIndex(m_startingIndex + i);
                    selectableGroup.Display(attackingGroups[i]);
                    continue;
                }
                selectableGroup.Display(null);
            }
        }

        public int GetTotalPages()
        {
            return Mathf.CeilToInt(m_groups.Count / (float) m_maxRows);
        }

        public void SetPage(int pageIndex)
        {
            m_page = pageIndex;
            m_startingIndex = m_page * m_maxRows;

            int rangeCount = (m_startingIndex + m_maxRows) < m_groups.Count ? m_maxRows : (m_groups.Count - m_startingIndex);

            m_filteredGroups = m_groups.GetRange(m_startingIndex, rangeCount);

            Display(m_filteredGroups);
        }


        public void NextPage()
        {
            m_page++;
            SetPage(m_page);

        }

        public void PreviousPage()
        {
            m_page--;
            SetPage(m_page);
        }

        public void HandleScroll()
        {
            int totalPages = GetTotalPages();
            int updatedPage = Mathf.FloorToInt(m_scrollBar.value / (1f / totalPages));
            updatedPage = Mathf.Clamp(updatedPage, 0, totalPages-1);

            if (m_page != updatedPage)
            {
                SetPage(updatedPage);
            }
        }
    }
}