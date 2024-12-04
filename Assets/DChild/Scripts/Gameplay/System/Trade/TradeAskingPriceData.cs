using DChild.Gameplay.Items;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DChild.Menu.Trade
{
    [CreateAssetMenu(fileName = "TradeAskingPriceData", menuName = "DChild/Gameplay/Trade/Trade Asking Price Data")]
    public class TradeAskingPriceData : SerializedScriptableObject
    {
        [OdinSerialize, HideReferenceObjectPicker, OnValueChanged("UpdatePrice", true)]
        private Dictionary<ItemData, int> m_priceModifier = new Dictionary<ItemData, int>();

        [OdinSerialize, HideReferenceObjectPicker, OnValueChanged("UpdatePrice", true)]
        private Dictionary<ItemData, ItemCost> m_newPriceModifier = new Dictionary<ItemData, ItemCost>();

        public bool TryGetPriceModifier(ItemData data, out int value)
        {
            return m_priceModifier.TryGetValue(data, out value);
        }

        public bool TryGetPriceModifierNew(ItemData data, out ItemCost value)
        {
            return m_newPriceModifier.TryGetValue(data, out value);
        }

#if UNITY_EDITOR
        [SerializeField, PropertyOrder(-1)]
        private ItemList m_reference;

        [SerializeField, ReadOnly, HideInInlineEditors, PropertySpace(SpaceBefore = 20)]
        private Dictionary<ItemData, int> m_price;

        private void UpdatePrice()
        {
            if (m_price == null)
            {
                m_price = new Dictionary<ItemData, int>();
            }

            m_price.Clear();
            foreach (var item in m_priceModifier.Keys)
            {
                if (m_priceModifier[item] < 0)
                {
                    //Use Original Price
                    m_price.Add(item, item.cost.GetCostOfType(Gameplay.Trade.CurrencyType.SoulEssence));
                }
                else
                {
                    m_price.Add(item, m_priceModifier[item]);
                }

            }
        }

        [Button, PropertyOrder(-1)]
        private void AddItemsToList()
        {
            var ids = m_reference.GetIDs();
            for (int i = 0; i < ids.Length; i++)
            {
                var item = m_reference.GetInfo(ids[i]);
                if (m_priceModifier.ContainsKey(item) == false)
                {
                    m_priceModifier.Add(item, -1);
                    EditorUtility.SetDirty(this);
                }
            }
            UpdatePrice();
        }


        [SerializeField, ReadOnly, HideInInlineEditors, PropertySpace(SpaceBefore = 20)]
        private Dictionary<ItemData, ItemCost> m_newPrice;

        private void UpdateNewPrice()
        {
            if (m_newPrice == null)
            {
                m_newPrice = new Dictionary<ItemData, ItemCost>();
            }

            m_newPrice.Clear();
            foreach (var item in m_priceModifier.Keys)
            {
                var modifiedPrice = m_newPriceModifier[item];
                var soulEssenceType = Gameplay.Trade.CurrencyType.SoulEssence;
                var soulEssencePrice = modifiedPrice.GetCostOfType(soulEssenceType) < 0 ? item.cost.GetCostOfType(soulEssenceType) : modifiedPrice.GetCostOfType(soulEssenceType);
                var silverCoinType = Gameplay.Trade.CurrencyType.SilverCoin;
                var silverCointPrice = modifiedPrice.GetCostOfType(silverCoinType) < 0 ? item.cost.GetCostOfType(silverCoinType) : modifiedPrice.GetCostOfType(silverCoinType);

                m_newPrice.Add(item, new ItemCost(soulEssencePrice,silverCointPrice));
            }
        }

        [Button, PropertyOrder(-1)]
        private void AddItemsToListNew()
        {
            var ids = m_reference.GetIDs();
            for (int i = 0; i < ids.Length; i++)
            {
                var item = m_reference.GetInfo(ids[i]);
                if (m_newPriceModifier.ContainsKey(item) == false)
                {
                    m_newPriceModifier.Add(item, new ItemCost(-1,-1));
                    EditorUtility.SetDirty(this);
                }
            }
            UpdatePrice();
        }

        [Button]
        private void TransistionToNewStructure()
        {
            foreach (var item in m_priceModifier.Keys)
            {
                m_newPriceModifier.Add(item, new ItemCost(m_priceModifier[item], 0));
            }
        }
#endif
    }
}