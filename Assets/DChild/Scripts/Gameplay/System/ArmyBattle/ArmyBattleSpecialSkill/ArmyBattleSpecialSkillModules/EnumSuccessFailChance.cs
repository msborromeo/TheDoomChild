using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    [InfoBox("This will not Remove the Effects of either fail or success results")]
    public class EnumSuccessFailChance : ISpecialSkillIEnumeratorModule
    {
        [SerializeField, Range(0, 100)]
        private float m_successChance;
        [SerializeField, TabGroup("OnSuccess")]
        private ISpecialSkillIEnumeratorModule[] m_onSuccess;
        [SerializeField, TabGroup("OnFail")]
        private ISpecialSkillIEnumeratorModule[] m_onFail;

 
        IEnumerator ISpecialSkillIEnumeratorModule.ApplyEffect(ArmyController owner, ArmyController target)
        {
            float chance = Random.Range(0, 100);
            if (chance <= m_successChance)
            {
                ApplyModules(m_onSuccess, owner, target);
            }
            else
            {
                ApplyModules(m_onFail, owner, target);
            }
            throw new System.NotImplementedException();
        }

        private void ApplyModules(ISpecialSkillIEnumeratorModule[] specialSkillModules, ArmyController owner, ArmyController target)
        {
            for (int i = 0; i < specialSkillModules.Length; i++)
            {
                specialSkillModules[i].ApplyEffect(owner, target);
            }
            throw new System.NotImplementedException();
        }

       

        IEnumerator ISpecialSkillIEnumeratorModule.RemoveEffect(ArmyController owner, ArmyController target)
        {
            throw new System.NotImplementedException();
        }
    }
}

