using DChild.Gameplay.UI.Alerts;
using DChild.Gameplay;
using DChild.Codex.Tutorial;
namespace DChild.Menu.Codex.Tutorials.Alerts
{
    public class TutorialCodexIndexAlertUI : UIAlertIconElement<TutorialCodexIndexButton>
    {
        public override bool HasAlert()
        {
            return GameplaySystem.gamplayUIHandle.alertManager.tutorialAlerts.HasNewNotification(m_reference.data);
        }
        public override void RenderAlertUseless()
        {
            GameplaySystem.gamplayUIHandle.alertManager.tutorialAlerts.RecordNewNotification(m_reference.data, false);
            base.RenderAlertUseless();
        }

        protected override void ConnectToDataUI() => UpdateState();

    }
}