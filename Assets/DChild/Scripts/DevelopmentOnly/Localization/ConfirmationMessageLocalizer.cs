using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using I2.Loc;

namespace DChild.Menu.MainMenu
{
    [RequireComponent(typeof(MainMenuNavigationHandle))]
    public class ConfirmationMessageLocalizer :  MonoBehaviour
    {
        [SerializeField]
        private ConfirmationHandler m_Handler;
        private string m_Message;
        MainMenuNavigationHandle m_savedHandle;
        ConfirmationRequestHandle m_requestHandle = new ConfirmationRequestHandle();
        [TermsPopup]
        public string _MyLocalizedHeader;
        [TermsPopup]
        public string _MyLocalizedString;


        private void Start()
        {
            if(!m_Handler)
            {
                //delete self if this is not set up
                Destroy(this);
            }
            m_savedHandle = GetComponent<MainMenuNavigationHandle>();
            m_requestHandle.ChangeHandler(m_Handler);
            m_requestHandle.ChangeMessage(LocalizationManager.GetTranslation(_MyLocalizedString));
            //m_requestHandle.ChangeMessage(LocalizationManager.GetTranslation(_MyLocalizedString), LocalizationManager.GetTranslation(_MyLocalizedHeader));
            //Debug.LogError(LocalizationManager.GetTranslation(_MyLocalizedString));
            m_savedHandle.QuitConfirmationLocalization(m_requestHandle);
        }

        public void UpdateMessageCall(string updateMessage)
        {
            m_Message = updateMessage;
            UpdateMessage();
        }

        private void UpdateMessage()
        {
            m_requestHandle.ChangeMessage(m_Message);
            m_savedHandle.QuitConfirmationLocalization(m_requestHandle);
        }

        public void UpdateLocalizeation()
        {
            //Debug.LogError("ALERTA Updated");
            m_Message = LocalizationManager.GetTranslation(_MyLocalizedString);
            UpdateMessage();
        }
    }
}
