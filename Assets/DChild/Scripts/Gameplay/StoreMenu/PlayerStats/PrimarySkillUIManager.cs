using TMPro;
using UnityEngine;
using DChild.Localization;
using System;
using DChild.Gameplay.Characters.Players;
using UnityEngine.Video;
using Doozy.Runtime.UIManager.Components;
using System.Collections;

namespace DChild.Gameplay.UI.PrimarySkills
{
    public class PrimarySkillUIManager : MonoBehaviour, IPrimarySkillLocalizer
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
            //m_skillList.InitializeList();
            m_skillList.UpdateListAvailability();
            SelectFirstUnlocked();
        }

        private void SelectFirstUnlocked()
        {
            Reset();

            var firstUnlocked = m_skillList.GetFirstAvailable();
            if (firstUnlocked == null) return; 

            Select(firstUnlocked);
            firstUnlocked.GetComponent<UIToggle>().SetIsOn(true);
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

            m_demoClipPlayer.clip = selectable.reference.demoClip;
            //m_demoClipPlayer.Play();

            m_descriptionLabel.text = selectable.reference.description;
            m_controlsLabel.text = selectable.reference.inputCommand;
            m_skillNameLabel.text = selectable.reference.skillName;

            if (localizePrimarySkill != null)
                localizePrimarySkill?.Invoke(selectable.reference);
        }

        private void OnPrimarySkillInstructionsLocalized()
        {
            m_skillDescriptionSetTextToTextBox?.SetText(m_controlsLabel.text);
        }

        private void Reset()
        {
            m_descriptionLabel.text = "";
            m_controlsLabel.text = "";
            m_skillNameLabel.text = "";
        }

        private void OnEnable()
        {
            m_skillList.InitializeList();
            m_primarySkillUILocalizer.PrimarySkillInstructionsLocalized += OnPrimarySkillInstructionsLocalized;
        }

        private void OnDisable()
        {
            m_primarySkillUILocalizer.PrimarySkillInstructionsLocalized -= OnPrimarySkillInstructionsLocalized;
        }
    }
}