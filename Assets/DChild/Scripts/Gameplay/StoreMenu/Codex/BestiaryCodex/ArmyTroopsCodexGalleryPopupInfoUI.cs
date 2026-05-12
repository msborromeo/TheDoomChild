using DChild.Codex.Characters;
using DChild.Gameplay.ArmyBattle;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Menu.Codex.ArmyTroops
{
    public class ArmyTroopsCodexGalleryPopupInfoUI : CodexGalleryPopupInfoUI<ArmyGroupTemplateData>
    {
        private List<CharacterCodexData> m_codexDatas;

        public void OnCodexDatasReceived(List<CharacterCodexData> value)
        {
            m_codexDatas = value;
        }

        protected override void UpdateInfo()
        {
            if (m_showDataOf == null) return;

            // Example: Accessing the codex data list you populated in the button
            //var data = m_currentButton.codexData;
            // Do your UI logic here

            Debug.Log($"group name: {m_showDataOf.armyCharacterGroup.name}");

            for (int i = 0; i < m_codexDatas.Count; i++)
            {
                Debug.Log($"codex entry name: {m_codexDatas[i].name}");
            }
        }
    }
}