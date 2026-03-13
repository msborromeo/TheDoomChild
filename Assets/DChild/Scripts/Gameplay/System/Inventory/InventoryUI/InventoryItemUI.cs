using Doozy.Runtime.UIManager.Components;
using Holysoft.Event;
using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.Inventories.UI
{
    public class InventoryItemUI : ItemUI
    {
        private UIToggle m_toggle;

        [SerializeField] private bool m_isQuickItem;
        public bool isQuickItem => m_isQuickItem;

        public override void Hide()
        {
            m_toggle.SetIsOn(false);
            m_toggle.interactable = false;
        }

        public override void Show()
        {
            m_toggle.interactable = true;
        }

        protected override void ShowDetailsOf(IStoredItem reference)
        {
            if (reference == null || reference.data.category == Items.ItemCategory.SoulEssence)
            {
                Hide();
                return;
            }

            Show();
            base.ShowDetailsOf(reference);
        }

        private void OnEnable()
        {
            m_toggle = GetComponent<UIToggle>();
        }

    }
}