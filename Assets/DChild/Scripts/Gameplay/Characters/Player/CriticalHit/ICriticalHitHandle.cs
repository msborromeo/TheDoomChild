using DChild.Gameplay.Characters.Players.Modules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Combat
{
    public interface ICriticalHitHandle
    {
        List<AttackBehaviour> attackerList { get; }

        void ModifyCritChance(float critChance);
        void ModifyCritDamage(float critDamage);
    }
}

