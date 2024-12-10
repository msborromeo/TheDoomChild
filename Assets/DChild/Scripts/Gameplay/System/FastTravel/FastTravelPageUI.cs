using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace DChild.Gameplay.FastTravel
{
    public class FastTravelPageUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_locationLabel;
        [SerializeField, AssetSelector(IsUniqueList = true)]
        private FastTravelOptionButton[] m_townGateButtons;
        [SerializeField]
        private FastTravelOptionButton m_overworldTownGateButtons;

        public void ShowPage(FastTravelPageData locationList)
        {
            m_locationLabel.text = locationList.location.ToString().Replace('_',' ');

            ResetButtons(locationList);
            for (int i = 0; i < locationList.count; i++)
            {
                Show(m_townGateButtons[i]);
                m_townGateButtons[i].SetData(locationList.GetUnderworldTravelData(i));
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
