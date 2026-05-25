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
        [SerializeField, BoxGroup("EDITOR ONLY")] private bool m_revealUnitDetails;

        public void OnCodexDatasReceived(List<CharacterCodexData> value)
        {
            m_codexDatas = value;
        }

        protected override void UpdateInfo()
        {
            if (m_showDataOf == null) return;

            m_groupInfoUI.Display(m_showDataOf);

            DisplayUnitEntries(m_codexDatas, m_showDataOf.damageType);
            m_modelsUI.Display(m_codexDatas.ToArray(), m_playerTracker, m_revealUnitDetails);
        }

        private void DisplayUnitEntries(List<CharacterCodexData> codexData, DamageType type)
        {
            var entriesUI = codexData.Count < 3
                ? new[] { m_entriesUI[0], m_entriesUI[2] }
                : m_entriesUI;

            m_entriesUI[1].gameObject.SetActive(codexData.Count > 2);

            int displayCount = Mathf.Min(entriesUI.Length, codexData.Count);

            for (int i = 0; i < displayCount; i++)
            {
                var data = codexData[i];
                var ui = entriesUI[i];

                bool hasEntryData = m_playerTracker.HasInfoOf(data.id);
                bool shouldReveal = m_revealUnitDetails || hasEntryData;

                ui.SetEntryVisuals(shouldReveal);

                if (hasEntryData)
                    ui.Display(data, type);
            }

            for (int i = displayCount; i < entriesUI.Length; i++)
                entriesUI[i].gameObject.SetActive(false);
        }


        //m_entriesUI[i].gameObject.SetActive(i < codexData.Count);

        //if (i >= codexData.Count) continue;

        ////debug
        //if (m_revealUnitDetails)
        //{
        //    m_entriesUI[i].SetEntryVisuals(true);
        //    m_entriesUI[i].Display(codexData[i], type);
        //    continue;
        //}


        //var hasEntryData = m_playerTracker.HasInfoOf(codexData[i].id);

        //m_entriesUI[i].SetEntryVisuals(hasEntryData);

        //if (hasEntryData)
        //    m_entriesUI[i].Display(codexData[i], type);
        //}
        //}

        [Button]
        private void DebugVisuals(ArmyGroupTemplateData groupData, CharacterCodexData[] codexData)
        {
            m_showDataOf = groupData;
            m_codexDatas = codexData.ToList();

            UpdateInfo();
        }
    }
}