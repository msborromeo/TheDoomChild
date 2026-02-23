using DChild.Gameplay.Characters.Players.SoulSkills;
using Doozy.Runtime.UIManager.Components;
using Holysoft.Event;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Gameplay.SoulSkills.UI
{
    public class SoulSkillNavigationHandle : MonoBehaviour
    {
        [SerializeField] private UIScrollbar m_soulScroll;

        [SerializeField] private SoulSkillListUI m_listUI;

        private int m_toggleCount;
        private int m_currentPage;

        private int m_totalSections;


        [Button]
        public void SetupScroll(SoulSkillList soulList, int toggleCount)
        {
            m_currentPage = -1;
            m_toggleCount = toggleCount;
            m_totalSections = Mathf.CeilToInt(soulList.Count / (float)toggleCount);

            m_soulScroll.numberOfSteps = m_totalSections;
            m_soulScroll.size = 1f/ m_totalSections;
        }

        public void HandleScroll()
        {
            int updatedPage = Mathf.RoundToInt(m_soulScroll.value * (m_totalSections - 1));

            if (m_currentPage != updatedPage)
            {
                m_currentPage = updatedPage;
                SetPage(m_currentPage);
            }
        }

        private void SetPage(int pageIndex)
        {
            m_listUI.UpdateToggleData(pageIndex);
        }
    }
}
