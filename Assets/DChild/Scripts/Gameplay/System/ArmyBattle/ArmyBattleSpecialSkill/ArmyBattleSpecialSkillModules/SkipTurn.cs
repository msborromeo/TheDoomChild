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
                        var participantConfiguration = configuration.enemyConfiguration;
                        participantConfiguration.willAttack = enableAttack;
                        configuration.enemyConfiguration = participantConfiguration;

                    }
                    else
                    {
                        var participantConfiguration = configuration.playerConfiguration;
                        participantConfiguration.willAttack = enableAttack;
                        configuration.playerConfiguration = participantConfiguration;
                    }
                    break;
                case TargetType.Self:
                    if (isUsedByPlayer)
                    {
                        var participantConfiguration = configuration.playerConfiguration;
                        participantConfiguration.willAttack = enableAttack;
                        configuration.playerConfiguration = participantConfiguration;
                    }
                    else
                    {
                        var participantConfiguration = configuration.enemyConfiguration;
                        participantConfiguration.willAttack = enableAttack;
                        configuration.enemyConfiguration = participantConfiguration;
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

