using DChild.Gameplay.Items;
using Doozy.Runtime.UIManager.Components;
using Holysoft.Event;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.Inventories.UI
{
    public class InventoryCategoryToggleUI : MonoBehaviour
    {
        [SerializeField] private UIToggle m_toggle;
        [SerializeField] private GridInventoryListUI m_inventoryUI;

        [SerializeField] private ItemCategory m_category;

        [BoxGroup("Category Icon"), SerializeField] private Image m_targetIcon;
        [FoldoutGroup("Category Icon/State Sprites"), SerializeField] private Sprite m_hasItems;
        [FoldoutGroup("Category Icon/State Sprites"), SerializeField] private Sprite m_noItems;
        [FoldoutGroup("Category Icon/State Sprites"), SerializeField] private Sprite m_hasItemsAndSelected;

        [BoxGroup("Category Background"), SerializeField] private Image m_targetBG;
        [FoldoutGroup("Category Background/State Sprites"), SerializeField] private Sprite m_notSelectedBG;
        [FoldoutGroup("Category Background/State Sprites"), SerializeField] private Sprite m_selectedBG;

        [SerializeField] private TextMeshProUGUI m_labelPanel;

        public void SelectFilter()
        {
            m_inventoryUI.SetFilter(m_category);
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            var updatedLabel = "";
            switch (m_category)
            {
                case ItemCategory.Key:
                case ItemCategory.Quest:
                    updatedLabel = "Quest Items";
                    break;
                case ItemCategory.All:
                    updatedLabel = "All Items";
                    break;
                default:
                    updatedLabel = $"{m_category}";
                    break;
            }

            m_labelPanel.text = updatedLabel;
        }

        private bool HasItemsOfCategory()
        {
            var categorizedInventory = m_inventoryUI.inventory.FindStoredItemsOfType(m_category);
            return categorizedInventory.Length > 0;
        }

        [Button]
        public void UpdateToggleVisuals()
        {
            m_targetIcon.sprite = HasItemsOfCategory()
                ? m_toggle.IsOn
                    ? m_hasItemsAndSelected
                    : m_hasItems
                : m_noItems;

            m_targetBG.sprite = m_toggle.IsOn ? m_selectedBG : m_notSelectedBG;
        }

    }
}