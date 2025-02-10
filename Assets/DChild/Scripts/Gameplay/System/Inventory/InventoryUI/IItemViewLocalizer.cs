using System;
using DChild.Gameplay.Inventories;

namespace DChild.Localization
{
    public interface IItemViewLocalizer
    {
        event Action<IStoredItem> localizeItemView;
    }
}