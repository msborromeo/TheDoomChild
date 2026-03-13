using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace DChild.Gameplay.Inventories.UI
{
    public class InventoryUISwapHandle : MonoBehaviour
    {
        [SerializeField] private PlayerInventoryUIHandle m_handle;
        [SerializeField] private InventorySwapHandle m_systemSwapHandle;

        [SerializeField, ReadOnly]
        private bool m_isSwapping = false;
        public bool isSwapping => m_isSwapping;

        private InventoryItemUI m_itemOne;
        public InventoryItemUI itemOne => m_itemOne;

        private InventoryItemUI m_itemTwo;
        public InventoryItemUI itemTwo => m_itemTwo;


        #region Setters
        public void SetFirstItem(InventoryItemUI value) => m_itemOne = value;
        public void SetSwappingStatus(bool value) => m_isSwapping = value;
        #endregion

        public void OnSecondItemSelected(InventoryItemUI slotUI)
        {
            m_itemTwo = slotUI;
            SwapItems();
        }

        public void SwapItems()
        {
            //check if either item is null due to a double call;
            if (m_itemOne == null || m_itemTwo == null) return;
         
            //check if both items for swap are in same list
            if (m_itemOne.isQuickItem == m_itemTwo.isQuickItem)
            {
                m_handle.SwapItems(m_itemOne, m_itemTwo);
                Reset();
                return;
            }

            MoveItemsBetweenInventories();
            Reset();
        }

        private void MoveItemsBetweenInventories()
        {
            PrepareTransferrableItem(m_itemOne);
            PrepareTransferrableItem(m_itemTwo);

            m_systemSwapHandle.SwapItemsBetweenInventories();
            m_handle.UpdateInventorySlots();
        }

        private void PrepareTransferrableItem(InventoryItemUI data)
        {
            if (data.isQuickItem)
            {
                m_systemSwapHandle.SetCurrentQuickItemInventoryItem(data.reference.data, data.reference.count);
                return;
            }
            m_systemSwapHandle.SetCurrentPlayerInventoryItem(data.reference.data, data.reference.count);
        }

        private void Reset()
        {
            m_itemOne = null;
            m_itemTwo = null;
        }
    }
}
