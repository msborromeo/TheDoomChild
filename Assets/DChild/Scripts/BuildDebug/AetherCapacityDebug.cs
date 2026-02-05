using DChild.Gameplay;
using Holysoft.Event;
using TMPro;
using UnityEngine;

namespace DChildDebug.Window
{
    public class AetherCapacityDebug : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_maxValue;

        public void IncrementMaxValue()
        {
            GameplaySystem.playerManager.player.soulSkills.AddMaxCapacity(1);
        }

        public void DecrementMaxValue()
        {
            var handle = GameplaySystem.playerManager.player.soulSkills;
            var value = Mathf.Max(0, handle.maxSoulCapacity - 1);
            handle.SetMaxCapacity(value);
        }

        private void OnCapacityChange(object sender, EventActionArgs eventArgs)
        {
            m_maxValue.text = GameplaySystem.playerManager.player.soulSkills.maxSoulCapacity.ToString();
        }

        public void IncreaseCurrentSoulCapacity()
        {
            GameplaySystem.playerManager.player.soulSkills.AddSoulSkillEnergyPoint(1);
        }

        public void DecreaseCurrentSoulCapacity()
        {
            GameplaySystem.playerManager.player.soulSkills.AddSoulSkillEnergyPoint(-1);
        }

        private void Awake()
        {
            var soulSkills = GameplaySystem.playerManager.player.soulSkills;
            soulSkills.MaxCapacityChanged += OnCapacityChange;
            m_maxValue.text = soulSkills.maxSoulCapacity.ToString();
        }

        private void OnDisable()
        {
            var soulSkills = GameplaySystem.playerManager.player.soulSkills;
            soulSkills.MaxCapacityChanged -= OnCapacityChange;
        }


    }
}
