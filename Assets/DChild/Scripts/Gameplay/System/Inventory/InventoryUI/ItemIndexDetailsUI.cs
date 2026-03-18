using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.Inventories.UI
{
    public class ItemIndexDetailsUI : ItemDetailsUI
    {
        [SerializeField]
        private Image m_icon;
        [SerializeField]
        private TextMeshProUGUI m_countText;

        [SerializeField] private CanvasGroup m_emptyIcon;
        [SerializeField] private CanvasGroup m_itemQuantityCG;

        private Canvas m_canvas;

        public override void ShowDetails(IStoredItem reference)
        {
            bool hasData = reference != null;
            if (m_emptyIcon != null) m_emptyIcon.alpha = hasData ? 0f : 1f;

            m_icon.gameObject.SetActive(hasData);

            if (hasData)
            {
                m_icon.sprite = reference.data.icon;
                //if (reference.data.name != "Health Shard")
                m_countText.text = reference.count.ToString();

                if (m_itemQuantityCG != null)
                    m_itemQuantityCG.alpha = hasData && reference.count > 1 ? 1f : 0f;
            }
        }

        public override void Show()
        {
            m_canvas.enabled = true;
        }

        public override void Hide()
        {
            m_canvas.enabled = false;
        }

        private void Awake()
        {
            m_canvas = GetComponent<Canvas>();
        }

        public override void AdjustIconColor(bool isModified)
        {
            if (isModified)
            {
                m_icon.color = new Color(0.6f, 0.6f, 0.6f, 1f);
                return;
            }
            m_icon.color = Color.white;
        }
    }
}