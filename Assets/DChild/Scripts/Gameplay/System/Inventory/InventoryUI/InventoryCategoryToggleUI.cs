using DChild.Gameplay.Trade;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.Inventories.UI
{
    public class InventoryCategoryToggleUI : InventoryFilterToggleUI
    {
        [SerializeField] private GridInventoryListUI m_attachedInventory;


        public override void SelectFilter()
        {
            m_attachedInventory.SetFilter(m_category);
            base.SelectFilter();
        }

        public override bool HasItemsOfCategory()
        {
            var categorizedInventory = m_attachedInventory.inventory.FindStoredItemsOfType(m_category);
            return categorizedInventory.Length > 0;
        }
    }
}