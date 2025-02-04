using DChild.Gameplay.Items;
using DChild.Localization;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DChildEditor.Tools.Localization
{

    public class LanguageSource_ItemDatabaseInjector : LanguageSource_TermInjector
    {
        [SerializeField]
        private ItemList m_items;

        [Button]
        public void Inject()
        {
            var ids = m_items.GetIDs();
            List<TermInfo> termInfos = new List<TermInfo>();
            foreach (var id in ids)
            {
                var itemData = m_items.GetInfo(id);
                termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(itemData, LocalizationUtility.BasicDatabaseElementField.Name), itemData.itemName));
                termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(itemData, LocalizationUtility.BasicDatabaseElementField.Description), itemData.description));
            }

            ParseToTerms(termInfos);
        }
    }

}