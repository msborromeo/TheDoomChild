using DChild.Gameplay.ArmyBattle;
using DChild.Gameplay.Characters.Players;
using DChild.Gameplay.Characters.Players.SoulSkills;
using DChild.Gameplay.Combat.StatusAilment;
using DChild.Gameplay.Environment;
using DChild.Gameplay.Items;
using DChild.Gameplay.Systems.Journal;
using DChild.Menu.Bestiary;
using System;
using System.Globalization;
using TMPro;

namespace DChild.Localization
{
    public static class LocalizationUtility
    {
        public enum Category
        {
            ConfirmationWindow,
            None,
        }

        public enum BasicDatabaseElementField
        {
            Name,
            Description,
        }

        public enum ArmyGroupField
        {
            Name,
            SpecialSkill
        }

        public enum BestiaryField
        {
            Name,
            Description,
            Title,
            StoreNotes,
            HunterNotes
        }

        public enum PrimarySkillField
        {
            Name,
            Description,
            Instruction,
            Command
        }

        public enum CombatArtField
        {
            Name,
            Description,
            Controls
        }

        public static string GetTermKey(PrimarySkillData data, PrimarySkillField field)
        {
            if (data == null)
                return string.Empty;

            var prefix = $"PrimarySkill/{data.skillName}/{data.skillName}_";

            switch (field)
            {
                case PrimarySkillField.Name:
                    return prefix + "Name";
                case PrimarySkillField.Description:
                    return prefix + "Description";
                case PrimarySkillField.Instruction:
                    return prefix + "Instruction";
                case PrimarySkillField.Command:
                    return prefix + "Command";
                default:
                    return string.Empty;
            }
        }

        public static string GetTermKey(CombatArtData data, CombatArtField field)
        {
            if (data == null)
                return string.Empty;

            var prefix = $"PrimarySkill/{data.connectedCombatArt}/{data.connectedCombatArt}_";

            switch (field)
            {
                case CombatArtField.Name:
                    return prefix + "Name";
                case CombatArtField.Description:
                    return prefix + "Description";
                case CombatArtField.Controls:
                    return prefix + "Controls";
                default:
                    return string.Empty;
            }
        }

        public static string GetTermKey(BestiaryData data,BestiaryField field)
        {
            if (data == null)
                return string.Empty;

            var id = data.id;
            var idString = id.ToString("000000");
            var prefix = $"Bestiary/{idString}/{idString}_";

            switch (field)
            {
                case BestiaryField.Name:
                    return prefix + "Name";
                case BestiaryField.Description:
                    return prefix + "Description";
                case BestiaryField.Title:
                    return prefix + "Title";
                case BestiaryField.StoreNotes:
                    return prefix + "Store Notes";
                case BestiaryField.HunterNotes:
                    return prefix + "Hunter Notes";
                default:
                    return string.Empty;
            }
        }

        public static string GetTermKey(SoulSkill soulSkill, BasicDatabaseElementField field)
        {
            if (soulSkill == null)
                return string.Empty;

            var id = soulSkill.id;
            var idString = id.ToString("000000");
            var prefix = $"SoulSkill/{idString}/{idString}_";

            switch (field)
            {
                case BasicDatabaseElementField.Name:
                    return prefix + "Name";
                case BasicDatabaseElementField.Description:
                    return prefix + "Description";
                default:
                    return string.Empty;
            }
        }

        public static string GetTermKey(JournalData data, BasicDatabaseElementField field)
        {
            if (data == null)
                return string.Empty;

            var id = data.ID;
            var idString = id.ToString("000000");
            var prefix = $"SoulSkill/{idString}/{idString}_";

            switch (field)
            {
                case BasicDatabaseElementField.Name:
                    return prefix + "Name";
                case BasicDatabaseElementField.Description:
                    return prefix + "Description";
                default:
                    return string.Empty;
            }
        }

        public static string GetTermKey(ItemData itemData, BasicDatabaseElementField field)
        {
            if (itemData == null)
                return string.Empty;

            var id = itemData.id;
            var idString = id.ToString("000000");
            var prefix = $"Items/{idString}/{idString}_";

            switch (field)
            {
                case BasicDatabaseElementField.Name:
                    return prefix + "Name";
                case BasicDatabaseElementField.Description:
                    return prefix + "Description";
                default:
                    return string.Empty;
            }
        }

        public static string GetTermKey(ArmyGroupTemplateData data, ArmyGroupField field)
        {
            if (data == null)
                return string.Empty;

            var idString = data.id.ToString("000");
            idString = idString.Replace("-", "");
            var prefix = $"ArmyBattle/Groups/{idString}/AG_{idString}_";

            switch (field)
            {
                case ArmyGroupField.Name:
                    return prefix + "Name";
                case ArmyGroupField.SpecialSkill:
                    return prefix + "SpecialSkill";
                default:
                    return string.Empty;
            }
        }

        public static string GetTermKey(ArmyCharacterData armyCharacterData, BasicDatabaseElementField field)
        {
            if (armyCharacterData == null)
                return string.Empty;

            var idString = armyCharacterData.ID.ToString("000");
            var prefix = $"ArmyBattle/Characters/{idString}/AC_{idString}_";

            switch (field)
            {
                case BasicDatabaseElementField.Name:
                    return prefix + "Name";
                case BasicDatabaseElementField.Description:
                    return prefix + "Description";
                default:
                    return string.Empty;
            }
        }

        public static string GetTermKey(ArmyOverviewData data)
        {
            var idString = data.ID.ToString("000");
            var prefix = $"ArmyBattle/Army Overview/{idString}/AO_{idString}_";
            return prefix + "Name";
        }

        public static string GetTermKey(Location location)
        {
            var category = "Location/";
            var key = location.ToString().Replace('_', ' ');
            return category + key;
        }

        public static string GetTermKey(StatusEffectType location)
        {
            var category = "Status Effect/";
            var key = location.ToString().Replace('_', ' ');
            return category + key;
        }

        public static string GetTermKey(string message, Category category)
        {
            return GetCategoryPrefix(category) + message; ;
        }

        public static string GetTermKey(TMP_FontAsset asset)
        {
            return "Fonts/" + asset.name;
        }

        private static string GetCategoryPrefix(Category category)
        {
            switch (category)
            {
                case Category.ConfirmationWindow:
                    return "Confirmation/Messages/";
                case Category.None:
                default:
                    return "";
            }
        }
    }
}