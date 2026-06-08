using Doozy.Runtime.UIManager.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.FastTravel
{
    public class FastTravelOptionButton : MonoBehaviour
    {
        [SerializeField] private FastTravelData m_data;
        [SerializeField] private TextMeshProUGUI m_buttonLabel;
        [SerializeField] private UIButton m_button;
        [SerializeField] private Image m_playerMarker;

        public FastTravelData data => m_data;

        public void SetData(FastTravelData data) => m_data = data;
        public void SetButtonLabel(string value) => m_buttonLabel.text = value;
        public void SetInteractability(bool interactability) => m_button.interactable = interactability;
        public bool IsInteractable() => m_button.interactable;
        public void Select() => m_button.Select();
        public void ToggleCurrentLocationIcon(bool value) => m_playerMarker.enabled = value;

    }
}
