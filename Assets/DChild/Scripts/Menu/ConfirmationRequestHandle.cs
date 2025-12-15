using Doozy.Runtime.Common.Extensions;
using Doozy.Runtime.UIManager.Containers;
using Holysoft.Event;
using UnityEngine;

namespace DChild.Menu
{
    [System.Serializable]
    public class ConfirmationRequestHandle
    {
        [SerializeField]
        private ConfirmationHandler m_confirmation;
        [SerializeField]
        private string m_confirmationHeader;
        [SerializeField]
        private string m_confirmationMessage;

        public void Execute(EventAction<EventActionArgs> OnAffirmation)
        {
            m_confirmation.RequestConfirmation(OnAffirmation, m_confirmationHeader, m_confirmationMessage);
        }

        //Below Functions are Hack Function and should be revised
        public void ShowView()
        {
            m_confirmation.GetComponent<UIView>().Show();
        }

        public void HideView()
        {
            m_confirmation.GetComponent<UIView>().Show();
        }

        public void ChangeMessage(string message, string header = "")
        {
            if (!header.IsNullOrEmpty())
                m_confirmationHeader = header;

            m_confirmationMessage = message;
        }

        public void ChangeHandler(ConfirmationHandler handler)
        {
            m_confirmation = handler;
        }
    }

}