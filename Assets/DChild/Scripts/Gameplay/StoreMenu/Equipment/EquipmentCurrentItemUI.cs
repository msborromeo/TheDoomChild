using DChild.Gameplay.EquipmentSystem;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentCurrentItemUI : MonoBehaviour
    {

        [BoxGroup("MAIN UI"), SerializeField] private EquipmentUI m_equipmentUI;

        [BoxGroup("ITEM PROPERTIES"), SerializeField] private Image m_itemImage;
        public Image itemImage => m_itemImage;

        [BoxGroup("ITEM PROPERTIES/Canvas Groups"), SerializeField] private CanvasGroup m_itemCG;
        [BoxGroup("ITEM PROPERTIES/Canvas Groups"), SerializeField] private CanvasGroup m_undiscoveredCG;

        [SerializeField] private SoulSlot m_soulSlot;
        public SoulSlot soulSlot => m_soulSlot;

        private SoulEquipmentItem m_currentItem;
        public SoulEquipmentItem currentItem => m_currentItem;

        public void OnGridItemSelected(object sender, EventActionArgs eventArgs)
        {
            m_equipmentUI.selectionUI.equipButtonUI.UpdateButtonLabel(this);
        }

        public void OnItemEquipped(object sender, ItemEquipEventArgs eventArgs)
        {
            m_currentItem = eventArgs.equipmentItem;

            if (m_currentItem.soulEquipment.Slot != m_soulSlot)
                return;

            m_itemImage.sprite = m_currentItem.equippedIcon;
            ToggleItemVisibility(true);

            m_equipmentUI.equipmentHandle.EquipSoulEquipment(m_currentItem);
        }

        public void OnItemRemoved(object sender, EventActionArgs eventArgs)
        {
            if (m_currentItem.soulEquipment.Slot != m_soulSlot)
                return;

            m_itemImage.sprite = null;
            ToggleItemVisibility(false);

            m_equipmentUI.equipmentHandle.UnequipSoulEquipment(m_currentItem);
        }

        private void ToggleItemVisibility(bool value)
        {
            m_itemCG.alpha = Convert.ToSingle(value);
            m_undiscoveredCG.alpha = Convert.ToSingle(!value);
        }

        private void Awake()
        {
            m_equipmentUI.selectionUI.equipButtonUI.OnItemEquipped += OnItemEquipped;
            m_equipmentUI.selectionUI.equipButtonUI.OnItemRemoved += OnItemRemoved;
        }

        private void OnDestroy()
        {
            m_equipmentUI.selectionUI.equipButtonUI.OnItemEquipped -= OnItemEquipped;
            m_equipmentUI.selectionUI.equipButtonUI.OnItemRemoved -= OnItemRemoved;
        }
    }
}
