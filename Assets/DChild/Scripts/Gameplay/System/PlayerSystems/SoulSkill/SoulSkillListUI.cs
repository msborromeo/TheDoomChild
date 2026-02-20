using DChild.Gameplay.Characters.Players.SoulSkills;
using NUnit.Framework.Internal.Filters;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace DChild.Gameplay.SoulSkills.UI
{
    public class SoulSkillListUI : MonoBehaviour
    {
        [SerializeField]
        private SoulSkillUI[] m_uiList;

        [SerializeField]
        private SoulSkillNavigationHandle m_navigationHandle;

        [SerializeField]
        private bool m_considerAllAsAvailable;
        private Dictionary<int, SoulSkillUI> m_uiPair;

        private SoulSkillList m_completeList;


        public SoulSkillUI GetButton(int index) => m_uiPair.Values.ElementAt(index);

        public void MakeAvailable(int soulSkillID)
        {
            //m_uiPair[soulSkillID].Show(false);
        }

        public SoulSkillUI MakeAvailableAndGetUI(int soulSkillID)
        {
            var button = m_uiPair[soulSkillID];
            return button;
        }

        public void MakeUnavailable(int soulSkillID)
        {
            //m_uiPair[soulSkillID].Hide(false);
        }

        public void MakeAllAvailable()
        {
            //foreach (var ui in m_uiPair.Values)
            //{
            //    ui.Show(false);
            //}
        }

        public void MakeAllUnavailable()
        {
            //foreach (var ui in m_uiPair.Values)
            //{
            //    ui.Hide(false);
            //}
        }

        public void SetActivatedUIState(int soulSkillID, bool isActivated)
        {
            //m_uiPair[soulSkillID].SetIsAnActivatedUIState(isActivated);
        }

        public void InitializeListAvailability(IReadOnlyCollection<int> availableSoulSkillIDs)
        {
            //foreach (var ui in m_uiPair.Values)
            //{
            //    if (m_considerAllAsAvailable || availableSoulSkillIDs.Contains(ui.soulSkillID))
            //    {
            //        ui.Show(true);
            //    }
            //    else
            //    {
            //        ui.Hide(true);
            //    }
            //}
        }

        public void InitializeListActivatedState(IReadOnlyCollection<int> activatedoulSkillIDs)
        {
            //foreach (var ui in m_uiPair.Values)
            //{
            //    ui.SetIsAnActivatedUIState(activatedoulSkillIDs.Contains(ui.soulSkillID));
            //}
        }

        public void InitializeList(SoulSkillList completeSoulSkillList)
        {
            m_completeList = completeSoulSkillList;

            if (m_uiPair == null)
            {
                m_uiPair = new Dictionary<int, SoulSkillUI>();
            }

            UpdateToggleData(0);
        }

        public void UpdateToggleData(int pageIndex)
        {
            m_uiPair.Clear();
            var idList = m_completeList.GetIDs();
            var filteredIDs = idList.Skip(pageIndex).ToArray();

            for (int i = 0; i < m_uiList.Length; i++)
            {
                var id = filteredIDs[i];
                var ui = m_uiList[i];
                m_uiPair.Add(id, ui);

                DisplayData(id);
            }
        }

        private void DisplayData(int id)
        {
            var data = m_completeList.GetInfo(id);
            m_uiPair[id].Display(data);
        }
    }
}
