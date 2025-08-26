using DChild.Gameplay.Characters.Players;
using DChild.Localization;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace DChildEditor.Tools.Localization
{
    public class LanguageSource_PrimarySkillDabataseInjector : LanguageSource_TermInjector
    {
        [SerializeField]
        private PrimarySkillList m_dataList;

        [Button]
        public void InjectData()
        {
            List<TermInfo> termInfos = new List<TermInfo>();

            for (int i = 0; i < m_dataList.Count; i++)
            {
                var data = m_dataList.GetData(i);

                termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(data, LocalizationUtility.PrimarySkillField.Name), data.name));
                termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(data, LocalizationUtility.PrimarySkillField.Description), data.description));
                if (data.instruction != null || data.instruction != string.Empty)
                {
                    termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(data, LocalizationUtility.PrimarySkillField.Instruction), data.instruction));
                }
            }

            ParseToTerms(termInfos);
        }
    }
}