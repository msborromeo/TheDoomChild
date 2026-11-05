using System.Collections.Generic;
using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Animators;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Visual;
using UnityEngine;

namespace DChild.Menu.Extras
{
    // Capture prefab poses before UISelectable/animator Awake or OnEnable can change them.
    [DefaultExecutionOrder(-1000), DisallowMultipleComponent]
    public class ExtrasTabVisualReset : MonoBehaviour
    {
        [SerializeField] private UIToggle[] m_tabs = new UIToggle[0];
        private readonly List<BaseUISelectableAnimator> m_animators = new List<BaseUISelectableAnimator>();
        private readonly List<Baseline> m_baselines = new List<Baseline>();
        private bool m_captured;
        private bool m_pending;

        private struct Baseline
        {
            public UISelectableUIAnimator animator;
            public Vector3 position;
            public Vector3 rotation;
            public Vector3 scale;
            public float alpha;

            public void Restore()
            {
                animator.SetStartPosition(position);
                animator.SetStartRotation(rotation);
                animator.SetStartScale(scale);
                animator.SetStartAlpha(alpha);
            }
        }

        private void Awake()
        {
            if (m_captured) return;
            m_captured = true;
            foreach (var tab in m_tabs)
            {
                if (tab == null) continue;
                foreach (var animator in tab.GetComponentsInChildren<BaseUISelectableAnimator>(true))
                {
                    if (animator.controller != tab || m_animators.Contains(animator)) continue;
                    m_animators.Add(animator);
                    if (!(animator is UISelectableUIAnimator ui)) continue;
                    var rect = ui.rectTransform;
                    var group = ui.GetComponent<CanvasGroup>();
                    m_baselines.Add(new Baseline
                    {
                        animator = ui,
                        position = rect.anchoredPosition3D,
                        rotation = rect.localEulerAngles,
                        scale = rect.localScale,
                        alpha = group != null ? group.alpha : 1f
                    });
                }
            }
        }

        internal void BeginReset()
        {
            m_pending = true;
            // Put targets at their desired pose before the next render. Deferred SetTarget may
            // subsequently recapture that pose; completion restores the immutable baselines again.
            ApplyPose();
        }

        internal bool TryCompleteReset()
        {
            if (!m_pending) return true;
            foreach (var animator in m_animators)
                if (animator != null && animator.isActiveAndEnabled &&
                    (!animator.animatorInitialized || !animator.isConnected)) return false;
            ApplyPose();
            m_pending = false;
            return true;
        }

        internal void CancelReset() => m_pending = false;

        private void LateUpdate()
        {
            // Poll connection readiness only while resetting; do not continuously snap animations.
            if (m_pending) TryCompleteReset();
        }

        private void OnDisable() => CancelReset();

        private void ApplyPose()
        {
            foreach (var animator in m_animators)
            {
                if (animator == null || !animator.isActiveAndEnabled) continue;
                if (!animator.animatorInitialized) animator.InitializeAnimator();
                animator.StopAllReactions();
            }
            // All UpdateSettings/SetTarget calls must precede restoration, including paired
            // ON/OFF animators that share a RectTransform.
            foreach (var baseline in m_baselines)
                if (baseline.animator != null && baseline.animator.isActiveAndEnabled) baseline.Restore();
            foreach (var animator in m_animators)
            {
                if (animator == null || !animator.isActiveAndEnabled || animator.controller == null) continue;
                if (animator.ToggleCommand == CommandToggle.On && !animator.controller.isOn ||
                    animator.ToggleCommand == CommandToggle.Off && animator.controller.isOn) continue;
                if (!animator.IsStateEnabled(UISelectionState.Normal)) continue;
                if (animator is UISelectableUIAnimator ui) ui.GetAnimation(UISelectionState.Normal).SetProgressAtOne();
                else if (animator is UISelectableColorAnimator color) color.GetAnimation(UISelectionState.Normal).SetProgressAtOne();
                else if (animator is UISelectableSpriteAnimator sprite) sprite.GetAnimation(UISelectionState.Normal).SetProgressAtOne();
                else if (animator is UISelectableSpriteSwapper swapper) swapper.Play(UISelectionState.Normal);
            }
        }
    }
}
