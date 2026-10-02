using DChild.Gameplay.Items;
using UnityEngine;
using UnityEngine.InputSystem;

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

        public void PreviousFilter() => CycleFilter(-1);
        public void NextFilter() => CycleFilter(1);

        public void BeforeFilterChange()
        {
            m_previousSlot = m_swapHandle.itemOne;
            m_previousItem = m_previousSlot?.reference?.data;
            m_previousIsQuickItem = m_previousSlot != null && m_previousSlot.isQuickItem;
            m_swapHandle.CancelPendingAction();
        }

        public void OnFilterChanged()
        {
            foreach (var filter in m_filterToggles)
                filter.UpdateToggleVisuals();

            // Initial category setup runs before the view's visible callback.
            if (!m_swapHandle.ownsInput)
                return;

            var focusItem = m_handle.FindSlot(m_previousItem, m_previousIsQuickItem);
            if (focusItem == null && m_previousIsQuickItem && m_previousItem == null)
                focusItem = m_previousSlot;
            if (focusItem == null)
                focusItem = m_inventoryUI.FindFirstOccupiedSlot();
            if (focusItem == null)
                focusItem = m_handle.firstSelectedItem;

            m_swapHandle.SelectForBrowse(focusItem, false);
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
            if (m_cycleSubTabInput != null)
                m_cycleSubTabInput.action.performed += OnCycleSubTab;
        }

        private void OnDisable()
        {
            if (m_cycleSubTabInput != null)
                m_cycleSubTabInput.action.performed -= OnCycleSubTab;
            m_previousSlot = null;
            m_previousItem = null;
        }
    }
}
