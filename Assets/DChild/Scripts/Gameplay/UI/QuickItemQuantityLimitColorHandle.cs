using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DChild.Gameplay.Inventories.UI
{
    public class QuickItemQuantityLimitColorHandle : QuantityLimitColorHandle
    {
        [SerializeField]
        private QuickItemHandle m_quickItemHandle;

        public override IStoredItem currentItem => m_quickItemHandle.currentItem;

        private void OnEnable()
        {
            m_quickItemHandle.CurrentItemChanged += OnCurrentItemChanged;
            m_quickItemHandle.CurrentItemConsumed += OnCurrentItemConsumed;
        }

        private void OnDisable()
        {
            m_quickItemHandle.CurrentItemChanged -= OnCurrentItemChanged;
            m_quickItemHandle.CurrentItemConsumed -= OnCurrentItemConsumed;
        }

        private void OnCurrentItemConsumed()
        {
            UpdateQuantityTextColor();
        }

        private void OnCurrentItemChanged()
        {
            UpdateQuantityTextColor();
        }
    }
}


