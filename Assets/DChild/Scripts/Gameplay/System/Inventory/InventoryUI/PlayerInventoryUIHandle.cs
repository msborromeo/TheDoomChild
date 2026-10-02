using DChild.Gameplay.Items;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using Holysoft.Event;
using Holysoft.UI;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DChild.Gameplay.Inventories.UI
{
    public class PlayerInventoryUIHandle : SerializedMonoBehaviour
    {
        [SerializeField] private ItemDetailsUI m_detailedUI;
        [SerializeField] private InventoryListUI<IInventory> m_listUI;
        [SerializeField] private QuickItemsListUI m_quickItemListUI;
        [SerializeField] private ItemUI m_firstSelectedItemUI;
        [SerializeField] private UsableInventoryItemHandle m_usableInventoryItemHandle;
        [SerializeField] private InventoryItemActionHandle m_itemActionsHandle;
        [SerializeField] private InventoryUISwapHandle m_swapHandle;
        [SerializeField] private InventoryCategoryToggleUI[] m_filterToggles;

        private UIContainer m_view;
        private Coroutine m_initialSelectionRoutine;
        private bool m_isInitializing;
        private bool m_isSelecting;

        public bool isInitializing => m_isInitializing;
        public bool isSelecting => m_isSelecting;

        public InventoryItemUI firstSelectedItem => m_firstSelectedItemUI as InventoryItemUI;

        public void Select(ItemUI itemUI)
        {
            var inventoryItem = itemUI as InventoryItemUI;
            if (inventoryItem == null || m_isInitializing || m_isSelecting ||
                m_view == null || !m_view.isVisible)
                return;

            m_swapHandle.SelectForBrowse(inventoryItem, true);
        }

        public void PresentSelection(InventoryItemUI inventoryItem)
        {
            if (inventoryItem == null)
            {
                m_detailedUI.ShowDetails(null);
                m_itemActionsHandle.ShowButtonActions(null);
                m_usableInventoryItemHandle.Hide();
                return;
            }

            m_detailedUI.ShowDetails(inventoryItem.reference);
            m_itemActionsHandle.ShowButtonActions(inventoryItem);

            if (inventoryItem.reference?.data?.category != ItemCategory.Consumable)
            {
                m_usableInventoryItemHandle.Hide();
                return;
            }

            m_usableInventoryItemHandle.Show();
            m_usableInventoryItemHandle.HandleUsageOfItem(inventoryItem.reference.data, inventoryItem.isQuickItem);
        }

        public void FocusAndPresent(InventoryItemUI inventoryItem)
        {
            if (m_isSelecting || !IsEligibleSlot(inventoryItem))
                return;

            m_isSelecting = true;
            PresentSelection(inventoryItem);
            var toggle = inventoryItem.GetComponent<UIToggle>();
            toggle.SetIsOn(true, true, false);
            toggle.Select();
            m_isSelecting = false;
        }

        public bool TryFocusFirstAction()
        {
            if (m_usableInventoryItemHandle.TryFocusUseButton())
                return true;

            return m_itemActionsHandle.TryFocusFirstActionButton();
        }

        public bool IsActionButton(GameObject target)
        {
            return m_usableInventoryItemHandle.IsUseButton(target) ||
                m_itemActionsHandle.IsActionButton(target);
        }

        [Button]
        public void SwapItems(ItemUI itemOne, ItemUI itemTwo)
        {
            if (itemOne == null || itemTwo == null)
                return;

            if (IsEitherSlotQuickItem(itemOne, itemTwo))
            {
                m_quickItemListUI.SwapItems(itemOne, itemTwo);
                return;
            }

            m_listUI.SwapItems(itemOne, itemTwo);
        }

        public void UpdateShardIcon(ItemSprite type)
        {
        }

        public void SelectFirstSlot()
        {
            if (m_view == null || !m_view.isVisible)
                return;

            if (m_initialSelectionRoutine != null)
                StopCoroutine(m_initialSelectionRoutine);
            m_isInitializing = true;
            m_swapHandle.CancelPendingActivations();
            FocusBrowseFallback();
            m_initialSelectionRoutine = StartCoroutine(FinishInitialSelection());
        }

        public void FocusBrowseFallback()
        {
            var firstItem = m_quickItemListUI.firstSlot;
            if (!IsEligibleSlot(firstItem))
                firstItem = (m_listUI as GridInventoryListUI)?.FindFirstInteractableOccupiedSlot();

            if (IsEligibleSlot(firstItem))
            {
                m_swapHandle.SelectForBrowse(firstItem, false);
                return;
            }

            m_swapHandle.ClearBrowseSelection();
            foreach (var filter in m_filterToggles)
            {
                if (!filter.isSelected || !filter.isAvailable)
                    continue;

                m_isSelecting = true;
                filter.GetComponent<Selectable>().Select();
                m_isSelecting = false;
                return;
            }
        }

        private static bool IsEligibleSlot(InventoryItemUI slot)
        {
            return slot != null && slot.gameObject.activeInHierarchy &&
                slot.GetComponent<Selectable>().IsInteractable();
        }

        private IEnumerator FinishInitialSelection()
        {
            yield return null;
            yield return new WaitForEndOfFrame();
            m_initialSelectionRoutine = null;
            if (m_view == null || !m_view.isVisible)
            {
                CancelInitialSelection();
                yield break;
            }

            m_swapHandle.CancelPendingActivations();
            FocusBrowseFallback();
            m_isInitializing = false;
        }

        public void CancelInitialSelection()
        {
            if (m_initialSelectionRoutine != null)
                StopCoroutine(m_initialSelectionRoutine);
            m_initialSelectionRoutine = null;
            m_isInitializing = false;
            m_isSelecting = false;
        }

        public void SetQuickSelectionMode(bool enabled)
        {
            if (m_listUI is GridInventoryListUI gridInventory)
                gridInventory.SetQuickSelectionMode(enabled);
        }

        public bool MoveInventoryItemToQuickItems(InventoryItemUI itemUI)
        {
            if (itemUI?.reference?.data == null || itemUI.isQuickItem || m_quickItemListUI.inventory.isInventoryFull)
                return false;

            m_quickItemListUI.MoveInventoryItemToQuickItems(itemUI);
            m_listUI.inventory.RemoveItem(itemUI.reference.data, itemUI.reference.count);
            return true;
        }

        public InventoryItemUI FindFirstEmptyQuickSlot()
        {
            return m_quickItemListUI.FindFirstEmptySlot();
        }

        public InventoryItemUI FindSlot(ItemData itemData, bool isQuickItem)
        {
            if (itemData == null)
                return null;

            if (isQuickItem)
                return m_quickItemListUI.FindSlot(itemData);

            return (m_listUI as GridInventoryListUI)?.FindSlot(itemData);
        }

        public InventoryItemUI FindNearestOccupiedSlot(InventoryItemUI origin, bool isQuickItem)
        {
            if (origin == null)
                return null;

            if (isQuickItem)
                return m_quickItemListUI.FindNearestOccupiedSlot(origin);

            return (m_listUI as GridInventoryListUI)?.FindNearestOccupiedSlot(origin);
        }

        private bool IsEitherSlotQuickItem(ItemUI itemOne, ItemUI itemTwo)
        {
            return (itemOne as InventoryItemUI).isQuickItem || (itemTwo as InventoryItemUI).isQuickItem;
        }

        public void UpdateInventorySlots()
        {
            m_quickItemListUI.UpdateUIList();
            m_listUI.UpdateUIList();
        }

        private void SetupFilterToggles()
        {
            foreach (var toggle in m_filterToggles)
                toggle.UpdateToggleVisuals();
        }

        public void Initialize()
        {
            CancelInitialSelection();
            m_isInitializing = true;
            m_swapHandle.ResetInteraction();
            m_swapHandle.BindCancelInput();
            m_listUI.Reset();
            SetQuickSelectionMode(false);
            UpdateInventorySlots();
            SetupFilterToggles();
        }

        private void OnListOverallChange(object sender, EventActionArgs eventArgs)
        {
            var selected = m_swapHandle.itemOne;
            if (selected != null)
                PresentSelection(selected);
            else
                m_detailedUI.ShowDetails(null);
        }

        private void OnItemCountReduced(ItemData itemData, bool isQuickItem, int remainingCount)
        {
            var previousSlot = m_swapHandle.itemOne;
            var restoreActionFocus = m_swapHandle.isActionFocused ||
                IsActionButton(EventSystem.current?.currentSelectedGameObject);
            UpdateInventorySlots();

            var focusItem = FindSlot(itemData, isQuickItem);
            if (focusItem == null)
                focusItem = FindNearestOccupiedSlot(previousSlot, isQuickItem);
            if (focusItem == null)
                focusItem = firstSelectedItem;

            m_swapHandle.RestoreAfterItemUse(focusItem, remainingCount > 0 && restoreActionFocus);
        }

        private void Awake()
        {
            m_view = GetComponent<UIContainer>();
            m_listUI.ListOverallChange += OnListOverallChange;
            m_usableInventoryItemHandle.OnItemCountReduced += OnItemCountReduced;
        }

        private void OnEnable()
        {
            m_isInitializing = true;
        }

        private void OnDisable()
        {
            CancelInitialSelection();
        }

        private void OnDestroy()
        {
            m_listUI.ListOverallChange -= OnListOverallChange;
            m_usableInventoryItemHandle.OnItemCountReduced -= OnItemCountReduced;
        }
    }
}
