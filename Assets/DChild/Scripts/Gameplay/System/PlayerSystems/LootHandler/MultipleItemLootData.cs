using DChild.Gameplay;
using DChild.Gameplay.Essence;
using DChild.Gameplay.Items;
using DChild.Gameplay.Systems;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static DChild.Gameplay.Systems.LootDropData;

namespace DChild.Gameplay.Systems { 
    public class MultipleItemLootData : ILootDataContainer
    {
        [SerializeField]
        private List<DropInfo> m_Drops = new List<DropInfo>();

        public void DropLoot(Vector2 position)
        {
            if (m_Drops.Count > 0)
            {
                for (int i = 0; i < m_Drops.Count; i++)
                { 
                    WillDrop(m_Drops[i])?.DropLoot(position);
                }
            }
        }

        public ILootDataContainer WillDrop(DropInfo drop)
        {

            if (drop.chance >= Random.Range(0, 100))
            {
                return drop.loot;
            }else
            {
                return null;
            }
        }

        public void GenerateLootInfo(ref LootList recordList)
        {
            for (int i = 0; i < m_Drops.Count; i++)
            {
                WillDrop(m_Drops[i])?.GenerateLootInfo(ref recordList);
            }
        }

#if UNITY_EDITOR
        void ILootDataContainer.DrawDetails(bool drawContainer, string label = null)
        {
            SirenixEditorGUI.BeginBox(label);
            EditorGUI.indentLevel++;
            for (int i = 0; i < m_Drops.Count; i++)
            {
                if (m_Drops[i].loot == null)
                {
                    EditorGUILayout.LabelField($"None - {m_Drops[i].chance}%");
                }
                else
                {
                    m_Drops[i].loot.DrawDetails(true, $" - {m_Drops[i].chance}%");
                }
            }
            EditorGUI.indentLevel--;
            SirenixEditorGUI.EndBox();
        }
#endif
    }
}
