using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.UI.PrimarySkills;
using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DChild.Localization;
using DChild.Gameplay.UI;
using UnityEngine.Video;

namespace DChild.Gameplay
{
    public class PrimarySkillNotificationUI : NotificationUI , IPrimarySkillLocalizer
    {
        [SerializeField]
        private PrimarySkillList m_notifiedSkill;
        [SerializeField]
        private PrimarySkillIcon m_icon;
        [SerializeField]
        private TextMeshProUGUI m_skillName;
        [SerializeField]
        private VideoPlayer m_demoPlayer;
        [SerializeField]
        private TextMeshProUGUI m_description;
        [SerializeField]
        private TextMeshProUGUI m_instruction;
        [SerializeField]
        private SetTextToTextBox m_buttonPromptSetter;
        [SerializeField]
        private PrimarySkillUILocalizer m_primarySkillUILocalizer;

        private const string INSTRUCTION_HEADER = "<color=#710B0D>Button:</color><indent=15%>";

        public event Action<PrimarySkillData> localizePrimarySkill;

        [Button]
        public void SetNotifiedSkill(PrimarySkillData skill)
        {
            m_icon.DisplayAs(skill);
            m_skillName.text = skill.skillName;
            m_demoPlayer.clip = skill.demoClip;
            m_description.text = skill.description;
            m_instruction.text = INSTRUCTION_HEADER + skill.instruction;

            switch (skill.numberOfActions)
            {
                case 1:
                    m_buttonPromptSetter.SetText(skill.instruction, skill.action);
                    break;
                case 2:
                    m_buttonPromptSetter.SetText(skill.instruction, skill.action, skill.action2);
                    break;
                case 3:
                    m_buttonPromptSetter.SetText(skill.instruction, skill.action, skill.action2, skill.action3);
                    break;
                case 4:
                    break;
                default:
                    m_buttonPromptSetter.SetText(skill.instruction, skill.action);
                    break;
            }

            m_demoPlayer.Play();
            localizePrimarySkill?.Invoke(skill);
        }

        public void SetNotifiedSkill(PrimarySkill skill)
        {
            for (int i = 0; i < m_notifiedSkill.Count; i++)
            {
                var skillData = m_notifiedSkill.GetData(i);
                if (skillData.skill == skill)
                {
                    SetNotifiedSkill(skillData);
                    break;
                }
            }
        }

        private void Start()
        {
            m_primarySkillUILocalizer.PrimarySkillInstructionsLocalized += OnPrimarySkillInstructionsLocalized;
        }

        private void OnDestroy()
        {
            m_primarySkillUILocalizer.PrimarySkillInstructionsLocalized -= OnPrimarySkillInstructionsLocalized;
        }

        private void OnPrimarySkillInstructionsLocalized()
        {
            m_buttonPromptSetter.SetText(m_instruction.text);
        }
    }
}