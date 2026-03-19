using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace DChild.Gameplay.Inventories.UI
{
    public class InventoryItemActionHandle : MonoBehaviour
    {
        [SerializeField] private UIButton m_swapButton;
        [SerializeField] private UIButton m_removeItemButton;

        public void ShowButtonActions(InventoryItemUI inventoryitemUI)
        {
            if (inventoryitemUI.reference == null)
            {
                Reset();
                return;
            }

            var itemCategory = inventoryitemUI.reference.data.category;

            var isSwappable = itemCategory == Items.ItemCategory.Consumable
                || itemCategory == Items.ItemCategory.Throwable
                || itemCategory == Items.ItemCategory.Key
                || itemCategory == Items.ItemCategory.Quest;

            m_swapButton.gameObject.SetActive(isSwappable);
            m_removeItemButton.gameObject.SetActive(inventoryitemUI.isQuickItem);
        }

        private void Reset()
        {
            m_swapButton.gameObject.SetActive(false);
            m_removeItemButton.gameObject.SetActive(false);
        }
    }
}