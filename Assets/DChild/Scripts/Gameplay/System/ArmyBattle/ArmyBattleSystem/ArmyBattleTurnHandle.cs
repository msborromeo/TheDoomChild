using DChild.Gameplay.ArmyBattle.Visualizer;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle
{
    public class ArmyBattleTurnHandle : MonoBehaviour
    {
        [System.Serializable]
        public struct TurnConfiguration
        {
            public bool playerWillAttack;
            public bool enemyWillAttack;
            public bool turnWillProgress;
        }

        public TurnConfiguration configuration;

        [SerializeField]
        private ArmyBattleCombatSimulator m_combatSimulator;
        [SerializeField]
        private ArmyFightManager m_fightManager;

        private ArmyController m_player;
        private ArmyController m_enemy;

        public event EventAction<EventActionArgs> OnTurnStart;
        public event EventAction<EventActionArgs> OnTurnEnd;

        [ShowInInspector, DisableInPlayMode, HideInEditorMode]
        private int m_turnCount;

        public int currentTurn => m_turnCount;

        [Button]
        public void TurnStart()
        {
            if (configuration.turnWillProgress)
            {
                m_turnCount++;
            }
            ResetConfiguration();
            OnTurnStart?.Invoke(this, EventActionArgs.Empty);
        }

        [Button]
        public void CommenceTurn()
        {
            var playerTurn = m_player.GetTurnAction(m_turnCount);
            playerTurn.willAttack = configuration.playerWillAttack;

            var enemyTurn = m_enemy.GetTurnAction(m_turnCount);
            enemyTurn.willAttack = configuration.enemyWillAttack;

            var result = m_combatSimulator.CalculateCombatResult(playerTurn, enemyTurn);
            m_player.controlledArmy.SubtractTroopCount(result.player.damageReceived);
            m_enemy.controlledArmy.SubtractTroopCount(result.enemy.damageReceived);
            m_fightManager.VisualizeCombat(result);
        }

        public void SetParticipants(ArmyController player, ArmyController enemy)
        {
            m_player = player;
            m_enemy = enemy;
        }

        private void OnFightEnd(object sender, EventActionArgs eventArgs)
        {
            m_player.CleanUpForNextTurn();
            m_enemy.CleanUpForNextTurn();
            EndTurn();

            Debug.Log("Turn End");
        }

        [Button]
        private void EndTurn()
        {
            OnTurnEnd?.Invoke(this, EventActionArgs.Empty);
        }

        private void ResetConfiguration()
        {
            configuration.turnWillProgress = true;
            configuration.enemyWillAttack = true;
            configuration.playerWillAttack = true;
        }

        private void Awake()
        {
            ResetConfiguration();
            m_fightManager.OnFightEnd += OnFightEnd;
        }
    }
}