using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI
{

    public class MoreGroupsClassLabel : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_panelLabel;

        public void SetPanelLabel(DamageType type)
        {
            switch (type)
            {
                case DamageType.Melee:
                    m_panelLabel.text = "MELEE";
                    break;
                case DamageType.Magic:
                    m_panelLabel.text = "MAGIC";
                    break;
                case DamageType.Range:
                    m_panelLabel.text = "RANGE";
                    break;
                default:
                    break;
            }
        }
    }
}
