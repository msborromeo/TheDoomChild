using DChild.Gameplay.UI.Alerts;
using DChild.Gameplay;
using DChild.Codex.Characters;
namespace DChild.Menu.Codex.ArmyTroops.Alerts
{
    public class ArmyTroopsCodexIndexAlertUI : UIAlertIconElement<ArmyTroopsIndexButton>
    {
        private CharacterCodexData m_notifyingUnit;

        public override bool HasAlert()
        {
            var unitCodexDaatas = m_reference.codexData;
            foreach (var unit in unitCodexDaatas)
            {
                if(GameplaySystem.gamplayUIHandle.alertManager.armyTroopAlerts.HasNewNotification(unit))
                {
                    m_notifyingUnit = unit;
                    return true;
                }
            }
            return false;
        }

        public override void RenderAlertUseless()
        {
            GameplaySystem.gamplayUIHandle.alertManager.armyTroopAlerts.RecordNewNotification(m_notifyingUnit, false);
            base.RenderAlertUseless();
        }

        protected override void ConnectToDataUI() => UpdateState();

    }
}