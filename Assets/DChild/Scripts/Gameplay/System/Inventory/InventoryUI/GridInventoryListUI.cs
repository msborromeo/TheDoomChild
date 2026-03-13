using DChild.Gameplay.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Gameplay.Inventories.UI
{
    public class GridInventoryListUI : FilteredInventoryListUI<IInventory>
    {
        [SerializeField, MinValue(1), PropertyOrder(-1)]
        private int m_page;
        private int m_startIndex;
        private int m_availableSlot;

        public void SetPage(int pageNumber)
        {
            m_page = pageNumber;
            m_startIndex = (pageNumber - 1) * itemUICount;
            m_availableSlot = itemUICount - 1;
        }

        public override void SwapItems(ItemUI itemOne, ItemUI itemTwo)
        {
            if (itemOne == null || itemTwo == null) return;

            m_inventory.SwapItems(itemOne.reference.data, itemTwo.reference.data);
        }


        [Button, HideInEditorMode, PropertyOrder(-1)]
        public override void UpdateUIList()
        {
            int i = 0;
            UpdateUIList(ref i, m_inventory.FindStoredItemsOfType(m_currentFilter));

            for (; i < itemUICount; i++)
            {
                m_itemUIs[i].Hide();
            }
            InvokeListOverallChange();
        }

        private void UpdateUIList(ref int i, IStoredItem[] items)
        {
            for (; i <= m_availableSlot; i++)
            {
                var itemIndex = m_startIndex + i;
                if (itemIndex >= items.Length)
                    break;
                var storedItem = items[itemIndex];
                if (storedItem != null)
                {
                    var itemUI = m_itemUIs[i];
                    itemUI.Show();
                    itemUI.SetReference(storedItem);
                }
            }
        }

        public override void Reset()
        {
            SetPage(1);
        }
    }
}