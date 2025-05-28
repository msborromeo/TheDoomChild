using TMPro;
using UnityEngine;
using DChild.Localization;
using System;
using DChild.Gameplay.Characters.Players;

namespace DChild.Gameplay.UI.PrimarySkills
{
    public class PrimarySkillUIManager : MonoBehaviour , IPrimarySkillLocalizer
    {
        [SerializeField]
        private PrimarySkillSelectableList m_skillList;
        [SerializeField]
        private TextMeshProUGUI m_descriptionLabel;
        [SerializeField]
        private TextMeshProUGUI m_controlsLabel;
        [SerializeField]
        private TextMeshProUGUI m_skillNameLabel;
        [SerializeField]
        private SetTextToTextBox m_skillDescriptionSetTextToTextBox;
        [SerializeField]
        private PrimarySkillUILocalizer m_primarySkillUILocalizer;

        public event Action<PrimarySkillData> localizePrimarySkill;

        public void UpdateSelectables()
        {
            m_skillList.UpdateListAvailability();
        }

        public void Select(PrimarySkillSelectable selectable)
        {
            if (localizePrimarySkill != null)
            {
                localizePrimarySkill?.Invoke(selectable.reference);
                return;
            }
            m_descriptionLabel.text = selectable.reference.description;
            m_controlsLabel.text = selectable.reference.instruction;
            m_skillNameLabel.text = selectable.reference.skillName;
            m_skillDescriptionSetTextToTextBox.SetText(selectable.reference.instruction, selectable.reference.action, selectable.reference.actionType);
        }

        private void OnPrimarySkillInstructionsLocalized()
        {
            m_skillDescriptionSetTextToTextBox?.SetText(m_controlsLabel.text);
        }

        private void Start()
        {
            m_skillList.InitializeList();
            m_primarySkillUILocalizer.PrimarySkillInstructionsLocalized += OnPrimarySkillInstructionsLocalized;
        }

        private void OnDestroy()
        {
            m_primarySkillUILocalizer.PrimarySkillInstructionsLocalized -= OnPrimarySkillInstructionsLocalized;
        }
    }
}