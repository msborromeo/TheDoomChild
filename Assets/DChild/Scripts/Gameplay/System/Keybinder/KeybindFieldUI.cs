using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DChild.CustomInput.Keybind
{
    public class KeybindFieldUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_buttonLabel;

        public string buttonLabel => m_buttonLabel.text;

        public void UpdateVisual(InputBinding binding)
        {
            m_buttonLabel.text = binding.effectivePath;
        }
    }
}
