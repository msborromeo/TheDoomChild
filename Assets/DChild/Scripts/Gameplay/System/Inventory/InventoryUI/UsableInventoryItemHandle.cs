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
        [SerializeField]
        private PlayerInventory m_inventory;
        [SerializeField]
        private QuickItemInventory m_quickItemInventory;
        private UsableItemData m_item;

        public event EventAction<EventActionArgs> AllItemCountConsumed;
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

        public void HandleUsageOfItem(ItemData itemData)
        {
            m_item = (UsableItemData)itemData;
        }

        public void UseItemOnPlayer()
        {
            if (m_item.CanBeUse(m_player))
            {
                m_item.Use(m_player);
                if (m_removeItemCountOnConsume)
                {
                    if(m_quickItemInventory.GetItem(m_item) != null)
                    {
                        m_quickItemInventory.RemoveItem(m_item);
                    }
                    else
                    {
                        m_inventory.RemoveItem(m_item);
                    }
                    if (m_inventory.GetCurrentAmount(m_item) == 0)
                    {
                        AllItemCountConsumed?.Invoke(this, EventActionArgs.Empty);
                    }
                }
                ItemConsumed?.Invoke((IStoredItem)m_item);
                ItemUsed?.Invoke(m_item.itemName);

            }
        }

        private void Awake()
        {
            m_player = GameplaySystem.playerManager.player;
            //m_inventory = m_player.inventory;
        }
    }
}