using DChild.Gameplay.ArmyBattle;
using Doozy.Runtime.UIManager.Components;
using Holysoft.Collections;
using Holysoft.Event;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Codex.Quests.UI
{

    public class QuestIndexHandle : MonoBehaviour, IPageHandle
    {
        [SerializeField] private UIScrollbar m_scrollBar;
        [SerializeField] private List<QuestButtonUI> m_questButtons;

        private Quest[] m_quests;
        private Quest[] m_filteredQuests;

        private bool m_isMain;
        private int m_page, m_maxRows = 8, m_startingIndex = 0;

        public int currentPage => throw new System.NotImplementedException();
        public event EventAction<EventActionArgs> PageChange;

        public void SetSectionType(bool value) => m_isMain = value;

        public void Initialize(Quest[] quests)
        {
            m_quests = quests;
            m_scrollBar.numberOfSteps = GetTotalPages();
            m_scrollBar.size = 1f / GetTotalPages();
            m_scrollBar.value = 0;

            SetPage(0);
        }

        public int GetTotalPages() => Mathf.CeilToInt(m_quests.Length / (float)m_maxRows);

        public void Display(Quest[] quests)
        {
            for (int i = 0; i < m_maxRows; i++)
            {
                var questButton = m_questButtons[i];

                if (i >= quests.Length)
                {
                    questButton.Display(null);
                    continue;
                }
                questButton.SetSelectionIndex(m_startingIndex + i);
                questButton.Display(quests[i]);
            }
        }

        public void SetPage(int pageIndex)
        {
            m_page = pageIndex;
            m_startingIndex = m_page * m_maxRows;

            int rangeCount = (m_startingIndex + m_maxRows) < m_quests.Length ? m_maxRows : (m_quests.Length - m_startingIndex);

            m_filteredQuests = m_quests.Skip(m_startingIndex).Take(rangeCount).ToArray();

            Display(m_filteredQuests);
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
            updatedPage = Mathf.Clamp(updatedPage, 0, totalPages - 1);

            if (m_page != updatedPage)
            {
                SetPage(updatedPage);
            }
        }
    }
}