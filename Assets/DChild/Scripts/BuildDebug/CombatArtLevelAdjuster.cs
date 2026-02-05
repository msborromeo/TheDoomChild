using DChild.Gameplay.Characters.Player.CombatArt.Leveling;
using Holysoft.Event;
using System;
using UnityEngine;

namespace DChildDebug.Window
{
    public class CombatArtLevelAdjuster : MonoBehaviour, ITrackableValue
    {
        [SerializeField]
        private CombatArtLevel m_level;

        public float value => m_level.currentLevel;

        public event EventAction<EventActionArgs> ValueChange;

        public void ForceLevelUp()
        {
            m_level.exp.AddCurrentValue(m_level.exp.maxValue);
        }
        private void OnValueChange(object sender, StatInfoEventArgs eventArgs)
        {
            ValueChange?.Invoke(this, EventActionArgs.Empty);
        }

        private void Awake()
        {
            m_level.exp.ValueChanged += OnValueChange;
        }

        private void OnDisable()
        {
            m_level.exp.ValueChanged -= OnValueChange;
        }
    }
}
