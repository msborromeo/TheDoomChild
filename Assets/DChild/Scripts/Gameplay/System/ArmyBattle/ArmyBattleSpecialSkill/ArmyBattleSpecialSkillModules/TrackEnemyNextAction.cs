using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    public class TrackEnemyNextAction : ISpecialSkillModule, ISpecialSkillImplementor
    {
        [SerializeField, VariablePopup(true)]
        private string m_enemyNextMoveVar;
        [SerializeField]
        private bool m_trackFalseInformation;

        public void ApplyEffect(ArmyController owner, ArmyController target)
        {
            var nextMove = (int)target.GetTurnAction(ArmyBattleSystem.GetCurrentTurnNumber()).attack.type;
            if (m_trackFalseInformation)
            {
                var randomRange = Random.Range(0, 100);
                var randomSignModifier = randomRange < 50 ? -1 : 1;
                nextMove += 1 * randomSignModifier;
                nextMove = (int)Mathf.Repeat(nextMove, (float)DamageType._COUNT);
            }

            DialogueLua.SetVariable(m_enemyNextMoveVar, nextMove);
        }

        public void RemoveEffect(ArmyController owner, ArmyController target)
        {

        }
    }
}

