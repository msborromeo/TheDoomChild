using Holysoft.Event;

namespace DChild.Gameplay.Inventories.UI
{
    public class InventorySwapEventArgs : IEventActionArgs
    {
        private ItemUI m_itemOne;
        public ItemUI itemOne => m_itemOne;

        private ItemUI m_itemTwo;
        public ItemUI itemTwo => m_itemTwo;
        public InventorySwapEventArgs(ItemUI firstItemUI)
        {
            m_itemOne = firstItemUI;
        }
        public InventorySwapEventArgs(ItemUI itemOne, ItemUI itemTwo)
        {
            m_itemOne = itemOne;
            m_itemTwo = itemTwo;
        }
    }
}
