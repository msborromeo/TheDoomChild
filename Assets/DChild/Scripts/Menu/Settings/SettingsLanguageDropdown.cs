using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Menu.UI
{
    public class SettingsLanguageDropdown: MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown m_dropdown;

        void OnEnable()
        {
            if (m_dropdown == null)
                return;

            var currentLanguage = LocalizationManager.CurrentLanguage;
            if (LocalizationManager.Sources.Count == 0) LocalizationManager.UpdateSources();
            var languages = LocalizationManager.GetAllLanguages();

            // Fill the dropdown elements
            m_dropdown.ClearOptions();
            m_dropdown.AddOptions(languages);

            m_dropdown.value = languages.IndexOf(currentLanguage);
            m_dropdown.onValueChanged.RemoveListener(OnValueChanged);
            m_dropdown.onValueChanged.AddListener(OnValueChanged);
        }


        void OnValueChanged(int index)
        {
            if (index < 0)
            {
                index = 0;
                m_dropdown.value = index;
            }

            LocalizationManager.CurrentLanguage = m_dropdown.options[index].text;
        }

    }
}