using Doozy.Runtime.UIManager.Containers;
using Holysoft.Event;
using UnityEngine;

namespace DChild.Gameplay.UI.Alerts
{
    public abstract class UIAlertIconBase : UIAlertElement
    {
        [SerializeField]
        private UIContainer m_iconContainer;


        public event EventAction<EventActionArgs> RenderedUseless;

        public void UpdateState()
        {
            hasAlert = HasAlert();
            if (hasAlert)
            {
                ShowIcon();
            }
            else
            {
                HideIcon();
            }
        }

        protected void InvokeRenderedUseless()
        {
            RenderedUseless?.Invoke(this,EventActionArgs.Empty);
        }

        protected void ShowIcon()
        {
            m_iconContainer.Show();
        }

        protected void HideIcon()
        {
            m_iconContainer.Hide();
        }
    }
}
