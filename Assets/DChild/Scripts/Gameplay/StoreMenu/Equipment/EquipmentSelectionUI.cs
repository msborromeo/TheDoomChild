using DChild.Gameplay.EquipmentSystem;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentSelectionUI : MonoBehaviour
    {

        [SerializeField] private List<SoulEquipmentItem> m_sampleData;
        [SerializeField] private List<EquipmentItemUI> m_itemGrid;
        [SerializeField] private TextMeshProUGUI m_noItemsLabel;

        private SoulSlot m_slotFilter;
        public void SetFilter(SoulSlot value) => m_slotFilter = value;
        
        public void SetupUI() => m_slotFilter = SoulSlot.Head;


        [Button]
        public void DisplayItems()
        {
            var filteredItems = m_sampleData.Where(item => item.soulEquipment.Slot == m_slotFilter).ToList();
            var hasItems = filteredItems != null && filteredItems.Count > 0;

            m_noItemsLabel.gameObject.SetActive(!hasItems);

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
