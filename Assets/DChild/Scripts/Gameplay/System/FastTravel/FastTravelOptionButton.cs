using Doozy.Runtime.UIManager.Components;
using TMPro;
using UnityEngine;

namespace DChild.Gameplay.FastTravel
{
    public class FastTravelOptionButton : MonoBehaviour
    {
        [SerializeField]
        private FastTravelData m_data;
        [SerializeField]
        private TextMeshProUGUI m_buttonLabel;
        [SerializeField]
        private UIButton m_button;

        public FastTravelData data => m_data;

        public void SetData(FastTravelData data)
        {
            m_data = data;
            if (m_data)
            {
                m_buttonLabel.text = m_data.pointName;
            }
        }

        public void SetInteractability(bool interactability)
        {
            m_button.interactable = interactability;
        }
    }
}
