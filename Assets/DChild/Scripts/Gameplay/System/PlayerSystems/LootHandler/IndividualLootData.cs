using UnityEngine;
using Sirenix.OdinInspector;
using DChild.Gameplay.Essence;
using DChild.Gameplay.Items;


#if UNITY_EDITOR
using UnityEditor;
using Sirenix.Utilities.Editor;
#endif

namespace DChild.Gameplay.Systems
{

    [System.Serializable]
    public class IndividualLootData : ILootDataContainer
    {
        [SerializeField]
        private LootReference m_reference;
        [SerializeField, Min(1), OnInspectorGUI("OnLootReferenceGUI")]
        private int m_count = 1;

        public void DropLoot(Vector2 position)
        {
            m_reference.ChangeReference(m_reference.data);
            GameplaySystem.lootHandler.DropLoot(new LootDropRequest(m_reference.loot, m_count, position,m_reference.data));
        }

        public void GenerateLootInfo(ref LootList recordList)
        {
            if (m_reference.data != null && m_reference.data.category != ItemCategory.SoulEssence)
            {
                recordList.Add(m_reference.data, m_count);
            }
            else
            {
                if (m_reference.loot.GetComponent<SoulEssenceLoot>() != null)
                {
                    var soulEssenceValue = m_reference.loot.GetComponent<SoulEssenceLoot>().value;
                    soulEssenceValue *= m_count;
                    recordList.AddSoulEssence(soulEssenceValue);
                    if(m_reference.data!=null)
                    {
                        recordList.Add(m_reference.data, soulEssenceValue);
                    }
                }
                else
                {
                    var aetherValue = m_reference.loot.GetComponent<AetherLoot>().value;
                    aetherValue *= m_count;
                    recordList.AddDarkAetherPoints(aetherValue);
                }
            }
        }

#if UNITY_EDITOR
        public LootReference reference => m_reference;
        public int count => m_count;

        private void OnLootReferenceGUI()
        {
            if (m_reference != null && m_reference.loot != null)
            {
                var soulEssence = m_reference?.loot?.GetComponent<SoulEssenceLoot>() ?? null;
                if (soulEssence)
                {
                    SirenixEditorGUI.InfoMessageBox($"Soul Essence: {soulEssence.value * m_count}");
                }
            }
        }

        void ILootDataContainer.DrawDetails(bool drawContainer, string label = null)
        {
            if (m_reference != null)
            {
                var suffix = label;
                if (m_reference.data == null)
                {
                    label = m_reference.name.Replace("LootReference", string.Empty);
                    var soulEssence = m_reference?.loot?.GetComponent<SoulEssenceLoot>() ?? null;
                    if (soulEssence)
                    {
                        EditorGUILayout.LabelField($"{label} ({soulEssence.value * m_count}){suffix}");
                    }
                }
                else
                {
                    label = m_reference.data.itemName;
                    EditorGUILayout.LabelField($"{label} ({m_count}){suffix}");
                }
            }
        }
#endif
    }
}