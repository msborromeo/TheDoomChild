using System;
using UnityEngine;

namespace DChild.Gameplay.Systems
{
    public enum StorePage
    {
        Map,
        Player,
        Items,
        Equipment,
        SoulSkills,
        CombatArts,
        Codex,
        Bestiary,
        [HideInInspector]
        _Count
    }
}