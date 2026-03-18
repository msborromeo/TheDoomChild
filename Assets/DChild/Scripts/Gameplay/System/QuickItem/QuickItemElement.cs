using DChild.Gameplay.Inventories.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DChild.Localization;
using DChild.Gameplay.Items;

namespace DChild.Gameplay.Inventories
{
    public class QuickItemElement : ItemDetailsUI, IItemViewLocalizer
    {
        [SerializeField]
        private Image m_icon;
        [SerializeField]
        private TextMeshProUGUI m_name;
        [SerializeField]
        private TextMeshProUGUI m_count;

        private IStoredItem m_currentSlot;

        public event Action<ItemData> LocalizeItemView;

        public override void AdjustIconColor(bool isModified)
        {
            throw new NotImplementedException();
        }

        public override void Hide()
        {
        }

        public override void Show()
        {
        }

        public override void ShowDetails(IStoredItem reference)
        {
            if (m_currentSlot != null)
            {
                //if (m_count)
                //{
                //    m_currentSlot.CountChange -= OnCountChange;
                //}
            }

            if (reference == null)
            {
                m_icon.sprite = null;
                m_icon.color = new Color(1, 1, 1, 0);
                if (m_name)
                {
                    m_name.text = "";
                }
                if (m_count)
                {
                    m_count.text = "";
                }
            }
            else
            {
                m_icon.sprite = reference.data.icon;
                m_icon.color = Color.white;
                if (m_name)
                {
                    m_name.text = reference.data.itemName;
                }
                if (m_count)
                {
                    if (reference.hasInfiniteCount)
                    {
                        m_count.text = "\u221E"; //Infinity Symbol
                    }
                    else
                    {
                        m_count.text = reference.count.ToString();
                        m_currentSlot = reference;
                        //m_currentSlot.reference += OnCountChange;
                    }
                }
                LocalizeItemView?.Invoke(reference.data);
            }

        }

        //private void OnCountChange(object sender, ItemSlot.InfoChangeEventArgs eventArgs)
        //{
        //    m_count.text = eventArgs.count.ToString();

        //}
    }
}
