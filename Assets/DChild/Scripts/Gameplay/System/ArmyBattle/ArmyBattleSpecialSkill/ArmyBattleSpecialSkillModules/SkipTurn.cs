using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    public class SkipTurn : ISpecialSkillModule, ISpecialSkillImplementor
    {
        private enum TargetType
        {
            Opponent,
            Self
        }

        [SerializeField]
        private TargetType m_targetType;
        [SerializeField]
        private bool m_turnCountWillNotProgress;

        public void ApplyEffect(ArmyController owner, ArmyController target)
        {
            SetTargetToSkipTurn(owner, true);
        }

        public void RemoveEffect(ArmyController owner, ArmyController target)
        {
            SetTargetToSkipTurn(owner, false);
        }

        private void SetTargetToSkipTurn(ArmyController owner, bool willSkipTurn)
        {
            var isUsedByPlayer = ArmyBattleSystem.GetPlayer() == owner;
            var configuration = ArmyBattleSystem.turnConfiguration;

            var enableAttack = !willSkipTurn;
            switch (m_targetType)
            {
                case TargetType.Opponent:
                    if (isUsedByPlayer)
                    {
                        configuration.enemyWillAttack = enableAttack;
                    }
                    else
                    {
                        configuration.playerWillAttack = enableAttack;
                    }
                    break;
                case TargetType.Self:
                    if (isUsedByPlayer)
                    {
                        configuration.playerWillAttack = enableAttack;
                    }
                    else
                    {
                        configuration.enemyWillAttack = enableAttack;
                    }
                    break;
            }

            if (m_turnCountWillNotProgress)
            {
                configuration.turnWillProgress = !willSkipTurn;
            }

            ArmyBattleSystem.turnConfiguration = configuration;
        }
    }
}

