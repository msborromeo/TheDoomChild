using DChild.Gameplay.Items;
using DChild.Gameplay.EquipmentSystem;
using DChild.Gameplay.SoulSkills;
using DChild.Gameplay.Systems;
using DChild.Gameplay.Trade;
using Holysoft.Collections;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace DChild.Gameplay.Inventories
{
    public class PlayerInventory : MonoBehaviour, ITradeInventory, IInventory, ICurrency, ISerializable<TradableInventorySerialization>
    {
        [SerializeField]
        private ItemList m_referenceList;
        [SerializeField, HideLabel, FoldoutGroup("Inventory")]
        private TradableInventory m_inventory = new TradableInventory(false, true);
        [SerializeField]
        private QuickItemInventory m_quickItemInventory;

        public event EventAction<CurrencyUpdateEventArgs> OnAmountSet;
        public event EventAction<CurrencyUpdateEventArgs> OnAmountAdded;
        public event EventAction<SoulSkillAcquiredEventArgs> SoulSkillItemAcquired;
        public event EventAction<SoulEquipmentAcquiredEventArgs> SoulEquipmentAcquired;
        public event EventAction<ItemEventArgs> InventoryItemUpdate
        {
            add
            {
                m_inventory.InventoryItemUpdate += value;
            }
            remove
            {
                m_inventory.InventoryItemUpdate -= value;
            }
        }
        public event EventAction<EventActionArgs> MassInventoryItemUpdate
        {
            add
            {
                m_inventory.MassInventoryItemUpdate += value;
            }
            remove
            {
                m_inventory.MassInventoryItemUpdate -= value;
            }
        }

        public int storedItemCount => m_inventory.storedItemCount;

        public int GetCurrencyAmount(CurrencyType currencyType) => m_inventory.GetCurrencyAmount(currencyType);

        public TradableInventorySerialization SaveData() => new TradableInventorySerialization(m_inventory);

        public void LoadData(TradableInventorySerialization serializedData)
        {
            m_inventory.ClearList();

            if (serializedData == null)
            {
                m_inventory.SetCurrency(CurrencyType.SoulEssence,0);
                SetSoulEssence(0);
                m_inventory.InvokeMassInventoryItemUpdate();
                return;
            }
            else
            {
                m_inventory.SetCurrency(CurrencyType.SoulEssence,serializedData.soulEssence);
                SetSoulEssence(serializedData.soulEssence);
            }

            TradableInventory.Item inventoryItem = null;
            for (int i = 0; i < serializedData.count; i++)
            {
                var serializedItem = serializedData.GetSerializedItem(i);
                var itemData = m_referenceList.GetInfo(serializedItem.id);
                m_inventory.AddItem(itemData, out inventoryItem, serializedItem.count);
                inventoryItem.SetCountToInfinite(serializedItem.isInfinite);
            }
            m_inventory.InvokeMassInventoryItemUpdate();
        }

        public void Load(IInventoryInfo reference)
        {
            m_inventory.ClearList();
            TradableInventory.Item inventoryItem;
            for (int i = 0; i < reference.storedItemCount; i++)
            {
                var storedItem = reference.GetItem(i);
                m_inventory.AddItem(storedItem.data, out inventoryItem, storedItem.count);
                inventoryItem.SetCountToInfinite(storedItem.hasInfiniteCount);
            }
        }

        public void SetItem(ItemData itemData, int count) => m_inventory.SetItem(itemData, count);

        public void AddItem(ItemData item, int count = 1)
        {
            if (item.category == ItemCategory.SoulSkill)
            {
                var eventArgs = new SoulSkillAcquiredEventArgs(((SoulSkillItem)item).soulSkill);

                SoulSkillItemAcquired?.Invoke(this, eventArgs);
            }
            else if(item.category == ItemCategory.SoulEquipment)
            {
                var soulEquipmentEventArgs = new SoulEquipmentAcquiredEventArgs(((SoulEquipmentItem)item));

                SoulEquipmentAcquired?.Invoke(this, soulEquipmentEventArgs);
            }
            else if(item.category == ItemCategory.Consumable || item.category == ItemCategory.Throwable)
            {
                if(m_quickItemInventory.isInventoryFull == false)
                {
                    m_quickItemInventory.AddItem(item, count);
                }
                else
                {
                    m_inventory.AddItem(item, count);
                }
            }
            else
            {
                m_inventory.AddItem(item, count);
            }
        }

        public void RemoveItem(ItemData item, int count = 1) => m_inventory.RemoveItem(item, count);

        public IStoredItem[] FindStoredItemsOfType(ItemCategory category) => m_inventory.FindStoredItemsOfType(category);

        public IStoredItem GetItem(int index) => m_inventory.GetStoredItem(index);
        public int GetCurrentAmount(ItemData item) => m_inventory.GetItem(item)?.count ?? 0;

        public bool HasSpaceFor(ItemData item) => m_inventory.HasSpaceFor(item, 1);

        public void AddSoulEssence(int value)
        {
            m_inventory.AddCurrency(CurrencyType.SoulEssence,value);
            OnAmountAdded?.Invoke(this, new CurrencyUpdateEventArgs(CurrencyType.SoulEssence,value));
        }

        public void SetSoulEssence(int value)
        {
            m_inventory.SetCurrency(CurrencyType.SoulEssence, value);
            OnAmountSet?.Invoke(this, new CurrencyUpdateEventArgs(CurrencyType.SoulEssence,value));
        }

        #region ITradeInventory Implementation

        void ITradeInventory.AddCurrency(CurrencyType type, int value)
        {
            m_inventory.AddCurrency(type,value);
            OnAmountAdded?.Invoke(this, new CurrencyUpdateEventArgs(type,value));
        }

        ITradeItem[] ITradeInventory.FindTradeItemsOfType(ItemCategory category) => m_inventory.FindTradeItemsOfType(category);

        ITradeItem[] ITradeInventory.GetTradableItems() => m_inventory.GetTradableItems();

        bool ITradeInventory.HasSpaceFor(ItemData item, int count) => m_inventory.HasSpaceFor(item, count);

        public ITradeItem GetTradeItem(ItemData item)
        {
            return m_inventory.GetTradeItem(item);
        }

        public IStoredItem GetItem(ItemData item)
        {
            return m_inventory.GetStoredItem(item);
        }

        public void SwapItems(ItemData itemOne, ItemData itemTwo)
        {
            m_inventory.SwapItems(itemOne, itemTwo);
        }

        public int GetItemIndex(ItemData itemData)
        {
            return m_inventory.GetItemIndex(itemData);
        }

        public void ReplaceItem(ItemData itemData, int count, int index)
        {
            m_inventory.ReplaceItem(itemData, count, index);
        }
        #endregion
    }
}