using DChild.Gameplay.EquipmentSystem;
using Doozy.Runtime.UIManager.Components;
using Holysoft.Event;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
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
        [BoxGroup("MAIN UI"), SerializeField] private EquipmentUI m_equipmentUI;

        [BoxGroup("ITEM GRID"), SerializeField] private List<EquipmentGridItemUI> m_itemGrid;
        [BoxGroup("ITEM GRID"), SerializeField] private TextMeshProUGUI m_noItemsLabel;
        [BoxGroup("ITEM GRID"), SerializeField] private EquipmentEquipButtonUI m_equipButtonUI;
        public EquipmentEquipButtonUI equipButtonUI => m_equipButtonUI;

        private List<SoulEquipmentItem> m_acquiredItems;
        private SoulSlot m_slotFilter;

        public void SetFilter(SoulSlot value) => m_slotFilter = value;

        public void SetupUI(List<SoulEquipmentItem> acquiredItems)
        {
            SetFilter(SoulSlot.Head);
            m_acquiredItems = acquiredItems;
        }

        public void UpdateItems(EquipmentCurrentItemUI currentItem)
        {
            Reset();
            var filteredItems = m_acquiredItems.Where(item => item.soulEquipment.Slot == m_slotFilter).ToList();
            var hasItems = filteredItems != null && filteredItems.Count > 0;

            m_noItemsLabel.gameObject.SetActive(!hasItems);

            int i = 0;
            for (; i < filteredItems.Count; i++)
            {
                var item = filteredItems[i];

                m_itemGrid[i].OnGridItemSelected += currentItem.OnGridItemSelected;
                m_equipmentUI.detailsUI.ConnectGridItem(m_itemGrid[i]);
                m_itemGrid[i].Display(item);
                m_itemGrid[i].GetEquippedStatus(currentItem);
            }

            for (; i < m_itemGrid.Count; i++)
            {
                m_itemGrid[i].OnGridItemSelected -= currentItem.OnGridItemSelected;
                m_equipmentUI.detailsUI.DisconnectGridItem(m_itemGrid[i]);

                m_itemGrid[i].Display();
            }

            m_equipButtonUI.UpdateButtonLabel(currentItem);
        }

        public void SetItemDetails(SoulEquipmentItem equipmentItem)
        {
            m_equipmentUI.detailsUI.SetHighlightedEquipment(equipmentItem);
        }

        public void Reset()
        {
            //m_itemGrid[0].GetComponent<UIToggle>().Select();
            m_itemGrid[0].GetComponent<UIToggle>().SetIsOn(true);
        }
    }
}
