using UnityEngine;

namespace DChild.Gameplay.SoulSkills.UI
{
    public sealed class ActivatedSoulSkillUI : SoulSkillUI
    {
        [SerializeField]
        private GameObject m_chain;


        protected override void Awake()
        {
            base.Awake();
            m_isAnActivatedSoulSkill = true;
        }
    }
}
