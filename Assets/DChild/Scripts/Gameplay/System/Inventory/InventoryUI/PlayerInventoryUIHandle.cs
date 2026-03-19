using DChild.Gameplay.Items;
using Doozy.Runtime.UIManager.Components;
using Holysoft.Event;
using Holysoft.UI;
using Sirenix.OdinInspector;
using System;
using System.Diagnostics;
using UnityEngine;

namespace DChild.Gameplay.Inventories.UI
{
    public class PlayerInventoryUIHandle : SerializedMonoBehaviour
    {
        [SerializeField]
        private ItemDetailsUI m_detailedUI;
        [SerializeField]
        private InventoryListUI<IInventory> m_listUI;
        [SerializeField]
        private QuickItemsListUI m_quickItemListUI;
        [SerializeField]
        private ItemUI m_firstSelectedItemUI;
        [SerializeField]
        private UsableInventoryItemHandle m_usableInventoryItemHandle;
        [SerializeField]
        private InventoryItemActionHandle m_itemActionsHandle;
        [SerializeField]
        private InventoryUISwapHandle m_swapHandle;


        public void Select(ItemUI itemUI)
        {
            if (itemUI == null) return;
            var inventoryItem = itemUI as InventoryItemUI;

            m_detailedUI.ShowDetails(itemUI.reference);
            m_itemActionsHandle.ShowButtonActions(inventoryItem);

            if ((itemUI?.reference?.data ?? null) == null || itemUI.reference.data.category != ItemCategory.Consumable)
            {
                m_usableInventoryItemHandle.Hide();
            }
            else
            {
                m_usableInventoryItemHandle.Show();
                m_usableInventoryItemHandle.HandleUsageOfItem(itemUI.reference.data, inventoryItem.isQuickItem);
            }

            m_swapHandle.SetFirstItem(inventoryItem);
        }

        [Button]
        public void SwapItems(ItemUI itemOne, ItemUI itemTwo)
        {
            if (IsEitherSlotQuickItem(itemOne, itemTwo))
            {
                m_quickItemListUI.SwapItems(itemOne, itemTwo);
                UpdateInventorySlots();
                return;
            }

            m_listUI.SwapItems(itemOne, itemTwo);
            UpdateInventorySlots();
        }

        public void UpdateShardIcon(ItemSprite type)
        {

        }

        public void SelectFirstSlot()
        {
            Select(m_firstSelectedItemUI);
            var button = m_firstSelectedItemUI.GetComponent<UIToggle>();
            button.SetIsOn(true);
        }

        public void FilterOutNonQuickItems(ItemUI itemUI)
        {
            var item = itemUI as InventoryItemUI;
            m_listUI.UpdateUIList(item.isQuickItem);
        }

        public void MoveInventoryItemToQuickItems(ItemUI itemUI)
        {
            m_quickItemListUI.MoveInventoryItemToQuickItems(itemUI as InventoryItemUI);
            m_listUI.inventory.RemoveItem(itemUI.reference.data, itemUI.reference.count);
            UpdateInventorySlots();
            itemUI.GetComponent<UIToggle>().SetIsOn(true);
        }

        //public void MoveQuickItemToInventory(ItemUI itemUI)
        //{
        //    m_listUI.inventory.ForceAddItem(itemUI.reference.data, itemUI.reference.count);
        //    m_quickItemListUI.RemoveQuickItem(itemUI);
        //    UpdateInventorySlots();
        //    //itemUI.GetComponent<UIToggle>().SetIsOn(true);
        //}

        private bool IsEitherSlotQuickItem(ItemUI itemOne, ItemUI itemTwo)
        {
            return (itemOne as InventoryItemUI).isQuickItem || (itemTwo as InventoryItemUI).isQuickItem;
        }

        public void UpdateInventorySlots()
        {
            m_quickItemListUI.UpdateUIList();
            m_listUI.UpdateUIList();
        }

        public void Initialize()
        {
            m_listUI.Reset();
            UpdateInventorySlots();

            SelectFirstSlot();
        }

        private void OnListOverallChange(object sender, EventActionArgs eventArgs)
        {
            m_detailedUI.ShowDetails(m_firstSelectedItemUI.reference);
        }

        private void OnItemUsedConsumed(object sender, EventActionArgs eventArgs)
        {
            Select(null);
        }

        private void OnItemCountReduced(object sender, EventActionArgs eventArgs)
        {
            UpdateInventorySlots();
        }

        private void Awake()
        {
            m_listUI.ListOverallChange += OnListOverallChange;
            m_usableInventoryItemHandle.OnItemCountReduced += OnItemCountReduced;
            m_usableInventoryItemHandle.AllItemCountConsumed += OnItemUsedConsumed;
        }

    }
}