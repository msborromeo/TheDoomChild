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
    public class ChanceDropLootData : ILootDataContainer
    {
        [SerializeField]
        private List<ILootDataContainer> m_Drops = new List<ILootDataContainer>();
        [SerializeField,Range(0,100)]
        private float m_Chance;

        public void DropLoot(Vector2 position)
        {
            if(m_Chance < Random.Range(0,100))
            {
                return;
            }
            if (m_Drops.Count > 0)
            {
                for (int i = 0; i < m_Drops.Count; i++)
                { 
                    m_Drops[i].DropLoot(position);
                }
            }
        }

        public void GenerateLootInfo(ref LootList recordList)
        {
            for (int i = 0; i < m_Drops.Count; i++)
            {
                m_Drops[i]?.GenerateLootInfo(ref recordList);
            }
        }

#if UNITY_EDITOR
        void ILootDataContainer.DrawDetails(bool drawContainer, string label = null)
        {
            EditorGUILayout.LabelField($"Drop Chance: {m_Chance}%");
            SirenixEditorGUI.BeginBox(label);
            EditorGUI.indentLevel++;
            for (int i = 0; i < m_Drops.Count; i++)
            {
                if (m_Drops[i] == null)
                {
                    EditorGUILayout.LabelField($"None%");
                }
                else
                {
                    m_Drops[i].DrawDetails(true, null);
                }
            }
            EditorGUI.indentLevel--;
            SirenixEditorGUI.EndBox();
        }
#endif
    }
}
