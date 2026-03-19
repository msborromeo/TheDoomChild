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

        public event EventAction<EventActionArgs> AllItemCountConsumed;
        public event EventAction<EventActionArgs> OnItemCountReduced;

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
            if (m_isQuickItem)
            {
                m_quickInventory.RemoveItem(item);
                return;
            }

            m_inventory.RemoveItem(item);
        }

        public void HandleUsageOfItem(ItemData itemData, bool isQuickItem)
        {
            m_item = (UsableItemData)itemData;
            m_isQuickItem = isQuickItem;
        }

        public void UseItemOnPlayer()
        {
            if (m_item.CanBeUse(m_player))
            {
                m_item.Use(m_player);
                ItemUsed?.Invoke(m_item.itemName);
                if (m_removeItemCountOnConsume)
                {
                    UseItemFromInventory(m_item);
                    //m_inventory.RemoveItem(m_item);
                    OnItemCountReduced?.Invoke(this, EventActionArgs.Empty);

                    if (m_inventory.GetCurrentAmount(m_item) == 0)
                    {
                        AllItemCountConsumed?.Invoke(this, EventActionArgs.Empty);
                    }
                }
            }
        }

        private void Awake()
        {
            m_player = GameplaySystem.playerManager.player;
            m_inventory = m_player.inventory;
            m_quickInventory = m_player.inventory.quickItemInventory;
        }
    }
}