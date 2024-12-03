using Sirenix.OdinInspector;
using UnityEngine;
using DChild.Gameplay.Trade;
#if UNITY_EDITOR
#endif
namespace DChild.Gameplay.Items
{
    [System.Serializable]
    public struct ItemCost
    {
        [SerializeField, MinValue(0)]
        private int m_soulEssenceCost;
        [SerializeField, MinValue(0)]
        private int m_silverCoinCost;

        public ItemCost(int soulEssenceCost, int silverCoinCost)
        {
            m_soulEssenceCost = soulEssenceCost;
            m_silverCoinCost = silverCoinCost;
        }

        public int GetCostOfType(CurrencyType currencyType)
        {
            switch (currencyType)
            {
                case CurrencyType.SoulEssence:
                    return m_soulEssenceCost;
                case CurrencyType.SilverCoin:
                    return m_silverCoinCost;
            }
            return 0;
        }
    }
}
