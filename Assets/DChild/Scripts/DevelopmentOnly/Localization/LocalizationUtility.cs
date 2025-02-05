using DChild.Gameplay.ArmyBattle;
using DChild.Gameplay.Combat.StatusAilment;
using DChild.Gameplay.Environment;
using DChild.Gameplay.Items;
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