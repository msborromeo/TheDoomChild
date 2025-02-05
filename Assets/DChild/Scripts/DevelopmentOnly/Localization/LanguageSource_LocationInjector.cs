using DChild.Gameplay.Environment;
using DChild.Localization;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DChildEditor.Tools.Localization
{

    public class LanguageSource_LocationInjector : LanguageSource_TermInjector
    {
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

            List<string> keys = new List<string>();
            foreach (var key in locations)
            {
                keys.Add(LocalizationUtility.GetTermKey(key));
            }

            ParseToTerms(keys);

        }

        
    }
}