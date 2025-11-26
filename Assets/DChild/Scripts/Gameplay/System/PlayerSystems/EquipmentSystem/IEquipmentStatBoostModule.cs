using DChild.Gameplay.Characters.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.EquipmentSystem
{
    public interface IEquipmentStatBoostModule
    {
        StatBoostType GetBoostType();

        void AttachTo(IPlayer player);
        void DetachFrom(IPlayer player);
    }
}

