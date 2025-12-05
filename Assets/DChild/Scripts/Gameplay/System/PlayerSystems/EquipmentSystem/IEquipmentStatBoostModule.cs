using DChild.Gameplay.Characters.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.EquipmentSystem
{
    public interface IEquipmentStatBoostModule
    {
        StatBoostType GetBoostType();
        float GetModifierValue();

        void AttachTo(IPlayer player);
        void DetachFrom(IPlayer player);
    }
}

