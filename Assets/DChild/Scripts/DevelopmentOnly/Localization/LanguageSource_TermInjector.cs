using DChild.Gameplay.Combat.StatusAilment.UI;
using DChild.Gameplay.Environment;
using I2.Loc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static DarkTonic.MasterAudio.MasterAudio;

namespace DChildEditor.Tools.Localization
{
    public class LanguageSource_TermInjector : MonoBehaviour
    {
        protected struct TermInfo
        {
            public string Key;
            public string Value;

            public TermInfo(string key, string value)
            {
                Key = key;
                Value = value;
            }
        }

        [SerializeField]
        private LanguageSourceAsset m_target;

        protected void ParseToTerms(IEnumerable<TermInfo> termInfos)
        {
            var source = m_target.mSource;
            var englishLanguageCodes = "en";

            var languageCodes = source.GetLanguagesCode();
            languageCodes.Remove("");
            languageCodes.Remove(string.Empty);
            languageCodes.Remove(englishLanguageCodes);

            foreach (var termInfo in termInfos)
            {
                TermData term = source.GetTermData(termInfo.Key);
                if (term == null)
                {
                    term = source.AddTerm(termInfo.Key);
                    Debug.Log($"{termInfo.Key} Added");
                }

                term.SetTranslation(source.GetLanguageIndexFromCode(englishLanguageCodes), termInfo.Value);
                foreach (var langaugeCode in languageCodes)
                {
                    Debug.Log($"{term.Term} Will be Translated");
                    GoogleTranslation.Translate(termInfo.Value, englishLanguageCodes, langaugeCode, (string Translation, string Errpr) =>
                    {
                        term.SetTranslation(source.GetLanguageIndexFromCode(langaugeCode), Translation);
                        Debug.Log($"{term.Term} Translated");
                    });
                }
            }
        }

        protected void ParseToTerms(IEnumerable<string> terms)
        {
            var source = m_target.mSource;

            var languageCodes = source.GetLanguagesCode();
            languageCodes.Remove("");
            languageCodes.Remove(string.Empty);
            foreach (var key in terms)
            {
                TermData term = source.GetTermData(key);
                if (term == null)
                {
                    term = source.AddTerm(key);
                    Debug.Log($"{key} Added");
                }

                var splitKey = key.Split('/');
                var keyOnly = splitKey[splitKey.Length - 1];
                foreach (var langaugeCode in languageCodes)
                {
                    Debug.Log($"{term.Term} Will be Translated");
                    GoogleTranslation.Translate(keyOnly, "en", langaugeCode, (string Translation, string Errpr) =>
                    {
                        term.SetTranslation(source.GetLanguageIndexFromCode(langaugeCode), Translation);
                        Debug.Log($"{term.Term} Translated");
                    });
                }
            }
        }
    }
}