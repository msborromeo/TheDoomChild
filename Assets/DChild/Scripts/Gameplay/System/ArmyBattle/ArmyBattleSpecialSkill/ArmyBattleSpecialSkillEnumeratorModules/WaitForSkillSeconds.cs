using System.Collections;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    [System.Serializable]
    public class WaitForSkillSeconds : ISpecialSkillIEnumeratorModule
    {
        [SerializeField]
        private float m_wait;

        public IEnumerator ApplyEffect(ArmyController owner, ArmyController target)
        {
            Debug.Log($"Waiting for {m_wait} To Imitiate Army Battle Skill Duration");
            yield return new WaitForSeconds(m_wait);
            Debug.Log("Waiting Done To Imitiate Army Battle Skill Duration");
        }

        public IEnumerator RemoveEffect(ArmyController owner, ArmyController target)
        {
            yield return null;
        }
    }
}