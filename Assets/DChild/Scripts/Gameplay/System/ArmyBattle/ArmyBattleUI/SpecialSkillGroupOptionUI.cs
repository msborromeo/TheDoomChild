using DChild.Gameplay.ArmyBattle.SpecialSkills;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public class SpecialSkillGroupOptionUI : AttackingGroupOptionUI
    {

        [SerializeField]
        private Sprite m_specialGlow;

        public void Display(ISpecialSkillGroup group)
        {
            SelectGlow(m_specialGlow);
            selectedSkill.DisplaySpecialIcon();
            characterGroupUI.Display(group.GetCharacterGroup() ?? null);
            partyName.Display(group);
            //attackingPowerUI.Display(group);
        }
    }
}