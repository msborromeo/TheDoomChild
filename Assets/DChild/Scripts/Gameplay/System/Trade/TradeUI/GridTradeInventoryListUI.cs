using DChild.Gameplay.Inventories;
using DChild.Gameplay.Inventories.UI;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace DChild.Gameplay.Trade.UI
{
    public class GridTradeInventoryListUI : FilteredInventoryListUI<ITradeInventory>
    {
        [SerializeField, MinValue(1), PropertyOrder(-1)]
        private int m_page;
        private int m_startIndex = 0;
        private int m_availableSlot;

        [SerializeField] private UIScrollbar m_gridScroll;
        private int m_currentPageIndex;
        private int m_totalSections;

        private ITradeItem[] m_filteredItemList;

        #region Scrollbar Methods
        [Button]
        public override void SetupScrollUI()
        {
            SetupScroll(m_filteredItemList);
        }
        public void SetupScroll(ITradeItem[] tradeItems, int toggleCount = 24)
        {
            m_currentPageIndex = -1;
            m_totalSections = Mathf.CeilToInt(tradeItems.Length / (float)toggleCount);

            m_gridScroll.gameObject.SetActive(m_totalSections > 1);
            m_gridScroll.numberOfSteps = m_totalSections;
            m_gridScroll.size = 1f / m_totalSections;

        }
        public void HandleScroll()
        {
            int updatedPage = Mathf.RoundToInt(m_gridScroll.value * (m_totalSections - 1));

            if (m_currentPageIndex != updatedPage)
            {
                m_currentPageIndex = updatedPage;
                SetPage(m_currentPageIndex);
                UpdateUIList();
            }
        }

        public void SetPage(int pageIndex)
        {
            m_page = pageIndex;
            m_startIndex = pageIndex * itemUICount;

            m_availableSlot = itemUICount;
        }
        #endregion

        #region UpdateUIList Overloading
        [Button, HideInEditorMode, PropertyOrder(-1)]
        public override void UpdateUIList()
        {
            int i = 0;

            if (m_currentFilter == Items.ItemCategory.All)
                m_filteredItemList = m_inventory.GetTradableItems();

            else
                m_filteredItemList = m_inventory.FindTradeItemsOfType(m_currentFilter);

            SetupScrollUI();
            UpdateUIList(ref i, m_filteredItemList);

            for (; i < itemUICount; i++)
            {
                //m_itemUIs[i].Hide();
                m_itemUIs[i].gameObject.SetActive(false);
            }

            InvokeListOverallChange();
        }

        private void UpdateUIList(ref int i, ITradeItem[] tradableItems)
        {
            for (int slotIndex = 0; slotIndex < itemUICount; slotIndex++)
            {
                int itemDataIndex = m_startIndex + slotIndex;

                if (itemDataIndex >= tradableItems.Length)
                {
                    break;
                }

                var storedItem = tradableItems[itemDataIndex];
                if (storedItem != null)
                {
                    var itemUI = m_itemUIs[slotIndex];
                    itemUI.gameObject.SetActive(true);
                    //itemUI.Show();
                    itemUI.SetReference(storedItem);

                    i = slotIndex + 1;
                }
            }
        }
        #endregion
        public override void SwapItems(ItemUI itemOne, ItemUI itemTwo)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateUIList(bool v)
        {
            throw new System.NotImplementedException();
        }

        public override void Reset()
        {
            SetPage(0);
            m_filteredItemList = null;
        }

     
    }

}