using DChild.Gameplay;
using DChild.Gameplay.UI;
using Holysoft.Event;
using UnityEngine;

namespace DChild.Gameplay.UI.Alerts
{
    public abstract class UIAlertDataObserver : UIAlertElement
    {
        [SerializeField]
        private UIAlertIconBase[] m_toObserve;

        protected UIAlertManager UIAlertManager => GameplaySystem.gamplayUIHandle.alertManager;
        private void OnAlertRenderedUseless(object sender, EventActionArgs eventArgs)
        {
            hasAlert = HasAlert();
        }

        private void Awake()
        {
            for (int i = 0; i < m_toObserve.Length; i++)
            {
                m_toObserve[i].RenderedUseless += OnAlertRenderedUseless;
            }
        }

    }
}
