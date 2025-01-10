using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.SpecialSkills.Modules
{
    public class CalculateRegenerationBasedOnLostTroops : ISpecialSkillModule, ISpecialSkillImplementor
    {
        [SerializeField,Min(0f)]
        private float m_percentageToRegen;
        [SerializeField,Min(1)]
        private int m_numberOfTurnsToRecover;
        [SerializeField, VariablePopup(true)]
        private string m_regenVariable;

        public void ApplyEffect(ArmyController owner, ArmyController target)
        {
            //var initialTroops = owner.controlledArmy.initialTroopCount;
            var initialTroops = 0;
            var currentTroops = owner.controlledArmy.troopCount;

            var lostTroopCount = currentTroops - initialTroops;
            var totalTroopsToRecover = lostTroopCount * m_percentageToRegen;
            var perTurnRegen = Mathf.RoundToInt(totalTroopsToRecover/ m_numberOfTurnsToRecover);

            DialogueLua.SetVariable(m_regenVariable, perTurnRegen);
        }

        public void RemoveEffect(ArmyController owner, ArmyController target)
        {

        }
    }
}

