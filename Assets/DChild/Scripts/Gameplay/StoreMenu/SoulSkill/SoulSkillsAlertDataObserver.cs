using DChild.Gameplay.Characters.Players.SoulSkills;
using DChild.Gameplay.UI.Alerts;

namespace DChild.Gameplay.UI.SoulSkills.Alerts
{
    public class SoulSkillsAlertDataObserver : UIAlertDataObserver
    {
        public override bool HasAlert() => GameplaySystem.gamplayUIHandle.alertManager.soulSkillAlerts.HasAnyNewNotification();

    }
}