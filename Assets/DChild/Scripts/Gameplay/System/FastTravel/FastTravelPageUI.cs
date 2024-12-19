using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField]
        private Image m_locationBackground;

        public void ShowPage(FastTravelPageData locationList)
        {
            m_locationLabel.text = locationList.location.ToString().Replace('_', ' ');
            m_locationBackground.sprite = locationList.locationBackground;
            ResetButtons(locationList);
            for (int i = 0; i < locationList.count; i++)
            {
                var button = m_townGateButtons[i];
                Show(button);
                var data = locationList.GetUnderworldTravelData(i);
                button.SetData(data);

                var isActivated = DialogueLua.GetVariable(FastTravelUtility.GenerateActivationVariableName(data)).asBool;
                button.SetInteractability(isActivated);
            }

            var isOverworldActivated = DialogueLua.GetVariable(FastTravelUtility.GenerateActivationVariableName(locationList.overworldTravelData)).asBool;
            m_overworldTownGateButtons.SetInteractability(isOverworldActivated);
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
