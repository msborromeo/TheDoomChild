using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DChild.Menu.UI
{
    [DisallowMultipleComponent]
    public class SettingsNavigationTarget : MonoBehaviour, ISelectHandler, ISubmitHandler, IPointerDownHandler
    {
        private SettingsNavigationController m_controller;
        private Selectable m_selectable;

        public void Initialize(SettingsNavigationController controller, Selectable selectable)
        {
            m_controller = controller;
            m_selectable = selectable;
        }

        public void OnSelect(BaseEventData eventData) => m_controller.Selected(m_selectable);
        public void OnSubmit(BaseEventData eventData) => m_controller.Submitted(m_selectable);
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && m_selectable.IsInteractable())
                m_selectable.Select();
        }
    }
}
