using TMPro;
using UnityEngine;
using DChild.Localization;
using System;
using DChild.Gameplay.Characters.Players;
using UnityEngine.Video;

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
        [SerializeField]
        private VideoPlayer m_demoClipPlayer;

        public event Action<PrimarySkillData> localizePrimarySkill;

        public void UpdateSelectables()
        {
            m_skillList.UpdateListAvailability();
            Select(m_skillList.GetFirstAvailable());
        }

        public void Select(PrimarySkillSelectable selectable)
        {
            switch (selectable.reference.numberOfActions)
            {
                case 1:
                    m_skillDescriptionSetTextToTextBox.SetText(selectable.reference.instruction, selectable.reference.action);
                    break;
                case 2:
                    m_skillDescriptionSetTextToTextBox.SetText(selectable.reference.instruction, selectable.reference.action, selectable.reference.action2);
                    break;
                case 3:
                    m_skillDescriptionSetTextToTextBox.SetText(selectable.reference.instruction, selectable.reference.action, selectable.reference.action2, selectable.reference.action3);
                    break;
                case 4:
                    break;
                default:
                    m_skillDescriptionSetTextToTextBox.SetText(selectable.reference.instruction, selectable.reference.action);
                    break;
            }

            if (localizePrimarySkill != null)
            {
                localizePrimarySkill?.Invoke(selectable.reference);
                return;
            }
            m_descriptionLabel.text = selectable.reference.description;
            m_controlsLabel.text = selectable.reference.inputCommand;
            m_skillNameLabel.text = selectable.reference.skillName;

            m_demoClipPlayer.clip = selectable.reference.demoClip;
            m_demoClipPlayer.Play();
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