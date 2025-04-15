using DChild.Codex.Quests.UI;
using DChild.Gameplay;
using DChild.Gameplay.UI.Alerts;

namespace DChild.Menu.Codex.Quest.Alerts
{
    public class QuestCodexIndexAlertUI : UIAlertIconElement<QuestButtonUI>
    {
        public override bool HasAlert() => GameplaySystem.gamplayUIHandle.alertManager.questAlerts.HasNewNotification(m_reference.questData);

        public override void RenderAlertUseless()
        {
            GameplaySystem.gamplayUIHandle.alertManager.questAlerts.RecordNewNotification(m_reference.questData, false);
            base.RenderAlertUseless();
        }

        protected override void ConnectToDataUI() => UpdateState();
    }
}