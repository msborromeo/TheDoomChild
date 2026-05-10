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
    public class CodexArmyTroopsGalleryUI : CodexGalleryUI<ArmyGroupTemplateData, CharacterCodexProgressTracker>
    {
        [BoxGroup("Full Codex & Army Group Data List")]
        [SerializeField, AssetSelector] private List<ArmyGroupTemplateData> m_battleDataList;
        [BoxGroup("Full Codex & Army Group Data List")]
        [SerializeField, AssetSelector] private CharacterCodexList m_codexDataList;

        [Header("Troops Specific UI")]
        [SerializeField] private List<ArmyTroopsIndexButton> m_entryButtons;

        public override void SetupGalleryEntries()
        {
            bool hasSelectedFirst = false;

            for (int i = 0; i < m_entryButtons.Count; i++)
            {
                var entryButton = m_entryButtons[i];

                //set gallery button w/ army group data
                entryButton.SetArmyData(m_battleDataList[i]);

                //TODO populate codex data 
                var characterGroupData = entryButton.armyData.armyCharacterGroup;

                if (characterGroupData != null)
                    PopulateButtonGroupData(characterGroupData, entryButton);

                if (i < m_filteredList.Count)
                {
                    var data = m_filteredList[i];
                    //entryButton.SetData(data);
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

        private void PopulateButtonGroupData(ArmyCharacterGroup armyGroup, ArmyTroopsIndexButton button)
        {
            for (int i = 0; i < armyGroup.memberCount; i++)
            {
                var unitCodexData = GetCodexData(armyGroup.GetCharacter(i));
                button.AddUnitCodexData(unitCodexData);
            }
        }

        private CharacterCodexData GetCodexData(ArmyCharacterData character)
        {
            return m_codexDataList.GetIDs().
                    Select(id => m_codexDataList.GetInfo(id)).FirstOrDefault(character => character.armyData == character);
        }

        protected override bool CheckPlayerProgress(ArmyGroupTemplateData data)
        {
            //TODO modify to check if army group has at least one member unlocked
            return m_playerTracker.HasInfoOf(data.id);
        }

        private bool CheckPlayerProgress(CharacterCodexData data) => m_playerTracker.HasInfoOf(data.id);

        protected override void RetrieveEntries()
        {
            if (m_filteredList.Count > 0 || m_battleDataList.Count > 0) return;

            //Get Army Characters from complete Codex List
            m_filteredList = m_battleDataList;
        }

        public override void SetPopupEntryData(ArmyGroupTemplateData data)
        {
            Debug.Log($"received data: {data}");
            base.SetPopupEntryData(data);
        }
    }
}