using DChild.Gameplay.UI.Alerts;
using DChild.Gameplay;
namespace DChild.Menu.Codex.ArmyTroops.Alerts
{
    public class ArmyTroopsCodexAlertDataObserver : UIAlertDataObserver
    {
        public override bool HasAlert() => GameplaySystem.gamplayUIHandle.alertManager.armyTroopAlerts.HasAnyNewNotification();

    }
}