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

        private void OnEnable()
        {
            m_itemIndexDetailsUI.ItemDetailsDisplayed += OnItemDetailsDisplayed;
        }

        private void OnDisable()
        {
            m_itemIndexDetailsUI.ItemDetailsDisplayed -= OnItemDetailsDisplayed;
        }

        private void OnItemDetailsDisplayed(IStoredItem item)
        {
            currentItem = item;
            UpdateQuantityTextColor();
        }
    }
}

