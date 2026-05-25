using DChild.Codex.Characters;
using DChild.Gameplay;
using DChild.Gameplay.ArmyBattle;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Menu.Codex.ArmyTroops
{
    public class ArmyTroopsCodexGalleryPopupInfoUI : CodexGalleryPopupInfoUI<ArmyGroupTemplateData>
    {
        private List<CharacterCodexData> m_codexDatas;
        [SerializeField] private ArmyTroopsGroupInfoUI m_groupInfoUI;
        [SerializeField] private ArmyTroopsModelsUI m_modelsUI;
        [SerializeField] private ArmyTroopsUnitEntryUI[] m_entriesUI;

        [SerializeField] private CharacterCodexProgressTracker m_playerTracker;

        public void OnCodexDatasReceived(List<CharacterCodexData> value)
        {
            m_codexDatas.Clear();
            m_codexDatas = value;
        }

        protected override void UpdateInfo()
        {
            if (m_showDataOf == null) return;

            m_groupInfoUI.Display(m_showDataOf);
            m_modelsUI.Display(m_codexDatas.ToArray(), m_playerTracker);
            DisplayUnitEntries(m_codexDatas, m_showDataOf.damageType);
        }

        private void DisplayUnitEntries(List<CharacterCodexData> codexData, DamageType type)
        {
            for (int i = 0; i < m_entriesUI.Length; i++)
            {
                m_entriesUI[i].gameObject.SetActive(i < codexData.Count);
                m_entriesUI[i].SetEntryVisuals(m_playerTracker.HasInfoOf(codexData[i].id));

                if (i < codexData.Count && m_playerTracker.HasInfoOf(codexData[i].id))
                    m_entriesUI[i].Display(codexData[i], type);
            }
        }

        [Button]
        private void DebugVisuals(ArmyGroupTemplateData groupData, CharacterCodexData[] codexData)
        {
            m_showDataOf = groupData;
            m_codexDatas = codexData.ToList();

            UpdateInfo();
        }
    }
}