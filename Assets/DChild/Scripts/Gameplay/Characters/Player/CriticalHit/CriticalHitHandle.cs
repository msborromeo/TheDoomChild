using DChild.Gameplay.Characters.Players.Modules;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Combat
{
    public class CriticalHitHandle : SerializedMonoBehaviour, ICriticalHitHandle
    {
        [OdinSerialize]
        private List<AttackBehaviour> attackers = new List<AttackBehaviour>();

        public List<AttackBehaviour> attackerList => attackers;

        public void ModifyCritChance(float critChance)
        {
            for(int i = 0; i < attackers.Count; i++)
            {
                attackers[i].IncreaseCritChance(critChance);
            }
        }

        public void ModifyCritDamage(float critDamage)
        {
            for (int i = 0; i < attackers.Count; i++)
            {
                attackers[i].IncreaseCritDamage(critDamage);
            }
        }
    }
}

