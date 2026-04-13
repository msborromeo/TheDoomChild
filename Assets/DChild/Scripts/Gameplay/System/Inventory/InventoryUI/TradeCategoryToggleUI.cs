using DChild.Gameplay.Trade;
using DChild.Gameplay.Trade.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.Inventories.UI
{
    public class TradeCategoryToggleUI : InventoryFilterToggleUI
    {
        [SerializeField] private GridTradeInventoryListUI m_attachedInventory;

        public override void SelectFilter()
        {
            m_attachedInventory.SetPage(0);
            m_attachedInventory.SetFilter(m_category);
            base.SelectFilter();
        }

        public override bool HasItemsOfCategory()
        {
            var categorizedInventory = m_attachedInventory.inventory.FindTradeItemsOfType(m_category);
            return categorizedInventory.Length > 0;
        }
    }
}