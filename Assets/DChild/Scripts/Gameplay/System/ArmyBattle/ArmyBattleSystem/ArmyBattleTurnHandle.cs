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
            public bool playerCanUseMelee;
            public bool playerCanUseMagic;
            public bool playerCanUseRange;

            public bool enemyCanUseMelee;
            public bool enemyCanUseMagic;
            public bool enemyCanUseRange;

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

        public void ForceSetTurnNumber(int turnNumber)
        {
            m_turnCount = turnNumber;
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


            //var playerTurn = m_player.GetTurnAction(m_turnCount);
            //playerTurn.willAttack = configuration.playerWillAttack;

            //var enemyTurn = m_enemy.GetTurnAction(m_turnCount);
            //enemyTurn.willAttack = configuration.enemyWillAttack;


            var playerTurn = ArmyTurnConfiguration(m_player, m_turnCount);
            var enemyTurn = ArmyTurnConfiguration(m_enemy, m_turnCount);

            var result = m_combatSimulator.CalculateCombatResult(playerTurn, enemyTurn);
            m_player.controlledArmy.SubtractTroopCount(result.player.damageReceived);
            m_enemy.controlledArmy.SubtractTroopCount(result.enemy.damageReceived);
            m_fightManager.VisualizeCombat(result);
        }
        private ArmyTurnAction ArmyTurnConfiguration(ArmyController participant, int turnCount)
        {
            ArmyTurnAction turnAction = new ArmyTurnAction();
            if (participant == m_player)
            {
                var playerTurn = m_player.GetTurnAction(turnCount);
                playerTurn.willAttack = configuration.playerWillAttack;
                if (playerTurn.willAttack)
                {
                    switch (playerTurn.attack.type)
                    {
                        case DamageType.Melee:
                            playerTurn.willAttack = configuration.playerCanUseMelee;
                            break;
                        case DamageType.Range:
                            playerTurn.willAttack = configuration.playerCanUseRange;
                            break;
                        case DamageType.Magic:
                            playerTurn.willAttack = configuration.playerCanUseMagic;
                            break;

                    }
                }
                turnAction = playerTurn;
            }
            else if (participant == m_enemy)
            {
                var enemyTurn = m_enemy.GetTurnAction(m_turnCount);
                enemyTurn.willAttack = configuration.enemyWillAttack;
                if (enemyTurn.willAttack)
                {
                    switch (enemyTurn.attack.type)
                    {
                        case DamageType.Melee:
                            enemyTurn.willAttack = configuration.enemyCanUseMelee;
                            break;
                        case DamageType.Range:
                            enemyTurn.willAttack = configuration.enemyCanUseRange;
                            break;
                        case DamageType.Magic:
                            enemyTurn.willAttack = configuration.enemyCanUseMagic;
                            break;
                    }
                }
                turnAction = enemyTurn;
            }


            return turnAction;
        }

        [Button]
        private void EndTurn()
        {
            OnTurnEnd?.Invoke(this, EventActionArgs.Empty);
        }

        private void ResetConfiguration()
        {

            configuration.playerCanUseMelee = true;
            configuration.playerCanUseRange = true;
            configuration.playerCanUseMagic = true;

            configuration.enemyCanUseMelee = true;
            configuration.enemyCanUseRange = true;
            configuration.enemyCanUseMagic = true;

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