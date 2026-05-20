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
            if (m_reference.armyData != null)
            {
                var unitCodexDatas = m_reference.codexData;
                foreach (var unit in unitCodexDatas)
                {
                    if (GameplaySystem.gamplayUIHandle.alertManager.armyTroopAlerts.HasNewNotification(unit))
                    {
                        m_notifyingUnit = unit;
                        return true;
                    }
                }
            }
            return false;
        }

        public override void RenderAlertUseless()
        {
            for (int i = 0; i < m_reference.armyData.armyCharacterGroup.memberCount; i++)
            {
                var unit = m_reference.armyData.armyCharacterGroup.GetCharacter(i);
                if (unit == m_notifyingUnit)
                {
                    GameplaySystem.gamplayUIHandle.alertManager.armyTroopAlerts.RecordNewNotification(m_notifyingUnit, false);
                    base.RenderAlertUseless();
                }
            }

        }

        protected override void ConnectToDataUI() => UpdateState();

    }
}