using DChild.Gameplay.Items;

namespace DChild.Gameplay.Inventories
{
    public interface ITradeItem : IStoredItem
    {
        ItemCost cost { get; }
        void OverrideCost(ItemCost newCost);
        void RemoveCostOverride();
    }
}