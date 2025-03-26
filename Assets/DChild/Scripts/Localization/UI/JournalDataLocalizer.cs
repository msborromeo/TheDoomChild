using DChild.Gameplay.Systems.Journal;
using DChild.Localization;
using I2.Loc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IJournalDataLocalizer))]
public class JournalDataLocalizer : MonoBehaviour
{
    [SerializeField]
    private Localize m_localizeDescriptionLabel;

    [SerializeField]
    private Localize m_localizeItemName;

    private IJournalDataLocalizer m_Injector;

    private void Awake()
    {
        m_Injector = GetComponent<IJournalDataLocalizer>();
        m_Injector.LocalizeJournal += onUpdate;
    }

    private void OnDestroy()
    {
        m_Injector.LocalizeJournal -= onUpdate;
    }

    private void onUpdate(JournalData reference)
    {
            m_localizeDescriptionLabel?.SetTerm(LocalizationUtility.GetTermKey(reference, LocalizationUtility.BasicDatabaseElementField.Description));
            m_localizeItemName?.SetTerm(LocalizationUtility.GetTermKey(reference, LocalizationUtility.BasicDatabaseElementField.Name));
    }
}
