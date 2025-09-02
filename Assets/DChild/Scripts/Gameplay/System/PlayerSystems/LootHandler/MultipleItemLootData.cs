using DChild.Gameplay.Essence;
using DChild.Gameplay.Items;
using Sirenix.OdinInspector;
using System.Collections.Generic;

using UnityEngine;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
using UnityEditor;
#endif

namespace DChild.Gameplay.Systems
{
    public class MultipleItemLootData : ILootDataContainer
    {
        [System.Serializable]
        public class LootDataRarity {
            public ItemData item;
            public float DropChance;
        }
        [SerializeField]
        private LootReference m_SoulEssenceData;
        [SerializeField, Min(1), OnInspectorGUI("OnLootReferenceGUI")]
        private int m_SoulEssenceCount = 1;

        [SerializeField]
        private List<LootDataRarity> m_Drops = new List<LootDataRarity>();

        [SerializeField]
        private LootReference m_ItemLootDrop;

        public void ChangeLootDrop(LootReference lootReference)
        {
            m_ItemLootDrop = lootReference;
        }

        public void DropLoot(Vector2 position)
        {
            GameplaySystem.lootHandler.DropLoot(new LootDropRequest(m_SoulEssenceData.loot, count, position));
            if (m_Drops.Count > 0 && m_ItemLootDrop!=null)
            {
                for (int x = 0;x < m_Drops.Count ;x++ )
                {  
                    Debug.LogError(x+" " + m_Drops[x].item.name);
                    //float value;
                    //m_Drops.TryGetValue(drop,out value);
                    if (m_Drops[x].DropChance >= Random.Range(0,105))
                    {
                        //Debug.LogError(drop.name);
                        m_ItemLootDrop.ChangeReference(m_Drops[x].item);
                        GameplaySystem.lootHandler.DropLoot(new LootDropRequest(m_ItemLootDrop.loot, 1, position));
                        break;
                    }
                }
            }
        }

        public void GenerateLootInfo(ref LootList recordList)
        {
            if (m_SoulEssenceData.data == null)
            {
                if (m_SoulEssenceData.loot.GetComponent<SoulEssenceLoot>() != null)
                {
                    var soulEssenceValue = m_SoulEssenceData.loot.GetComponent<SoulEssenceLoot>().value;
                    soulEssenceValue *= m_SoulEssenceCount;
                    recordList.AddSoulEssence(soulEssenceValue);
                }
                else
                {
                    var aetherValue = m_SoulEssenceData.loot.GetComponent<AetherLoot>().value;
                    aetherValue *= m_SoulEssenceCount;
                    recordList.AddDarkAetherPoints(aetherValue);
                }

            }
            else
            {
                recordList.Add(m_SoulEssenceData.data, m_SoulEssenceCount);
            }
        }
        public int count => m_SoulEssenceCount;
#if UNITY_EDITOR
        public LootReference SoulEssence => m_SoulEssenceData;
 

        private void OnLootReferenceGUI()
        {
            if (m_SoulEssenceData != null && m_SoulEssenceData.loot != null)
            {
                var soulEssence = m_SoulEssenceData?.loot?.GetComponent<SoulEssenceLoot>() ?? null;
                if (soulEssence)
                {
                    SirenixEditorGUI.InfoMessageBox($"Soul Essence: {soulEssence.value * m_SoulEssenceCount}");
                }
            }
        }

        void ILootDataContainer.DrawDetails(bool drawContainer, string label = null)
        {
            if (m_SoulEssenceData != null)
            {
                var suffix = label;
                if (m_SoulEssenceData.data == null)
                {
                    label = m_SoulEssenceData.name.Replace("LootReference", string.Empty);
                    var soulEssence = m_SoulEssenceData?.loot?.GetComponent<SoulEssenceLoot>() ?? null;
                    if (soulEssence)
                    {
                        EditorGUILayout.LabelField($"{label} ({soulEssence.value * m_SoulEssenceCount}){suffix}");
                    }
                }
                else
                {
                    label = m_SoulEssenceData.data.itemName;
                    EditorGUILayout.LabelField($"{label} ({m_SoulEssenceCount}){suffix}");
                }
            }
        }
#endif
    }
}
