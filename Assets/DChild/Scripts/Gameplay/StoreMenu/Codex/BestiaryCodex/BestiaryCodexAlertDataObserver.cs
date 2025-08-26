using DChild.Gameplay.UI.Alerts;
using DChild.Gameplay;

namespace DChild.Menu.Codex.Bestiary.Alerts
{
    public class BestiaryCodexAlertDataObserver : UIAlertDataObserver
    {
        public override bool HasAlert() => GameplaySystem.gamplayUIHandle.alertManager.bestiaryAlerts.HasAnyNewNotification();
    }
}