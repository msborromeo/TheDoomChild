using DChild.Gameplay.UI.Alerts;
using DChild.Gameplay;

namespace DChild.Menu.Codex.Locations.Alerts
{
    public class LocationCodexAlertDataObserver : UIAlertDataObserver
    {
        public override bool HasAlert() => GameplaySystem.gamplayUIHandle.alertManager.locationAlerts.HasAnyNewNotification();
    }
}