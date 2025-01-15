using UnityEngine;

namespace DChild.Gameplay.ArmyBattle
{
    public class ArmyAI : ArmyController
    {
        [SerializeField]
        private ArmyAIData m_AiAttackData;

        public void SetAI(ArmyAIData aiAttackData) => m_AiAttackData = aiAttackData;

        public override ArmyTurnAction GetTurnAction(int turnNumber)
        {
            var chosenAttack = m_AiAttackData.ChooseAttack(turnNumber);
            return new ArmyTurnAction()
            {
                troopCount = m_controlledArmy.troopCount,
                modifiers = m_controlledArmy.modifiers,
                attack = new ArmyDamage(chosenAttack.damageType, chosenAttack.forcedDamageValue)
            };
        }

        public override void CleanUpForNextTurn()
        {
            m_controlledArmy.modifiers.Reset();
            return;
        }
    }
}