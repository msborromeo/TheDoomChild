using System;
using UnityEngine;

namespace DChild.UI
{
    public class GameplayUIStateSignalGroup : MonoBehaviour
    {
        [SerializeField] private GameplayUIState m_categoryState;

        public Action<GameplayUIState> OnSignalReceived;
        public Action<bool> OnSignalValueReceived;

        public void UpdateCurrentState()
        {
            OnSignalReceived?.Invoke(m_categoryState);
        }
    }
}

