using DChild.Gameplay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    [System.Serializable]
    public class InstantSpecialSkill : ISpecialSkillIEnumeratorModule
    {
        [SerializeField]
        private ISpecialSkillModule[] m_modules;

        public IEnumerator ApplyEffect(ArmyController owner, ArmyController target)
        {
            for (int i = 0; i < m_modules.Length; i++)
            {
                m_modules[i].ApplyEffect(owner, target);
            }
            yield return null;
        }

        public IEnumerator RemoveEffect(ArmyController owner, ArmyController target)
        {
            for (int i = 0; i < m_modules.Length; i++)
            {
                m_modules[i].RemoveEffect(owner, target);
            }
            yield return null;
        }
    }
}