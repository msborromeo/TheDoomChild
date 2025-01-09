using DChild.Gameplay.ArmyBattle.SpecialSkills;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public class SpecialSkillGroupOptionUI : MonoBehaviour
    {

        [SerializeField]
        private Sprite m_specialGlow;
        [SerializeField]
        private UIButton m_moreGroupsButton;
        [SerializeField]
        private AttackingGroupOptionUI m_attackingGroup;

        [SerializeField]
        private SpecialSkillNavigationToggleUI m_navigationToggle;

        public void Display(ISpecialSkillGroup group)
        {
            if (m_moreGroupsButton != null && m_moreGroupsButton.Id.Name != "MoreSpecialGroups")
            {
                m_navigationToggle.ToggleSpecialUnitNavigation(true);
                m_navigationToggle.UpdateMoreGroupButtonNavigation(m_moreGroupsButton);
                m_moreGroupsButton.Id.Name = "MoreSpecialGroups";
            }

            SelectGlow(m_specialGlow);
            m_attackingGroup.selectedSkill.DisplaySpecialIcon();
            m_attackingGroup.attackingPowerUI.Display(group);
            m_attackingGroup.characterGroupUI.Display(group.GetCharacterGroup() ?? null);
            m_attackingGroup.partyName.Display(group);
        }

        private void SelectGlow(Sprite glow)
        {
            m_attackingGroup.SelectGlow(glow);
        }
    }
}