using DChild.Gameplay;
using DChild.Gameplay.Environment;
using DChild.Gameplay.EquipmentSystem;
using Holysoft.Event;
using TMPro;
using UnityEngine;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentEquipButtonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_labelText;

        private SoulEquipmentItem m_selectedItem;

        public event EventAction<ItemEquipEventArgs> OnItemEquipped;
        public event EventAction<EventActionArgs> OnItemRemoved;

        private enum EquipButtonLabel
        {
            Equip,
            Replace,
            Remove
        }

        private void SetLabel(EquipButtonLabel label)
        {
            m_labelText.text = label.ToString();
        }

        public void UpdateButtonLabel(EquipmentCurrentItemUI itemSlot)
        {
            var currentItem = itemSlot.currentItem;

            var label = currentItem == null || itemSlot.itemImage.sprite == null
                ? EquipButtonLabel.Equip
                : currentItem != m_selectedItem
                    ? EquipButtonLabel.Replace
                    : EquipButtonLabel.Remove;
         
            SetLabel(label);
        }

        public void SetSelectedItem(SoulEquipmentItem item) => m_selectedItem = item;

        public void EquipItem()
        {
            if (m_labelText.text != $"{EquipButtonLabel.Remove}")
            {
                //TODO: equipItem based on current value of m_selectedItem
                OnItemEquipped?.Invoke(this, new ItemEquipEventArgs(m_selectedItem));
                SetLabel(EquipButtonLabel.Remove);
                return;
            }

            OnItemRemoved?.Invoke(this, EventActionArgs.Empty);
            SetLabel(EquipButtonLabel.Equip);
        }

    }
}
