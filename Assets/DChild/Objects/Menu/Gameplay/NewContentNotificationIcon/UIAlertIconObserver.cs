using Holysoft.Event;
using UnityEngine;

namespace DChild.Gameplay.UI.Alerts
{
    public class UIAlertIconObserver : UIAlertIconBase
    {
        [SerializeField]
        private UIAlertElement[] m_toObserve;

        public override bool HasAlert()
        {
            for (int i = 0; i < m_toObserve.Length; i++)
            {
                if (m_toObserve[i].HasAlert())
                    return true;
            }

            return false;
        }
        private void OnStateChange(object sender, EventActionArgs eventArgs)
        {
            hasAlert = HasAlert();
        }

        private void Awake()
        {
            for (int i = 0; i < m_toObserve.Length; i++)
            {
                m_toObserve[i].StateChange += OnStateChange;
            }
        }
    }
}
