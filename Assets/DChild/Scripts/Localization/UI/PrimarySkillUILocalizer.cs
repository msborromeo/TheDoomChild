using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using I2.Loc;
using DChild.Gameplay.UI.PrimarySkills;

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

        private void Awake()
        {
            m_Injector = GetComponent<IPrimarySkillLocalizer>();
            m_Injector.localizePrimarySkill += onUpdate;
        }

        private void OnDestroy()
        {
            m_Injector.localizePrimarySkill -= onUpdate;
        }

        private void onUpdate(PrimarySkillSelectable soulSkill)
        {
            m_localizeDescriptionLabel.SetTerm(LocalizationUtility.GetTermKey(soulSkill.reference,LocalizationUtility.PrimarySkillField.Description));
            m_localizeControlLabel.SetTerm(LocalizationUtility.GetTermKey(soulSkill.reference, LocalizationUtility.PrimarySkillField.Instruction));
            m_localizeSkillNameLabel.SetTerm(LocalizationUtility.GetTermKey(soulSkill.reference, LocalizationUtility.PrimarySkillField.Name));
        }
    }
}
