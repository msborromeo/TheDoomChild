using DChild.Gameplay.ArmyBattle.UI;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills
{
    public class ExecuteAttackConfirmationUI : MonoBehaviour
    {
        [SerializeField]
        private ArmyBattlePlayerOption m_playerOptions;
        [SerializeField]
        private ArmyBattleTurnHandle m_turnHandle;
        [SerializeField]
        private ArmyBattleSpecialSkillHandle m_specialSkillHandle;
        [SerializeField]
        private ArmyBattleSpecialSkillSelection m_specialSelection;
        [SerializeField]
        private UIButton m_confirmationButton;

        private bool m_isSpecial = false;

        public void ToggleSpecialConfirmation(bool toggle)
        {
            m_isSpecial = toggle;
            m_confirmationButton.Id.Name = m_isSpecial ? "ExecuteSpecial" : "ExecuteAttack";
        }

        public void ExecuteAttack()
        {
            switch (m_isSpecial)
            {
                case true:
                    var receivedSpecialGroup = m_specialSelection.GetSelectedSpecialSkillGroup();
                    m_specialSkillHandle.Activate(receivedSpecialGroup.GetSpecialSkill(), m_playerOptions.player);
                    break;
                default:
                    m_playerOptions.SelectCurrentAttackingGroup();
                    m_turnHandle.CommenceTurn();
                    break;
            }
        }

    }
}