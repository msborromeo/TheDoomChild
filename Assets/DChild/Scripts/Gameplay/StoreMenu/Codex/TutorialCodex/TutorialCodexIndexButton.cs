using Dchild.Localization;
using DChild.Codex.Characters;
using DChild.Gameplay.ArmyBattle;
using DChild.Gameplay.Narrative;
using DChild.Menu.Bestiary;
using DChild.Menu.Codex;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DChild.Codex.Tutorial
{
    public class TutorialCodexIndexButton : CodexIndexButton<TutorialCodexData, ICodexIndexInfo>
    {

        #region TutorialData Ver.
//        [SerializeField] private TextMeshProUGUI m_entryTitle;
        
//        private TutorialData m_data;
//        public TutorialData tutorialData => m_data;

//        private UIButton m_button;
//        public Action<TutorialData> OnEntrySelected;


//        #region Setters
//        public void SetTutorialData(TutorialData value)
//        {
//            m_data = value;

//            UpdateUI(value);
//        }
//        public void SetGalleryPopupData() => OnEntrySelected.Invoke(m_data);
//        #endregion

//        #region UI Visuals

//        private void UpdateUI(TutorialData data) => m_entryTitle.text = data.entryTitle;
        
//        private void EnsureReferences()
//        {
//#if UNITY_EDITOR
//            if (m_button == null)
//            {
//                m_button = GetComponent<UIButton>();
//            }
//#endif
//        }

//        public void SetInteractable(bool isInteractable)
//        {
//            EnsureReferences();
//            if (m_button != null)
//                m_button.interactable = isInteractable;
//        }

//        public void Select() => m_button?.Select();
//        #endregion

        #endregion
    }
}
