using DChild.Gameplay;
using DChild.Gameplay.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Gameplay.UI.Alerts
{
    public abstract class UIAlertIconElement<T> : UIAlertIconBase where T : MonoBehaviour
    {
        protected T m_reference;

        protected UIAlertManager UIAlertManager => GameplaySystem.gamplayUIHandle.alertManager;

        protected abstract void ConnectToDataUI();

        [Button]
        public virtual void RenderAlertUseless()
        {
            hasAlert = false;
            InvokeRenderedUseless();
            HideIcon();
        }

        private void Awake()
        {
            m_reference = GetComponentInParent<T>();
            ConnectToDataUI();
        }
    }
}
