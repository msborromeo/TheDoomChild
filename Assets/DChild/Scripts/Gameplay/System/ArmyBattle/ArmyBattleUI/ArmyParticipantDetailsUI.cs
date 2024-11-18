using TMPro;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI
{
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

        private string CheckNegativeTroops(ArmyController army)
        {
            return army.controlledArmy.troopCount > -1 ? $"{army.controlledArmy.troopCount}" : "0";
        }
    }
}