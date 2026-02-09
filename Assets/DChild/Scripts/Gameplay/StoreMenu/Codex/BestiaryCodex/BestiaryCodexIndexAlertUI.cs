using DChild.Menu.Bestiary;
using DChild.Gameplay.UI.Alerts;
using DChild.Gameplay;

namespace DChild.Menu.Codex.Bestiary.Alerts
{
    public class BestiaryCodexIndexAlertUI : UIAlertIconElement<BestiaryCodexIndexButton>
    {
        public override bool HasAlert()
        {
            return GameplaySystem.gamplayUIHandle.alertManager.bestiaryAlerts.HasNewNotification(m_reference.data);
        }

        public override void RenderAlertUseless()
        {
            GameplaySystem.gamplayUIHandle.alertManager.bestiaryAlerts.RecordNewNotification(m_reference.data, false);
            base.RenderAlertUseless();
        }

        protected override void ConnectToDataUI()
        {
            m_reference.OnBestiaryDataChanged += OnDataChanged;
        }

        private void OnDataChanged(BestiaryData data)
        {
            UpdateState();
        }

        private void OnDisable()
        {
            m_reference.OnBestiaryDataChanged -= OnDataChanged;
        }
    }
}