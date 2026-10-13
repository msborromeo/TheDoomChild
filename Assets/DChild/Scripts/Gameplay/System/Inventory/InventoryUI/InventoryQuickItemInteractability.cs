using Doozy.Runtime.UIManager.Components;
using Holysoft.Event;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.Inventories.UI
{
    public class InventoryQuickItemInteractability : MonoBehaviour
    {
        [SerializeField] private UIToggle m_questFilter;
        [SerializeField] private Selectable[] m_quickItemSlots;
        [SerializeField] private GridInventoryListUI m_inventoryUI;

        private bool[] m_previousInteractability;
        private bool m_isRestricted;

        public void RefreshInteractability()
        {
            if (!m_questFilter.IsOn)
            {
                RestoreInteractability();
                CaptureInteractability();
                return;
            }

            m_isRestricted = true;
            foreach (var slot in m_quickItemSlots)
                slot.interactable = false;
        }

        private void CaptureInteractability()
        {
            m_previousInteractability = new bool[m_quickItemSlots.Length];
            for (int i = 0; i < m_quickItemSlots.Length; i++)
                m_previousInteractability[i] = m_quickItemSlots[i].interactable;
        }

        private void RestoreInteractability()
        {
            if (!m_isRestricted)
                return;

            for (int i = 0; i < m_quickItemSlots.Length; i++)
                m_quickItemSlots[i].interactable = m_previousInteractability[i];
            m_isRestricted = false;
        }

        private void OnListOverallChange(object sender, EventActionArgs eventArgs)
        {
            RefreshInteractability();
        }

        private void OnQuestFilterChanged(bool isOn)
        {
            RefreshInteractability();
        }

        private void OnEnable()
        {
            CaptureInteractability();
            m_inventoryUI.ListOverallChange += OnListOverallChange;
            m_questFilter.OnValueChangedCallback.AddListener(OnQuestFilterChanged);
            RefreshInteractability();
        }

        private void OnDisable()
        {
            m_inventoryUI.ListOverallChange -= OnListOverallChange;
            m_questFilter.OnValueChangedCallback.RemoveListener(OnQuestFilterChanged);
            RestoreInteractability();
        }
    }
}
