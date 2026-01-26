using System;

namespace DChild.Gameplay.Items
{
    [Flags]
    public enum ItemCategory
    {
        Throwable = 1 << 0,
        Consumable = 1 << 1,
        Quest = 1 << 2,
        Key = 1 << 3,
        SoulSkill = 1 << 4,
        SoulEquipment = 1 << 5,
        SoulCharacter = 1 << 6,

        QuickItem = Throwable | Consumable,
        All = Throwable | Consumable | Quest| Key| SoulSkill,
        SoulEssence = 1<< 7,
        PlayerSkin = 1<< 8,
    }
}
