using DChild.Gameplay.ArmyBattle.SpecialSkills;
using Holysoft.Collections;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI.BulletinBoard
{
    public class BulletinBoardUI : MonoBehaviour, IPageHandle
    {
        [SerializeField] private List<CharacterRecruitmentData> m_sampleRecruitables;

        [SerializeField] private List<BulletinCardUI> m_bulletinCards;
        
        private int m_startingIndex = 0;
        private int m_currentPage;
        public int currentPage => throw new System.NotImplementedException();

        private List<CharacterRecruitmentData> m_filteredRecruitables;
        
        public event EventAction<EventActionArgs> PageChange;
        
        public int GetTotalPages() => Mathf.CeilToInt(m_sampleRecruitables.Count / 10f);

        public void PreviousPage()
        {
            m_currentPage--;
            SetPage(m_currentPage);
        }

        public void NextPage()
        {
            m_currentPage++;
            SetPage(m_currentPage);
        }

        public void SetPage(int page)
        {
            m_currentPage = page;
            m_startingIndex = m_currentPage * 10;
            int rangeCount = (m_startingIndex + 10) < m_sampleRecruitables.Count ? 10 : (m_sampleRecruitables.Count - m_startingIndex);
            
            m_filteredRecruitables = m_sampleRecruitables.GetRange(m_startingIndex, rangeCount);
            Display(m_filteredRecruitables);
        }

        public void Display(List<CharacterRecruitmentData> recruitables)
        {
            for (int i = 0; i < m_bulletinCards.Count; i++)
            {
                if (i >= recruitables.Count)
                {
                    m_bulletinCards[i].Display(null);
                    continue;
                }
                m_bulletinCards[i].Display(recruitables[i]);
            }
        }

        [Button]
        public void SetupBulletin()
        {
            //m_totalPages = GetTotalPages();

            SetPage(0);
        }
    }
}