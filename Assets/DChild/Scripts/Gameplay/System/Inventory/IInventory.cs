using DChild.Gameplay.Items;
using Holysoft.Event;
using System.Collections.Generic;

namespace DChild.Gameplay.Inventories
{
    public interface IInventory : IInventoryInfo
    {
        event EventAction<ItemEventArgs> InventoryItemUpdate;
        event EventAction<EventActionArgs> MassInventoryItemUpdate;

        void AddItem(ItemData itemData, int count = 1);
        void ForceAddItem(ItemData itemData, int count = 1);
        void RemoveItem(ItemData itemData, int count = 1);
        void SetItem(ItemData itemData, int count = 1);

        void SwapItems(ItemData itemOne, ItemData itemTwo);

        int GetItemIndex(ItemData itemData);

        void ReplaceItem(ItemData itemData, int count, int index);

        IStoredItem[] FindStoredItemsOfType(ItemCategory category);
    }
}