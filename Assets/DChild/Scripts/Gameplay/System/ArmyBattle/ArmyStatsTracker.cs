using UnityEngine;
using PixelCrushers.DialogueSystem;

namespace DChild.Gameplay.ArmyBattle
{
    public class ArmyStatsTracker : MonoBehaviour
    {
        [SerializeField]
        private ArmyController m_toTrack;
        [SerializeField, VariablePopup(true)]
        private string m_troopCountVar;

        public void RecordStats()
        {
            DialogueLua.SetVariable(m_troopCountVar, m_toTrack.controlledArmy.troopCount);
        }

        public int GetTrackedTroopCount() => DialogueLua.GetVariable(m_troopCountVar).AsInt;
    }
}