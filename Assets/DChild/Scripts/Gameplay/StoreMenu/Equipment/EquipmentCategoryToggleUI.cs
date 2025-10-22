using DChild.Gameplay.EquipmentSystem;
using Holysoft.Event;
using UnityEngine;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentCategoryToggleUI : MonoBehaviour
    {
        [SerializeField] private EquipmentSelectionUI m_selectionUI;
        [SerializeField] private SoulSlot m_category;

        [SerializeField] private EquipmentCurrentItemUI m_activeSlot;

        public void UpdateItemGrid()
        {
            m_selectionUI.SetFilter(m_category);
            m_selectionUI.UpdateItems(m_activeSlot);
        }
    }
}
