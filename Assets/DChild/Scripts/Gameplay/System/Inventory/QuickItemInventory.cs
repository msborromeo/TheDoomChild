using DChild.Gameplay.Items;
using DChild.Gameplay.Systems;
using DChild.Gameplay.Trade;
using Holysoft.Collections;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DChild.Serialization.AcquisitionData;

namespace DChild.Gameplay.Inventories
{
    public class QuickItemInventory : MonoBehaviour, IInventory, ISerializable<TradableInventorySerialization>
    {
        [SerializeField]
        private ItemList m_referenceList;
        [SerializeField, HideLabel, FoldoutGroup("Inventory")]
        private TradableInventory m_quickItemInventory = new TradableInventory(false, true);
        [SerializeField]
        private int m_maxItems = 7;
        [SerializeField]
        private bool m_isInventoryFull = false;

        public bool isInventoryFull => m_isInventoryFull;

        public int storedItemCount => m_quickItemInventory.storedItemCount;

        public event EventAction<ItemEventArgs> InventoryItemUpdate
        {
            add
            {
                m_quickItemInventory.InventoryItemUpdate += value;
            }
            remove
            {
                m_quickItemInventory.InventoryItemUpdate -= value;
            }
        }
        public event EventAction<EventActionArgs> MassInventoryItemUpdate
        {
            add
            {
                m_quickItemInventory.MassInventoryItemUpdate += value;
            }
            remove
            {
                m_quickItemInventory.MassInventoryItemUpdate -= value;
            }
        }

        public void AddItem(ItemData itemData, int count = 1)
        {
            if (m_isInventoryFull == true)
                return;

            m_quickItemInventory.AddItem(itemData, count);
            if (m_quickItemInventory.storedItemCount >= m_maxItems)
                m_isInventoryFull = true;
        }

        public IStoredItem[] FindStoredItemsOfType(ItemCategory category)
        {
            return m_quickItemInventory.FindStoredItemsOfType(category);
        }

        public IStoredItem GetItem(int index)
        {
            return m_quickItemInventory.GetStoredItem(index);
        }

        public IStoredItem GetItem(ItemData item)
        {
            return m_quickItemInventory.GetStoredItem(item);
        }

        public int GetItemIndex(ItemData itemData)
        {          
            return m_quickItemInventory.GetItemIndex(itemData);
        }

        public void LoadData(TradableInventorySerialization data)
        {
            m_quickItemInventory.ClearList();
            
            if(data == null)
            {
                m_quickItemInventory.InvokeMassInventoryItemUpdate();
                return;
            }

            TradableInventory.Item inventoryItem = null;
            for (int i = 0; i < data.count; i++)
            {
                var serializedItem = data.GetSerializedItem(i);
                var itemData = m_referenceList.GetInfo(serializedItem.id);
                m_quickItemInventory.AddItem(itemData, out inventoryItem, serializedItem.count);
                inventoryItem.SetCountToInfinite(serializedItem.isInfinite);
            }
            m_quickItemInventory.InvokeMassInventoryItemUpdate();

        }

        public void RemoveItem(ItemData itemData, int count = 1)
        {
            m_quickItemInventory.RemoveItem(itemData, count);
            if (m_quickItemInventory.storedItemCount < 7)
                m_isInventoryFull = false;
        }

        public void ReplaceItem(ItemData itemData, int count, int index)
        {
            m_quickItemInventory.ReplaceItem(itemData, count, index);
        }

        public TradableInventorySerialization SaveData()
        {
            return new TradableInventorySerialization(m_quickItemInventory);
        }

        public void SetItem(ItemData itemData, int count = 1)
        {
            m_quickItemInventory.SetItem(itemData, count);
        }

        public void SwapItems(ItemData itemOne, ItemData itemTwo)
        {
            m_quickItemInventory.SwapItems(itemOne, itemTwo);
        }
    }
}

