using DChild.Gameplay.Environment;
using DChild.Localization;
using I2.Loc;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DChildEditor.Tools.Localization
{
    public class LanguageSource_LocationInjector : MonoBehaviour
    {
        [SerializeField]
        private LanguageSourceAsset m_target;

        [Button]
        public void InjectLocations()
        {
            List<Location> locations = new List<Location>();
            locations.Add(Location.City_Of_The_Dead);
            locations.Add(Location.Graveyard);
            locations.Add(Location.Garden);
            locations.Add(Location.Unholy_Forest);
            locations.Add(Location.Morden);
            locations.Add(Location.Laboratory);
            locations.Add(Location.Library);
            locations.Add(Location.Prison);
            locations.Add(Location.Overworld);
            locations.Add(Location.Realm_Of_Nightmare);
            locations.Add(Location.Throne_Room);
            locations.Add(Location.Realm_Of_Nightmare);

            var source = m_target.mSource;

            var languageCodes = source.GetLanguagesCode();
            languageCodes.Remove("");
            languageCodes.Remove(string.Empty);
            foreach (var location in locations)
            {
                var key = LocalizationUtility.GetTermKey(location);
                TermData term = source.GetTermData(key);
                if (term == null)
                {
                    term = source.AddTerm(key);
                }

                var splitKey = key.Split('/');
                var keyOnly = splitKey[splitKey.Length - 1];
                foreach (var langaugeCode in languageCodes)
                {
                    GoogleTranslation.Translate(keyOnly, "en", langaugeCode, (string Translation, string Errpr) =>
                    {
                        term.SetTranslation(source.GetLanguageIndexFromCode(langaugeCode), Translation);
                    });
                }
            }

        }
    }
}