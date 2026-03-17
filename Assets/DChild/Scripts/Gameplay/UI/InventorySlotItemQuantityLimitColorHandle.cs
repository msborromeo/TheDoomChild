using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Inventories.UI
{
    public class InventorySlotItemQuantityLimitColorHandle : QuantityLimitColorHandle
    {
        [SerializeField]
        private ItemIndexDetailsUI m_itemIndexDetailsUI;
        [SerializeField]
        private UsableInventoryItemHandle m_usableItemHandle;

        private void OnEnable()
        {
            m_itemIndexDetailsUI.ItemDetailsDisplayed += OnItemDetailsDisplayed;
            m_usableItemHandle.ItemConsumed += OnItemConsumed;
        }

        private void OnDisable()
        {
            m_itemIndexDetailsUI.ItemDetailsDisplayed -= OnItemDetailsDisplayed;
            m_usableItemHandle.ItemConsumed -= OnItemConsumed;
        }

        private void OnItemConsumed(IStoredItem item)
        {
            if(item == currentItem)
            {
                UpdateQuantityTextColor();
            }
        }

        private void OnItemDetailsDisplayed(IStoredItem item)
        {
            currentItem = item;
            UpdateQuantityTextColor();
        }
    }
}

