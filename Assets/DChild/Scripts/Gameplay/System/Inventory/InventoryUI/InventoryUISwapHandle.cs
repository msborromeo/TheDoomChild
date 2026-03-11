using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace DChild.Gameplay.Inventories.UI
{
    public class InventoryUISwapHandle : MonoBehaviour
    {
        [SerializeField] private PlayerInventoryUIHandle m_handle;

        [SerializeField, ReadOnly]
        private bool m_isSwapping = false;
        public bool isSwapping => m_isSwapping;

        private ItemUI m_itemOne;
        public ItemUI itemOne => m_itemOne;

        private ItemUI m_itemTwo;
        public ItemUI itemTwo => m_itemTwo;


        #region Setters
        public void SetFirstItem(ItemUI value) => m_itemOne = value;
        public void SetSwappingStatus(bool value) => m_isSwapping = value;
        #endregion

        public void SwapItems()
        {
            m_handle.SwapItems(m_itemOne, m_itemTwo);
            Reset();
        }

        public void OnSecondItemSelected(ItemUI slotUI)
        {
            m_itemTwo = slotUI;
            SwapItems();
        }
        private void Reset()
        {
            m_itemOne = null;
            m_itemTwo = null;
        }
    }
}
