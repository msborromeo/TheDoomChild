using DChild.Gameplay.Combat.StatusAilment;
using DChild.Localization;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace DChildEditor.Tools.Localization
{
    public class LanguageSource_StatusEffectsInjector : LanguageSource_TermInjector
    {
        [Button]
        public void Inject()
        {
            List<string> keys = new List<string>();
            for (int i = 0; i < (int)StatusEffectType._COUNT; i++)
            {
                keys.Add(LocalizationUtility.GetTermKey((StatusEffectType)i));
            }

            ParseToTerms(keys);
        }
    }

}