using DChild.UI;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DChild.Gameplay.FastTravel;
using static PixelCrushers.DialogueSystem.Articy.ArticyData;

namespace DChildDebug.Serialization
{
    public class FastTravelDatabasePopulator_Window : OdinEditorWindow
    {

        [MenuItem("Tools/DChild Utility/FastTravel Database Populator")]
        private static void ShowWindow()
        {
            var window = GetWindow<FastTravelDatabasePopulator_Window>(false, "Collectathon Database Populator", true);
        }

        [SerializeField, AssetList]
        private FastTravelPageData[] m_pageDatas;

        [Button]
        public void PopulateDatabaseVariables(DialogueDatabase database)
        {

            List<string> variablesToAdd = new List<string>();


            for (int i = 0; i < (int)m_pageDatas.Length; i++)
            {
                //Create Variable names
                var data = m_pageDatas[i];
                for (int j = 0; j < data.count; j++)
                {
                    var variable = FastTravelUtility.GenerateActivationVariableName(data.GetUnderworldTravelData(j));
                    variablesToAdd.Add(variable);
                }
            }

            Template template = TemplateTools.LoadFromEditorPrefs();

            foreach (string variableName in variablesToAdd)
            {
                PixelCrushers.DialogueSystem.Variable variable = template.CreateVariable(template.GetNextVariableID(database), variableName, "False", FieldType.Boolean);
                database.variables.Add(variable);
            }
            EditorUtility.SetDirty(database);
        }

        [Button]
        public void RemoveAllVariables(DialogueDatabase database)
        {
            database.variables.Clear();
        }

    }
}