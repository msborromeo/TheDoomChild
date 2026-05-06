
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Codex.Characters
{
    public class CodexCharactersGalleryUI : CodexGalleryUI<CharacterCodexData, CharacterCodexProgressTracker>
    {
        [Header("Character Specific UI")]
        [SerializeField] private CharacterCodexList m_completeList;
        [SerializeField] private List<CharacterCodexIndexButton> m_entryButtons;

        protected override void RetrieveEntries()
        {
            if (m_filteredList.Count > 0) return;

            m_filteredList = m_completeList.GetIDs()
                .Select(id => m_completeList.GetInfo(id))
                .Where(npc => npc.characterType == CharacterType.NPC)
                .ToList();
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

        protected override bool CheckPlayerProgress(CharacterCodexData data)
        {
            // Using the instance ID logic from your original code
            return m_playerTracker.HasInfoOf(data.GetInstanceID());
        }

        public override void SetPopupEntryData(CharacterCodexData data)
        {
            Debug.Log($"received data: {data}");
            base.SetPopupEntryData(data);
        }
    }
}