using DChild.Gameplay.Inventories;
using UnityEngine;
using I2.Loc;
using DChild.Localization;

[RequireComponent(typeof(IItemViewLocalizer))]
public class ItemViewLocalizer : MonoBehaviour
{
    [SerializeField]
    private Localize m_localizeDescriptionLabel;

    [SerializeField]
    private Localize m_localizeItemName;

    private IItemViewLocalizer m_Injector;

    private void Awake()
    {
        m_Injector = GetComponent<IItemViewLocalizer>();
        m_Injector.localizeItemView += onUpdate;
    }

    private void OnDestroy()
    {
        m_Injector.localizeItemView -= onUpdate;
    }

    private void onUpdate(IStoredItem itemReference)
    {
        if(itemReference==null)
        {
            m_localizeItemName.SetTerm("Items/Nothing_Name");
            m_localizeDescriptionLabel.SetTerm("Items/Nothing_Description");
        }else
        {
            m_localizeDescriptionLabel.SetTerm(LocalizationUtility.GetTermKey(itemReference.data, LocalizationUtility.BasicDatabaseElementField.Description));
            m_localizeItemName.SetTerm(LocalizationUtility.GetTermKey(itemReference.data, LocalizationUtility.BasicDatabaseElementField.Name));
        }
        
    }
}
