using DChild.Gameplay.SoulSkills.UI;
using DChild.Gameplay.UI.Alerts;

namespace DChild.Gameplay.UI.SoulSkills.Alerts
{
    public class SoulSkillsIndexAlertUI : UIAlertIconElement<SoulSkillUI>
    {

        public override bool HasAlert()
        {
            return GameplaySystem.gamplayUIHandle.alertManager.soulSkillAlerts.HasNewNotification(m_reference.soulSkillID);
        }

        public override void RenderAlertUseless()
        {
            GameplaySystem.gamplayUIHandle.alertManager.soulSkillAlerts.RecordNewNotification(m_reference.soulSkillID, false);
            base.RenderAlertUseless();
        }

        protected override void ConnectToDataUI()
        {
            UpdateState();
        }
    }
}