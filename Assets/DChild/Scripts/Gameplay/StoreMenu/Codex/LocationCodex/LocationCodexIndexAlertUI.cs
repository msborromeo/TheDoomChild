using DChild.Gameplay.UI.Alerts;
using DChild.Gameplay;
using DChild.Codex.LocationCodex;

namespace DChild.Menu.Codex.Locations.Alerts
{
    public class LocationCodexIndexAlertUI : UIAlertIconElement<LocationCodexIndexButton>
    {
        public override bool HasAlert()
        {
            return GameplaySystem.gamplayUIHandle.alertManager.locationAlerts.HasNewNotification(m_reference.data);
        }
        public override void RenderAlertUseless()
        {
            GameplaySystem.gamplayUIHandle.alertManager.locationAlerts.RecordNewNotification(m_reference.data, false);
            base.RenderAlertUseless();
        }

        protected override void ConnectToDataUI() => UpdateState();

    }
}
