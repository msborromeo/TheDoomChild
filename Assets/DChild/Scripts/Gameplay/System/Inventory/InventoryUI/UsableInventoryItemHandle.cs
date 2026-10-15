using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Items;
using Doozy.Runtime.UIManager.Components;
using Holysoft.Event;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.Inventories.UI
{
    public class UsableInventoryItemHandle : MonoBehaviour
    {
        [SerializeField]
        private UIButton m_useItemButton;

        [SerializeField]
        private bool m_removeItemCountOnConsume;

        private Player m_player;
        private PlayerInventory m_inventory;
        private QuickItemInventory m_quickInventory;
        private UsableItemData m_item;

        private bool m_isQuickItem;
        private bool m_isUsing;
        private int m_lastUseFrame = -1;

        public event Action<ItemData, bool, int> OnItemCountReduced;
        public event Action<IStoredItem> ItemConsumed;

        #region PRE_ALPHA
        public event Action<string> ItemUsed;
        #endregion

        public void Show()
        {
            m_useItemButton.gameObject.SetActive(true);
        }

        public void Hide()
        {
            m_useItemButton.gameObject.SetActive(false);
        }

        public void UseItemFromInventory(UsableItemData item)
        {
            RemoveItem(item, m_isQuickItem);
        }

        private void RemoveItem(UsableItemData item, bool isQuickItem)
        {
            if (isQuickItem)
            {
                m_quickInventory.RemoveItem(item);
                return;
            }

            m_inventory.RemoveItem(item);
        }

        public void HandleUsageOfItem(ItemData itemData, bool isQuickItem)
        {
            m_item = itemData as UsableItemData;
            m_isQuickItem = isQuickItem;
            RefreshAvailability();
        }

        public void UseItemOnPlayer()
        {
            // Submit can also trigger PointerLeftClick in Doozy. Consume once
            // per activation, even if both configured callbacks run this frame.
            if (m_isUsing || m_lastUseFrame == Time.frameCount)
                return;

            var item = m_item;
            bool isQuickItem = m_isQuickItem;
            var storedItem = GetStoredItem(item, isQuickItem);
            if (item == null || storedItem == null || storedItem.count <= 0 || !item.CanBeUse(m_player))
            {
                RefreshAvailability();
                return;
            }

            m_isUsing = true;
            m_lastUseFrame = Time.frameCount;
            try
            {
                item.Use(m_player);
                ItemUsed?.Invoke(item.itemName);
                if (m_removeItemCountOnConsume)
                {
                    RemoveItem(item, isQuickItem);
                    int remainingCount = GetStoredItem(item, isQuickItem)?.count ?? 0;
                    OnItemCountReduced?.Invoke(item, isQuickItem, remainingCount);
                    ItemConsumed?.Invoke(storedItem);
                }
                RefreshAvailability();
            }
            finally
            {
                m_isUsing = false;
            }
        }

        public bool TryFocusUseButton()
        {
            if (!m_useItemButton.gameObject.activeInHierarchy || !m_useItemButton.interactable)
                return false;

            m_useItemButton.Select();
            return true;
        }

        public bool IsUseButton(GameObject target)
        {
            return target != null && target == m_useItemButton.gameObject;
        }

        private IStoredItem GetStoredItem(UsableItemData item, bool isQuickItem)
        {
            if (item == null)
                return null;
            return isQuickItem ? m_quickInventory.GetItem(item) : m_inventory.GetItem(item);
        }

        public void RefreshAvailability()
        {
            var storedItem = GetStoredItem(m_item, m_isQuickItem);
            m_useItemButton.interactable = storedItem != null && storedItem.count > 0 &&
                m_item.CanBeUse(m_player);
        }

        private void Awake()
        {
            m_player = GameplaySystem.playerManager.player;
            m_inventory = m_player.inventory;
            m_quickInventory = m_player.inventory.quickItemInventory;
        }
    }
}
