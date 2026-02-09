using DChild.Gameplay.ArmyBattle.SpecialSkills;
using DChild.Gameplay.ArmyBattle.UI;
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
        public struct ParticipantConfiguration
        {
            public bool canUseMelee;
            public bool canUseMagic;
            public bool canUseRange;

            public bool willAttack;

            public void Reset()
            {
                canUseMelee = true;
                canUseMagic = true;
                canUseRange = true;

                willAttack = true;
            }
        }

        [System.Serializable]
        public struct TurnConfiguration
        {
            public ParticipantConfiguration playerConfiguration;
            public ParticipantConfiguration enemyConfiguration;
            public bool turnWillProgress;

            public void Reset()
            {
                playerConfiguration.Reset();
                enemyConfiguration.Reset();
                turnWillProgress = true;
            }
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

        public event EventAction<ReceivedTurnDamageArgs> OnExecuteAttack;

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

        public void ForceEndTurn()
        {
            StopAllCoroutines();
            OnFightEnd(this, EventActionArgs.Empty);
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

            var playerTurn = GenerateTurnAction(m_player, m_turnCount);
            var enemyTurn = GenerateTurnAction(m_enemy, m_turnCount);

            var result = m_combatSimulator.CalculateCombatResult(playerTurn, enemyTurn);

            m_player.controlledArmy.SubtractTroopCount(result.player.damageReceived);
            m_enemy.controlledArmy.SubtractTroopCount(result.enemy.damageReceived);

            m_fightManager.VisualizeCombat(result);

            OnExecuteAttack?.Invoke(this, new ReceivedTurnDamageArgs(m_player, m_enemy, result.player.damageReceived, result.enemy.damageReceived));

        }

        private ArmyTurnAction ConfigureParticipantTurnAction(ArmyTurnAction turnAction, ParticipantConfiguration configuration)
        {
            turnAction.willAttack = configuration.willAttack;
            if (turnAction.willAttack)
            {
                switch (turnAction.attack.type)
                {
                    case DamageType.Melee:
                        turnAction.willAttack = configuration.canUseMelee;
                        break;
                    case DamageType.Range:
                        turnAction.willAttack = configuration.canUseRange;
                        break;
                    case DamageType.Magic:
                        turnAction.willAttack = configuration.canUseMagic;
                        break;

                }
            }
            return turnAction;
        }

        private ArmyTurnAction GenerateTurnAction(ArmyController participant, int turnCount)
        {
            ArmyTurnAction turnAction = new ArmyTurnAction();
            if (participant == m_player)
            {
                var playerTurn = m_player.GetTurnAction(turnCount);
                turnAction = ConfigureParticipantTurnAction(playerTurn, configuration.playerConfiguration);
            }
            else if (participant == m_enemy)
            {
                var enemyTurn = m_enemy.GetTurnAction(m_turnCount);
                turnAction = ConfigureParticipantTurnAction(enemyTurn, configuration.enemyConfiguration);
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
            configuration.Reset();
        }

        private void Awake()
        {
            ResetConfiguration();

            m_fightManager.OnFightEnd += OnFightEnd;
        }

        private void OnDisable()
        {
            m_fightManager.OnFightEnd -= OnFightEnd;
        }
    }
}