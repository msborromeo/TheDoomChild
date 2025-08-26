using DChild.Gameplay.Characters.Players;
using DChild.Localization;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace DChildEditor.Tools.Localization
{
    public class LanguageSource_CombatArtDabataseInjector : LanguageSource_TermInjector
    {
        [SerializeField]
        private CombatArtList m_dataList;

        [Button]
        public void InjectData()
        {
            List<TermInfo> termInfos = new List<TermInfo>();

            for (int i = 0; i < (int)CombatArt._Count; i++)
            {
                var data = m_dataList.GetCombatArtData((CombatArt)i);

                if (data == null)
                    continue;

                termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(data, LocalizationUtility.CombatArtField.Name), data.combatArtName));
                termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(data, LocalizationUtility.CombatArtField.Controls), data.controls));
                for (int k = 0; k < data.maxLevel; k++)
                {
                    var levelNumber = k + 1;
                    var descriptionKey = LocalizationUtility.GetTermKey(data, LocalizationUtility.CombatArtField.Description) + (levelNumber);
                    termInfos.Add(new TermInfo(descriptionKey, data.GetCombatArtLevelData(levelNumber).description));
                }
            }

            ParseToTerms(termInfos);
        }
    }

}