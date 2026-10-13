using Doozy.Runtime.UIManager.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.UI
{
    public abstract class ReferenceSlider : MonoBehaviour
    {
        [SerializeField]
        protected UISlider m_slider;
        protected abstract float value { get; set; }

        protected virtual void OnValueChange(float arg0) => value = arg0;

        private void Awake() => m_slider = GetComponentInChildren<UISlider>();

#if UNITY_EDITOR
        [SerializeField]
        [HideInInspector]
        private bool m_instantiated;

#endif
        private void OnValidate()
        {
#if UNITY_EDITOR
            if (m_instantiated == false)
            {
                if (m_slider == null)
                {
                    m_slider = GetComponentInChildren<UISlider>();
                }
                if (m_slider != null)
                {
                    m_instantiated = true;
                }
            }
#endif
        }
    }
}