using UnityEditor;
using UnityEngine;

namespace DChild.Gameplay.FastTravel
{
    public class FastTravelPageUI : MonoBehaviour
    {
        [SerializeField]
        private Transform m_listContainer;
        [SerializeField]
        private FastTravelOptionButton[] m_townGateButtons;

        public void ShowPage(FastTravelPageData locationList)
        {
            ResetButtons(locationList);
            for (int i = 0; i < locationList.count; i++)
            {
                Show(m_townGateButtons[i]);
                m_townGateButtons[i].SetData(locationList.GetData(i));
            }

        }
        private void ResetButtons(FastTravelPageData locationList)
        {
            for (int i = locationList.count; i < m_townGateButtons.Length; i++)
            {
                Hide(m_townGateButtons[i]);
                m_townGateButtons[i].SetData(null);
            }
        }

        private void Show(FastTravelOptionButton button)
        {
            button.gameObject.SetActive(true);
        }

        private void Hide(FastTravelOptionButton button)
        {
            button.gameObject.SetActive(false);
        }


    }
}
