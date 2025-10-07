using DChild.Gameplay.EquipmentSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentSelectionUI : MonoBehaviour
    {

        [SerializeField] private List<SoulEquipmentItem> m_sampleData;
        [SerializeField] private List<EquipmentItemUI> m_itemGrid;

        private SoulSlot m_slotFilter;

        public void SetFilter(SoulSlot value) => m_slotFilter = value;
        public void SetupUI() => m_slotFilter = SoulSlot.Head;


        [Button]
        public void DisplayItems(SoulSlot filter)
        {
            m_slotFilter = filter;
            var filteredItems = m_sampleData.Where(item => item.soulEquipment.Slot == m_slotFilter).ToList();
            int i = 0;
            for (; i < filteredItems.Count; i++)
            {
                var item = filteredItems[i];
                m_itemGrid[i].Display(item);
            }

            for (; i < m_itemGrid.Count; i++)
            {
                m_itemGrid[i].Display(null);
            }
        }
    }
}
