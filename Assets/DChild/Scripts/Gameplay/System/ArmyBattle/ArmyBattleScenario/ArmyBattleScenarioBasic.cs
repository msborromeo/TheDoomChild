using DChild.Gameplay.Systems.Serialization;
using Sirenix.OdinInspector;
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
        private LocationData m_afterBattleOverworldWinPosition;
        [SerializeField]
        private LocationData m_afterBattleOverworldLosePosition;
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
                PlayerWin();
                Debug.Log("Army Battle Scenario: Player Won");
            }
            else
            {
                PlayerLose();
                Debug.Log("Army Battle Scenario: Player Lost");
            }
        }
        [Button]
        public void PlayerWin()
        {
            m_RewardGiver.GiveReward();
            GameSystem.LoadZone(GameMode.Underworld, m_afterBattleOverworldWinPosition.sceneInfo, true);
        }
        [Button]
        public void PlayerLose()
        {
            GameSystem.LoadZone(GameMode.Underworld, m_afterBattleOverworldLosePosition.sceneInfo, true);
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
