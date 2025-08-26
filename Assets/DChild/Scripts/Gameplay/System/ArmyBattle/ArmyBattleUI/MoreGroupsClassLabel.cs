using System.Collections;
using System.Collections.Generic;
using TMPro;
using I2.Loc;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI
{

    public class MoreGroupsClassLabel : MonoBehaviour
    {
        [SerializeField]
        private Localize m_label;

        private string m_termPath = "ARMY BATTLE/DamageType";

        public void SetPanelLabel(DamageType type)
        {

            switch (type)
            {
                case DamageType.Melee:
                    m_label.SetTerm($"{m_termPath}/Melee");
                    break;
                case DamageType.Magic:
                    m_label.SetTerm($"{m_termPath}/Magic");
                    break;
                case DamageType.Range:
                    m_label.SetTerm($"{m_termPath}/Range");
                    break;
                default:
                    m_label.SetTerm($"{m_termPath}/Special");
                    break;
            }
        }
    }
}
