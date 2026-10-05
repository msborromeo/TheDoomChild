using DChild.Gameplay.Items;
using Doozy.Runtime.UIManager.Input;
using Doozy.Runtime.UIManager.Containers;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace DChild.Gameplay.Inventories.UI
{
    public enum InventoryInteractionMode
    {
        Browse,
        AssignQuickItem,
        SwapItem
    }

    public class InventoryUISwapHandle : MonoBehaviour
    {
        [SerializeField] private PlayerInventoryUIHandle m_handle;
        [SerializeField] private InventorySwapHandle m_systemSwapHandle;
        [SerializeField] private GameObject m_quickItemSectionBlocker;

        [SerializeField, ReadOnly]
        private InventoryInteractionMode m_mode = InventoryInteractionMode.Browse;
        public InventoryInteractionMode mode => m_mode;

        private InventoryItemUI m_selectedItem;
        public InventoryItemUI itemOne => m_selectedItem;

        private InventoryItemUI m_operationOrigin;
        private InventoryItemUI m_actionFocusOrigin;
        private InventoryItemUI m_pendingToggleOff;
        private InventoryItemUI m_suppressedActivation;
        private Coroutine m_resolveToggleOffRoutine;
        private Coroutine m_submitRoutine;

        private InputAction m_cancelAction;
        private InputAction m_submitAction;
        private bool m_backButtonBlocked;
        private Coroutine m_releaseBackButtonRoutine;
        private UIContainer m_view;

        private bool interactionBlocked => m_handle.isInitializing || m_handle.isSelecting ||
            m_view == null || !m_view.isVisible;

        public bool ownsInput
        {
            get
            {
                var selected = EventSystem.current?.currentSelectedGameObject;
                return m_view != null && m_view.isVisible && selected != null &&
                    selected.transform.IsChildOf(m_handle.transform);
            }
        }

        public static bool CanAssignToQuickItems(InventoryItemUI slotUI)
        {
            if (slotUI?.reference?.data == null)
                return false;

            var category = slotUI.reference.data.category;
            return category == ItemCategory.Consumable || category == ItemCategory.Throwable;
        }

        private static bool IsInteractable(InventoryItemUI slotUI)
        {
            return slotUI != null && slotUI.GetComponent<Selectable>().IsInteractable();
        }

        public void SelectForBrowse(InventoryItemUI slotUI, bool suppressNextActivation)
        {
            if (m_handle.isSelecting || !IsInteractable(slotUI))
                return;

            if (m_mode != InventoryInteractionMode.Browse)
                CancelPendingAction();

            ClearActionFocus();

            m_selectedItem = slotUI;
            m_handle.FocusAndPresent(slotUI);

            if (suppressNextActivation)
                m_suppressedActivation = slotUI;
            else
                m_suppressedActivation = null;
        }

        public void OnSlotToggleChanged(InventoryItemUI slotUI, bool isOn)
        {
            if (interactionBlocked || !IsInteractable(slotUI))
                return;

            if (m_suppressedActivation == slotUI)
            {
                m_suppressedActivation = null;
                CancelPendingToggleOff();
                return;
            }

            if (isOn)
            {
                CancelPendingToggleOff();
                HandleSlotActivated(slotUI, false);
                return;
            }

            CancelPendingToggleOff();
            m_pendingToggleOff = slotUI;
            m_resolveToggleOffRoutine = StartCoroutine(ResolveToggleOffNextFrame());
        }

        public void SetSwappingStatus(bool value)
        {
            if (!value)
            {
                CancelPendingAction();
                return;
            }

            if (!interactionBlocked)
                BeginSwap();
        }

        private void BeginQuickItemAssignment(InventoryItemUI slotUI)
        {
            if (slotUI == null || !slotUI.isQuickItem || slotUI.reference != null)
                return;

            EnterMode(InventoryInteractionMode.AssignQuickItem, slotUI);
        }

        public void CancelPendingAction()
        {
            CancelPendingAction(false);
        }

        public void ResetInteraction()
        {
            UnbindInput();
            CancelPendingActivations();
            if (m_releaseBackButtonRoutine != null)
                StopCoroutine(m_releaseBackButtonRoutine);
            m_releaseBackButtonRoutine = null;
            ClearOperation(false);
            m_selectedItem = null;
            m_handle.SetQuickSelectionMode(false);
        }

        public void CancelPendingActivations()
        {
            CancelPendingToggleOff();
            if (m_submitRoutine != null)
                StopCoroutine(m_submitRoutine);
            m_submitRoutine = null;
            m_suppressedActivation = null;
        }

        public void ClearBrowseSelection()
        {
            ClearActionFocus();
            m_selectedItem = null;
            m_handle.PresentSelection(null);
        }

        public void MoveQuickItemToInventory(InventoryItemUI slotUI)
        {
            if (interactionBlocked || slotUI?.reference?.data == null || !IsInteractable(slotUI) || !slotUI.isQuickItem || m_systemSwapHandle == null)
                return;

            var focusItem = slotUI;
            PrepareTransferrableItem(slotUI);
            m_systemSwapHandle.MoveQuickItemItemToPlayerInventory();
            ClearOperation(false);
            m_handle.SetQuickSelectionMode(false);
            m_handle.UpdateInventorySlots();
            m_selectedItem = focusItem;
            m_handle.FocusAndPresent(focusItem);
        }

        public void RestoreAfterItemUse(InventoryItemUI slotUI, bool restoreActionFocus)
        {
            if (slotUI == null)
                return;

            m_selectedItem = slotUI;
            m_handle.FocusAndPresent(slotUI);

            if (restoreActionFocus && m_handle.TryFocusFirstAction())
            {
                m_actionFocusOrigin = slotUI;
                BlockBackButton();
                return;
            }

            ClearActionFocus();
        }

        public bool isActionFocused => m_actionFocusOrigin != null;

        private void HandleSlotActivated(InventoryItemUI slotUI, bool focusItemActions)
        {
            if (interactionBlocked || !IsInteractable(slotUI))
                return;

            switch (m_mode)
            {
                case InventoryInteractionMode.Browse:
                    if (slotUI.isQuickItem && slotUI.reference == null)
                    {
                        BeginQuickItemAssignment(slotUI);
                        return;
                    }

                    SelectForBrowse(slotUI, false);
                    if (focusItemActions)
                        TryFocusItemActions(slotUI);
                    break;

                case InventoryInteractionMode.AssignQuickItem:
                    HandleQuickItemAssignment(slotUI);
                    break;

                case InventoryInteractionMode.SwapItem:
                    HandleSwap(slotUI);
                    break;
            }
        }

        private void BeginSwap()
        {
            if (m_mode != InventoryInteractionMode.Browse || m_selectedItem?.reference?.data == null || !IsInteractable(m_selectedItem))
                return;

            EnterMode(InventoryInteractionMode.SwapItem, m_selectedItem);
        }

        private void EnterMode(InventoryInteractionMode mode, InventoryItemUI origin)
        {
            if (origin == null)
                return;

            m_mode = mode;
            m_operationOrigin = origin;
            m_selectedItem = origin;

            var restrictInventory = mode == InventoryInteractionMode.AssignQuickItem || origin.isQuickItem;
            m_handle.SetQuickSelectionMode(restrictInventory);

            var category = origin.reference?.data?.category;
            var blockQuickItems = mode == InventoryInteractionMode.SwapItem &&
                (category == ItemCategory.Key || category == ItemCategory.Quest);
            if (m_quickItemSectionBlocker != null)
                m_quickItemSectionBlocker.SetActive(blockQuickItems);

            BlockBackButton();
            m_handle.UpdateInventorySlots();
            m_handle.FocusAndPresent(origin);
        }

        private void HandleQuickItemAssignment(InventoryItemUI slotUI)
        {
            if (slotUI == m_operationOrigin)
            {
                CancelPendingAction();
                return;
            }

            if (slotUI.isQuickItem || !CanAssignToQuickItems(slotUI))
            {
                m_handle.FocusAndPresent(m_operationOrigin);
                return;
            }

            var quickSlot = m_handle.FindFirstEmptyQuickSlot();
            if (quickSlot == null || !m_handle.MoveInventoryItemToQuickItems(slotUI))
            {
                CancelPendingAction();
                return;
            }

            CompleteOperation(quickSlot);
        }

        private void HandleSwap(InventoryItemUI slotUI)
        {
            if (slotUI == m_operationOrigin)
            {
                CancelPendingAction();
                return;
            }

            if (slotUI?.reference?.data == null || m_operationOrigin?.reference?.data == null)
            {
                m_handle.FocusAndPresent(m_operationOrigin);
                return;
            }

            if (m_operationOrigin.isQuickItem != slotUI.isQuickItem)
            {
                var itemMovingToQuickItems = m_operationOrigin.isQuickItem ? slotUI : m_operationOrigin;
                if (!CanAssignToQuickItems(itemMovingToQuickItems) || m_systemSwapHandle == null)
                {
                    m_handle.FocusAndPresent(m_operationOrigin);
                    return;
                }

                PrepareTransferrableItem(m_operationOrigin);
                PrepareTransferrableItem(slotUI);
                m_systemSwapHandle.SwapItemsBetweenInventories();
            }
            else
            {
                m_handle.SwapItems(m_operationOrigin, slotUI);
            }

            CompleteOperation(slotUI);
        }

        private void CompleteOperation(InventoryItemUI focusItem)
        {
            ClearOperation(false);
            m_handle.SetQuickSelectionMode(false);
            m_handle.UpdateInventorySlots();
            m_selectedItem = focusItem;
            m_handle.FocusAndPresent(focusItem);
        }

        private void CancelPendingAction(bool deferBackButtonRelease)
        {
            if (m_mode == InventoryInteractionMode.Browse)
                return;

            var focusItem = m_operationOrigin;
            ClearOperation(deferBackButtonRelease);
            m_handle.SetQuickSelectionMode(false);
            m_handle.UpdateInventorySlots();
            m_selectedItem = focusItem;
            m_handle.FocusAndPresent(focusItem);
        }

        private void ClearOperation(bool deferBackButtonRelease)
        {
            m_mode = InventoryInteractionMode.Browse;
            m_operationOrigin = null;
            m_actionFocusOrigin = null;
            m_suppressedActivation = null;
            CancelPendingToggleOff();

            if (m_quickItemSectionBlocker != null)
                m_quickItemSectionBlocker.SetActive(false);

            if (deferBackButtonRelease)
            {
                if (m_releaseBackButtonRoutine != null)
                    StopCoroutine(m_releaseBackButtonRoutine);
                m_releaseBackButtonRoutine = StartCoroutine(ReleaseBackButtonNextFrame());
            }
            else
            {
                ReleaseBackButton();
            }
        }

        private void PrepareTransferrableItem(InventoryItemUI slotUI)
        {
            if (slotUI?.reference?.data == null || m_systemSwapHandle == null)
                return;

            if (slotUI.isQuickItem)
            {
                m_systemSwapHandle.SetCurrentQuickItemInventoryItem(slotUI.reference.data, slotUI.reference.count);
                return;
            }

            m_systemSwapHandle.SetCurrentPlayerInventoryItem(slotUI.reference.data, slotUI.reference.count);
        }

        public void BindCancelInput()
        {
            if (m_view == null)
                m_view = GetComponentInParent<UIContainer>();
            // The Items prefab cannot serialize a reference to the scene's player.
            // Keep an assigned service; resolve the active player's service otherwise.
            if (m_systemSwapHandle == null && GameplaySystem.playerManager?.player != null)
                m_systemSwapHandle = GameplaySystem.playerManager.player.inventory.GetComponent<InventorySwapHandle>();

            var inputModule = EventSystem.current?.currentInputModule as InputSystemUIInputModule;
            if (inputModule == null)
                return;

            if (m_cancelAction == null)
            {
                m_cancelAction = inputModule.cancel?.action;
                if (m_cancelAction != null)
                    m_cancelAction.performed += OnCancelPerformed;
            }

            if (m_submitAction == null)
            {
                m_submitAction = inputModule.submit?.action;
                if (m_submitAction != null)
                    m_submitAction.performed += OnSubmitPerformed;
            }
        }

        private void UnbindInput()
        {
            if (m_cancelAction != null)
            {
                m_cancelAction.performed -= OnCancelPerformed;
                m_cancelAction = null;
            }

            if (m_submitAction != null)
            {
                m_submitAction.performed -= OnSubmitPerformed;
                m_submitAction = null;
            }
        }

        private void OnSubmitPerformed(InputAction.CallbackContext context)
        {
            if (interactionBlocked || !ownsInput)
                return;

            var selectedObject = EventSystem.current?.currentSelectedGameObject;
            var slotUI = selectedObject?.GetComponent<InventoryItemUI>();
            if (!IsInteractable(slotUI))
                return;

            m_suppressedActivation = slotUI;
            CancelPendingToggleOff();

            if (m_submitRoutine != null)
                StopCoroutine(m_submitRoutine);
            m_submitRoutine = StartCoroutine(HandleControllerSubmitNextFrame(slotUI));
        }

        private void OnCancelPerformed(InputAction.CallbackContext context)
        {
            if (!ownsInput)
                return;

            if (m_mode == InventoryInteractionMode.Browse && m_actionFocusOrigin != null)
            {
                var focusItem = m_actionFocusOrigin;
                m_actionFocusOrigin = null;
                m_handle.FocusAndPresent(focusItem);
                ReleaseBackButtonDeferred();
                return;
            }

            if (m_mode == InventoryInteractionMode.Browse)
                return;

            CancelPendingAction(true);
        }

        private void TryFocusItemActions(InventoryItemUI slotUI)
        {
            ClearActionFocus();

            if (slotUI.reference == null)
                return;

            if (!m_handle.TryFocusFirstAction())
                return;

            m_actionFocusOrigin = slotUI;
            BlockBackButton();
        }

        private void ClearActionFocus()
        {
            m_actionFocusOrigin = null;
            if (m_mode == InventoryInteractionMode.Browse)
                ReleaseBackButton();
        }

        private void ReleaseBackButtonDeferred()
        {
            if (m_releaseBackButtonRoutine != null)
                StopCoroutine(m_releaseBackButtonRoutine);
            m_releaseBackButtonRoutine = StartCoroutine(ReleaseBackButtonNextFrame());
        }

        private IEnumerator ResolveToggleOffNextFrame()
        {
            yield return null;

            var slotUI = m_pendingToggleOff;
            m_pendingToggleOff = null;
            m_resolveToggleOffRoutine = null;
            HandleSlotActivated(slotUI, false);
        }

        private IEnumerator HandleControllerSubmitNextFrame(InventoryItemUI slotUI)
        {
            yield return null;
            m_submitRoutine = null;
            HandleSlotActivated(slotUI, true);
        }

        private void CancelPendingToggleOff()
        {
            if (m_resolveToggleOffRoutine != null)
                StopCoroutine(m_resolveToggleOffRoutine);

            m_resolveToggleOffRoutine = null;
            m_pendingToggleOff = null;
        }

        private void BlockBackButton()
        {
            if (m_releaseBackButtonRoutine != null)
            {
                StopCoroutine(m_releaseBackButtonRoutine);
                m_releaseBackButtonRoutine = null;
            }

            if (m_backButtonBlocked)
                return;

            BackButton.Disable();
            m_backButtonBlocked = true;
        }

        private IEnumerator ReleaseBackButtonNextFrame()
        {
            yield return null;
            m_releaseBackButtonRoutine = null;
            ReleaseBackButton();
        }

        private void ReleaseBackButton()
        {
            if (!m_backButtonBlocked)
                return;

            BackButton.Enable();
            m_backButtonBlocked = false;
        }

        private void LateUpdate()
        {
            if (m_view != null && (m_view.isHidden || m_view.isHiding))
            {
                m_handle.CancelInitialSelection();
                if (m_mode != InventoryInteractionMode.Browse || m_actionFocusOrigin != null || m_backButtonBlocked)
                    ResetInteraction();
                return;
            }

            if (m_actionFocusOrigin == null)
                return;

            var currentSelection = EventSystem.current?.currentSelectedGameObject;
            if (m_handle.IsActionButton(currentSelection))
                return;

            ClearActionFocus();
        }

        private void OnEnable()
        {
            BindCancelInput();
        }

        private void OnDisable()
        {
            m_handle.CancelInitialSelection();
            ResetInteraction();
        }
    }
}
