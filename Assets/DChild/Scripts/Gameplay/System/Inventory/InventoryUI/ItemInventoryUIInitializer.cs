using DChild.Gameplay.Trade;
using DChild.Gameplay.Trade.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Inventories.UI
{
    public class ItemInventoryUIInitializer : MonoBehaviour
    {
        // THIS IS A DEBUG AND SHOULD BE REPLACED, GRIDINVENTORYLISTUI cant intialize inventory in prefab which breaks the players inventory
        public GridInventoryListUI uiGrid;

        [SerializeField]
        private PlayerInventory referenceInventory;

        public void Awake()
        {
            uiGrid.SetInventoryReference(referenceInventory);
        }
    }
}

