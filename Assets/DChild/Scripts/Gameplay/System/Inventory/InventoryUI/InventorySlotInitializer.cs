using Doozy.Runtime.UIManager.Components;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DChild.Gameplay.Inventories.UI
{
    public class InventorySlotInitializer : MonoBehaviour
    {
        [SerializeField]
        private PlayerInventoryUIHandle m_handle;
        [SerializeField]
        private InventoryUISwapHandle m_swapHandle;
        [SerializeField]
        private UIToggleGroup m_itemGroup;

        public event Action<InventoryItemUI> OnItemSelectDuringSwap;

        private void OnItemSelected(ItemUI tradeFilter)
        {
            m_handle.Select(tradeFilter);
        }

        private void HandleSwap(ItemUI itemForSwap)
        {
            if (!m_swapHandle.isSwapping)
                return;

            m_swapHandle.SetSwappingStatus(false);

            OnItemSelectDuringSwap?.Invoke(itemForSwap as InventoryItemUI);
        }


        private void AddToggleOnListener(UIToggle toggle)
        {
            var events = new[] { toggle.OnToggleOnCallback.Event, toggle.OnInstantToggleOnCallback.Event };
            var item = toggle.GetComponent<ItemUI>();

            foreach (var @event in events)
            {
                @event.RemoveAllListeners();
                @event.AddListener(() =>
                {
                    HandleSwap(item);
                    OnItemSelected(item);
                });
            }

            OnItemSelectDuringSwap += m_swapHandle.OnSecondItemSelected;
        }

        private void RemoveToggleEvents(UIToggle toggle)
        {
            OnItemSelectDuringSwap -= m_swapHandle.OnSecondItemSelected;
        }

        //private IEnumerator Start()
        //{
        //    while (m_itemGroup.numberOfToggles == 0)
        //        yield return null;

        //    var toggles = m_itemGroup.toggles;
        //    AddToggleOnListener(m_itemGroup.FirstToggle);
        //    for (int i = 0; i < toggles.Count; i++)
        //    {
        //        var toggle = toggles[i];
        //        AddToggleOnListener(toggle);
        //    }

        //    Debug.Log("Inventory Slots Initialized: " + m_itemGroup.numberOfToggles);
        //}

        private void OnEnable()
        {
            var toggles = m_itemGroup.toggles;
            //AddToggleOnListener(m_itemGroup.FirstToggle);
            for (int i = 0; i < toggles.Count; i++)
            {
                var toggle = toggles[i];
                AddToggleOnListener(toggle);
            }

            Debug.Log("Inventory Slots Initialized: " + m_itemGroup.numberOfToggles);
        }

        private void OnDisable()
        {
            var toggles = m_itemGroup.toggles;
            //RemoveToggleEvents(m_itemGroup.FirstToggle);
            for (int i = 0; i < toggles.Count; i++)
            {
                var toggle = toggles[i];
                RemoveToggleEvents(toggle);
            }
        }
    }
}