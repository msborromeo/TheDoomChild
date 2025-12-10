using DChild.Gameplay.EquipmentSystem;
using Holysoft.Event;
using NSubstitute.Exceptions;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentGridItemUI : MonoBehaviour
    {
        [SerializeField] private EquipmentSelectionUI m_selectionUI;
        [SerializeField] private Image m_itemIcon;

        private SoulEquipmentItem m_attachedItem;
        public SoulEquipmentItem attachedItem => m_attachedItem;
        public event EventAction<EventActionArgs> OnGridItemSelected;

        public void Display(SoulEquipmentItem item = null)
        {
            m_attachedItem = item;

            bool hasItem = m_attachedItem != null;
            gameObject.SetActive(hasItem);

            if (!hasItem)
                return;

            m_itemIcon.sprite = m_attachedItem.icon;
        }

        public void PrepareAttachedItem()
        {
            if (m_attachedItem == null)
                return;
            m_selectionUI.equipButtonUI.SetSelectedItem(m_attachedItem);
            m_selectionUI.SetItemDetails(m_attachedItem.soulEquipment);
            OnGridItemSelected?.Invoke(this, EventActionArgs.Empty);
        }
    }

}