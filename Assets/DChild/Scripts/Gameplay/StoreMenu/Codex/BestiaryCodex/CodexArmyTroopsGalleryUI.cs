using DChild.Codex.Characters;
using DChild.Gameplay.ArmyBattle;
using DChild.Menu.Bestiary;
using DChild.Menu.Codex.Bestiary;
using DChild.Menu.Codex.Characters;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Menu.Codex.ArmyTroops
{
    public class CodexArmyTroopsGalleryUI : CodexGalleryUI<CharacterCodexData, CharacterCodexProgressTracker>
    {
        [BoxGroup("Full Codex & Army Group Data List")]
        [SerializeField, AssetSelector] private ArmyGroupTemplateList m_battleDataList;
        [BoxGroup("Full Codex & Army Group Data List")]
        [SerializeField, AssetSelector] private CharacterCodexList m_codexDataList;

        [Header("Troops Specific UI")]
        [SerializeField] private List<CharacterCodexIndexButton> m_entryButtons;

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
            return m_playerTracker.HasInfoOf(data.id);
        }

        protected override void RetrieveEntries()
        {
            if (m_filteredList.Count > 0 || m_battleDataList.count > 0) return;

            //Codex data filtering from NPCs
            m_filteredList = m_codexDataList.GetIDs().
                Select(id => m_codexDataList.GetInfo(id))
                .Where(npc => npc.characterType == CharacterType.Army)
                .ToList();
        }
    }
}