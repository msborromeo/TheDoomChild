using DChild.Gameplay.EquipmentSystem;
using TMPro;
using UnityEngine;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentStatBuffUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_buffPanel;

        public void Display(IEquipmentStatBoostModule buff)
        {
            m_buffPanel.text = $"{buff.GetBoostType()}".Replace("_", " ");
        }
    }
}
