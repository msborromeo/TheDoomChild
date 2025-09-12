using Holysoft.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.SoulSkills
{
    public struct SoulCharacterAcquiredEventArgs : IEventActionArgs
    {
        public SoulCharacterAcquiredEventArgs(SoulCharacter item) : this()
        {
            this.Item = item;
        }

        public SoulCharacter Item { get; }
        public int ID => Item.id;
    }
}

