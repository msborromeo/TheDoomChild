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

        // Keep the shared merchant filter base unchanged; inventory empty filters
        // still need their icon and selected background refreshed.
        public new void UpdateToggleVisuals()
        {
            bool hasItems = HasItemsOfCategory();
            m_targetIcon.sprite = hasItems
                ? (m_toggle.IsOn ? m_hasItemsAndSelected : m_hasItems)
                : m_noItems;
            m_targetBG.sprite = m_toggle.IsOn ? m_selectedBG : m_notSelectedBG;
        }
    }
}
