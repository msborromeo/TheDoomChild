using UnityEngine;

namespace DChild.Menu.Equipment.UI
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class EquipmentResponsiveLayout : MonoBehaviour
    {
        [SerializeField] private RectTransform m_content;
        [Tooltip("Equipment root's local rect size in the existing 16:9 layout.")]
        [SerializeField] private Vector2 m_referenceSize = new Vector2(1920f, 1080f);

        private bool m_layoutDirty;

        private void UpdateLayout()
        {
            if (m_content == null || m_referenceSize.x <= 0f || m_referenceSize.y <= 0f)
                return;

            var availableSize = ((RectTransform)transform).rect.size;
            if (availableSize.x <= 0f || availableSize.y <= 0f)
                return;

            float scale = Mathf.Min(availableSize.x / m_referenceSize.x,
                availableSize.y / m_referenceSize.y);

            // Keep child anchors and layout groups in their original 16:9 coordinate space.
            m_content.anchorMin = new Vector2(0.5f, 0.5f);
            m_content.anchorMax = new Vector2(0.5f, 0.5f);
            m_content.pivot = new Vector2(0.5f, 0.5f);
            m_content.sizeDelta = m_referenceSize;
            m_content.anchoredPosition = Vector2.zero;
            m_content.localScale = new Vector3(scale, scale, 1f);
            m_layoutDirty = false;
        }

        private void OnEnable() => UpdateLayout();
        private void OnRectTransformDimensionsChange() => UpdateLayout();

        private void OnValidate() => m_layoutDirty = true;

        private void LateUpdate()
        {
            if (m_layoutDirty)
                UpdateLayout();
        }
    }
}
