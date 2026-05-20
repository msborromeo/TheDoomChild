using DChild.Gameplay.UI.Alerts;
using DChild.Gameplay;
using DChild.Codex.Characters;
using UnityEngine;
namespace DChild.Menu.Codex.ArmyTroops.Alerts
{
    public class ArmyTroopsCodexIndexAlertUI : UIAlertIconElement<ArmyTroopsIndexButton>
    {
        private CharacterCodexData m_notifyingUnit;

        public override bool HasAlert()
        {
            var battleData = m_reference.armyData; // Replace with your button's actual data field/property
            if (battleData == null || battleData.armyCharacterGroup == null)
                return false;

            var charactersInGroup = m_reference.codexData;
            if (charactersInGroup == null)
                return false;

            foreach (var unit in charactersInGroup)
            {
                if (unit != null && GameplaySystem.gamplayUIHandle.alertManager.armyTroopAlerts.HasNewNotification(unit.id))
                {
                    return true;
                }
            }
            return false;
        }

        public override void RenderAlertUseless()
        {
            var battleData = m_reference.armyData;
            if (battleData != null && battleData.armyCharacterGroup != null)
            {
                var charactersInGroup = m_reference.codexData;
                if (charactersInGroup != null)
                {
                    foreach (var unit in charactersInGroup)
                    {
                        if (unit != null && GameplaySystem.gamplayUIHandle.alertManager.armyTroopAlerts.HasNewNotification(unit.id))
                        {
                            GameplaySystem.gamplayUIHandle.alertManager.armyTroopAlerts.RecordNewNotification(unit, false);
                        }
                    }
                }
            }

            base.RenderAlertUseless();
        }

        protected override void ConnectToDataUI() => UpdateState();

    }
}