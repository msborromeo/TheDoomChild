using Doozy.Runtime.UIManager.Containers;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.Characters.Enemies
{
    public class CinderBoltHeatGaugeBarUI : MonoBehaviour
    {
        [SerializeField]
        private Image[] m_fills;
        [SerializeField]
        private Slider m_fillEdge;
        [SerializeField]
        private Canvas m_fillEdgeGroup;

        [SerializeField]
        private UIContainer m_heatIncreaseIndiciator;
        [SerializeField]
        private UIContainer m_heatDecreaseIndicator;

        [SerializeField]
        private Image m_flashBar;

        [SerializeField]
        private float m_syncDelay;
        [SerializeField]
        private float m_syncDuration;

        public void IncreaseBar(float percentValue)
        {
            ResetIndicators();
            m_heatIncreaseIndiciator?.Show();
            StartCoroutine(IncreaseBarRoutine(percentValue));
        }

        public void DecreaseBar(float percentValue)
        {
            ResetIndicators();
            m_heatDecreaseIndicator?.Show();
            StartCoroutine(DecreaseBarRoutine(percentValue));
        }

        public void Reset()
        {
            SetFillValue(0);
            m_flashBar.fillAmount = 0;
        }

        private IEnumerator IncreaseBarRoutine(float percentValue)
        {
            var startValue = m_fillEdge.value;
            var lerpSpeed = Mathf.Abs(startValue - percentValue) / m_syncDuration;
            m_flashBar.fillAmount = percentValue;
            yield return new WaitForSeconds(m_syncDelay);
            yield return LerpFills(startValue, percentValue, lerpSpeed, SetFillValue);
        }

        private IEnumerator DecreaseBarRoutine(float percentValue)
        {
            var startValue = m_fillEdge.value;
            var lerpSpeed = Mathf.Abs(startValue - percentValue) / m_syncDuration;
            SetFillValue(percentValue);
            yield return new WaitForSeconds(m_syncDelay);
            yield return LerpFills(startValue, percentValue, lerpSpeed, SetFlashBarAmount);

            void SetFlashBarAmount(float value)
            {
                m_flashBar.fillAmount = value;
            }
        }

        private IEnumerator LerpFills(float start, float end, float lerpSpeed, Action<float> OnUpdate)
        {
            float lerpTime = 0;
            do
            {
                lerpTime += Time.deltaTime * lerpSpeed;
                var value = Mathf.Lerp(start, end, lerpTime);
                OnUpdate(value);
                yield return null;
            } while (lerpTime < 1);

        }

        private void SetFillValue(float percentValue)
        {
            for (int i = 0; i < m_fills.Length; i++)
            {
                m_fills[i].fillAmount = percentValue;
            }

            m_fillEdge.value = percentValue;
            m_fillEdgeGroup.enabled = percentValue > 0;
        }

        private void ResetIndicators()
        {
            StopAllCoroutines();
            m_heatIncreaseIndiciator?.InstantHide();
            m_heatDecreaseIndicator?.InstantHide();
        }
    }


}