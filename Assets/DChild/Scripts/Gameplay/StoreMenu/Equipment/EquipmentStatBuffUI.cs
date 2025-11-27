using DChild.Gameplay.EquipmentSystem;
using TMPro;
using UnityEngine;

namespace DChild.Menu.Equipment.UI
{
    public class EquipmentStatBuffUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_buffPanel;
        [SerializeField] private TextMeshProUGUI m_modifierValuePanel;

        public void Display(IEquipmentStatBoostModule buff)
        {
            var value = buff.GetModifierValue();
            
            m_buffPanel.text = $"{buff.GetBoostType()}".Replace("_", " ");
            m_modifierValuePanel.text = $"{value}".Insert(0, value > -1 ? "+" : "-");
        }
    }
}
