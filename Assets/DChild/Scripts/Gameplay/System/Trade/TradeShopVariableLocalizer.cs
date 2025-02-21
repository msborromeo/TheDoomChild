using UnityEngine;
using I2.Loc;
using DChild.Gameplay.Items;

namespace DChild.Localization
{
    public class TradeShopVariableLocalizer : MonoBehaviour
    {
        [SerializeField]
        private LocalizationParamsManager paramsManager;
        public void TradeValueLocalize(string _Count, ItemData _itemName, string _pluralization, string _currencyMSG,string _Cost)
        {
            paramsManager.SetParameterValue("COUNT",_Count);
            paramsManager.SetParameterValue("ITEMNAME", LocalizationManager.GetTranslation(LocalizationUtility.GetTermKey(_itemName,LocalizationUtility.BasicDatabaseElementField.Name)));
            paramsManager.SetParameterValue("PLURALIZATION", _pluralization);
            paramsManager.SetParameterValue("CURRENCY", _currencyMSG);
            paramsManager.SetParameterValue("COST", _Cost);

            //paramsManager.
        }
    }
}


