using DChild.Gameplay.Characters.Players.SoulSkills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.Equipment.UI
{
    [RequireComponent(typeof(LayoutElement))]
    public class ActiveEquipmentSkillRowUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_soulName;
        [SerializeField] private TextMeshProUGUI m_soulDescription;
        [SerializeField] private Image m_icon;
        [SerializeField] private LayoutElement m_iconLayout;
        [SerializeField] private HorizontalLayoutGroup m_rowContent;
        [SerializeField] private VerticalLayoutGroup m_textColumn;
        [SerializeField, Min(0f)] private float m_minimumHeight = 68.82761f;

        public float MeasureHeight(SoulSkill skill, float rowWidth)
        {
            float textWidth = Mathf.Max(1f, rowWidth - m_rowContent.padding.horizontal -
                m_iconLayout.preferredWidth - m_rowContent.spacing);
            float nameHeight = m_soulName.GetPreferredValues(skill.name, textWidth, Mathf.Infinity).y;
            float descriptionHeight = m_soulDescription.GetPreferredValues(skill.description,
                textWidth, Mathf.Infinity).y;
            float textHeight = nameHeight + m_textColumn.spacing + descriptionHeight +
                m_textColumn.padding.vertical;
            return Mathf.Max(m_minimumHeight, Mathf.Ceil(Mathf.Max(m_iconLayout.preferredHeight,
                textHeight) + m_rowContent.padding.vertical));
        }

        public void Display(SoulSkill skill, float height)
        {
            m_soulName.text = skill.name;
            m_soulDescription.text = skill.description;
            m_icon.sprite = skill.icon;
            m_icon.enabled = skill.icon != null;
            GetComponent<LayoutElement>().preferredHeight = height;
        }

        public void Clear()
        {
            m_soulName.text = string.Empty;
            m_soulDescription.text = string.Empty;
            m_icon.sprite = null;
            m_icon.enabled = false;
        }
    }
}
