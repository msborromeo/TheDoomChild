using DChild.Gameplay.Environment;
using DChild.Serialization;
using Holysoft;
using DChild.Menu;
using TMPro;
using DChild.Localization;
using System;

namespace DChild.Menu.Campaign
{
    public class CampaignLocation : CampaignInfoLabel, ILocationLabelInjector
    {
        private Location m_currentLocation;

        public event Action<TextMeshProUGUI, Location> LocationLabelUpdated;

        protected override void OnCampaignSelected(object sender, SelectedCampaignSlotEventArgs eventArgs)
        {
            if (eventArgs.location == Location._COUNT)
            {
                m_target.text = "EMPTY";
                LocationLabelUpdated?.Invoke(m_target, Location.None);
                return;
            }

            if (m_currentLocation != eventArgs.location)
            {
                m_currentLocation = eventArgs.location;
                var locationString = m_currentLocation.ToString().ToUpper().Replace('_', ' ');
                m_target.text = locationString;
                LocationLabelUpdated?.Invoke(m_target, m_currentLocation);
            }

        }

        protected override void Awake()
        {
            base.Awake();
            m_currentLocation = Location._COUNT;
        }
    }
}