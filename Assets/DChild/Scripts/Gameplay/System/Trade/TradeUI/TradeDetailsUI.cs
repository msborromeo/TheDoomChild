using DChild.Gameplay.Inventories;
using DChild.Gameplay.Inventories.UI;
using System;
using TMPro;
using UnityEngine;

namespace DChild.Gameplay.Trade.UI
{
    public class TradeDetailsUI : FullItemDetailsUI
    {
        [SerializeField]
        private TextMeshProUGUI m_costTypeLabel;
        [SerializeField]
        private TextMeshProUGUI m_costLabel;
        [SerializeField]
        private TextMeshProUGUI m_countLabel;

        private CurrencyType m_costType;

        public void SetCostTypeToDisplay(CurrencyType costType)
        {
            m_costType = costType;
            switch (m_costType)
            {
                case CurrencyType.SoulEssence:
                    m_costTypeLabel.text = "S.E./";
                    break;
                case CurrencyType.SilverCoin:
                    m_costTypeLabel.text = "S.C./";
                    break;
            }
        }

        public override void Hide()
        {
        }

        public override void Show()
        {
        }

        public override void ShowDetails(IStoredItem reference)
        {
            base.ShowDetails(reference);
            if(reference == null)
            {
                m_costLabel.text = "";
                m_countLabel.text = "";
            }
            else
            {
                m_costLabel.text = ((ITradeItem)reference).cost.GetCostOfType(m_costType).ToString();
                m_countLabel.text = reference.hasInfiniteCount? "99" : reference.count.ToString();
            }
        }
    }
}
