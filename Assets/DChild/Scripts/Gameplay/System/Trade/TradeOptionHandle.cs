using Doozy.Runtime.UIManager.Components;
using Holysoft.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;

namespace DChild.Menu.Trade
{
    public class TradeOptionHandle : MonoBehaviour
    {
        private TradeType m_tradeType;
        [SerializeField]
        private UIButton m_tradeButton;
        [SerializeField]
        private TextMeshProUGUI m_tradeButtonLabel;
        [SerializeField]
        private Localize m_tradeButtonLabelLocalize;

        public TradeType tradeType => m_tradeType;

        public void SetInteractability(bool interactability)
        {
            m_tradeButton.interactable = interactability;
        }

        public void ChangeToBuyOption(bool instant)
        {
            m_tradeType = TradeType.Buy;
            m_tradeButtonLabel.text = "Buy";
            m_tradeButtonLabelLocalize?.SetTerm("ShopUI/ItemInfo/Buy");
        }
        public void ChangeToSellOption(bool instant)
        {
            m_tradeType = TradeType.Sell;
            m_tradeButtonLabel.text = "Sell";
            m_tradeButtonLabelLocalize?.SetTerm("ShopUI/ItemInfo/Sell");
        }
    }
}