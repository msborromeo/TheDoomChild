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
            yield return new WaitForSeconds(m_wait);
        }

        public IEnumerator RemoveEffect(ArmyController owner, ArmyController target)
        {
            yield return null;
        }
    }
}