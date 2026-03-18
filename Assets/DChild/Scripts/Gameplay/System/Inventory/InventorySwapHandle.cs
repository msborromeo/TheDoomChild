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
            m_currentSelectedPlayerInventoryItemCount = 0;
            m_currentSelectedQuickItemInventoryItem = null;
            m_currentSelectedQuickItemInventoryItemCount = 0;
        }

        [Button]
        public void SetCurrentPlayerInventoryItem(ItemData data, int count)
        {
            if (m_playerInventory.GetItem(data) == null)
                return;
            if (data.category == ItemCategory.Consumable || data.category == ItemCategory.Throwable)
            {
                m_currentSelectedPlayerInventoryItem = data;
                m_currentSelectedPlayerInventoryItemCount = count;
            }
        }

        [Button]
        public void SetCurrentQuickItemInventoryItem(ItemData data, int count)
        {
            if (m_quickItemInventory.GetItem(data) == null)
                return;
            if (data.category == ItemCategory.Consumable || data.category == ItemCategory.Throwable)
            {
                m_currentSelectedQuickItemInventoryItem = data;
                m_currentSelectedQuickItemInventoryItemCount = count;
            }
        }

        [Button]
        public void MoveQuickItemItemToPlayerInventory()
        {
            if (m_currentSelectedQuickItemInventoryItem == null)
                return;

            m_quickItemInventory.RemoveItem(m_currentSelectedQuickItemInventoryItem, m_currentSelectedQuickItemInventoryItemCount);
            m_playerInventory.ForceAddItem(m_currentSelectedQuickItemInventoryItem, m_currentSelectedQuickItemInventoryItemCount);

            m_currentSelectedQuickItemInventoryItem = null;
            m_currentSelectedQuickItemInventoryItemCount = 0;
        }

        [Button]
        public void MovePlayerInventoryItemToQuickItem()
        {
            if (m_currentSelectedPlayerInventoryItem == null)
                return;
            if (m_quickItemInventory.isInventoryFull)
                return;

            m_playerInventory.RemoveItem(m_currentSelectedPlayerInventoryItem, m_currentSelectedPlayerInventoryItemCount);
            m_quickItemInventory.AddItem(m_currentSelectedPlayerInventoryItem, m_currentSelectedPlayerInventoryItemCount);

            m_currentSelectedPlayerInventoryItem = null;
            m_currentSelectedPlayerInventoryItemCount = 0;
        }
    }
}

