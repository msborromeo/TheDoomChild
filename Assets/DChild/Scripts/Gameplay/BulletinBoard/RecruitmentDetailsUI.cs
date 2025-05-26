using TMPro;
using UnityEngine;

namespace DChild.Gameplay.ArmyBattle.UI.BulletinBoard
{
    public class RecruitmentDetailsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_conditions;
        [SerializeField] private TextMeshProUGUI m_recruitmentFee;

        public void Display()
        {
            m_conditions.text = "";
            m_recruitmentFee.text = "";
        }

        public void RecruitUnit()
        {

        }
    }
}