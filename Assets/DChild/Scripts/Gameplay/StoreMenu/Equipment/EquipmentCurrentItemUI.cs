using DChild.Gameplay.EquipmentSystem;
using Holysoft.Event;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentCurrentItemUI : MonoBehaviour
    {

        [BoxGroup("MAIN UI"), SerializeField] private EquipmentUI m_equipmentUI;

        [BoxGroup("ITEM PROPERTIES"), SerializeField] private TextMeshProUGUI m_itemName;
        [BoxGroup("ITEM PROPERTIES"), SerializeField] private Image m_itemImage;
        public Image itemImage => m_itemImage;

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

            m_itemName.text = $"{m_currentItem.itemName}";
            m_itemImage.sprite = m_currentItem.icon;
            m_equipmentUI.equipmentHandle.EquipSoulEquipment(m_currentItem);
        }

        public void OnItemRemoved(object sender, EventActionArgs eventArgs)
        {
            if (m_currentItem.soulEquipment.Slot != m_soulSlot)
                return;
            m_itemImage.sprite = null;
            m_equipmentUI.equipmentHandle.UnequipSoulEquipment(m_currentItem);
        }

        private void Start()
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
