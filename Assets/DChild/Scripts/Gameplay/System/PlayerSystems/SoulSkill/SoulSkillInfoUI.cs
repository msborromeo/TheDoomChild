using DChild.Gameplay.Characters.Players.SoulSkills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DChild.Localization;

namespace DChild.Gameplay.SoulSkills.UI
{
    public class SoulSkillInfoUI : MonoBehaviour, ISoulSkillLocalizer
    {
        [SerializeField] private CanvasGroup m_parentCanvas;
        [SerializeField] private Image m_soulIcon;
        [SerializeField] private TextMeshProUGUI m_name;
        [SerializeField] private TextMeshProUGUI m_capcity;
        [SerializeField] private TextMeshProUGUI m_description;

        public event System.Action<TextMeshProUGUI, TextMeshProUGUI, SoulSkill> soulSkillLocalize;

        public void DisplayInfo(SoulSkill soulSkill)
        {
            m_parentCanvas.enabled = soulSkill != null;
            m_capcity.text = soulSkill.capacity.ToString();

            m_soulIcon.sprite = soulSkill.icon;
            
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
