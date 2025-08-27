using DChild.Gameplay.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Gameplay.Systems
{
    [CreateAssetMenu(fileName = "LootReference", menuName = "DChild/Gameplay/Loot/Loot Reference")]
    public class LootReference : ScriptableObject
    {
        [SerializeField]
        private ItemData m_dataReference;
        [SerializeField,ValidateInput("ValidateLoot","GameObject must have Loot component")]
        private GameObject m_loot;

        public ItemData data => m_dataReference;
        public GameObject loot => m_loot;

        public void ChangeReference(ItemData data)
        {
            m_dataReference = data;
            m_loot.TryGetComponent<ItemLoot>(out ItemLoot lootItem);
            if(!lootItem)
            {
                return;
            }
            lootItem.SetData(m_dataReference);
        }

#if UNITY_EDITOR
        public void Initialize(GameObject loot) => m_loot = loot;

        private bool ValidateLoot(GameObject loot)
        {
            return loot?.GetComponent<Loot>() ?? false;
        }
#endif

    }
}