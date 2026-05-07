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

        public void SetupGalleryEntries(int page)
        {
            bool hasSelectedFirst = false;
            int startOffset = page * m_entryButtons.Count;

            int i = 0;
            for (; i < m_entryButtons.Count; i++)
            {
                var entryButton = m_entryButtons[i];
                var data = m_filteredList[i + startOffset];

                entryButton.gameObject.SetActive(true);
                entryButton.SetData(data);
                entryButton.OnEntrySelected += SetPopupEntryData;

                bool isUnlocked = m_revealAllData || CheckPlayerProgress(data);
                entryButton.SetInteractable(isUnlocked);

                if (!hasSelectedFirst && isUnlocked)
                {
                    entryButton.Select();
                    hasSelectedFirst = true;
                }
            }

            for (; i < m_entryButtons.Count; i++)
            {
                m_entryButtons[i].SetInteractable(false);
                continue;
            }
        }


        public override void SetupGalleryEntries()
        {
            bool hasSelectedFirst = false;

            for (int i = 0; i < m_entryButtons.Count; i++)
            {
                var entryButton = m_entryButtons[i];

                if (i < m_filteredList.Count)
                {
                    var data = m_filteredList[i];

                    entryButton.SetData(data);
                    entryButton.OnEntrySelected += SetPopupEntryData;

                    bool isUnlocked = m_revealAllData || CheckPlayerProgress(data);
                    entryButton.SetInteractable(isUnlocked);

                    if (!hasSelectedFirst && isUnlocked)
                    {
                        entryButton.Select();
                        hasSelectedFirst = true;
                    }
                }
            }
        }

        protected override bool CheckPlayerProgress(BestiaryData data)
        {
            return m_playerTracker.HasInfoOf(data.GetInstanceID());
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