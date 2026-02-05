using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using I2.Loc;
using TMPro;
using DChild.Gameplay.ArmyBattle;

namespace DChild.Localization
{
    [RequireComponent(typeof(IArmyNameInjector))]
    public class ArmyNameLocalizer : MonoBehaviour
    {
        [SerializeField]
        private string m_TermKey;
        [SerializeField]
        private Localize m_localizer;

        IArmyNameInjector m_nameLocalizer;

        public void LocalizeName(TextMeshProUGUI text, ArmyOverviewData army)
        {
            if(army.localize)
            {
                text.text = LocalizationManager.GetTranslation(m_TermKey + "/" + army.name);
            }
            
            //m_localizer.mTerm = (m_TermKey + "/" + name);
            //m_localizer.OnLocalize(true);
            //return LocalizationManager.GetTranslation(m_TermKey+"/"+name);
        }

        private void Awake()
        {
            m_nameLocalizer = GetComponent<IArmyNameInjector>();
            m_nameLocalizer.nameUpdate += LocalizeName;
        }

        private void OnDisable()
        {
            m_nameLocalizer.nameUpdate -= LocalizeName;
        }
    }
}

