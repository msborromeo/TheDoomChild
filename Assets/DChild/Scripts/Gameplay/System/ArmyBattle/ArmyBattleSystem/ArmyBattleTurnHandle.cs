using DChild.Gameplay.ArmyBattle.SpecialSkills;
using DChild.Gameplay.ArmyBattle.Visualizer;
using Holysoft.Event;
using Sirenix.OdinInspector;
using System;
using System.Collections;
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
        private ArmyBattleSpecialSkillHandle m_skillHandle;

        [SerializeField]
        private ArmyBattleCombatSimulator m_combatSimulator;
        [SerializeField]
        private ArmyFightManager m_fightManager;

        private ArmyController m_player;
        private ArmyController m_enemy;

        public event EventAction<EventActionArgs> OnTurnStart;
        public event EventAction<EventActionArgs> OnTurnEnd;

        private bool m_hasTurnCommenced;
        private bool m_hasTurnIsSettingUp;

        [ShowInInspector, DisableInPlayMode, HideInEditorMode]
        private int m_turnCount;

        public int currentTurn => m_turnCount;

        [Button]
        public void TurnStart()
        {
            if (m_hasTurnIsSettingUp)
                return;

            StartCoroutine(TurnStartRoutine());

            if (configuration.turnWillProgress)
            {
                m_turnCount++;
            }
            ResetConfiguration();
            
        }

        [Button]
        public void CommenceTurn()
        {
            if (m_hasTurnCommenced == false)
            {
                StartCoroutine(TurnRoutine());
            }
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
            m_hasTurnCommenced = false;
            EndTurn();
            Debug.Log("Turn End");
        }

        private IEnumerator TurnStartRoutine()
        {
            m_hasTurnIsSettingUp = true;

            yield return m_skillHandle.ApplyWaitingSkillsRoutine();
            OnTurnStart?.Invoke(this, EventActionArgs.Empty);

            m_hasTurnIsSettingUp = false;
        }

        private IEnumerator TurnRoutine()
        {
            m_hasTurnCommenced = true;

            yield return m_skillHandle.ApplyTurnSpecialSkillsRoutine();
            // Wait for Scenarios ******************


            var playerTurn = m_player.GetTurnAction(m_turnCount);
            playerTurn.willAttack = configuration.playerWillAttack;

            var enemyTurn = m_enemy.GetTurnAction(m_turnCount);
            enemyTurn.willAttack = configuration.enemyWillAttack;

            var result = m_combatSimulator.CalculateCombatResult(playerTurn, enemyTurn);
            m_player.controlledArmy.SubtractTroopCount(result.player.damageReceived);
            m_enemy.controlledArmy.SubtractTroopCount(result.enemy.damageReceived);
            m_fightManager.VisualizeCombat(result);
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