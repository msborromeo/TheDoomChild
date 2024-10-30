using UnityEngine;

namespace DChild.Gameplay.ArmyBattle
{
    public class ArmyBattleScenarioBasic : ArmyBattleScenarioHandle
    {
        [SerializeField]
        private ArmyBattleScenarioIntroHandle m_introHandle;
        [SerializeField]
        private ArmyBattleScenarioUpdateHandle m_updateHandle;

        [SerializeField]
        private ArmyBattleRewardGiver m_RewardGiver;

        public void ForceStartBattleGameplay()
        {
            ArmyBattleSystem.StartBattleGameplay();
            if(!ArmyBattleSystem.BattleScenario.battleRewards)
            {
                return;
            }
            m_RewardGiver.InitializeReward(ArmyBattleSystem.BattleScenario.battleRewards);
        }

        public override void EndScenario(bool playerWon)
        {
            if (playerWon)
            {
                m_RewardGiver.GiveReward();
                Debug.Log("Army Battle Scenario: Player Won");
            }
            else
            {
                Debug.Log("Army Battle Scenario: Player Lost");
            }
        }

        public override void StartScenario()
        {
            m_introHandle.Execute();
        }

        public override void UpdateScenario()
        {
            m_updateHandle.UpdateScenario();
        }
    }
}
