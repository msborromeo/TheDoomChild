using DChild.Gameplay.Items;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace DChild.Menu.Trade
{
    [System.Serializable]
    public class TradeAskingPrice
    {
        [SerializeField, MinValue(0f)]
        private float m_defaultPriceModifier = 100f;
        [SerializeField, InlineEditor]
        private TradeAskingPriceData m_priceModifierData;

        public ItemCost GetAskingPrice(ItemData data)
        {
            var modifiedPrice = new ItemCost(-1, -1);
            if (m_priceModifierData != null)
            {
                if (m_priceModifierData.TryGetPriceModifier(data, out ItemCost value))
                {
                    modifiedPrice = value;
                }
            }

            var soulEssenceType = Gameplay.Trade.CurrencyType.SoulEssence;
            var soulEssencePrice = modifiedPrice.GetCostOfType(soulEssenceType) < 0 ? data.cost.GetCostOfType(soulEssenceType) : modifiedPrice.GetCostOfType(soulEssenceType);
            var silverCoinType = Gameplay.Trade.CurrencyType.SilverCoin;
            var silverCointPrice = modifiedPrice.GetCostOfType(silverCoinType) < 0 ? data.cost.GetCostOfType(silverCoinType) : modifiedPrice.GetCostOfType(silverCoinType);

            return new ItemCost(soulEssencePrice, silverCointPrice);
        }
    }
}