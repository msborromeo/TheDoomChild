using Doozy.Runtime.UIManager.Listeners;
using UnityEngine;

namespace DChild.UI
{
    public class GameplayUIStateSignalHandler : MonoBehaviour
    {
        [SerializeField] private GameplayUIStateSignalGroup m_parentGroup;

        private SignalListener m_listener;

        private void EnsureReference()
        {
            m_listener = GetComponent<SignalListener>();
        }

        public void HandleSignalValue()
        {
            EnsureReference();
            m_listener.stream.currentSignal.TryGetValue(out bool value);

            if (!value)
                return;


            m_parentGroup.OnSignalValueReceived.Invoke(value);
            m_parentGroup.UpdateCurrentState();
        }
    }
}

