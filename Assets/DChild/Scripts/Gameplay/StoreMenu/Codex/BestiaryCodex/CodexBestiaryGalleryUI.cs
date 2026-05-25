using DChild.Gameplay.SoulSkills.UI;
using DChild.Menu.Bestiary;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Menu.Codex.Bestiary
{
    public class CodexBestiaryGalleryUI : CodexGalleryUI<BestiaryData, BestiaryCodexProgressTracker>
    {
        [Header("Bestiary Specific UI")]
        [SerializeField, AssetSelector] private BestiaryList m_completeList;
        [SerializeField] private List<BestiaryCodexIndexButton> m_entryButtons;

        protected override void RetrieveEntries()
        {
            if (m_filteredList.Count > 0) return;
            m_filteredList = m_completeList.GetIDs()
                .Select(id => m_completeList.GetInfo(id))
                .ToList();
        }

        public void OnPageChange(object sender, EventActionArgs args)
        {
            SetupGalleryEntries();
        }

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

        private bool SetUnlockedStatus(BestiaryCodexIndexButton button, BestiaryData data)
        {
            bool isUnlocked = m_revealAllData || CheckPlayerProgress(data);
            button.SetInteractable(isUnlocked);

            return isUnlocked;
        }

        private void ResubscribeButtonEvents(BestiaryCodexIndexButton button)
        {
            button.OnEntrySelected -= SetPopupEntryData;
            button.OnEntrySelected += SetPopupEntryData;
        }


        public override void SetupGalleryEntries() => SetupGalleryEntries(0);

        protected override bool CheckPlayerProgress(BestiaryData data)
        {
            return m_playerTracker.HasInfoOf(data.id);
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