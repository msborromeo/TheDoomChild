using DChild.Gameplay.Items;
using Doozy.Runtime.UIManager.Components;
using Holysoft.Event;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DChild.Gameplay.Inventories.UI
{
    public class InventoryFilterNavigationController : MonoBehaviour
    {
        [SerializeField] private PlayerInventoryUIHandle m_handle;
        [SerializeField] private InventoryUISwapHandle m_swapHandle;
        [SerializeField] private GridInventoryListUI m_inventoryUI;
        [SerializeField] private InventoryCategoryToggleUI[] m_filterToggles;
        [SerializeField] private InputActionReference m_cycleSubTabInput;

        private InventoryItemUI m_previousSlot;
        private ItemData m_previousItem;
        private bool m_previousIsQuickItem;
        private bool m_restoreFocusAfterFilterChange;
        private readonly Dictionary<Selectable, Navigation> m_originalNavigation = new Dictionary<Selectable, Navigation>();

        public void PreviousFilter() => CycleFilter(-1);
        public void NextFilter() => CycleFilter(1);

        public void BeforeFilterChange()
        {
            // Hiding a focused action button can clear EventSystem selection.
            // Remember ownership before clearing the old item's presentation.
            m_restoreFocusAfterFilterChange = m_swapHandle.ownsInput;
            m_previousSlot = m_swapHandle.itemOne;
            m_previousItem = m_previousSlot?.reference?.data;
            m_previousIsQuickItem = m_previousSlot != null && m_previousSlot.isQuickItem;
            m_swapHandle.ClearSelection();
        }

        public void OnFilterChanged()
        {
            RefreshInteractionPolicy();
            foreach (var filter in m_filterToggles)
                filter.UpdateToggleVisuals();

            // Initial category setup runs before the view's visible callback.
            bool restoreFocus = m_restoreFocusAfterFilterChange || m_swapHandle.ownsInput;
            m_restoreFocusAfterFilterChange = false;
            if (!m_swapHandle.isViewVisible || !restoreFocus)
                return;

            FocusAvailableSlot();
        }

        public void RefreshInteractionPolicy()
        {
            var selectedFilter = FindSelectedFilter();
            bool isQuestFilter = selectedFilter != null &&
                selectedFilter.category == (ItemCategory.Key | ItemCategory.Quest);
            m_swapHandle.SetQuickItemsBlockedByFilter(isQuestFilter);
            UpdateQuickItemNavigation();
        }

        public void FocusAvailableSlot()
        {
            var focusItem = m_handle.FindSlot(m_previousItem, m_previousIsQuickItem);
            if (focusItem == null && m_previousIsQuickItem && m_previousItem == null &&
                m_previousSlot != null && m_previousSlot.isAvailable)
                focusItem = m_previousSlot;
            if (focusItem == null)
                focusItem = m_inventoryUI.FindFirstOccupiedSlot();
            if (focusItem == null && m_handle.firstSelectedItem != null && m_handle.firstSelectedItem.isAvailable)
                focusItem = m_handle.firstSelectedItem;

            if (focusItem != null)
            {
                m_swapHandle.SelectForBrowse(focusItem, false);
                return;
            }

            m_swapHandle.ClearSelection();
            FindSelectedFilter()?.GetComponent<UIToggle>().Select();
        }

        private InventoryCategoryToggleUI FindSelectedFilter()
        {
            return System.Array.Find(m_filterToggles, filter => filter.isSelected);
        }

        private static bool IsQuickItem(Selectable selectable)
        {
            return selectable != null && selectable.TryGetComponent<InventoryItemUI>(out var slot) && slot.isQuickItem;
        }

        private void UpdateQuickItemNavigation()
        {
            if (!m_swapHandle.quickItemsBlocked)
            {
                RestoreNavigation();
                return;
            }

            if (m_originalNavigation.Count == 0)
            {
                foreach (var selectable in m_handle.GetComponentsInChildren<Selectable>(true))
                {
                    var navigation = selectable.navigation;
                    if (IsQuickItem(selectable) || IsQuickItem(navigation.selectOnUp) ||
                        IsQuickItem(navigation.selectOnDown) || IsQuickItem(navigation.selectOnLeft) ||
                        IsQuickItem(navigation.selectOnRight))
                        m_originalNavigation.Add(selectable, navigation);
                }
            }

            var firstGridSlot = m_inventoryUI.FindFirstOccupiedSlot();
            Selectable fallback = firstGridSlot != null
                ? firstGridSlot.GetComponent<UIToggle>()
                : FindSelectedFilter()?.GetComponent<UIToggle>();

            foreach (var entry in m_originalNavigation)
            {
                if (entry.Key == null)
                    continue;

                var navigation = entry.Value;
                var target = fallback == entry.Key ? FindSelectedFilter()?.GetComponent<UIToggle>() : fallback;
                if (IsQuickItem(entry.Key))
                    navigation.mode = Navigation.Mode.None;
                if (IsQuickItem(navigation.selectOnUp)) navigation.selectOnUp = target;
                if (IsQuickItem(navigation.selectOnDown)) navigation.selectOnDown = target;
                if (IsQuickItem(navigation.selectOnLeft)) navigation.selectOnLeft = target;
                if (IsQuickItem(navigation.selectOnRight)) navigation.selectOnRight = target;
                entry.Key.navigation = navigation;
            }
        }

        private void RestoreNavigation()
        {
            foreach (var entry in m_originalNavigation)
            {
                if (entry.Key != null)
                    entry.Key.navigation = entry.Value;
            }
            m_originalNavigation.Clear();
        }

        private void OnListOverallChange(object sender, EventActionArgs eventArgs)
        {
            UpdateQuickItemNavigation();
        }

        private void LateUpdate()
        {
            if (!m_swapHandle.ownsInput)
                return;

            var selectedSlot = EventSystem.current.currentSelectedGameObject.GetComponent<InventoryItemUI>();
            if (selectedSlot != null && !selectedSlot.isAvailable)
                FocusAvailableSlot();
        }

        private void CycleFilter(int direction)
        {
            if (!m_swapHandle.ownsInput || m_filterToggles.Length == 0)
                return;

            int currentIndex = System.Array.FindIndex(m_filterToggles, filter => filter.isSelected);
            if (currentIndex < 0)
                currentIndex = direction > 0 ? -1 : 0;

            for (int offset = 1; offset <= m_filterToggles.Length; offset++)
            {
                int index = (currentIndex + direction * offset + m_filterToggles.Length) % m_filterToggles.Length;
                if (!m_filterToggles[index].isAvailable)
                    continue;

                m_filterToggles[index].Select();
                return;
            }
        }

        private void OnCycleSubTab(InputAction.CallbackContext context)
        {
            float direction = context.ReadValue<float>();
            if (!Mathf.Approximately(direction, 0f))
                CycleFilter(direction > 0f ? 1 : -1);
        }

        private void OnEnable()
        {
            m_swapHandle.QuickItemInteractionChanged += UpdateQuickItemNavigation;
            m_inventoryUI.ListOverallChange += OnListOverallChange;
            if (m_cycleSubTabInput != null)
                m_cycleSubTabInput.action.performed += OnCycleSubTab;
        }

        private void OnDisable()
        {
            m_swapHandle.QuickItemInteractionChanged -= UpdateQuickItemNavigation;
            m_inventoryUI.ListOverallChange -= OnListOverallChange;
            RestoreNavigation();
            m_swapHandle.SetQuickItemsBlockedByFilter(false);
            if (m_cycleSubTabInput != null)
                m_cycleSubTabInput.action.performed -= OnCycleSubTab;
            m_previousSlot = null;
            m_previousItem = null;
            m_restoreFocusAfterFilterChange = false;
        }
    }
}
