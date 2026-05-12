using DChild.Codex.Characters;
using DChild.Gameplay.ArmyBattle;
using DChild.Menu.Bestiary;
using DChild.Menu.Codex.Bestiary;
using DChild.Menu.Codex.Characters;
using Sirenix.OdinInspector;
using System;
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

        public Action<List<CharacterCodexData>> OnCodexDatasReceived;

        protected override void RetrieveEntries()
        {
            if (m_filteredList.Count > 0) return;
            m_filteredList = m_battleDataList;
        }

        #region Setup Methods w/ Page Handling
        public override void SetupGalleryEntries() => SetupGalleryEntries(0);

        public override void SetupGalleryEntries(int page)
        {
            bool hasSelectedFirst = false;
            int slotsPerPage = m_entryButtons.Count;
            int startOffset = page * slotsPerPage;

            for (int i = 0; i < slotsPerPage; i++)
            {
                var entryButton = m_entryButtons[i];
                int dataIndex = i + startOffset;

                bool hasData = dataIndex < m_filteredList.Count;

                entryButton.gameObject.SetActive(hasData);
                if (!hasData) continue;

                var battleData = m_filteredList[dataIndex];
                entryButton.SetArmyData(battleData);

                if (battleData.armyCharacterGroup != null)
                    PopulateButtonGroupData(battleData.armyCharacterGroup, entryButton);

                ResubscribeButtonEvents(entryButton);

                bool isUnlocked = SetUnlockedStatus(entryButton, m_filteredList[dataIndex]);

                if (!hasSelectedFirst && isUnlocked)
                {
                    entryButton.Select();
                    hasSelectedFirst = true;
                }
            }
        }
        #endregion

        private bool SetUnlockedStatus(ArmyTroopsIndexButton button, ArmyGroupTemplateData data)
        {
            bool isUnlocked = m_revealAllData || CheckPlayerProgress(data);
            button.SetInteractable(isUnlocked);

            return isUnlocked;
        }

        private void ResubscribeButtonEvents(ArmyTroopsIndexButton button)
        {
            button.OnCodexDataSent -= PrepareCodexData;
            button.OnArmyDataSent -= SetPopupEntryData;

            button.OnCodexDataSent += PrepareCodexData;
            button.OnArmyDataSent += SetPopupEntryData;
        }

        private void PrepareCodexData(List<CharacterCodexData> codexDatas)
        {
            OnCodexDatasReceived.Invoke(codexDatas);
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
                    Select(id => m_codexDataList.GetInfo(id)).FirstOrDefault(codexData => codexData.characterType == CharacterType.Army && codexData.armyData == character);
        }

        protected override bool CheckPlayerProgress(ArmyGroupTemplateData data)
        {
            //TODO modify to check if army group has at least one member unlocked
            return m_playerTracker.HasInfoOf(data.id);
        }

        private bool CheckPlayerProgress(CharacterCodexData data) => m_playerTracker.HasInfoOf(data.id);


        public override void Initialize()
        {
            base.Initialize();
            if (m_navigationHandle != null)
                m_navigationHandle.SetupScroll(m_battleDataList.Count, m_entryButtons.Count);
        }
        private new void Awake()
        {
            m_navigationHandle.OnCurrentPageChange += SetupGalleryEntries;
            
            foreach (var button in m_entryButtons)
                ResubscribeButtonEvents(button);
        }

        private void OnDestroy()
        {
            m_navigationHandle.OnCurrentPageChange -= SetupGalleryEntries;
        }

    }
}