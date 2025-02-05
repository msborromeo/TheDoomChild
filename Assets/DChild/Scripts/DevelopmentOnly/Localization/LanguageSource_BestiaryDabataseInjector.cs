using DChild.Localization;
using DChild.Menu.Bestiary;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace DChildEditor.Tools.Localization
{
    public class LanguageSource_BestiaryDabataseInjector : LanguageSource_TermInjector
    {
        [SerializeField]
        private BestiaryList m_dataList;

        [Button]
        public void InjectData()
        {
            List<TermInfo> termInfos = new List<TermInfo>();

            var ids = m_dataList.GetIDs();
            foreach (var id in ids)
            {
                var data = m_dataList.GetInfo(id);

                termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(data, LocalizationUtility.BestiaryField.Name), data.creatureName));
                termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(data, LocalizationUtility.BestiaryField.Description), data.description));
                if (data.title != null || data.title != string.Empty)
                {
                    termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(data, LocalizationUtility.BestiaryField.Title), data.title));
                }
                termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(data, LocalizationUtility.BestiaryField.StoreNotes), data.storeNotes));
                termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(data, LocalizationUtility.BestiaryField.HunterNotes), data.hunterNotes));
                Debug.Log($"Bestiary {id} is To BE Injected");
            }

            ParseToTerms(termInfos);
        }
    }
}