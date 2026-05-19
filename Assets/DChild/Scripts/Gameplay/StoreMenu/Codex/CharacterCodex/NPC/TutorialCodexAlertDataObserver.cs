using DChild.Gameplay.UI.Alerts;
using DChild.Gameplay;

namespace DChild.Menu.Codex.Tutorials.Alerts
{
    public class TutorialCodexAlertDataObserver : UIAlertDataObserver
    {
        public override bool HasAlert() => GameplaySystem.gamplayUIHandle.alertManager.tutorialAlerts.HasAnyNewNotification();
    }
}