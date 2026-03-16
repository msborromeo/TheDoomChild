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
            var itemCategory = inventoryitemUI.reference.data.category;

            m_swapButton.gameObject.SetActive(itemCategory == Items.ItemCategory.Consumable || itemCategory == Items.ItemCategory.Throwable);
            m_removeItemButton.gameObject.SetActive(inventoryitemUI.isQuickItem);
        }
    }
}