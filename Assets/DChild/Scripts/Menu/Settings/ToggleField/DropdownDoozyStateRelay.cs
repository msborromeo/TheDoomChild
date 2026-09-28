namespace DChild.Menu.UI
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    using Doozy.Runtime.UIManager.Components;

    [DisallowMultipleComponent]
    [RequireComponent(typeof(Selectable))]
    public class DropdownDoozyStateRelay :
        MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerDownHandler,
        IPointerUpHandler,
        ISelectHandler,
        IDeselectHandler
    {
        [Header("References")]
        [SerializeField]
        private Selectable dropdown;

        [SerializeField]
        private UISelectable visualController;

        [Header("Options")]
        [Tooltip(
            "Enable if you want keyboard/gamepad selection to use Doozy's Selected state. " +
            "Leave disabled if you only want mouse states: Normal -> Highlighted -> Pressed -> Highlighted."
        )]
        [SerializeField]
        private bool forwardSelectionState = false;


        private void Reset()
        {
            dropdown = GetComponent<Selectable>();
        }


        private void Awake()
        {
            if (dropdown == null)
                dropdown = GetComponent<Selectable>();

            if (visualController == null)
            {
                Debug.LogWarning(
                    $"{nameof(DropdownDoozyStateRelay)} on {name} has no Visual Controller assigned.",
                    this
                );

                return;
            }

            // Important:
            // Don't let the proxy selectable become an EventSystem navigation target.
            Navigation navigation = visualController.navigation;
            navigation.mode = Navigation.Mode.None;
            visualController.navigation = navigation;

            SyncInteractable();
        }


        private void Update()
        {
            SyncInteractable();
        }


        private void SyncInteractable()
        {
            if (dropdown == null || visualController == null)
                return;

            bool isInteractable = dropdown.IsInteractable();

            if (visualController.interactable != isInteractable)
                visualController.interactable = isInteractable;
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (visualController == null)
                return;

            visualController.OnPointerEnter(eventData);
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            if (visualController == null)
                return;

            visualController.OnPointerExit(eventData);
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            if (visualController == null)
                return;

            visualController.OnPointerDown(eventData);
        }


        public void OnPointerUp(PointerEventData eventData)
        {
            if (visualController == null)
                return;

            visualController.OnPointerUp(eventData);
        }


        public void OnSelect(BaseEventData eventData)
        {
            if (!forwardSelectionState || visualController == null)
                return;

            visualController.OnSelect(eventData);
        }


        public void OnDeselect(BaseEventData eventData)
        {
            if (!forwardSelectionState || visualController == null)
                return;

            visualController.OnDeselect(eventData);
        }
    }
}