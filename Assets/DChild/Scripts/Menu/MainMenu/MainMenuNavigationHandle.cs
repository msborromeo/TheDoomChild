using Doozy.Runtime.Signals;
using Holysoft.Event;
using UnityEngine;

namespace DChild.Menu.MainMenu
{
    public class MainMenuNavigationHandle : MonoBehaviour
    {
        [SerializeField]
        private MainMenuVideoHandle m_backgroundVideo;
        [SerializeField]
        private SignalSender m_hideNavigationMenuSignal;

        [SerializeField]
        private ConfirmationRequestHandle m_applicationQuitRequest;

        ConfirmationMessageLocalizer localizer;

        public void TransistionToCampaignSelect()
        {
            m_backgroundVideo.PlayEnd();
            m_backgroundVideo.OnVideoEnd += OnTransistionVideoEnd;
        }
        public void RequestToExitGame()
        {
            if(TryGetComponent(out localizer))
            {
                localizer.UpdateLocalizeation();
            }
            m_applicationQuitRequest.Execute(OnAffirmation);
        }

        private void OnTransistionVideoEnd(object sender, EventActionArgs eventArgs)
        {
            m_hideNavigationMenuSignal.SendSignal();
            m_backgroundVideo.OnVideoEnd -= OnTransistionVideoEnd;
        }
        private void OnAffirmation(object sender, EventActionArgs eventArgs)
        {
            Debug.Log("Quitting");
            Application.Quit();
        }

        public void QuitConfirmationLocalization(ConfirmationRequestHandle ConfirmationRequest)
        {
            m_applicationQuitRequest = ConfirmationRequest;
        }
    }
}