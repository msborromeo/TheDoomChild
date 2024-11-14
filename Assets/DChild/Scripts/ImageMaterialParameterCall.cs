using DChild.Gameplay;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DChild
{
    public class ImageMaterialParameterCall : MonoBehaviour
    {
        [SerializeField]
        private string m_parameter;
        [SerializeField]
        private Image[] m_renderers;

        private int m_shaderID;
        private bool m_isInitialized;
        private float m_lerpSpeed;

        public void SetTargetParameter(string parameter)
        {
            m_parameter = parameter;
            m_shaderID = Shader.PropertyToID(m_parameter);
        }

        public void SetLerpSpeed(float speed) => m_lerpSpeed = speed;

        public void SetValue(bool value)
        {
            Initialize();
            SetPropertyBlock((Material materialPropertyBlock) => { materialPropertyBlock.SetInt(m_shaderID, value ? 1 : 0); });
        }

        public void SetValue(float value)
        {
            Initialize();
            SetPropertyBlock((Material materialPropertyBlock) => { materialPropertyBlock.SetFloat(m_shaderID, value); });
        }

        public void LerpValue(float toValue)
        {
            Initialize();
            StartCoroutine(LerpRoutine(m_shaderID, toValue, m_lerpSpeed));
        }

        private void SetPropertyBlock(Action<Material> action)
        {
            for (int i = 0; i < m_renderers.Length; i++)
            {
                var material = m_renderers[i].materialForRendering;
                action?.Invoke(material);
            }
        }

        private void Initialize()
        {
            if (m_isInitialized == false)
            {
                if (m_parameter != string.Empty)
                {
                    m_shaderID = Shader.PropertyToID(m_parameter);
                }
                m_isInitialized = true;
            }
        }

        private IEnumerator LerpRoutine(int shaderID, float destinationValue, float speed)
        {
            var lerpValue = 0f;
            var originValue = new float[m_renderers.Length];
            for (int i = 0; i < m_renderers.Length; i++)
            {
                originValue[i] = m_renderers[i].materialForRendering.GetFloat(shaderID);
            }


            var value = 0f;
            Action<Material> action = (Material materialPropertyBlock) => { materialPropertyBlock.SetFloat(shaderID, value); };
            do
            {
                lerpValue += speed * GameplaySystem.time.deltaTime;
                for (int i = 0; i < m_renderers.Length; i++)
                {
                    var material = m_renderers[i].materialForRendering;
                    value = Mathf.Lerp(originValue[i], destinationValue, lerpValue);
                    action?.Invoke(material);
                }
                yield return null;
            } while (lerpValue < 1);
        }

        private void Awake()
        {
            Initialize();
        }
    }
}