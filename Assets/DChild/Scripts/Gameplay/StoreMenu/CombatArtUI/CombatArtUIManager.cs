using DChild.Gameplay.Characters.Player.CombatArt.Leveling;
using DChild.Gameplay.Characters.Players;
using DChild.Menu.Inputs;
using Doozy.Runtime.UIManager.Containers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

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
        private CombatArtSelectButton m_previewedButton;
        private CombatArtSelectButton[] m_buttons;
        private UIView m_view;
        private InputAction m_submitAction;
        private bool m_controllerUnlockHeld;
        private bool m_hasConfirmedPreview;
        private bool m_isInitialized;

        public void Initialize()
        {
            ClearInteractionState();
            ValidateButtonVisuals();

            m_selectorHighlight.Initialize();
            m_unlockArtHandler.UnlockSuccessful -= OnUnlockSuccessFull;
            m_unlockArtHandler.UnlockSuccessful += OnUnlockSuccessFull;
            m_unlockArtHandler.InitializeReferences(m_progressionReference, m_referenceList);
            m_unlockArtHandler.ResetUnlockProgress();
            BindSubmitInput();
            m_isInitialized = true;
            Preview(m_firstSelected);
            m_firstSelected?.uiButton.Select();

        }

        public void SyncButtonStates()
        {
            //InitializeButtonStates();
            ValidateButtonVisuals();
            RefreshUnlockFunction();
        }

        private void Preview(CombatArtSelectButton button)
        {
            if (!m_isInitialized || button == null || button == m_previewedButton)
                return;

            CancelUnlockProgress();
            m_hasConfirmedPreview = false;
            m_unlockArtHandler.DisableUnlockFunction();
            m_previewedButton = button;
            var combatArtData = m_referenceList.GetCombatArtData(button.skillUnlock);
            m_uiDetail.Display(combatArtData, button.unlockLevel);
        }

        public void Select(CombatArtSelectButton button)
        {
            Select(button, InputIconHandle.useGamepad);
        }

        private void Select(CombatArtSelectButton button, bool selectUnlockButton)
        {
            if (!m_isInitialized || button == null)
                return;

            Preview(button);
            CancelUnlockProgress();
            m_currentSelectedButton = button;
            m_hasConfirmedPreview = true;
            m_selectorHighlight.Highlight(button);
            RefreshUnlockFunction();
            if (selectUnlockButton)
                m_unlockArtHandler.SelectUnlockButton();
            //static bool CanAfford(CombatSkillPoints points, CombatArtLevelData combatArtLevelData) => points.points >= combatArtLevelData.cost;
        }

        public void StartUnlockSelectedCombatArt()
        {
            if (!CanUnlockSelectedCombatArt())
                return;

            m_unlockArtHandler.StartUnlockProgress();
        }

        private bool CanUnlockSelectedCombatArt()
        {
            if (!m_isInitialized || !m_hasConfirmedPreview || m_currentSelectedButton == null ||
                m_currentSelectedButton != m_previewedButton ||
                m_currentSelectedButton.currentState != CombatArtUnlockState.Unlockable)
                return false;

            var combatArtData = m_referenceList.GetCombatArtData(m_currentSelectedButton.skillUnlock);
            var combatArtCost = combatArtData.GetCombatArtLevelData(m_currentSelectedButton.unlockLevel).cost;
            return m_progressionReference.skillPoints.points >= combatArtCost;
        }

        private void RefreshUnlockFunction()
        {
            if (!m_isInitialized || !m_hasConfirmedPreview || m_currentSelectedButton == null ||
                m_currentSelectedButton != m_previewedButton)
            {
                m_unlockArtHandler.DisableUnlockFunction();
                return;
            }

            m_unlockArtHandler.VerifyUnlockFunction(m_currentSelectedButton, CanUnlockSelectedCombatArt());
        }

        private void CancelUnlockProgress()
        {
            m_controllerUnlockHeld = false;
            m_unlockArtHandler.ResetUnlockProgress();
            if (m_currentSelectedButton != null && m_currentSelectedButton.currentState == CombatArtUnlockState.Unlockable)
                m_unlockArtHandler.ResetBranchingUIProgressors();
        }

        private void ClearInteractionState()
        {
            m_isInitialized = false;
            CancelUnlockProgress();
            m_hasConfirmedPreview = false;
            m_currentSelectedButton = null;
            m_previewedButton = null;
            m_unlockArtHandler.DisableUnlockFunction();
            m_selectorHighlight.Clear();
        }

        public void ResetUnlock()
        {
            m_unlockArtHandler.ResetUnlockProgress();
            m_unlockArtHandler.ResetBranchingUIProgressors();
        }

        private void BindSubmitInput()
        {
            if (m_submitAction != null)
                return;

            var inputModule = EventSystem.current?.currentInputModule as InputSystemUIInputModule;
            m_submitAction = inputModule?.submit?.action;
            if (m_submitAction == null)
                return;

            m_submitAction.started += OnSubmitStarted;
            m_submitAction.canceled += OnSubmitCanceled;
        }

        private void UnbindSubmitInput()
        {
            if (m_submitAction == null)
                return;

            m_submitAction.started -= OnSubmitStarted;
            m_submitAction.canceled -= OnSubmitCanceled;
            m_submitAction = null;
        }

        private void OnSubmitStarted(InputAction.CallbackContext context)
        {
            if (!(context.control.device is Gamepad) || !m_unlockArtHandler.isUnlockButtonSelected || !CanUnlockSelectedCombatArt())
                return;

            m_controllerUnlockHeld = true;
            StartUnlockSelectedCombatArt();
        }

        private void OnSubmitCanceled(InputAction.CallbackContext context)
        {
            if (!m_controllerUnlockHeld)
                return;

            m_controllerUnlockHeld = false;
            ResetUnlock();
        }

        private void OnUnlockSuccessFull()
        {
            m_controllerUnlockHeld = false;
            m_hasConfirmedPreview = false;
            m_unlockArtHandler.DisableUnlockFunction();
            ValidateButtonVisuals();

            var combatArtData = m_referenceList.GetCombatArtData(m_currentSelectedButton.skillUnlock);
            var combatArtLevelData = combatArtData.GetCombatArtLevelData(m_currentSelectedButton.unlockLevel);
            m_progressionReference.skillPoints.AddPoint(-combatArtLevelData.cost);
            m_currentSelectedButton.SetState(CombatArtUnlockState.Unlocked);
            if (InputIconHandle.useGamepad)
                m_currentSelectedButton.uiButton.Select();
        }

        private void PopulateCombatArtList(CombatArtSelectButton[] buttons)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                var button = buttons[i];

                button.OnButtonSelected += Select;
                button.OnButtonPreviewed += Preview;

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
            foreach (var requirementButton in m_artRequirements)
            {
                requirementButton.ValidateButtonState(m_progressionReference);
            }
        }

        //private void InitializeButtonStates()
        //{
        //    var combatArtCount = (int)CombatArt._Count;
        //    for (int i = 0; i < combatArtCount; i++)
        //    {
        //        var combatArt = (CombatArt)i;
        //        InitializeArtLevelButtons(combatArt);
        //    }
        //}

        //private void InitializeArtLevelButtons(CombatArt combatArt)
        //{
        //    if (!m_abilityButtonPair.TryGetValue(combatArt, out CombatArtSelectButton[] levelButtons))
        //        return;

        //    if (!m_progressionReference.IsAbilityActivated(combatArt))
        //    {
        //        for (int k = 0; k < levelButtons.Length; k++)
        //            levelButtons[k].SetState(CombatArtUnlockState.Locked);
        //        return;
        //    }

        //    var currentLevel = m_progressionReference.GetAbilityLevel(combatArt);
        //    for (int k = 0; k < currentLevel; k++)
        //    {
        //        levelButtons[k].SetState(CombatArtUnlockState.Unlocked);
        //    }
        //    return;
        //}
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
            m_buttons = GetComponentsInChildren<CombatArtSelectButton>();
            PopulateCombatArtList(m_buttons);
            m_artRequirements = GetComponentsInChildren<CombatArtSelectRequirements>();
            m_view = GetComponent<UIView>();
            m_view?.OnHideCallback.Event.AddListener(ClearInteractionState);
        }

        private void OnEnable()
        {
            BindSubmitInput();
        }

        private void OnDisable()
        {
            ClearInteractionState();
            UnbindSubmitInput();
        }

        private void OnDestroy()
        {
            m_view?.OnHideCallback.Event.RemoveListener(ClearInteractionState);
            m_unlockArtHandler.UnlockSuccessful -= OnUnlockSuccessFull;
            if (m_buttons == null)
                return;

            foreach (var button in m_buttons)
            {
                if (button == null)
                    continue;
                button.OnButtonSelected -= Select;
                button.OnButtonPreviewed -= Preview;
            }
        }
    }
        #endregion

}
