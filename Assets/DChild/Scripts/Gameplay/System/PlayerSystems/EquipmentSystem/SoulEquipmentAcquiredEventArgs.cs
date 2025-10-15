using Holysoft.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.EquipmentSystem
{
    public struct SoulEquipmentAcquiredEventArgs : IEventActionArgs
    {
        public SoulEquipmentAcquiredEventArgs(SoulEquipmentItem item) : this()
        {
            this.Item = item;
        }

        public SoulEquipmentItem Item { get; }
    }
}

