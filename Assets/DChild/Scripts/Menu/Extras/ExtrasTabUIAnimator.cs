using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Animators;

namespace DChild.Menu.Extras
{
    public class ExtrasTabUIAnimator : UISelectableUIAnimator
    {
        public override void Play(UISelectionState state)
        {
            // Startup LateUpdate calls Play directly, bypassing the base event handler's filter.
            if (controller == null) return;
            if (controllerIsToggle &&
                (ToggleCommand == CommandToggle.On && !controller.isOn ||
                 ToggleCommand == CommandToggle.Off && controller.isOn)) return;

            base.Play(state);
        }
    }
}
