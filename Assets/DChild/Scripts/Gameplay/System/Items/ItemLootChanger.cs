using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.Items
{
    [RequireComponent(typeof(ItemLoot))]
    public class ItemLootChanger : MonoBehaviour
    {
        private ItemLoot m_ItemLootScript;
        private ItemData m_Data;

        void Awake()
        {
            m_ItemLootScript = GetComponent<ItemLoot>();
        }
        // Start is called before the first frame update

        public void SetData(ItemData loot)
        {
            if(loot == null)
            {
                return;
            }
            Debug.LogError("Changed Loot "+ loot.name);
            m_Data = loot;
            Debug.LogError("Changed Loot " + m_Data.name);
            if (m_Data == null)
            {
                return;
            }
            StartCoroutine(DelayChange(0.1f));
        }

        IEnumerator DelayChange(float delay)
        {
            yield return new WaitForSeconds(delay);
            m_ItemLootScript?.SetData(m_Data);
        }
    }
}
