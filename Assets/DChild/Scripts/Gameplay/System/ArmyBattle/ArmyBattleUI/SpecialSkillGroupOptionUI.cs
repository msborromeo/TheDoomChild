using DChild.Gameplay.ArmyBattle.SpecialSkills;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public class SpecialSkillGroupOptionUI : AttackingGroupOptionUI
    {
        [SerializeField]
        private ArmyBattleSpecialSkillSelection m_skillSelection;
        [SerializeField]
        private Sprite m_specialGlow;

        private ISpecialSkillGroup m_group;
        public ISpecialSkillGroup group => m_group;

        public void Display(ISpecialSkillGroup group)
        {
            m_group = group;
            //if (group == null)
            //{
            //    NullifyArmyGroupUI();
            //    return;
            //}

            //if (targetCommandIcon.enabled == false)
            //{
            //    RestoreArmyGroupUI();
            //}

            SelectGlow(m_specialGlow);
            selectedSkill.DisplaySpecialIcon();
            characterGroupUI.Display(group.GetCharacterGroup() ?? null);
            partyName.Display(group);
        }
    }
}