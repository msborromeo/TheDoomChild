using DChild.Gameplay.UI.Alerts;
using DChild.Gameplay;
using DChild.Codex.Characters;

namespace DChild.Menu.Codex.Characters.Alerts
{
    public class CharacterCodexIndexAlertUI : UIAlertIconElement<CharacterCodexIndexButton>
    {
        public override bool HasAlert()
        {
            return GameplaySystem.gamplayUIHandle.alertManager.charactersAlerts.HasNewNotification(m_reference.data);
        }
        public override void RenderAlertUseless()
        {
            GameplaySystem.gamplayUIHandle.alertManager.charactersAlerts.RecordNewNotification(m_reference.data, false);
            base.RenderAlertUseless();
        }

        protected override void ConnectToDataUI() => UpdateState();

    }
}
