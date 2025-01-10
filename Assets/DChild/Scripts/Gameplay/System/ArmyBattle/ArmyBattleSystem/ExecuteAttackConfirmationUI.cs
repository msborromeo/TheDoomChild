using DChild.Gameplay.ArmyBattle.UI;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills
{
    public class ExecuteAttackConfirmationUI: MonoBehaviour
    {
        [SerializeField]
        private ArmyBattlePlayerOption m_playerOptions;
        [SerializeField]
        private ArmyBattleTurnHandle m_turnHandle;
        [SerializeField]
        private ArmyBattleSpecialSkillHandle m_specialSkillHandle;
        [SerializeField]
        private ArmyBattleSpecialSkillSelection m_specialSelection;

        private bool m_isSpecial = false;

        public void ToggleSpecialConfirmation(bool toggle)
        {
            m_isSpecial = toggle;
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
                    break;
            }

            m_turnHandle.CommenceTurn();

        }

    }
}