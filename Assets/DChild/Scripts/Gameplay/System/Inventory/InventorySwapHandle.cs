using DChild.Gameplay.Items;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Inventories
{
    public class InventorySwapHandle : MonoBehaviour
    {
        [SerializeField]
        private PlayerInventory m_playerInventory;
        [SerializeField]
        private QuickItemInventory m_quickItemInventory;
        [SerializeField]
        private ItemData m_currentSelectedPlayerInventoryItem;
        [SerializeField]
        private int m_currentSelectedPlayerInventoryItemCount;
        [SerializeField]
        private ItemData m_currentSelectedQuickItemInventoryItem;
        [SerializeField]
        private int m_currentSelectedQuickItemInventoryItemCount;

        [Button]
        public void SwapItemsBetweenInventories()
        {
            if (m_currentSelectedPlayerInventoryItem == null || m_currentSelectedQuickItemInventoryItem == null)
                return;
            //Add condition to prevent non consumable and throwable items from being swapped
            //if (m_currentSelectedPlayerInventoryItem.category != ItemCategory.Consumable || m_currentSelectedPlayerInventoryItem.category != ItemCategory.Throwable)
            //    return;
            //if (m_currentSelectedQuickItemInventoryItem.category != ItemCategory.Consumable || m_currentSelectedQuickItemInventoryItem.category != ItemCategory.Throwable)
            //    return;

            int playerInventoryIndex = m_playerInventory.GetItemIndex(m_currentSelectedPlayerInventoryItem);
            int quickItemInventoryIndex = m_quickItemInventory.GetItemIndex (m_currentSelectedQuickItemInventoryItem);

            //set quick item item in player inventory at swapped player inventory item index
            m_playerInventory.ReplaceItem(m_currentSelectedQuickItemInventoryItem, 
                m_currentSelectedQuickItemInventoryItemCount, 
                playerInventoryIndex);

            m_quickItemInventory.ReplaceItem(m_currentSelectedPlayerInventoryItem,
                m_currentSelectedPlayerInventoryItemCount,
                quickItemInventoryIndex);

            //Reset selected items after swap
            m_currentSelectedPlayerInventoryItem = null;
            m_currentSelectedQuickItemInventoryItem = null;
            m_currentSelectedPlayerInventoryItemCount = 0;
            m_currentSelectedQuickItemInventoryItemCount = 0;
        }

        [Button]
        public void SetCurrentPlayerInventoryItem(ItemData data, int count)
        {
            if (data.category == ItemCategory.Consumable || data.category == ItemCategory.Throwable)
            {
                m_currentSelectedPlayerInventoryItem = data;
                m_currentSelectedPlayerInventoryItemCount = count;
            }
        }

        [Button]
        public void SetCurrentQuickItemInventoryItem(ItemData data, int count)
        {
            if (data.category == ItemCategory.Consumable || data.category == ItemCategory.Throwable)
            {
                m_currentSelectedQuickItemInventoryItem = data;
                m_currentSelectedQuickItemInventoryItemCount = count;
            }
        }
    }
}

