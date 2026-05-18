using DChild.Codex.LocationCodex;
using DChild.Codex.Tutorial;
using DChild.Gameplay.Narrative;
using DChild.Menu.Bestiary;
using DChild.Menu.Codex.Bestiary;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Menu.Codex.Tutorials
{
    public class CodexTutorialGalleryUI : CodexGalleryUI<TutorialCodexData, TutorialCodexProgressTracker>
    {
        [Header("Tutorials Specific UI")]
        [SerializeField, AssetSelector] private TutorialCodexList m_completeList;
        [SerializeField] private List<TutorialCodexIndexButton> m_entryButtons;

        public override void SetupGalleryEntries() => SetupGalleryEntries(0);
        public override void SetupGalleryEntries(int page)
        {
            bool hasSelectedFirst = false;
            bool noAvailable = true;

            int startOffset = page * m_entryButtons.Count;

            for (int i = 0; i < m_entryButtons.Count; i++)
            {
                var entryButton = m_entryButtons[i];

                int dataIndex = i + startOffset;

                bool hasData = dataIndex < m_filteredList.Count;
                entryButton.gameObject.SetActive(hasData);
                if (!hasData) continue;

                var data = m_filteredList[dataIndex];
                //entryButton.SetTutorialData(data);

                ResubscribeButtonEvents(entryButton);

                bool isUnlocked = SetUnlockedStatus(entryButton, data);
                entryButton.SetData(entryButton.isAvailable ? data : null);

                if (!hasSelectedFirst && isUnlocked)
                {
                    entryButton.Select();
                    entryButton.SetGalleryPopupData();
                    hasSelectedFirst = true;
                    noAvailable = false;
                }
            }

            if (noAvailable)
                m_entryButtons[0].SetGalleryPopupData();
        }

        private bool SetUnlockedStatus(TutorialCodexIndexButton button, TutorialCodexData data)
        {
            bool isUnlocked = m_revealAllData || CheckPlayerProgress(data);
            button.SetInteractable(isUnlocked);

            return isUnlocked;
        }

        private void ResubscribeButtonEvents(TutorialCodexIndexButton button)
        {
            button.OnEntrySelected -= SetPopupEntryData;
            button.OnEntrySelected += SetPopupEntryData;
        }

        protected override bool CheckPlayerProgress(TutorialCodexData data) => m_playerTracker != null && m_playerTracker.HasInfoOf(data.id);

        protected override void RetrieveEntries()
        {
            m_filteredList = m_completeList.GetIDs()
                .Select(id => m_completeList.GetInfo(id))
                .ToList();
        }
        public override void Initialize()
        {
            base.Initialize();
            if (m_navigationHandle != null)
            {
                m_navigationHandle.SetupScroll(m_completeList.Count, m_entryButtons.Count);
            }

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
