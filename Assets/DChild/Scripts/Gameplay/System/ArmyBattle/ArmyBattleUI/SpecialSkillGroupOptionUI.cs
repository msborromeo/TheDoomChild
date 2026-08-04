using DChild.Gameplay.ArmyBattle.SpecialSkills;
using Doozy.Runtime.UIManager.Components;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public class SpecialSkillGroupOptionUI : AttackingGroupOptionUI
    {
        [SerializeField]
        private ArmyBattleSpecialSkillSelection m_skillSelection;
        [SerializeField]
        private Localize m_description;
        [SerializeField]
        private Image m_icon;
        [SerializeField]
        private Sprite m_specialGlow;

        private ISpecialSkillGroup m_group;
        public ISpecialSkillGroup group => m_group;

        public void Display(ISpecialSkillGroup group)
        {
            m_group = group;

            if (group != null)
            {
                selectedSkill.DisplaySpecialIcon();
                characterGroupUI.Display(group.GetCharacterGroup() ?? null);
                partyName.Display(group);
                gameObject.SetActive(true);

                var groupId = group.id.ToString("000");

                m_description.SetTerm($"ArmyBattle/Groups/{groupId}/AG_{groupId}_SpecialSkill");
                m_icon.sprite = group.GetSpecialSkill().icon;

                m_icon.color = m_icon.sprite ? Color.white : Color.clear;
                return;
            }
            gameObject.SetActive(false);
        }
    }
}