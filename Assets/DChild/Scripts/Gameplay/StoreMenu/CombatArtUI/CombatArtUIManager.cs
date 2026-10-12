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

        private CombatArtSelectButton m_previewedButton;
        private CombatArtSelectButton m_unlockingButton;
        private CombatArtSelectButton[] m_buttons;
        private UIView m_view;
        private InputAction m_submitAction;
        private bool m_controllerUnlockHeld;
        private bool m_canUnlockPreview;
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
            if (!m_isInitialized || button == null)
                return;

            if (button != m_previewedButton)
            {
                CancelUnlockProgress();
                m_previewedButton = button;
                var combatArtData = m_referenceList.GetCombatArtData(button.skillUnlock);
                m_uiDetail.Display(combatArtData, button.unlockLevel);
            }

            RefreshUnlockFunction();
        }

        public void Select(CombatArtSelectButton button)
        {
            if (!m_isInitialized || button == null || button != m_previewedButton)
                return;

            m_selectorHighlight.Highlight(button);
        }

        private void OnButtonSubmitted(CombatArtSelectButton button)
        {
            if (!m_isInitialized || button == null || button != m_previewedButton || !m_canUnlockPreview)
                return;

            m_unlockArtHandler.SelectUnlockButton();
        }

        public void StartUnlockSelectedCombatArt()
        {
            if (!CanUnlockPreviewedCombatArt())
                return;

            m_unlockingButton = m_previewedButton;
            m_unlockArtHandler.StartUnlockProgress();
        }

        private bool CanUnlockPreviewedCombatArt()
        {
            if (!m_isInitialized || m_previewedButton == null ||
                m_previewedButton.currentState != CombatArtUnlockState.Unlockable)
                return false;

            var combatArtData = m_referenceList.GetCombatArtData(m_previewedButton.skillUnlock);
            var combatArtCost = combatArtData.GetCombatArtLevelData(m_previewedButton.unlockLevel).cost;
            return m_progressionReference.skillPoints.points >= combatArtCost;
        }

        private void RefreshUnlockFunction()
        {
            if (!m_isInitialized || m_previewedButton == null)
            {
                m_canUnlockPreview = false;
                m_unlockArtHandler.DisableUnlockFunction();
                return;
            }

            m_canUnlockPreview = CanUnlockPreviewedCombatArt();
            if (!m_canUnlockPreview && m_unlockingButton != null)
                CancelUnlockProgress();
            m_unlockArtHandler.VerifyUnlockFunction(m_previewedButton, m_canUnlockPreview);
        }

        private void CancelUnlockProgress()
        {
            m_controllerUnlockHeld = false;
            m_unlockArtHandler.ResetUnlockProgress();
            if (m_previewedButton != null && m_previewedButton.currentState == CombatArtUnlockState.Unlockable)
                m_unlockArtHandler.ResetBranchingUIProgressors();
            m_unlockingButton = null;
        }

        private void ClearInteractionState()
        {
            m_isInitialized = false;
            CancelUnlockProgress();
            m_canUnlockPreview = false;
            m_previewedButton = null;
            m_unlockArtHandler.DisableUnlockFunction();
            m_selectorHighlight.Clear();
        }

        public void ResetUnlock()
        {
            CancelUnlockProgress();
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
            if (!(context.control.device is Gamepad) || !m_unlockArtHandler.isUnlockButtonSelected || !CanUnlockPreviewedCombatArt())
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
            var unlockedButton = m_unlockingButton;
            m_unlockingButton = null;
            m_controllerUnlockHeld = false;
            if (unlockedButton == null)
                return;

            var combatArtData = m_referenceList.GetCombatArtData(unlockedButton.skillUnlock);
            var combatArtLevelData = combatArtData.GetCombatArtLevelData(unlockedButton.unlockLevel);
            m_progressionReference.skillPoints.AddPoint(-combatArtLevelData.cost);
            ValidateButtonVisuals();
            unlockedButton.SetState(CombatArtUnlockState.Unlocked);
            RefreshUnlockFunction();
            if (InputIconHandle.useGamepad)
                unlockedButton.uiButton.Select();
        }

        private void PopulateCombatArtList(CombatArtSelectButton[] buttons)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                var button = buttons[i];

                button.OnButtonSelected += Select;
                button.OnButtonPreviewed += Preview;
                button.OnButtonSubmitted += OnButtonSubmitted;

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
                button.OnButtonSubmitted -= OnButtonSubmitted;
            }
        }
    }
        #endregion

}
