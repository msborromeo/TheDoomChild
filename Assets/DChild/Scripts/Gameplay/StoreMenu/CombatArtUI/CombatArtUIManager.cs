using DChild.Gameplay.Characters.Player.CombatArt.Leveling;
using DChild.Gameplay.Characters.Players;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.UI.CombatArts
{

    public class CombatArtUIManager : MonoBehaviour
    {
        [SerializeField]
        private CombatArtList m_referenceList;
        [SerializeField]
        private Characters.Players.CombatArts m_progressionReference;

        [SerializeField]
        private CombatArtUIDetail m_uiDetail;
        [SerializeField]
        private CombatArtSelectorHighlight m_selectorHighlight;
        [SerializeField]
        private CombatArtUnlockHandle m_unlockArtHandler;

        [SerializeField]
        private CombatArtSelectButton m_firstSelected;

        private Dictionary<CombatArt, CombatArtSelectButton[]> m_abilityButtonPair;
        private CombatArtSelectRequirements[] m_artRequirements;

        private CombatArtSelectButton m_currentSelectedButton;

        public void Initialize()
        {
            m_selectorHighlight.Initialize();
            m_unlockArtHandler.UnlockSuccessful -= OnUnlockSuccessFull;
            m_unlockArtHandler.UnlockSuccessful += OnUnlockSuccessFull;
            m_unlockArtHandler.InitializeReferences(m_progressionReference, m_referenceList);
            m_unlockArtHandler.ResetUnlockProgress();
            SyncButtonStates();

            SetupUIData();
        }

        private void SetupUIData()
        {
            var combatArtData = m_referenceList.GetCombatArtData(m_firstSelected.skillUnlock);
            m_uiDetail.Display(combatArtData, m_firstSelected.unlockLevel);
            m_firstSelected.uiButton.Select();
        }

        public void SyncButtonStates()
        {
            InitializeButtonStates();
            ValidateButtonVisuals();
        }

        public void Select(CombatArtSelectButton button)
        {
            if (button == m_currentSelectedButton)
                return;

            m_currentSelectedButton = button;
            var combatArtData = m_referenceList.GetCombatArtData(m_currentSelectedButton.skillUnlock);
            m_uiDetail.Display(combatArtData, m_currentSelectedButton.unlockLevel);
            m_selectorHighlight.Highlight(button);

            m_unlockArtHandler.ResetUnlockProgress();

            var availableSkillPoints = m_progressionReference.skillPoints.points;
            var combatArtCost = combatArtData.GetCombatArtLevelData(m_currentSelectedButton.unlockLevel).cost;

            if (availableSkillPoints < combatArtCost)
            {
                m_unlockArtHandler.DisableUnlockFunction();
                return;
            }

            m_unlockArtHandler.VerifyUnlockFunction(m_currentSelectedButton);
            //static bool CanAfford(CombatSkillPoints points, CombatArtLevelData combatArtLevelData) => points.points >= combatArtLevelData.cost;
        }

        public void StartUnlockSelectedCombatArt()
        {
            if (m_currentSelectedButton.currentState != CombatArtUnlockState.Unlockable)
                return;

            m_unlockArtHandler.StartUnlockProgress();
        }

        public void ResetUnlock() => m_unlockArtHandler.ResetUnlockProgress();

        private void OnUnlockSuccessFull()
        {
            m_unlockArtHandler.DisableUnlockFunction();

            var combatArtData = m_referenceList.GetCombatArtData(m_currentSelectedButton.skillUnlock);
            var combatArtLevelData = combatArtData.GetCombatArtLevelData(m_currentSelectedButton.unlockLevel);
            m_progressionReference.skillPoints.AddPoint(-combatArtLevelData.cost);
            m_currentSelectedButton.SetState(CombatArtUnlockState.Unlocked);
            ValidateButtonVisuals();
        }

        private void PopulateCombatArtList(CombatArtSelectButton[] buttons)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                var button = buttons[i];

                button.OnButtonSelected += Select;

                if (m_abilityButtonPair.TryGetValue(button.skillUnlock, out CombatArtSelectButton[] array))
                {
                    array[button.unlockLevel - 1] = button;
                }

                else
                {
                    var combatArtData = m_referenceList.GetCombatArtData(button.skillUnlock);
                    try
                    {
                        array = new CombatArtSelectButton[combatArtData.maxLevel];
                        array[button.unlockLevel - 1] = button;
                        m_abilityButtonPair.Add(button.skillUnlock, array);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Combat Arts Reference File Doesn't Have {button.skillUnlock}");
                    }
                }
            }
        }

        #region Combat Art Button State Handling
        private void ValidateButtonVisuals()
        {
            for (int i = 0; i < m_artRequirements.Length; i--)
            {
                m_artRequirements[i].ValidateButtonState();
            }
        }

        private void InitializeButtonStates()
        {
            var combatArtCount = (int)CombatArt._Count;
            for (int i = 0; i < combatArtCount; i++)
            {
                var combatArt = (CombatArt)i;
                InitializeArtLevelButtons(combatArt);
            }
        }

        private void InitializeArtLevelButtons(CombatArt combatArt)
        {
            if (!m_abilityButtonPair.TryGetValue(combatArt, out CombatArtSelectButton[] levelButtons))
                return;

            if (m_progressionReference.IsAbilityActivated(combatArt))
            {
                var currentLevel = m_progressionReference.GetAbilityLevel(combatArt);
                for (int k = 0; k < levelButtons.Length; k++)
                {
                    var state = (k < currentLevel) ? CombatArtUnlockState.Unlocked : CombatArtUnlockState.Locked;
                    levelButtons[k].SetState(state);
                }
                return;
            }

            for (int k = 0; k < levelButtons.Length; k++)
                levelButtons[k].SetState(CombatArtUnlockState.Locked);
            return;
        }
        #endregion


        #region Boilerplate & Editor Utils
#if UNITY_EDITOR
        [ContextMenu("Editor/Update SelectButtonVisuals")]
        private void UpdateSelectButtonVisuals()
        {
            var buttons = GetComponentsInChildren<CombatArtSelectButton>();
            foreach (var button in buttons)
            {
                var data = m_referenceList.GetCombatArtData(button.skillUnlock);
                var levelData = data.GetCombatArtLevelData(button.unlockLevel);
                button.DisplayAs(levelData);
            }
        }
#endif

        private void Awake()
        {
            m_abilityButtonPair = new Dictionary<CombatArt, CombatArtSelectButton[]>();
            var buttons = GetComponentsInChildren<CombatArtSelectButton>();
            PopulateCombatArtList(buttons);
            m_artRequirements = GetComponentsInChildren<CombatArtSelectRequirements>();
        }
    }
        #endregion

}