using Doozy.Runtime.UIManager.Components;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
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
        [SerializeField]
        private Image m_showcaseImage;

        private List<FastTravelOptionButton> m_activatedButtons = new();


        private void SetShowCaseImageVisibility(bool value) => m_showcaseImage.gameObject.transform.parent.gameObject.SetActive(value);
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
                button.SetButtonLabel($"Town Gate #{i + 1}");

                var isActivated = DialogueLua.GetVariable(FastTravelUtility.GenerateActivationVariableName(data)).asBool;
                button.SetInteractability(isActivated);

                if (button.IsInteractable())
                    m_activatedButtons.Add(button);
            }

            var isOverworldActivated = DialogueLua.GetVariable(FastTravelUtility.GenerateActivationVariableName(locationList.overworldTravelData)).asBool;
            m_overworldTownGateButtons.SetData(locationList.overworldTravelData);
            m_overworldTownGateButtons.SetButtonLabel("Overworld");
            m_overworldTownGateButtons.SetInteractability(isOverworldActivated);

            var hasAvailableTownGates = m_activatedButtons.Count > 0;

            SetShowCaseImageVisibility(hasAvailableTownGates);

            if (hasAvailableTownGates)
                ShowCase(m_activatedButtons[0]);
        }

        public void ShowCase(FastTravelOptionButton button)
        {
            if (button == null && !button.IsInteractable())
            {
                SetShowCaseImageVisibility(false);
                return;
            }
            m_showcaseImage.sprite = button.data.image;
        }

        private void ResetButtons(FastTravelPageData locationList)
        {
            for (int i = locationList.count; i < m_townGateButtons.Length; i++)
            {
                Hide(m_townGateButtons[i]);
                m_townGateButtons[i].SetData(null);
            }

            m_activatedButtons.Clear();
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
