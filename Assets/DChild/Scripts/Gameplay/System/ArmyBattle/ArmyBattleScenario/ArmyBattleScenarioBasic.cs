using DChild.Gameplay.Systems;
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
            ChangeScene(m_afterBattleOverworldWinPosition);
        }
        [Button]
        public void PlayerLose()
        {
            ChangeScene(m_afterBattleOverworldLosePosition);
        }
        public override void StartScenario()
        {
            m_introHandle.Execute();
        }

        public override void UpdateScenario()
        {
            m_updateHandle.UpdateScenario();
        }

        private void ChangeScene(LocationData loc)
        {
            var WorldTypeVar = FindObjectOfType<WorldTypeManager>();
            WorldTypeVar.SetCurrentWorldType(loc.location);
            switch (WorldTypeVar.CurrentWorldType)
            {
                case WorldType.Underworld:
                    GameSystem.LoadZone(GameMode.Underworld, loc.sceneInfo, true);
                    break;
                case WorldType.Overworld:
                    GameSystem.LoadZone(GameMode.Overworld, loc.sceneInfo, true);
                    break;
            }
        }
    }
}
