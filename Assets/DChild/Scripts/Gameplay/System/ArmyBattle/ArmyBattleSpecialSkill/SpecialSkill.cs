using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills
{
    [System.Serializable]
    public class SpecialSkill : ISpecialSkillImplementor
    {
        public enum Type
        {
            Instant,
            Turn,
            Waiting
        }

        [SerializeField]
        private Type m_type;
        [SerializeField]
        private string m_description;
        [SerializeField, ShowIf("@m_type == Type.Waiting")]
        private int m_delay =0;
        [SerializeField, Min(1)]
        private int m_duration = 1;
        [SerializeField, TabGroup("Upon Select")]
        private ISpecialSkillIEnumeratorModule[] m_onSelect = new ISpecialSkillIEnumeratorModule[0];
        [SerializeField, HideLabel, HideReferenceObjectPicker, TabGroup("Upon Effect")]
        private SpecialSkillVisualizerInfo m_visualizerInfo = new SpecialSkillVisualizerInfo();
        [SerializeField, TabGroup("Upon Effect")]
        private ISpecialSkillIEnumeratorModule[] m_specialSkillModules = new ISpecialSkillIEnumeratorModule[0];

        public Type type => m_type;
        public int delay => m_delay;
        public int duration => m_duration;
        public SpecialSkillVisualizerInfo visualizerInfo => m_visualizerInfo;
        public string GetDescription() { return m_description; }

        public IEnumerator ExecuteOnSelect(ArmyController owner, ArmyController target)
        {
            for (int i = 0; i < m_onSelect.Length; i++)
            {
                yield return m_onSelect[i].ApplyEffect(owner, target);
            }

            Debug.Log($"{m_description} \n Is Selected");
        }

        public IEnumerator ApplyEffect(ArmyController owner, ArmyController target)
        {
            for (int i = 0; i < m_specialSkillModules.Length; i++)
            {
                yield return m_specialSkillModules[i].ApplyEffect(owner, target);
            }

            Debug.Log($"{m_description} \n Is Activated");
        }

        public IEnumerator RemoveEffect(ArmyController owner, ArmyController target)
        {
            for (int i = 0; i < m_specialSkillModules.Length; i++)
            {
                yield return m_specialSkillModules[i].RemoveEffect(owner, target);
            }

            Debug.Log($"{m_description} \n Is Deactivated");
        }

        void ISpecialSkillImplementor.ApplyEffect(ArmyController owner, ArmyController target)
        {
            throw new System.NotImplementedException();
        }

        void ISpecialSkillImplementor.RemoveEffect(ArmyController owner, ArmyController target)
        {
            throw new System.NotImplementedException();
        }
    }
}

