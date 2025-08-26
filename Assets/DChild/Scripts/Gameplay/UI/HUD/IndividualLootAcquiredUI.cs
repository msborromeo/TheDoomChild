using System;
using DChild.Gameplay.Items;
using DChild.Localization;
using TMPro;
using UnityEngine;

namespace DChild.Gameplay.UI
{

    public class IndividualLootAcquiredUI : MonoBehaviour , IItemViewLocalizer
    {
        [SerializeField]
        private TextMeshProUGUI m_nameLabel;
        [SerializeField]
        private TextMeshProUGUI m_countLabel;

        private Canvas m_canvas;

        public event Action<ItemData> LocalizeItemView;

        //Assumin Soul Essence is the only loot without data
        public void SetDetails(ItemData item, int count)
        {
            if (item == null)
            {
                m_nameLabel.text = "Soul Essence";
            }
            else
            {
                m_nameLabel.text = item.itemName;

            }
            m_countLabel.text = $"+ {count}";
            LocalizeItemView?.Invoke(item);
        }

        public void Show()
        {
            m_canvas.enabled = true;
        }

        public void Hide()
        {
            m_canvas.enabled = false;
        }

        private void Awake()
        {
            m_canvas = GetComponent<Canvas>();
        }
    }
}