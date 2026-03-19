using DChild.Gameplay.Items;
using Holysoft.Event;
using System;
using UnityEngine;

namespace DChild.Gameplay.Inventories.UI
{
    public class InventorySwapButtonUI : MonoBehaviour
    {
        [SerializeField]
        private InventoryUISwapHandle m_swapHandle;

        public void StartSwappingItem()
        {
            m_swapHandle.SetSwappingStatus(true);
        }
    }
}