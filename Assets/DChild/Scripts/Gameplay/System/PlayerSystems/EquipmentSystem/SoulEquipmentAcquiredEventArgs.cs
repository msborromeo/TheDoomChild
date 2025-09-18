using Holysoft.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.SoulEquipment
{
    public struct SoulEquipmentAcquiredEventArgs : IEventActionArgs
    {
        public SoulEquipmentAcquiredEventArgs(SoulEquipment item) : this()
        {
            this.Item = item;
        }

        public SoulEquipment Item { get; }
    }
}

