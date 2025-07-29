using DChild.Gameplay.Combat;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Combat
{
    [System.Serializable]
    public struct AttackDamageInfo
    {
        public AttackDamageInfo(Damage damage, Invulnerability ignoreInvulnerability = Invulnerability.None, bool ignoresBlock = false, bool isCrit = false)
        {
            this.damage = damage;
            this.criticalDamageInfo = new CriticalDamageInfo(0, 1);
            this.ignoreInvulnerability = ignoreInvulnerability;
            this.ignoresBlock = ignoresBlock;
            this.isCrit = isCrit;
        }

        public AttackDamageInfo(Damage damage, CriticalDamageInfo criticalDamageInfo, Invulnerability ignoreInvulnerability = Invulnerability.None, bool ignoresBlock = false, bool isCrit = false)
        {
            this.damage = damage;
            this.criticalDamageInfo = criticalDamageInfo;
            this.ignoreInvulnerability = ignoreInvulnerability;
            this.ignoresBlock = ignoresBlock;
            this.isCrit = isCrit;
        }

        [HideLabel]
        public Damage damage;
        [HideLabel,BoxGroup("Critical Damage Configuration")]
        public CriticalDamageInfo criticalDamageInfo;
        public Invulnerability ignoreInvulnerability;
        public bool ignoresBlock;
        public bool isCrit;
    }
}