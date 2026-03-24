using UnityEngine;

namespace DChild.Gameplay.Inventories.UI
{
    public class RemoveQuickItemButtonUI : MonoBehaviour
    {
        [SerializeField] private InventoryUISwapHandle m_swapHandle;

        public void RemoveItem()
        {
            m_swapHandle.MoveQuickItemToInventory(m_swapHandle.itemOne);
        }
    }
}
