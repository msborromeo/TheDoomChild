using System;
using Holysoft.Event;
using UnityEngine;

namespace DChild.Menu
{
    public class ConfirmationHandler : MonoBehaviour
    {
        [SerializeField]
        private ConfirmationWindow m_confirmationWindow;
        private EventAction<EventActionArgs> m_listener;
        private EventAction<EventActionArgs> m_declineListener;
        private bool m_isListenerSubscribed;

        public ConfirmationWindow window => m_confirmationWindow;

        public void RequestConfirmation(EventAction<EventActionArgs> listener, string messageHeader, string message, bool noMessage = false, EventAction<EventActionArgs> OnDecline = null)
        {
            m_listener = listener;
            m_declineListener = OnDecline;
            
            // m_confirmationWindow.RequestAffirmed += m_listener;
            m_isListenerSubscribed = true;
            if(noMessage)
            {
                return;
            }
            m_confirmationWindow.SetMessage(messageHeader, message);
        }

        private void OnAffirm(object sender, EventActionArgs eventArgs)
        {
            if (m_listener == null)
                return;

            m_listener?.Invoke(this, EventActionArgs.Empty);
            UnsubcribeListener();
        }

        private void OnDecline(object sender, EventActionArgs eventArgs)
        {
            if (m_declineListener == null)
                return;

            m_declineListener?.Invoke(this, EventActionArgs.Empty);
            UnsubcribeListener();
        }

        public void UnsubcribeListener()
        {
            if (m_isListenerSubscribed && m_listener != null)
            {
                //m_confirmationWindow.RequestAffirmed -= m_listener;
                m_listener = null;
                m_declineListener = null;
                m_isListenerSubscribed = false;
            }
        }

        private void OnConfirmationHide(object sender, EventActionArgs eventArgs)
        {
            if (m_isListenerSubscribed)
            {
                UnsubcribeListener();
            }
        }

        private void Awake()
        {
            m_confirmationWindow.RequestAffirmed += OnAffirm;
            m_confirmationWindow.RequestDeclined += OnDecline;
        }

        private void OnDisable()
        {
            m_confirmationWindow.RequestAffirmed -= OnAffirm;
            m_confirmationWindow.RequestDeclined -= OnDecline;
        }
    }

}