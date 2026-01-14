using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using DChild.Gameplay.UI;
using UnityEngine.Video;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DChild.Gameplay.Characters.Players
{
    [CreateAssetMenu(fileName = "PrimarySkillData", menuName = "DChild/Database/Primary Skill Data")]
    public class PrimarySkillData : ScriptableObject
    {
        [SerializeField]
        private string m_name;
        [SerializeField]
        private string m_description;
        [SerializeField]
        private string m_instruction;
        [SerializeField, OnValueChanged("SkillChanged")]
        private PrimarySkill m_skill;
        [SerializeField, PreviewField]
        private Sprite m_border;
        [SerializeField, PreviewField]
        private Sprite m_icon;
        [SerializeField, PreviewField]
        private VideoClip m_demoClip;
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

        public string skillName => m_name;
        public string description => m_description;
        public string instruction => m_instruction;
        public PrimarySkill skill => m_skill;
        public Sprite border => m_border;
        public Sprite icon => m_icon;
        public VideoClip demoClip => m_demoClip;
        public int numberOfActions => m_numberOfActions;
        public InputActionConfiguration action => m_actionConfiguration1;
        public InputActionConfiguration action2 => m_actionConfiguration2;
        public InputActionConfiguration action3 => m_actionConfiguration3;
        public InputActionConfiguration action4 => m_actionConfiguration4;
#if UNITY_EDITOR
        private void SkillChanged()
        {
            m_name = m_skill.ToString();
            var path = AssetDatabase.GetAssetPath(this);
            AssetDatabase.RenameAsset(path, m_name + "SkillData");
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
#endif
    }
}
