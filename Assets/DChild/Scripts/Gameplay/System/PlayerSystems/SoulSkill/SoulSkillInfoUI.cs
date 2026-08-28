using DChild.Gameplay.Characters.Players.SoulSkills;
using DChild.Gameplay.EquipmentSystem;
using DChild.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.SoulSkills.UI
{
    public class SoulSkillInfoUI : MonoBehaviour, ISoulSkillLocalizer
    {
        [SerializeField] private CanvasGroup m_parentCanvas;
        //[SerializeField] private Image m_soulIcon;
        [SerializeField] private TextMeshProUGUI m_name;
        [SerializeField] private TextMeshProUGUI m_capcity;
        [SerializeField] private TextMeshProUGUI m_description;
        [SerializeField] private TextMeshProUGUI m_originEquipmentName;

        public event System.Action<TextMeshProUGUI, TextMeshProUGUI, SoulSkill> soulSkillLocalize;

        public void DisplayInfo(SoulSkill soulSkill, SoulEquipmentItem originEquipment)
        {
            m_parentCanvas.enabled = soulSkill != null;
            m_capcity.text = soulSkill.capacity.ToString();

            //m_soulIcon.sprite = soulSkill.icon;

            m_originEquipmentName.text =
    originEquipment != null
        ? $"Origin: {originEquipment.itemName}"
        : "";


            if (soulSkillLocalize == null)
            {
                m_name.text = soulSkill.name;
                m_description.text = soulSkill.description;
                return;
            }
            soulSkillLocalize?.Invoke(m_name, m_description, soulSkill);
        }
    }
}
