using Doozy.Runtime.UIManager.Components;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.FastTravel
{
    public class FastTravelPageUI : MonoBehaviour
    {
        [SerializeField, BoxGroup("UI Labels")] private TextMeshProUGUI m_locationLabel;
        [SerializeField, BoxGroup("UI Labels")] private TextMeshProUGUI m_townGateLabel;

        [SerializeField, AssetSelector(IsUniqueList = true)] private FastTravelOptionButton[] m_townGateButtons;
        [SerializeField] private FastTravelOptionButton m_overworldTownGateButtons;
        //[SerializeField]
        //private Image m_locationBackground;
        [SerializeField] private Image m_showcaseImage;

        private FastTravelData m_currentLocation;
        private List<FastTravelOptionButton> m_activatedButtons = new();

        private void SetShowCaseImageVisibility(bool value) => m_showcaseImage.gameObject.transform.parent.gameObject.SetActive(value);
        public void SetCurrentPlayerPosition(FastTravelData data) => m_currentLocation = data;


        public void ShowPage(FastTravelPageData locationList)
        {
            m_locationLabel.text = locationList.location.ToString().Replace('_', ' ');
            ResetButtons(locationList);

            bool hasSelectedFirst = false;
            int listCount = locationList.count;

            for (int i = 0; i < listCount; i++)
            {
                var button = m_townGateButtons[i];
                var data = locationList.GetUnderworldTravelData(i);
                var isActivated = SetupTownGateButton(button, data);

                if (isActivated)
                    m_activatedButtons.Add(button);

                if (!hasSelectedFirst)
                {
                    button.Select();
                    hasSelectedFirst = true;
                }
            }

            var unlockedOverworld = SetupTownGateButton(m_overworldTownGateButtons, locationList.overworldTravelData, isOverworld: true);
            if (unlockedOverworld)
            {
                m_activatedButtons.Add(m_overworldTownGateButtons);
                
                if (!hasSelectedFirst)
                {
                    m_overworldTownGateButtons.Select();
                }
            }

            bool hasAvailableTownGates = m_activatedButtons.Count > 0;
            SetShowCaseImageVisibility(hasAvailableTownGates);

            if (hasAvailableTownGates)
                ShowCase(m_activatedButtons[0]);
        }

        private bool SetupTownGateButton(FastTravelOptionButton button, FastTravelData travelData, bool isOverworld = false)
        {
            button.SetData(travelData);
            button.ToggleCurrentLocationIcon(travelData == m_currentLocation);

            button.SetButtonLabel(!isOverworld ? travelData.pointName : "Overworld");

            string varName = FastTravelUtility.GenerateActivationVariableName(travelData);
            bool isActivated = DialogueLua.GetVariable(varName).asBool;

            button.SetInteractability(isActivated);
            Show(button);

            return isActivated;
        }

        public void ShowCase(FastTravelOptionButton button)
        {
            if (button == null || !button.IsInteractable())
            {
                SetShowCaseImageVisibility(false);
                return;
            }

            m_showcaseImage.sprite = button.data.image;
            m_townGateLabel.text = button.label.text;
        }

        private void ResetButtons(FastTravelPageData locationList)
        {
            for (int i = locationList.count; i < m_townGateButtons.Length; i++)
            {
                Hide(m_townGateButtons[i]);
                m_townGateButtons[i].SetData(null);

                if (!m_townGateButtons[i].playerMarker.enabled)
                    continue;

                m_townGateButtons[i].ToggleCurrentLocationIcon(false);

            }

            m_activatedButtons.Clear();
        }

        private void Show(Component component) => component.gameObject.SetActive(true);
        private void Hide(Component component) => component.gameObject.SetActive(false);

    }
}
