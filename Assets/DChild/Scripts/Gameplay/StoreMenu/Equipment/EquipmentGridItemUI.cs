using DChild.Gameplay.EquipmentSystem;
using Holysoft.Event;
using NSubstitute.Exceptions;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentGridItemUI : MonoBehaviour
    {
        [SerializeField] private EquipmentSelectionUI m_selectionUI;
        [SerializeField] private Image m_itemIcon;
        [SerializeField] private TextMeshProUGUI m_questionMark;
        [SerializeField] private Image m_equippedIcon;



        private SoulEquipmentItem m_attachedItem;
        public SoulEquipmentItem attachedItem => m_attachedItem;
        public event EventAction<EventActionArgs> OnGridItemSelected;

        public void Display(SoulEquipmentItem item = null)
        {
            m_attachedItem = item;

            bool hasItem = m_attachedItem != null;
            
            m_questionMark.gameObject.SetActive(!hasItem);
            m_itemIcon.gameObject.SetActive(hasItem);

            if (!hasItem)
                return;

            m_itemIcon.sprite = m_attachedItem.icon;
        }

        [Button]
        public void GetEquippedStatus(EquipmentCurrentItemUI currentItem)
        {
            m_equippedIcon.gameObject.SetActive(currentItem.currentItem == m_attachedItem);
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