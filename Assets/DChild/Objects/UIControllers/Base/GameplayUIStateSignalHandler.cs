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
            var signal = m_listener.stream.currentSignal;
            signal.TryGetValue(out bool value);

            //specifically for inDialogue Observer value
            if (signal.stream.category == "Dialogue" && signal.stream.name == "Toggle")
                m_parentGroup.DialogueSignalValueReceived.Invoke(value);

            if (!value)
                return;

            m_parentGroup.UpdateCurrentState();
        }
    }
}

