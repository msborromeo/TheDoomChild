using DChild.Gameplay.ArmyBattle;
using System;
using TMPro;
using UnityEngine;

namespace DChild.Scripts.Gameplay.Systems.ArmyBattle.ArmyBattleUI
{
    public class ArmyHealthUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_armyPower;
        public void MonitorHealth(Army army)
        {
            //Set Current UI to Count without anim
            army.OnTroopCountChange += OnTroopCountChange;
        }

        private void OnTroopCountChange(object sender, Army.TroopCountChangeEventArgs eventArgs)
        {
            m_armyPower.text = $"{eventArgs.currentTroopCount}";
        }
    }
}