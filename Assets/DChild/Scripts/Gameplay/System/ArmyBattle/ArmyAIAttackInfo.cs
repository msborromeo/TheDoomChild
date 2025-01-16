using UnityEngine;
using Sirenix.OdinInspector;

namespace DChild.Gameplay.ArmyBattle
{
    [System.Serializable]
    public struct ArmyAIAttackInfo
    {
        [SerializeField]
        private DamageType m_damageType;
        [SerializeField]
        private bool m_useRangedDamageValue;
        [SerializeField,HideIf("m_useRangedDamageValue")]
        private int m_damage;
        [SerializeField,MinMaxSlider(1,999,true),ShowIf("m_useRangedDamageValue")]
        private Vector2 m_rangeDamage;

        public DamageType damageType => m_damageType;
        public int GetDamageValue() => m_useRangedDamageValue ? (int)Random.Range(m_rangeDamage.x, m_rangeDamage.y) : m_damage;
    }


}