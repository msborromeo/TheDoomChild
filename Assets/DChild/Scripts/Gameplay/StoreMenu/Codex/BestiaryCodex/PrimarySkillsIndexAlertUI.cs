using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.UI.Alerts;

namespace DChild.Gameplay.UI.PrimarySkills.Alerts
{
    public class PrimarySkillsIndexAlertUI : UIAlertIconElement<PrimarySkillSelectable>
    {
        public override bool HasAlert()
        {
            return GameplaySystem.gamplayUIHandle.alertManager.primarySkillAlerts.HasNewNotification(m_reference.reference.skill);
        }
        public override void RenderAlertUseless()
        {
            GameplaySystem.gamplayUIHandle.alertManager.primarySkillAlerts.RecordNewNotification(m_reference.reference.skill, false);
            base.RenderAlertUseless();
        }

        protected override void ConnectToDataUI()
        {
            m_reference.OnPrimarySkillDataChanged += OnDataChange;
        }

        private void OnDataChange(PrimarySkillData data)
        {
            UpdateState();
        }

        private void Awake()
        {
            m_reference.OnPrimarySkillDataChanged += OnDataChange;
        }

        private void OnDisable()
        {
            m_reference.OnPrimarySkillDataChanged -= OnDataChange;
        }
    }
}