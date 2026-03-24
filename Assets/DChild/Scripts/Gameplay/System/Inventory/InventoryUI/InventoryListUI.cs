using Holysoft.Event;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Gameplay.Inventories.UI
{
    public abstract class InventoryListUI<T> : SerializedMonoBehaviour
    {
        [SerializeField]
        protected T m_inventory;
        protected ItemUI[] m_itemUIs;

        public event EventAction<EventActionArgs> ListOverallChange;

        public T inventory => m_inventory;
        protected virtual int itemUICount => m_itemUIs.Length;

        public void SetInventoryReference(T tradeInventory)
        {
            m_inventory = tradeInventory;
            if (tradeInventory != null)
            {
                UpdateUIList();
            }
        }

        public abstract void UpdateUIList();
        public abstract void UpdateUIList(bool v);

        public abstract void SwapItems(ItemUI itemOne, ItemUI itemTwo);


        protected void InvokeListOverallChange()
        {
            ListOverallChange?.Invoke(this, EventActionArgs.Empty);
        }

        private void Awake()
        {
            m_itemUIs = GetComponentsInChildren<ItemUI>();
        }
        public abstract void Reset();
    }
}