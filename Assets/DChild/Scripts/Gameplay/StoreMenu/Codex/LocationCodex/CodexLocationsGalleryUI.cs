using DChild.Codex.LocationCodex;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
namespace DChild.Menu.Codex.Locations
{
    public class CodexLocationsGalleryUI : CodexGalleryUI<LocationCodexData, LocationCodexProgressTracker>
    {
        [Header("Locations Specific UI")]
        [SerializeField, AssetSelector] private List<LocationCodexData> m_completeList;
        [SerializeField] private List<LocationCodexIndexButton> m_entryButtons;

        public override void SetupGalleryEntries(int page)
        {
            bool hasSelectedFirst = false;
            int startOffset = page * m_entryButtons.Count;

            for (int i = 0; i < m_entryButtons.Count; i++)
            {
                var entryButton = m_entryButtons[i];

                int dataIndex = i + startOffset;

                bool hasData = dataIndex < m_filteredList.Count;
                entryButton.gameObject.SetActive(hasData);
                if (!hasData) continue;

                var data = m_filteredList[dataIndex];
                entryButton.SetData(data);

                ResubscribeButtonEvents(entryButton);

                bool isUnlocked = SetUnlockedStatus(entryButton, data);

                if (!hasSelectedFirst && isUnlocked)
                {
                    entryButton.Select();
                    hasSelectedFirst = true;
                }
            }
        }

        private bool SetUnlockedStatus(LocationCodexIndexButton button, LocationCodexData data)
        {
            bool isUnlocked = m_revealAllData || CheckPlayerProgress(data);
            button.SetInteractable(isUnlocked);

            return isUnlocked;
        }

        private void ResubscribeButtonEvents(LocationCodexIndexButton button)
        {
            button.OnEntrySelected -= SetPopupEntryData;
            button.OnEntrySelected += SetPopupEntryData;
        }
        public override void SetupGalleryEntries()
        {
            SetupGalleryEntries(0);
        }

        protected override bool CheckPlayerProgress(LocationCodexData data)
        {
            return m_playerTracker != null && m_playerTracker.HasInfoOf(data.id);
        }

        protected override void RetrieveEntries()
        {
            if (m_filteredList.Count > 0) return;

            m_filteredList = m_completeList;
        }
        public override void Initialize()
        {
            base.Initialize();
            if (m_navigationHandle != null)
            {
                m_navigationHandle.SetupScroll(m_completeList.Count, m_entryButtons.Count);
            }

            m_entryButtons[0].SetGalleryPopupData();
        }
        private new void Awake()
        {
            m_navigationHandle.OnCurrentPageChange += SetupGalleryEntries;
        }

        private void OnDestroy()
        {
            m_navigationHandle.OnCurrentPageChange -= SetupGalleryEntries;
        }
    }
}

