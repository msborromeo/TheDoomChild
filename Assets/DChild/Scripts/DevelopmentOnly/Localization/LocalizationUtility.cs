using DChild.Gameplay.Environment;
using DChild.Gameplay.Items;

namespace DChild.Localization
{
    public static class LocalizationUtility
    {
        public enum Category
        {
            ConfirmationWindow,
            None,
        }

        public static string GetTermKey(Location location)
        {
            var category = "Location/";
            var key = location.ToString().Replace('_', ' ');
            return category + key;
        }

        public static string GetTermKey(string message, Category category)
        {
            return GetCategoryPrefix(category) + message;;
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