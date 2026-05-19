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

        public Action<List<CharacterCodexData>> OnCodexDataReceived;

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

            int remainingDataCount = m_filteredList.Count - startOffset;
            if (remainingDataCount < slotsPerPage)
            {
                slotsPerPage = Mathf.Max(0, remainingDataCount);
            }

            for (int i = slotsPerPage; i < m_entryButtons.Count; i++)
            {
                m_entryButtons[i].gameObject.SetActive(false);
            }

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
                {
                    entryButton.codexData.Clear();
                    PopulateButtonGroupData(battleData.armyCharacterGroup, entryButton);
                }

                ResubscribeButtonEvents(entryButton);

                bool isUnlocked = SetUnlockedStatus(entryButton, battleData);

                if (!isUnlocked)
                    continue;
                
                SetUnitsOpacity(entryButton);

                if (hasSelectedFirst)
                    continue;

                entryButton.Select();
                hasSelectedFirst = true;
            }
        }

        private void PopulateButtonGroupData(ArmyCharacterGroup armyGroup, ArmyTroopsIndexButton button)
        {
            for (int i = 0; i < armyGroup.memberCount; i++)
            {
                var unitCodexData = GetCodexData(armyGroup.GetCharacter(i));
                if (unitCodexData != null)
                {
                    button.AddUnitCodexData(unitCodexData);
                }
            }
        }

        private void SetUnitsOpacity(ArmyTroopsIndexButton button)
        {
            for (int i = 0; i < button.codexData.Count; i++)
            {
                bool isRecorded = CheckPlayerProgress(button.codexData[i]);
                button.SetUnitOpacity(i, isRecorded);
            }
        }

        private bool SetUnlockedStatus(ArmyTroopsIndexButton button, ArmyGroupTemplateData data)
        {
            bool isUnlocked = m_revealAllData || CheckPlayerProgress(data);
            button.SetInteractable(isUnlocked);
            return isUnlocked;
        }

        private void ResubscribeButtonEvents(ArmyTroopsIndexButton button)
        {
            button.OnEntrySelected -= OnEntrySelected;
            button.OnEntrySelected += OnEntrySelected;
        }

        private void OnEntrySelected(ArmyGroupTemplateData armyData, List<CharacterCodexData> codexDatas)
        {
            OnCodexDataReceived.Invoke(codexDatas);
            SetPopupEntryData(armyData);
        }

        private CharacterCodexData GetCodexData(ArmyCharacterData character)
        {
            if (character == null || m_codexDataList == null) return null;

            // Optimized to avoid heavy allocation chains (.GetIDs().Select().FirstOrDefault())
            var ids = m_codexDataList.GetIDs();
            for (int i = 0; i < ids.Length; i++)
            {
                var codexData = m_codexDataList.GetInfo(ids[i]);
                if (codexData != null &&
                    codexData.characterType == CharacterType.Army &&
                    codexData.armyData == character)
                {
                    return codexData;
                }
            }
            return null;
        }
        #endregion

        protected override bool CheckPlayerProgress(ArmyGroupTemplateData data)
        {
            if (data == null) return false;

            // TODO Fixed: Checks if the army group has at least one member unlocked
            if (data.armyCharacterGroup != null)
            {
                for (int i = 0; i < data.armyCharacterGroup.memberCount; i++)
                {
                    var unit = data.armyCharacterGroup.GetCharacter(i);
                    var codexData = GetCodexData(unit);

                    if (codexData != null && CheckPlayerProgress(codexData))
                    {
                        return true; // Found at least one unlocked member
                    }
                }
            }

            // Fallback to tracking the overall group ID if no individual group data structure exists
            return m_playerTracker.HasInfoOf(data.id);
        }

        private bool CheckPlayerProgress(CharacterCodexData data) => data != null && m_playerTracker.HasInfoOf(data.id);
        public override void Initialize()
        {
            base.Initialize();
            if (m_navigationHandle != null)
                m_navigationHandle.SetupScroll(m_battleDataList.Count, m_entryButtons.Count);
        }

        private new void Awake()
        {
            m_navigationHandle.OnCurrentPageChange -= SetupGalleryEntries;
            m_navigationHandle.OnCurrentPageChange += SetupGalleryEntries;
        }

        private void OnDestroy()
        {
        }
    }
}