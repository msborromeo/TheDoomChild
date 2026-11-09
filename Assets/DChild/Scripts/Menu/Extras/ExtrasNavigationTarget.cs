using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DChild.Menu.Extras
{
    [DisallowMultipleComponent]
    public class ExtrasNavigationTarget : MonoBehaviour, ISelectHandler, IDeselectHandler, ISubmitHandler, IMoveHandler, IPointerDownHandler
    {
        private ExtrasNavigationController m_controller;
        private Selectable m_selectable;
        private Outline m_outline;

        internal void Initialize(ExtrasNavigationController controller, Selectable selectable)
        {
            m_controller = controller;
            m_selectable = selectable;
            if (m_outline == null && selectable is Button && selectable.GetComponent<Graphic>() != null)
            {
                m_outline = selectable.gameObject.AddComponent<Outline>();
                m_outline.effectColor = new Color(1f, 0.75f, 0.15f, 1f);
                m_outline.effectDistance = new Vector2(3f, -3f);
                m_outline.enabled = false;
            }
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (m_outline != null) m_outline.enabled = true;
            m_controller.Selected(m_selectable);
        }
        public void OnDeselect(BaseEventData eventData)
        {
            if (m_outline != null) m_outline.enabled = false;
        }
        public void OnSubmit(BaseEventData eventData) => m_controller.Submitted(m_selectable);
        public void OnMove(AxisEventData eventData) => m_controller.Moved(m_selectable, eventData);
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && m_selectable.IsInteractable())
                m_selectable.Select();
        }
    }
}
