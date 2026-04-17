using DChild.Gameplay.Characters.NPC;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.Trade
{
    public class TradeUIAppearanceInitializer : MonoBehaviour
    {
        [SerializeField] private Image m_shopBackground;
        [SerializeField] private Image m_frontPanel;
        [SerializeField] private Image m_categoryBar;
        [SerializeField] private Image m_tradeActionsPanel;
        [SerializeField] private Image m_bottomPanel;

        public void SetShopAppearance(NPCProfile merchant)
        {
            if (merchant == null) return;

            m_shopBackground.sprite = merchant.shopBackground;
            m_frontPanel.sprite = merchant.frontPanel;
            m_categoryBar.sprite = merchant.categoryBar;
            m_tradeActionsPanel.sprite = merchant.tradeActionsPanel;
            m_bottomPanel.sprite = merchant.bottomPanel;
        }
    }
}