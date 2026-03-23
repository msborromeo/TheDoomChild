using DChild.Gameplay.UI.Controller;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChild.UI
{
    public class UnderworldUIStateObserver : MonoBehaviour
    {
        [SerializeField]
        private UnderworldUIState m_currentUnderworldUIState;
        public UnderworldUIState currentUnderworldUIState => m_currentUnderworldUIState;

        public event Action<UnderworldUIState> UnderworldUIStateChanged;

        public void SetCurrentUnderworldUIState(int underworldUIState)
        {
            m_currentUnderworldUIState = (UnderworldUIState)underworldUIState;
            UnderworldUIStateChanged?.Invoke(m_currentUnderworldUIState);
        }
    }
}

