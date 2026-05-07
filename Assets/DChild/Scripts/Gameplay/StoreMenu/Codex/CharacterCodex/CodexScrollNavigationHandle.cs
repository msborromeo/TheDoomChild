using DChild.Gameplay.Characters.Players.SoulSkills;
using DChild.Gameplay.SoulSkills.UI;
using Doozy.Runtime.UIManager.Components;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DChild.Menu.Codex
{
    public class CodexScrollNavigationHandle : MonoBehaviour
    {
        [SerializeField] private UIScrollbar m_scrollBar;
        [SerializeField] private TextMeshProUGUI m_pageLabel;

        private int m_toggleCount;
        private int m_currentPageIndex;

        private int m_totalSections;

        public Action<int> OnCurrentPageChange;

        [Button]
        public void SetupScroll(int entryListCount, int toggleCount)
        {
            m_currentPageIndex = -1;
            m_toggleCount = toggleCount;
            m_totalSections = Mathf.CeilToInt(entryListCount / (float)toggleCount);

            m_scrollBar.numberOfSteps = m_totalSections;
            m_scrollBar.size = 1f / m_totalSections;

            //UpdatePageLabel(1);
        }

        public void HandleScroll()
        {
            int updatedPage = Mathf.RoundToInt(m_scrollBar.value * (m_totalSections - 1));

            if (m_currentPageIndex != updatedPage)
            {
                m_currentPageIndex = updatedPage;
                SetPage(m_currentPageIndex);
            }
        }

        private void SetPage(int pageIndex)
        {
            OnCurrentPageChange.Invoke(pageIndex);
        }
    }


}
