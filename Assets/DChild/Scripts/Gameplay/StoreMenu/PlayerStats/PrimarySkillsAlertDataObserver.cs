using DChild.Gameplay.UI.Alerts;

namespace DChild.Gameplay.UI.PrimarySkills.Alerts
{
    public class PrimarySkillsAlertDataObserver : UIAlertDataObserver
    {
        public override bool HasAlert() => GameplaySystem.gamplayUIHandle.alertManager.primarySkillAlerts.HasAnyNewNotification();
    }
}