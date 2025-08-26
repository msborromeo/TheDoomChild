using DChild.Gameplay;
using DChild.Gameplay.UI.Alerts;

namespace DChild.Menu.Codex.Quest.Alerts
{
    public class QuestCodexAlertDataObserver : UIAlertDataObserver
    {
        public override bool HasAlert() => GameplaySystem.gamplayUIHandle.alertManager.questAlerts.HasAnyNewNotification();
    }
}