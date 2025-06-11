using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using Sirenix.Serialization;
using UnityEngine.InputSystem;
using DChild.Gameplay.UI;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DChild.Gameplay.Characters.Players
{

    [CreateAssetMenu(fileName = "CombatArtData", menuName = "DChild/Database/Combat Art Data")]
    public class CombatArtData : SerializedScriptableObject
    {
        [SerializeField, OnValueChanged("RenameFilename")]
        private CombatArt m_ability;
        [SerializeField]
        private string m_name;


#if UNITY_EDITOR
        [SerializeField, ValueDropdown("GetCombatArtConfigrationClasses"), OnValueChanged("OverrideConfigurations")]
        private string m_configurationType;
#endif
        [SerializeField]
        private InputActionReference m_actionReference;

        [SerializeField]
        private string m_controls;


        [OdinSerialize, TableList(ShowIndexLabels = true), ListDrawerSettings(ShowIndexLabels = true)]
        private CombatArtLevelData[] m_levelDatas = new CombatArtLevelData[1];

        public CombatArt connectedCombatArt => m_ability;
        public string combatArtName => m_name;
        public string controls => m_controls;
        public InputActionReference actionReference => m_actionReference;
        public int maxLevel => m_levelDatas.Length;

        [SerializeField, MinValue(1), MaxValue(4)]
        private int m_numberOfActions = 1;

        [SerializeField]
        private InputActionConfiguration m_actionConfiguration1;
        [SerializeField, ShowIf("@m_numberOfActions > 1")]
        private InputActionConfiguration m_actionConfiguration2;
        [SerializeField, ShowIf("@m_numberOfActions > 2")]
        private InputActionConfiguration m_actionConfiguration3;
        [SerializeField, ShowIf("@m_numberOfActions > 3")]
        private InputActionConfiguration m_actionConfiguration4;

        public int numberOfActions => m_numberOfActions;
        public InputActionConfiguration action => m_actionConfiguration1;
        public InputActionConfiguration action2 => m_actionConfiguration2;
        public InputActionConfiguration action3 => m_actionConfiguration3;
        public InputActionConfiguration action4 => m_actionConfiguration4;

        public CombatArtLevelData GetCombatArtLevelData(int index) => m_levelDatas[index - 1];

        private void RenameFilename()

        {

#if UNITY_EDITOR
            var assetPath = AssetDatabase.GetAssetPath(this);
            var name = m_ability.ToString().Replace(" ", "");
            AssetDatabase.RenameAsset(assetPath, $"{name}CombatArtData");
#endif
        }

#if UNITY_EDITOR
        private IEnumerable GetCombatArtConfigrationClasses()
        {
            return DChildUtility.GetDerivedInterfacesNames<ICombatArtLevelConfiguration>();
        }

        private void OverrideConfigurations()
        {
            var currentType = Type.GetType(m_configurationType);
            if (m_configurationType != null || m_levelDatas[0].configuration.GetType() != currentType)
            {
                for (int i = 0; i < m_levelDatas.Length; i++)
                {
                    m_levelDatas[i].SetConfiguration((ICombatArtLevelConfiguration)Activator.CreateInstance(currentType));
                }
            }
        }
#endif
    }
}