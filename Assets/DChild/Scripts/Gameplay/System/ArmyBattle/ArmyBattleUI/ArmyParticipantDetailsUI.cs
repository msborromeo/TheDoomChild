using Holysoft.Event;
using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI
{
    public struct ReceivedTurnDamageArgs : IEventActionArgs
    {
        public ReceivedTurnDamageArgs(ArmyController player, ArmyController enemy, int receivedPlayerDamage, int receivedEnemyDamage)
        {
            this.player = player;
            this.enemy = enemy;
            this.receivedPlayerDamage = receivedPlayerDamage;
            this.receivedEnemyDamage = receivedEnemyDamage;
        }

        public ArmyController player { get; }
        public ArmyController enemy { get; }
        public int receivedPlayerDamage { get; }
        public int receivedEnemyDamage { get; }
    }

    public class ArmyParticipantDetailsUI : MonoBehaviour
    {
        [SerializeField]
        private ArmyBannerUI m_playerBanner;
        [SerializeField]
        private TextMeshProUGUI m_playerPower;
        [SerializeField]
        private ArmyBannerUI m_enemyBanner;
        [SerializeField]
        private TextMeshProUGUI m_enemyPower;

        public void Display(ArmyController player, ArmyController enemy)
        {
            m_playerBanner.Display(player.controlledArmy.overview);
            m_enemyBanner.Display(enemy.controlledArmy.overview);

            UpdateTroopCount(player, enemy);
        }

        public void UpdateTroopCount(ArmyController player, ArmyController enemy)
        {
            m_playerPower.text = CheckNegativeTroops(player);
            m_enemyPower.text = CheckNegativeTroops(enemy);
        }

        public void OnExecuteAttack(object sender, ReceivedTurnDamageArgs eventArgs)
        {
            StartCoroutine(AnimateDamageReduction(eventArgs));
        }

        public IEnumerator AnimateDamageReduction(ReceivedTurnDamageArgs eventArgs)
        {
            var playerDamage = eventArgs.receivedPlayerDamage;
            var enemyDamage = eventArgs.receivedEnemyDamage;


            yield return new WaitForSeconds(3);
            m_playerBanner.DisplayReceivedDamage(playerDamage);
            m_enemyBanner.DisplayReceivedDamage(enemyDamage);

            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(1);

            StartCoroutine(DecreaseArmyHealth(m_playerBanner, m_playerPower, playerDamage));
            StartCoroutine(DecreaseArmyHealth(m_enemyBanner, m_enemyPower, enemyDamage));
        }

        private IEnumerator DecreaseArmyHealth(ArmyBannerUI armyBanner, TextMeshProUGUI powerPanel, int damageCounter)
        {
            for (; damageCounter > 0; damageCounter -= 5)
            {
                if (powerPanel != null && !string.IsNullOrEmpty(powerPanel.text))
                {
                    var currentHealth = Int32.Parse(powerPanel.text) - 5;
                    powerPanel.text = currentHealth > 0 ? $"{currentHealth}" : "0";
                }
                yield return new WaitForEndOfFrame();
            }

            armyBanner.DisplayReceivedDamage(damageCounter, true);
            yield return null;
        }

        private string CheckNegativeTroops(ArmyController army)
        {
            return army.controlledArmy.troopCount > -1 ? $"{army.controlledArmy.troopCount}" : "0";
        }
    }
}