using UnityEngine;

namespace DChild.Gameplay.FastTravel
{
    public class FastTravelUIManager: MonoBehaviour
    {
        [SerializeField]
        private FastTravelHandle m_handle;
        [SerializeField]
        private FastTravelPageUI m_locationPage;

        public void SelectLocationTab(FastTravelLocationTab locationTab)
        {
            m_locationPage.ShowPage(locationTab.locationList);
        }

        public void FastTravelTo(FastTravelOptionButton travelButton)
        {
            m_handle.TransferPlayerTo(travelButton.data.fastTravelPoint);
        }
    }
}
