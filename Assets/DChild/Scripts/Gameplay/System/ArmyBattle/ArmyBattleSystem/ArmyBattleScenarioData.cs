using UnityEngine;

namespace DChild.Gameplay.ArmyBattle
{
    [CreateAssetMenu(fileName = "ArmyBattleScenario", menuName = "DChild/Gameplay/Army/BattleScenario")]
    public class ArmyBattleScenarioData : ScriptableObject
    {
        [SerializeField]
        private ArmyAIData m_enemyToBattle;
        [SerializeField]
        private ArmyBattleLocation m_location;
        [SerializeField]
        private GameObject m_scenarioHandle;

        [SerializeField]
        private ArmyBattleRewardData m_battleRewards;

        public ArmyAIData enemyToBattle => m_enemyToBattle;
        public ArmyBattleLocation location => m_location;

        public GameObject scenarioHandle => m_scenarioHandle;

        public ArmyBattleRewardData battleRewards => m_battleRewards;
    }
}