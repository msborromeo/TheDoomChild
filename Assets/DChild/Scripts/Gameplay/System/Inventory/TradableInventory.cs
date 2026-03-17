using DChild.Gameplay.Items;
using DChild.Gameplay.Trade;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Gameplay.Inventories
{
    [System.Serializable]
    public class TradableInventory : StoredItemList<TradableInventory.Item>, ITradeInventory
    {
        [System.Serializable]
        public class Item : StoredItem, ITradeItem
        {
            private ItemCost m_newCost;
            private bool m_overrideCost;

            public Item(ItemData data, int count) : base(data, count)
            {

            }

            [ShowInInspector, ReadOnly]
            public ItemCost cost
            {
                get
                {
                    if (m_overrideCost)
                    {
                        return m_newCost;
                    }
                    else
                    {
                        return m_data?.cost ?? new ItemCost(0, 0);
                    }
                }
            }

            public void OverrideCost(ItemCost newCost)
            {
                m_overrideCost = true;
                m_newCost = newCost;
            }

            public void RemoveCostOverride()
            {
                m_overrideCost = false;
            }
        }

        [SerializeField, ReadOnly, FoldoutGroup("Restrictions")]
        private bool m_markAllItemsAsTradable;
        [SerializeField, ReadOnly, FoldoutGroup("Restrictions")]
        private bool m_restrictItemsToQuanitityLimit;
        [SerializeField]
        private int soulEssence;

        public TradableInventory(bool markAllItemsAsTradable, bool restrictItemsToQuanitityLimit)
        {
            m_markAllItemsAsTradable = markAllItemsAsTradable;
            m_restrictItemsToQuanitityLimit = restrictItemsToQuanitityLimit;
        }

        public int GetCurrencyAmount(CurrencyType currencyType)
        {
            switch (currencyType)
            {
                case CurrencyType.SoulEssence:
                    return soulEssence;
                case CurrencyType.SilverCoin:
                    return GetStoredItem(GameplaySystem.constantsReference.silverCoinItemData)?.count ?? 0;
            }
            return 0;
        }

        public void AddCurrency(CurrencyType type, int amount)
        {
            switch (type)
            {
                case CurrencyType.SoulEssence:
                    soulEssence += amount;
                    break;
                case CurrencyType.SilverCoin:
                    if (amount >= 0)
                    {
                        AddItem(GameplaySystem.constantsReference.silverCoinItemData, amount);
                    }
                    else
                    {
                        RemoveItem(GameplaySystem.constantsReference.silverCoinItemData, Mathf.Abs(amount));
                    }
                    break;
            }
        }
        public void SetCurrency(CurrencyType type, int amount)
        {
            switch (type)
            {
                case CurrencyType.SoulEssence:
                    soulEssence = amount;
                    break;
                case CurrencyType.SilverCoin:
                    SetItem(GameplaySystem.constantsReference.silverCoinItemData, amount);
                    break;
            }

            soulEssence = amount;
        }

        public Item GetStoredItem(int index)
        {
            if (index >= m_items.Count)
                return null;

            return m_items[index];
        }

        protected override Item CreateNewStoredItem(ItemData itemData, int count)
        {
            return new Item(itemData, count);
        }
        public override void AddItem(ItemData itemData, out Item storedItem, int count = 1)
        {
            base.AddItem(itemData, out storedItem, count);

            //commented out to allow item appending
            //if (m_restrictItemsToQuanitityLimit && storedItem.hasInfiniteCount == false)
            //{
            //    if (storedItem.count > itemData.quantityLimit)
            //    {
            //        storedItem.SetCount(itemData.quantityLimit);
            //    }
            //}
        }

        public override void RemoveItem(ItemData itemData, int count = 1)
        {
            if (TryGetStoredItem(itemData, out Item storedItem))
            {
                if (storedItem.hasInfiniteCount == false)
                {
                    base.RemoveItem(itemData, count);
                }
            }
        }

        public bool HasSpaceFor(ItemData item, int count)
        {
            if (m_restrictItemsToQuanitityLimit)
            {
                int currentItemStoredCount = 0;
                if (TryGetStoredItem(item, out Item storedItem))
                {
                    currentItemStoredCount = storedItem.count;
                }

                return item.quantityLimit >= (count + currentItemStoredCount);
            }
            return true;
        }

        public ITradeItem[] FindTradeItemsOfType(ItemCategory category)
        {
            if (m_markAllItemsAsTradable)
            {
                return m_items.Where((x) => category.HasFlag(x.data.category)).ToArray();
            }
            else
            {
                return m_items.Where((x) => category.HasFlag(x.data.category) && x.data.canBeSold).ToArray();
            }
        }

        public ITradeItem[] GetTradableItems()
        {
            if (m_markAllItemsAsTradable)
            {
                return m_items.ToArray();
            }
            else
            {
                return m_items.Where((x) => x.data.canBeSold).ToArray();
            }
        }

        public ITradeItem GetTradeItem(ItemData item)
        {
            for (int i = 0; i < m_items.Count; i++)
            {
                if (m_items[i].data == item)
                {
                    return m_items[i];
                }
            }

            return null;
        }
    }
}