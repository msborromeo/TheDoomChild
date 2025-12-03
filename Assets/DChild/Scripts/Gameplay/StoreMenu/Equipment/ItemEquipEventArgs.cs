using DChild.Gameplay.EquipmentSystem;
using Holysoft.Event;

namespace DChild.Menu.Equipment.UI
{
    public class ItemEquipEventArgs : IEventActionArgs
    {
        private SoulEquipmentItem m_equipmentItem;
        public SoulEquipmentItem equipmentItem => m_equipmentItem;

        public ItemEquipEventArgs(SoulEquipmentItem item)
        {
            this.m_equipmentItem = item;
        }
    }
}
