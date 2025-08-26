using System;
using DChild.Gameplay.Inventories;
using DChild.Gameplay.Items;

namespace DChild.Localization
{
    public interface IItemViewLocalizer
    {
        event Action<ItemData> LocalizeItemView;
    }
}