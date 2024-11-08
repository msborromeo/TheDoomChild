using DChild.Gameplay.Environment;
using DChild.Serialization;
using Holysoft;
using DChild.Menu;
using TMPro;

namespace DChild.Menu.Campaign
{
    public class CampaignLocation : CampaignInfoLabel
    {
        private Location m_currentLocation;

        protected override void OnCampaignSelected(object sender, SelectedCampaignSlotEventArgs eventArgs)
        {
            if (eventArgs.location == Location._COUNT)
            {
                m_target.text = "EMPTY";
                return;
            }

            if (m_currentLocation != eventArgs.location)
            {
                m_currentLocation = eventArgs.location;
                var locationString = m_currentLocation.ToString().ToUpper().Replace('_', ' ');
                m_target.text = locationString;
            }

        }

        protected override void Awake()
        {
            base.Awake();
            m_currentLocation = Location._COUNT;
        }
    }
}