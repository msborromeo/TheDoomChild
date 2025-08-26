using DChild.Gameplay.Characters.Players.SoulSkills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DChild.Localization;

namespace DChild.Gameplay.SoulSkills.UI
{
    public class SoulSkillInfoUI : MonoBehaviour , ISoulSkillLocalizer
    {
        [SerializeField]
        private CanvasGroup m_parentCanvas;
        [SerializeField]
        private SoulSkillUI m_skillUI;
        [SerializeField]
        private TextMeshProUGUI m_name;
        [SerializeField]
        private TextMeshProUGUI m_capcity;
        [SerializeField]
        private TextMeshProUGUI m_description;

        public event System.Action<TextMeshProUGUI, TextMeshProUGUI, SoulSkill> soulSkillLocalize;

        public void DisplayInfoOf(SoulSkill soulSkill)
        {
            m_parentCanvas.enabled = soulSkill != null;
            m_capcity.text = soulSkill.capacity.ToString();
            if (soulSkillLocalize!=null)
            {
                soulSkillLocalize?.Invoke(m_name,m_description,soulSkill);
                return;
            }
                m_skillUI.DisplayAs(soulSkill);
                m_name.text = soulSkill.name;
                
                m_description.text = soulSkill.description;  
        }
    }
}
