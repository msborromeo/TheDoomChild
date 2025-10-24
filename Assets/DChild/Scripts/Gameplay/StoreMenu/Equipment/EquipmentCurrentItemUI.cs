using DChild.Gameplay.EquipmentSystem;
using Holysoft.Event;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentCurrentItemUI : MonoBehaviour
    {

        [SerializeField] private EquipmentSelectionUI m_selectionUI;

        [SerializeField] private Image m_itemImage;
        public Image itemImage => m_itemImage;

        [SerializeField] private SoulSlot m_soulSlot;
        public SoulSlot soulSlot => m_soulSlot;

        private SoulEquipmentItem m_currentItem;
        public SoulEquipmentItem currentItem => m_currentItem;

        public void OnGridItemSelected(object sender, EventActionArgs eventArgs) => m_selectionUI.equipButtonUI.UpdateButtonLabel(this);

        public void OnItemEquipped(object sender, ItemEquipEventArgs eventArgs)
        {
            m_currentItem = eventArgs.equipmentItem;

            if (m_currentItem.soulEquipment.Slot != m_soulSlot)
                return;

            m_itemImage.sprite = m_currentItem.icon;
        }

        public void OnItemRemoved(object sender, EventActionArgs eventArgs)
        {
            if (m_currentItem.soulEquipment.Slot != m_soulSlot)
                return;
            m_itemImage.sprite = null;
        }

        private void Start()
        {
            m_selectionUI.equipButtonUI.OnItemEquipped += OnItemEquipped;
            m_selectionUI.equipButtonUI.OnItemRemoved += OnItemRemoved;
        }

        private void OnDestroy()
        {
            m_selectionUI.equipButtonUI.OnItemEquipped -= OnItemEquipped;
            m_selectionUI.equipButtonUI.OnItemRemoved -= OnItemRemoved;
        }
    }
}
