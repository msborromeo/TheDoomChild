using DChild.Gameplay.Systems.Journal;
using DChild.Localization;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace DChildEditor.Tools.Localization
{
    public class LanguageSource_JournalDabataseInjector : LanguageSource_TermInjector
    {
        [SerializeField, AssetList]
        private JournalData[] m_dataList;

        [Button]
        public void InjectData()
        {
            List<TermInfo> termInfos = new List<TermInfo>();

            foreach (var data in m_dataList)
            {
                termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(data, LocalizationUtility.BasicDatabaseElementField.Name), data.itemName));
                termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(data, LocalizationUtility.BasicDatabaseElementField.Description), data.itemDescription));
            }

            ParseToTerms(termInfos);
        }
    }
}