using DChild.Gameplay.Items;
using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Animators;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.UI;

namespace DChild.Gameplay.Inventories.UI
{
    public class InventoryItemUI : ItemUI
    {
        private UIToggle m_toggle;
        private bool m_interactionAllowed = true;

        [SerializeField] private UISelectableUIAnimator m_detailsAnimator;

        [SerializeField] private Image m_backgroundFrame;
        [SerializeField] private bool m_isQuickItem;
        public bool isQuickItem => m_isQuickItem;
        public bool isAvailable => m_interactionAllowed && gameObject.activeInHierarchy &&
            m_toggle != null && m_toggle.isActiveAndEnabled && m_toggle.IsInteractable();

        public void SetInteractionAllowed(bool allowed)
        {
            m_interactionAllowed = allowed;
            if (m_toggle == null)
                m_toggle = GetComponent<UIToggle>();

            m_toggle.interactable = allowed && (m_isQuickItem || m_reference != null);
            if (!allowed)
            {
                m_toggle.SetIsOn(false, true, false);
                m_toggle.SetState(UISelectionState.Disabled);
                CompleteDisabledAnimation();
                return;
            }

            if (m_toggle.interactable)
                RestorePresentation();
        }

        public override void Hide()
        {
            m_reference = null;
            m_detailsUI.ShowDetails(null);
            m_toggle.SetIsOn(false, true, false);

            if (m_isQuickItem && m_interactionAllowed)
            {
                m_toggle.interactable = true;
                RestorePresentation();
                return;
            }

            m_toggle.interactable = false;
            CompleteDisabledAnimation();
        }

        public override void SetIconColor(bool isModified)
        {
            m_detailsUI.AdjustIconColor(isModified);
        }

        public override void SetItemFrame(Sprite value)
        {
            m_backgroundFrame.sprite = value;
        }

        public override void Show()
        {
            bool wasDisabled = !m_toggle.interactable ||
                m_toggle.currentUISelectionState == UISelectionState.Disabled;
            m_toggle.interactable = m_interactionAllowed;
            if (!m_interactionAllowed)
            {
                m_toggle.SetState(UISelectionState.Disabled);
                CompleteDisabledAnimation();
                return;
            }
            if (!wasDisabled)
                return;

            RestorePresentation();
        }

        private void RestorePresentation()
        {
            // Selected details may have no animation of their own. Restore the
            // authored visible baseline before applying the final selection state.
            CompleteDetailsAnimation(UISelectionState.Normal);
            var state = m_toggle.IsOn ? UISelectionState.Selected : UISelectionState.Normal;
            m_toggle.SetState(state);
            CompleteDetailsAnimation(state);
        }

        protected override void ShowDetailsOf(IStoredItem reference)
        {
            if (reference == null || reference.data.category == ItemCategory.SoulEssence)
            {
                Hide();
                base.ShowDetailsOf(null);
                return;
            }

            Show();
            base.ShowDetailsOf(reference);
        }

        private void OnEnable()
        {
            m_toggle = GetComponent<UIToggle>();
        }

        private void CompleteDisabledAnimation()
        {
            CompleteDetailsAnimation(UISelectionState.Disabled);
        }

        private void CompleteDetailsAnimation(UISelectionState state)
        {
            if (m_detailsAnimator == null || !m_detailsAnimator.IsStateEnabled(state))
                return;

            m_detailsAnimator.StopAllReactions();
            m_detailsAnimator.GetAnimation(state).SetProgressAtOne();
        }
    }
}
