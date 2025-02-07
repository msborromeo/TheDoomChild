using DChild.Gameplay.Characters.Players.SoulSkills;
using DChild.Localization;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace DChildEditor.Tools.Localization
{
    public class LanguageSource_SoulSkillDabataseInjector : LanguageSource_TermInjector
    {
        [SerializeField]
        private SoulSkillList m_dataList;

        [Button]
        public void InjectData()
        {
            List<TermInfo> termInfos = new List<TermInfo>();

            var ids = m_dataList.GetIDs();

            foreach (var id in ids)
            {
                var data = m_dataList.GetInfo(id);

                termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(data, LocalizationUtility.BasicDatabaseElementField.Name), data.name));
                termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(data, LocalizationUtility.BasicDatabaseElementField.Description), data.description));
            }

            ParseToTerms(termInfos);
        }
    }
}