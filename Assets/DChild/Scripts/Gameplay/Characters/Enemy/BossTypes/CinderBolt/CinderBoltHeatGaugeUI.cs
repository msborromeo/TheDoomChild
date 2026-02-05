using Doozy.Runtime.UIManager.Containers;
using Holysoft.Event;
using UnityEngine;

namespace DChild.Gameplay.Characters.Enemies
{
    public class CinderBoltHeatGaugeUI : MonoBehaviour
    {
        [SerializeField]
        private CinderBoltHeatGauge m_refrence;


        [SerializeField]
        private UIContainer m_fullHeatIndicator;

        [SerializeField]
        private CinderBoltHeatGaugeBarUI[] m_bars;

        private int m_cachedHeatValue;
        private Transform m_originalParent;

        public void MoveToGameplayCanvas()
        {
            transform.parent = GameplaySystem.gamplayUIHandle.GetReference().m_BossHealth;
            transform.localPosition = Vector3.zero;
        }

        public void RemoveFromGameplayCanvas()
        {
            transform.SetParent(m_originalParent);
        }

        private void OnHeatFull(object sender, EventActionArgs eventArgs)
        {
            m_fullHeatIndicator?.Show();
        }

        private void OnHeatChanged(object sender, EventActionArgs eventArgs)
        {
            var currentValue = m_refrence.currentValue;
            if (m_cachedHeatValue == currentValue)
                return;

            var percentValue = (float)currentValue / m_refrence.maxValue;
            if (m_cachedHeatValue < currentValue)
            {
                for (int i = 0; i < m_bars.Length; i++)
                {
                    m_bars[i].IncreaseBar(percentValue);
                }
            }
            else
            {
                for (int i = 0; i < m_bars.Length; i++)
                {
                    m_bars[i].DecreaseBar(percentValue);
                }
            }

            if (currentValue != m_refrence.maxValue)
            {
                m_fullHeatIndicator?.Hide();
            }

            m_cachedHeatValue = currentValue;
        }

        private void Awake()
        {
            m_cachedHeatValue = 0;
            for (int i = 0; i < m_bars.Length; i++)
            {
                m_bars[i].Reset();
            }
            m_refrence.HeatChanged += OnHeatChanged;
            m_refrence.HeatFull += OnHeatFull;
            m_originalParent = transform.parent;
        }

        private void OnDisable()
        {
            m_refrence.HeatChanged -= OnHeatChanged;
            m_refrence.HeatFull -= OnHeatFull;
        }

    }


}