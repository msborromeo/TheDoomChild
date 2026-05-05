using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace DChild.Codex.Characters
{
    public class CodexCharactersGalleryUI : MonoBehaviour
    {
        [SerializeField] private CharacterCodexList m_completeList;
        [SerializeField] private CharacterCodexProgressTracker m_playerTracker;


        private List<CharacterCodexData> m_filteredList;
        public List<CharacterCodexData> completeNpcList => m_filteredList;

        [SerializeField] private List<CharacterCodexIndexButton> m_entryButtons;

        [SerializeField, BoxGroup("EDITOR ONLY")]
        private bool m_revealAllData;
        
        public void Initialize()
        {
            RetrieveNPCs();
            SetupGalleryEntries();
        }
        public void SetupGalleryEntries()
        {
            bool hasSelectedFirst = false;

            for (int i = 0; i < m_entryButtons.Count; i++)
            {
                var entryButton = m_entryButtons[i];

                if (entryButton.data == null && i < m_filteredList.Count)
                    entryButton.SetData(m_filteredList[i]);

                bool isInteractable = SetEntryInteractability(entryButton);

                if (!hasSelectedFirst && isInteractable)
                {
                    entryButton.Select(); 
                    hasSelectedFirst = true;
                }
            }
        }
        public bool SetEntryInteractability(CharacterCodexIndexButton entryButton)
        {
            var hasRecordedEntry = m_playerTracker.HasInfoOf(entryButton.GetInstanceID());
            bool canInteract = m_revealAllData || hasRecordedEntry;

            entryButton.SetInteractable(canInteract);
            return canInteract;
        }
        private void RetrieveNPCs()
        {
            if (!m_filteredList.IsNullOrEmpty()) return;

            int[] m_npcIDs = m_completeList.GetIDs();

            m_filteredList = m_npcIDs
                .Select(id => m_completeList.GetInfo(id))
                .Where(npc => npc.characterType == CharacterType.NPC)
                .ToList();
        }
    }
}