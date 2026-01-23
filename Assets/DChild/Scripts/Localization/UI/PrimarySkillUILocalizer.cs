using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using I2.Loc;
using DChild.Gameplay.UI.PrimarySkills;
using DChild.Gameplay.Characters.Players;
using System;

namespace DChild.Localization
{
    [RequireComponent(typeof(IPrimarySkillLocalizer))]
    public class PrimarySkillUILocalizer : MonoBehaviour
    {
        [SerializeField]
        private Localize m_localizeDescriptionLabel;

        [SerializeField]
        private Localize m_localizeControlLabel;

        [SerializeField]
        private Localize m_localizeSkillNameLabel;

        private IPrimarySkillLocalizer m_Injector;

        public Action PrimarySkillInstructionsLocalized;

        private void Awake()
        {
            m_Injector = GetComponent<IPrimarySkillLocalizer>();
            m_Injector.localizePrimarySkill += onUpdate;
        }

        private void OnDestroy()
        {
            m_Injector.localizePrimarySkill -= onUpdate;
        }

        private void onUpdate(PrimarySkillData soulSkill)
        {
            m_localizeDescriptionLabel.SetTerm(LocalizationUtility.GetTermKey(soulSkill,LocalizationUtility.PrimarySkillField.Description));
            m_localizeControlLabel.SetTerm(LocalizationUtility.GetTermKey(soulSkill, LocalizationUtility.PrimarySkillField.Command));
            m_localizeSkillNameLabel.SetTerm(LocalizationUtility.GetTermKey(soulSkill, LocalizationUtility.PrimarySkillField.Name));
            PrimarySkillInstructionsLocalized?.Invoke();
        }
    }
}
