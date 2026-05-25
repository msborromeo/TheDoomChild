using DChild.Gameplay.UI.Alerts;
using DChild.Gameplay;
using DChild.Codex.Lore;


namespace DChild.Menu.Codex.Lore.Alerts
{
    public class LoreCodexIndexAlertUI : UIAlertIconElement<LoreCodexIndexButton>
    {
        public override bool HasAlert()
        {
            return GameplaySystem.gamplayUIHandle.alertManager.loreAlerts.HasNewNotification(m_reference.data);
        }
        public override void RenderAlertUseless()
        {
            GameplaySystem.gamplayUIHandle.alertManager.loreAlerts.RecordNewNotification(m_reference.data, false);
            base.RenderAlertUseless();
        }

        protected override void ConnectToDataUI() => UpdateState();

    }
}
