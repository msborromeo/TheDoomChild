using DChild.Gameplay.Characters.Players;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.EquipmentSystem
{
    public class CritChanceModifier : IEquipmentStatBoostModule
    {
        [SerializeField, SuffixLabel("%", Overlay = true)]
        private float m_critChanceValue;

        public void AttachTo(IPlayer player)
        {
            
        }

        public void DetachFrom(IPlayer player)
        {
            
        }
    }
}

