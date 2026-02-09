using DChild.Gameplay;
using DChild.Gameplay.Environment;
using DChild.Gameplay.EquipmentSystem;
using DChild.Gameplay.UI;
using Holysoft.Event;
using TMPro;
using UnityEngine;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentEquipButtonUI : MonoBehaviour
    {
        [SerializeField] private SetTextToTextBox m_labelText;

        private SoulEquipmentItem m_selectedItem;
        private SoulEquipmentItem m_currentEquipped;

        public event EventAction<ItemEquipEventArgs> OnItemEquipped;
        public event EventAction<EventActionArgs> OnItemRemoved;

        private EquipButtonLabel m_currentLabel;

        private enum EquipButtonLabel
        {
            Equip,
            Replace,
            Remove
        }

        private void SetLabel(EquipButtonLabel label)
        {
            m_currentLabel = label;
            m_labelText.SetText($"BUTTONPROMPT{label}");
        }

        public void UpdateButtonLabel(EquipmentCurrentItemUI itemSlot)
        {
            m_currentEquipped = itemSlot.currentItem;

            var label = m_currentEquipped == null || itemSlot.itemImage.sprite == null
                ? EquipButtonLabel.Equip
                : m_currentEquipped != m_selectedItem
                    ? EquipButtonLabel.Replace
                    : EquipButtonLabel.Remove;

            SetLabel(label);
        }

        public void SetSelectedItem(SoulEquipmentItem item) => m_selectedItem = item;

        public void EquipItem()
        {
            if (m_currentLabel != EquipButtonLabel.Remove)
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
