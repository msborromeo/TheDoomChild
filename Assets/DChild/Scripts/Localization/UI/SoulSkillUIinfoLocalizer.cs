using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using I2.Loc;
using System;
using TMPro;
using DChild.Gameplay.Characters.Players.SoulSkills;

namespace DChild.Localization
{
    [RequireComponent(typeof(ISoulSkillLocalizer))]
    public class SoulSkillUIinfoLocalizer : MonoBehaviour
    {
        [SerializeField]
        private Localize m_localizeName;
        [SerializeField]
        private Localize m_localizeDescription;

        private ISoulSkillLocalizer m_injector;

        private void Awake()
        {
            m_injector = GetComponent<ISoulSkillLocalizer>();
            m_injector.soulSkillLocalize += onUpdate;
        }

        private void OnDestroy()
        {
            m_injector.soulSkillLocalize -= onUpdate;
        }

        private void onUpdate(TextMeshProUGUI name, TextMeshProUGUI description, SoulSkill soulSkill)
        {
            var nameTerm = LocalizationUtility.GetTermKey(soulSkill , LocalizationUtility.BasicDatabaseElementField.Name);
            m_localizeName.SetTerm(nameTerm);
            //name.GetComponent<Localize>().SetTerm(nameTerm);
            var descTerm = LocalizationUtility.GetTermKey(soulSkill, LocalizationUtility.BasicDatabaseElementField.Description);
            m_localizeDescription.SetTerm(descTerm);
            //description.GetComponent<Localize>().SetTerm(descTerm);

        }
    }
}
