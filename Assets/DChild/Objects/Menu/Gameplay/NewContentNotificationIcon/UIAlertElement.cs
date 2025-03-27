using Holysoft.Event;
using UnityEngine;

namespace DChild.Gameplay.UI.Alerts
{
    public abstract class UIAlertElement : MonoBehaviour
    {
        [SerializeField]
        private bool m_hasAlert;
        protected bool hasAlert
        {
            get => m_hasAlert;
            set
            {
                if (m_hasAlert == value)
                    return;

                m_hasAlert = value;
                StateChange?.Invoke(this, EventActionArgs.Empty);
            }
        }

        public abstract bool HasAlert();
        public event EventAction<EventActionArgs> StateChange;
    }
}
