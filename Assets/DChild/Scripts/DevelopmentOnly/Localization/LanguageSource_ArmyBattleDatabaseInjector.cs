using DChild.Gameplay.ArmyBattle;
using DChild.Localization;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace DChildEditor.Tools.Localization
{
    public class LanguageSource_ArmyBattleDatabaseInjector : LanguageSource_TermInjector
    {
        [SerializeField, BoxGroup("Generator")]
        private ArmyGeneratorConfigurationData m_generatorConfigurationData;
        [SerializeField, AssetList, BoxGroup("Overviews")]
        private ArmyOverviewData[] m_overViewDatas;

        [Button, BoxGroup("Generator")]
        public void InjectPlayerGeneratorInfo()
        {
            var groupList = m_generatorConfigurationData.generatableArmyGroups;
            List<TermInfo> termInfos = new List<TermInfo>();

            for (int i = 0; i < groupList.count; i++)
            {
                var group = groupList.GetData(i);

                //Skip Temporary Group
                if (group.id >= 1000)
                    continue;


                //Group

                InjectGroupInfo(termInfos, group);

                InjectCharacterInfos(termInfos, group);
            }

            ParseToTerms(termInfos);
        }

        [Button, BoxGroup("Overviews")]
        public void InjectArmyOverviews()
        {
            List<TermInfo> termInfos = new List<TermInfo>();
            foreach (var data in m_overViewDatas)
            {
                termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(data), data.name));
            }

            ParseToTerms(termInfos);
        }

        private static void InjectCharacterInfos(List<TermInfo> termInfos, ArmyGroupTemplateData group)
        {
            var characters = group.armyCharacterGroup;
            for (int k = 0; k < characters.memberCount; k++)
            {
                var character = characters.GetCharacter(k);
                termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(character, LocalizationUtility.BasicDatabaseElementField.Name), character.name));
                // termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(character, LocalizationUtility.BasicDatabaseElementField.Description), character.name));
            }
        }

        private static void InjectGroupInfo(List<TermInfo> termInfos, ArmyGroupTemplateData group)
        {
            termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(group, LocalizationUtility.ArmyGroupField.Name), group.armyCharacterGroup.name));

            if (group.specialSkill != null)
            {
                termInfos.Add(new TermInfo(LocalizationUtility.GetTermKey(group, LocalizationUtility.ArmyGroupField.SpecialSkill), group.specialSkill.GetDescription()));
            }
        }
    }

}