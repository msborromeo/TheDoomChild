using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Codex.Quests.UI
{


    public class QuestTypeToggleUI : MonoBehaviour
    {
        [SerializeField] private QuestLogUIManager m_uiManager;
        private Quest[] m_quests;

        public void Display(bool isMain)
        {
            m_quests = isMain ? m_uiManager.questList.mainQuests : m_uiManager.questList.sideQuests;
            
            m_uiManager.indexHandle.SetSectionType(isMain);
            m_uiManager.ResetDisplay();
            m_uiManager.indexHandle.Initialize(m_quests);
        }
    }
}