using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DChild.Menu.Bestiary;
using I2.Loc;
using DChild.Localization;

namespace Dchild.Localization
{
    [RequireComponent(typeof(ICodexBestiaryLocalizer))]
    public class CodexBestiaryLocalizer : MonoBehaviour
    {

        private ICodexBestiaryLocalizer m_injector;
        [SerializeField]
        private Localize m_alphabetName;
        [SerializeField]
        private Localize m_location;
        [SerializeField]
        private Localize m_descritpion;

        [SerializeField]
        private Localize m_storeNotes;
        [SerializeField]
        private Localize m_hunterNotes;


        private void OnUpdate(BestiaryData data)
        {
            m_alphabetName?.SetTerm(LocalizationUtility.GetTermKey(data,LocalizationUtility.BestiaryField.Name));
            m_descritpion?.SetTerm(LocalizationUtility.GetTermKey(data, LocalizationUtility.BestiaryField.Description));
            m_storeNotes?.SetTerm(LocalizationUtility.GetTermKey(data, LocalizationUtility.BestiaryField.StoreNotes));
            m_hunterNotes?.SetTerm(LocalizationUtility.GetTermKey(data, LocalizationUtility.BestiaryField.HunterNotes));
            //m_location?.SetTerm(LocalizationUtility.GetTermKey(data.locatedIn));
        }
        private void Awake()
        {
            m_injector = GetComponent<ICodexBestiaryLocalizer>();
            m_injector.localizeBestiaryData += OnUpdate;
        }

        private void OnDisable()
        {
            m_injector.localizeBestiaryData -= OnUpdate;
        }
    }
}